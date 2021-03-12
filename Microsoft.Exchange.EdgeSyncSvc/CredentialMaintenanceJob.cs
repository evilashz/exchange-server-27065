using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Logging;
using Microsoft.Exchange.MessageSecurity.EdgeSync;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200000B RID: 11
	internal class CredentialMaintenanceJob : Job
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000040B9 File Offset: 0x000022B9
		public EdgeSyncLogSession LogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000040C4 File Offset: 0x000022C4
		public CredentialMaintenanceJob(long serviceStartTime, TimeSpan interval, EdgeSyncLogSession logSession) : base(interval)
		{
			this.logSession = logSession;
			this.serviceStartTime = serviceStartTime;
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 91, ".ctor", "f:\\15.00.1497\\sources\\dev\\EdgeSync\\src\\EdgeSyncMain\\CredentialMaintenanceJob.cs");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004144 File Offset: 0x00002344
		public void UpdateEdgeSyncCredentials(ITopologyConfigurationSession session, Dictionary<string, ProbeRecord> credentialProbeRecords)
		{
			Server localHubServer = null;
			X509Certificate2 x509Certificate = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				localHubServer = session.FindLocalServer();
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<Exception>(0L, "Credential renewal failed because EdgeSync could not load local Hub server with exception {0}", adoperationResult.Exception);
				this.LogSession.LogException(EdgeSyncLoggingLevel.None, EdgeSyncEvent.Credential, adoperationResult.Exception, "Failed to renew the EdgeSync credentials.  Hub server not found.");
				return;
			}
			if (localHubServer.EdgeSyncCredentials == null || localHubServer.EdgeSyncCredentials.Count == 0)
			{
				ExTraceGlobals.SubscriptionTracer.TraceDebug(0L, "Local Hub server doesn't have any EdgeSync credentials, skip the renewal");
				return;
			}
			int count = localHubServer.EdgeSyncCredentials.Count;
			EdgeSyncCredential[] array = new EdgeSyncCredential[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = EdgeSyncCredential.DeserializeEdgeSyncCredential(localHubServer.EdgeSyncCredentials[i]);
			}
			try
			{
				x509Certificate = new X509Certificate2(localHubServer.InternalTransportCertificate);
			}
			catch (CryptographicException ex)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<CryptographicException>(0L, "Credential renewal failed because the default TLS cert for local hub is corrupted with {0}", ex);
				this.LogSession.LogException(EdgeSyncLoggingLevel.None, EdgeSyncEvent.Credential, ex, "Failed to renew the EdgeSync credentials.  Can not load default certificate.");
				return;
			}
			if (!TlsCertificateInfo.IsCNGProvider(x509Certificate))
			{
				bool flag = false;
				MultiValuedProperty<byte[]> multiValuedProperty = new MultiValuedProperty<byte[]>();
				for (int j = 0; j < count; j++)
				{
					CredentialMaintenanceJob.UpdateResult updateResult = this.UpdateEdgeSyncCredential(j, array, session, localHubServer, x509Certificate, credentialProbeRecords);
					if (updateResult != CredentialMaintenanceJob.UpdateResult.Removed)
					{
						multiValuedProperty.Add(localHubServer.EdgeSyncCredentials[j]);
					}
					if (updateResult == CredentialMaintenanceJob.UpdateResult.Removed || updateResult == CredentialMaintenanceJob.UpdateResult.Updated)
					{
						flag = true;
					}
				}
				if (flag)
				{
					localHubServer.EdgeSyncCredentials = multiValuedProperty;
					adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						session.Save(localHubServer);
					}, 3);
					if (!adoperationResult.Succeeded)
					{
						ExTraceGlobals.SubscriptionTracer.TraceError<Exception>(0L, "Credential renewal failed because can't save local Hubserver with exception {0}", adoperationResult.Exception);
						this.LogSession.LogException(EdgeSyncLoggingLevel.None, EdgeSyncEvent.Credential, adoperationResult.Exception, "Failed to renew the EdgeSync credentials.  Could not save updates on the Hub server.");
					}
				}
				return;
			}
			ExTraceGlobals.SubscriptionTracer.TraceError<string>(0L, "Failed to renew the EdgeSync credentials because we only support CAPI certificate. The certificate with thumbprint {0} is not a CAPI certificate", x509Certificate.Thumbprint);
			this.LogSession.LogEvent(EdgeSyncLoggingLevel.None, EdgeSyncEvent.Credential, "Failed to renew the EdgeSync credentials because we only support CAPI certificate.", x509Certificate.Thumbprint);
			EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_CAPICertificateRequired, x509Certificate.Thumbprint, new object[]
			{
				x509Certificate.Thumbprint
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000043A8 File Offset: 0x000025A8
		protected override JobBackgroundResult BackgroundExecute()
		{
			this.UpdateEdgeSyncCredentials(this.configurationSession, this.credentialProbeRecords);
			if (DateTime.UtcNow.Ticks - this.serviceStartTime > EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval.Ticks)
			{
				this.ProbeEdgeSyncCredentials();
			}
			return JobBackgroundResult.ReSchedule;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004400 File Offset: 0x00002600
		private static int GetSubscriptionSSLPort(ITopologyConfigurationSession configurationSession, string subscriptionFQDN)
		{
			int num = 50636;
			int result;
			try
			{
				Server server = configurationSession.FindServerByFqdn(subscriptionFQDN);
				if (server == null)
				{
					result = num;
				}
				else
				{
					result = server.EdgeSyncAdamSslPort;
				}
			}
			catch (ADTransientException)
			{
				result = num;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000446C File Offset: 0x0000266C
		private CredentialMaintenanceJob.UpdateResult UpdateEdgeSyncCredential(int index, EdgeSyncCredential[] edgeSyncCredentials, ITopologyConfigurationSession configurationSession, Server localHubServer, X509Certificate2 hubCert, Dictionary<string, ProbeRecord> credentialProbeRecords)
		{
			long effectiveDate = 0L;
			Server edgeServer = null;
			EdgeSyncCredential cred = edgeSyncCredentials[index];
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				edgeServer = configurationSession.FindServerByFqdn(cred.EdgeServerFQDN);
			}, 3);
			if (!adoperationResult.Succeeded || edgeServer == null)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string, Exception>(0L, "ProbeEdgeSyncCredentials failed because it could not load Edge server {0} from AD.  Exception: {1}.", cred.EdgeServerFQDN, adoperationResult.Exception);
				this.LogSession.LogRenewalException("Failed to load the Edge Server.", adoperationResult.Exception, cred.EdgeServerFQDN);
				return CredentialMaintenanceJob.UpdateResult.NoChange;
			}
			if (!localHubServer.ServerSite.Equals(edgeServer.ServerSite))
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<string, ADObjectId, ADObjectId>(0L, "ProbeEdgeSyncCredentials site mismatch, skipped Edge server {0}.  Local Site: {1}.  Edge Site: {2}.", cred.EdgeServerFQDN, localHubServer.ServerSite, edgeServer.ServerSite);
				return CredentialMaintenanceJob.UpdateResult.Removed;
			}
			if (cred.IsBootStrapAccount)
			{
				return CredentialMaintenanceJob.UpdateResult.NoChange;
			}
			if (cred.EffectiveDate + cred.Duration > DateTime.UtcNow.Ticks)
			{
				return CredentialMaintenanceJob.UpdateResult.NoChange;
			}
			EdgeSyncCredential edgeSyncCredential = null;
			for (int i = 0; i < edgeSyncCredentials.Length; i++)
			{
				if (!edgeSyncCredentials[i].IsBootStrapAccount && string.Equals(cred.EdgeServerFQDN, edgeSyncCredentials[i].EdgeServerFQDN, StringComparison.OrdinalIgnoreCase) && i != index)
				{
					edgeSyncCredential = edgeSyncCredentials[i];
					break;
				}
			}
			if (edgeSyncCredential.EffectiveDate + edgeSyncCredential.Duration < DateTime.UtcNow.Ticks)
			{
				effectiveDate = DateTime.UtcNow.Ticks + EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval.Ticks;
			}
			else
			{
				effectiveDate = edgeSyncCredential.EffectiveDate + edgeSyncCredential.Duration;
			}
			X509Certificate2 cert = null;
			try
			{
				cert = new X509Certificate2(edgeServer.InternalTransportCertificate);
			}
			catch (CryptographicException ex)
			{
				ExTraceGlobals.SubscriptionTracer.TraceError<CryptographicException>(0L, "Credential renewal failed because the default TLS cert for local hub is corrupted with {0}", ex);
				this.LogSession.LogRenewalException("Failed to load the certificate", ex, edgeServer.Name);
				return CredentialMaintenanceJob.UpdateResult.NoChange;
			}
			if (credentialProbeRecords.ContainsKey(cred.ESRAUsername))
			{
				credentialProbeRecords[cred.ESRAUsername].Confirmed = false;
				credentialProbeRecords[cred.ESRAUsername].LastProbedTime = DateTime.MinValue.Ticks;
			}
			string password = AdamUserManagement.GeneratePassword();
			cred.EncryptedESRAPassword = AdamUserManagement.EncryptPassword(hubCert, password);
			cred.EdgeEncryptedESRAPassword = AdamUserManagement.EncryptPassword(cert, password);
			cred.EffectiveDate = effectiveDate;
			localHubServer.EdgeSyncCredentials[index] = EdgeSyncCredential.SerializeEdgeSyncCredential(cred);
			this.LogSession.LogRenewal("Successfully rolled the credential.", cred.ESRAUsername, cred.EdgeServerFQDN, new DateTime(cred.EffectiveDate), AdamUserManagement.GetPasswordHash(password));
			return CredentialMaintenanceJob.UpdateResult.Updated;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000047C4 File Offset: 0x000029C4
		private void ProbeEdgeSyncCredentials()
		{
			EdgeSyncCredential[] array = null;
			Dictionary<string, EdgeSyncCredential> dictionary = null;
			Server server = null;
			Server edgeServer = null;
			try
			{
				server = this.configurationSession.ReadLocalServer();
				if (server == null)
				{
					ExTraceGlobals.ProcessTracer.TraceError(0L, "Local server not found. Aborting ProbeEdgeSyncCredentials");
					return;
				}
				if (server.EdgeSyncCredentials == null || server.EdgeSyncCredentials.Count == 0)
				{
					ExTraceGlobals.ProcessTracer.TraceDebug(0L, "Local Hub server doesn't have any EdgeSync credentials, skip the ProbeEdgeSyncCredentials");
					return;
				}
				dictionary = new Dictionary<string, EdgeSyncCredential>();
				array = new EdgeSyncCredential[server.EdgeSyncCredentials.Count];
				for (int i = 0; i < server.EdgeSyncCredentials.Count; i++)
				{
					array[i] = EdgeSyncCredential.DeserializeEdgeSyncCredential(server.EdgeSyncCredentials[i]);
					if (array[i].IsBootStrapAccount)
					{
						dictionary.Add(array[i].EdgeServerFQDN, array[i]);
					}
				}
			}
			catch (ADTransientException arg)
			{
				ExTraceGlobals.ProcessTracer.TraceError<ADTransientException>(0L, "ProbeEdgeSyncCredentials hits ADTransientException {0}", arg);
				return;
			}
			catch (DataValidationException arg2)
			{
				ExTraceGlobals.ProcessTracer.TraceError<DataValidationException>(0L, "ProbeEdgeSyncCredentials hits DataValidationException {0}", arg2);
				return;
			}
			catch (ADOperationException arg3)
			{
				ExTraceGlobals.ProcessTracer.TraceError<ADOperationException>(0L, "ProbeEdgeSyncCredentials hits ADOperationException {0}", arg3);
				return;
			}
			catch (InvalidProgramException arg4)
			{
				ExTraceGlobals.ProcessTracer.TraceError<InvalidProgramException>(0L, "ProbeEdgeSyncCredentials hits InvalidProgramException {0}", arg4);
				return;
			}
			for (int j = 0; j < array.Length; j++)
			{
				EdgeSyncCredential current = array[j];
				if (!current.IsBootStrapAccount && current.EffectiveDate > DateTime.UtcNow.Ticks)
				{
					ProbeRecord probeRecord = null;
					if (!this.credentialProbeRecords.TryGetValue(current.ESRAUsername, out probeRecord) || (!probeRecord.Confirmed && DateTime.UtcNow.Ticks - probeRecord.LastProbedTime >= EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval.Ticks))
					{
						EdgeSyncCredential edgeSyncCredential = null;
						if (!dictionary.TryGetValue(current.EdgeServerFQDN, out edgeSyncCredential))
						{
							ExTraceGlobals.ProcessTracer.TraceError(0L, "failed to extract bootstrap account associated with " + current.EdgeServerFQDN);
						}
						else
						{
							long num;
							if (current.EffectiveDate - edgeSyncCredential.EffectiveDate == edgeSyncCredential.Duration)
							{
								num = edgeSyncCredential.EffectiveDate;
							}
							else
							{
								num = current.EffectiveDate - current.Duration;
							}
							if (DateTime.UtcNow.Ticks - num >= 2L * EdgeSyncSvc.EdgeSync.Config.ServiceConfig.ConfigurationSyncInterval.Ticks)
							{
								ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
								{
									edgeServer = this.configurationSession.FindServerByFqdn(current.EdgeServerFQDN);
								}, 3);
								if (!adoperationResult.Succeeded || edgeServer == null)
								{
									ExTraceGlobals.ProcessTracer.TraceError<string, Exception>(0L, "ProbeEdgeSyncCredentials failed because it could not load Edge server {0} from AD.  Exception: {1}.", current.EdgeServerFQDN, adoperationResult.Exception);
								}
								else if (!server.ServerSite.Equals(edgeServer.ServerSite))
								{
									ExTraceGlobals.ProcessTracer.TraceDebug<string, ADObjectId, ADObjectId>(0L, "ProbeEdgeSyncCredentials site mismatch, skipped Edge server {0}.  Local Site: {1}.  Edge Site: {2}.", current.EdgeServerFQDN, server.ServerSite, edgeServer.ServerSite);
								}
								else
								{
									if (probeRecord == null)
									{
										probeRecord = new ProbeRecord();
										probeRecord.Credential = current;
										probeRecord.Confirmed = false;
									}
									NetworkCredential networkCredential = null;
									try
									{
										int subscriptionSSLPort = CredentialMaintenanceJob.GetSubscriptionSSLPort(this.configurationSession, current.EdgeServerFQDN);
										new LdapDirectoryIdentifier(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
										{
											current.EdgeServerFQDN,
											subscriptionSSLPort
										}));
										networkCredential = Util.DecryptEdgeSyncCredential(server, current, this.LogSession);
										if (networkCredential != null)
										{
											using (new LdapTargetConnection(server.VersionNumber, new TargetServerConfig(current.EdgeServerFQDN, current.EdgeServerFQDN, subscriptionSSLPort), networkCredential, SyncTreeType.General, this.LogSession))
											{
											}
											probeRecord.Confirmed = true;
											probeRecord.LastProbedTime = DateTime.UtcNow.Ticks;
											if (this.LogSession != null)
											{
												this.LogSession.LogProbe("Successfully confirmed the credentials.", current.ESRAUsername, current.EdgeServerFQDN, new DateTime(current.EffectiveDate), string.Empty);
											}
										}
									}
									catch (ExDirectoryException ex)
									{
										probeRecord.LastProbedTime = DateTime.UtcNow.Ticks;
										ExTraceGlobals.ProcessTracer.TraceError<string, string>(0L, "ProbeEdgeSyncCredentials failed for {0}, exception {1}", current.ESRAUsername, ex.StackTrace);
										this.LogSession.LogProbe("Failed to confirm the credentials.  Ensure MSExchangeEdgeCredential is running properly on the Edge.", current.ESRAUsername, current.EdgeServerFQDN, new DateTime(current.EffectiveDate), AdamUserManagement.GetPasswordHash(networkCredential.Password));
										EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_ProbeFailed, current.ESRAUsername, new object[]
										{
											current.ESRAUsername,
											current.EdgeServerFQDN,
											new DateTime(current.EffectiveDate).ToLocalTime(),
											AdamUserManagement.GetPasswordHash(networkCredential.Password)
										});
									}
									finally
									{
										this.credentialProbeRecords[current.ESRAUsername] = probeRecord;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04000031 RID: 49
		private readonly long serviceStartTime;

		// Token: 0x04000032 RID: 50
		private EdgeSyncLogSession logSession;

		// Token: 0x04000033 RID: 51
		private Dictionary<string, ProbeRecord> credentialProbeRecords = new Dictionary<string, ProbeRecord>();

		// Token: 0x04000034 RID: 52
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x0200000C RID: 12
		internal enum UpdateResult
		{
			// Token: 0x04000036 RID: 54
			NoChange,
			// Token: 0x04000037 RID: 55
			Updated,
			// Token: 0x04000038 RID: 56
			Removed
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.CertificateDeploymentServicelet;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.CertificateDeployment.Messages;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Servicelets.CertificateDeployment
{
	// Token: 0x02000002 RID: 2
	public class Servicelet : Servicelet
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020F4 File Offset: 0x000002F4
		public override void Work()
		{
			Servicelet.Tracer.TraceDebug((long)this.GetHashCode(), "Work(): Entering");
			this.session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 104, "Work", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\CertificateDeployment\\Program\\CertificateDeploymentServicelet.cs");
			try
			{
				this.localServer = this.session.FindLocalServer();
			}
			catch (LocalServerNotFoundException ex)
			{
				Servicelet.Tracer.TraceError<LocalServerNotFoundException>((long)this.GetHashCode(), "LocalServerNotFound: {0}", ex);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_PermanentException, null, new object[]
				{
					ex
				});
				return;
			}
			ADNotificationRequestCookie adnotificationRequestCookie = null;
			ADNotificationRequestCookie adnotificationRequestCookie2 = null;
			try
			{
				this.distributionTimer = new GuardedTimer(new TimerCallback(this.DoScheduledWork), null, Servicelet.NotificationDelay, Servicelet.ReloadDelay);
				adnotificationRequestCookie = this.InstallConfigMonitor<FederationTrust>("Federation Trusts");
				adnotificationRequestCookie2 = this.InstallConfigMonitor<AuthConfig>(AuthConfig.ContainerName);
				base.StopEvent.WaitOne();
				Servicelet.Tracer.TraceDebug((long)this.GetHashCode(), "Work(): Exiting");
			}
			finally
			{
				if (adnotificationRequestCookie != null)
				{
					ADNotificationAdapter.UnregisterChangeNotification(adnotificationRequestCookie);
				}
				if (adnotificationRequestCookie2 != null)
				{
					ADNotificationAdapter.UnregisterChangeNotification(adnotificationRequestCookie2);
				}
				if (this.distributionTimer != null)
				{
					this.distributionTimer.Dispose(false);
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000227C File Offset: 0x0000047C
		private ADNotificationRequestCookie InstallConfigMonitor<T>(string containerName) where T : ADConfigurationObject, new()
		{
			ADNotificationRequestCookie notificationCookie = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADObjectId childId = this.session.GetOrgContainerId().GetChildId(containerName);
				notificationCookie = ADNotificationAdapter.RegisterChangeNotification<T>(childId, new ADNotificationCallback(this.ChangeNotificationDispatch));
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				Servicelet.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Notification Registration Failed: {0}", adoperationResult.Exception);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_NotificationException, null, new object[]
				{
					adoperationResult.Exception
				});
			}
			return notificationCookie;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002304 File Offset: 0x00000504
		private void ChangeNotificationDispatch(ADNotificationEventArgs args)
		{
			try
			{
				if (Interlocked.Increment(ref this.notificationHandlerCount) == 1)
				{
					this.distributionTimer.Change(Servicelet.NotificationDelay, Servicelet.ReloadDelay);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.notificationHandlerCount);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002354 File Offset: 0x00000554
		private void DoScheduledWork(object reloadState)
		{
			try
			{
				List<CertificateRecord> federationTrustCertificates = this.GetFederationTrustCertificates();
				List<CertificateRecord> authCertificates = this.GetAuthCertificates();
				foreach (CertificateRecord certificateRecord in authCertificates)
				{
					CertificateRecord certificateRecord2 = federationTrustCertificates.Find(new Predicate<CertificateRecord>(certificateRecord.Equals));
					if (certificateRecord2 != null)
					{
						certificateRecord2.Type |= certificateRecord.Type;
					}
					else
					{
						federationTrustCertificates.Add(certificateRecord);
					}
				}
				if (federationTrustCertificates.Count > 0)
				{
					this.PerformDistribution(federationTrustCertificates);
				}
			}
			catch (ADTransientException ex)
			{
				Servicelet.Tracer.TraceError<ADTransientException>((long)this.GetHashCode(), "ActiveDirecotry transient  exception occurred during distribution: {0}", ex);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_TransientException, null, new object[]
				{
					ex
				});
			}
			catch (DataSourceOperationException ex2)
			{
				Servicelet.Tracer.TraceError<DataSourceOperationException>((long)this.GetHashCode(), "ActiveDirecotry permanent exception occurred during distribution: {0}", ex2);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_PermanentException, null, new object[]
				{
					ex2
				});
			}
			catch (DataValidationException ex3)
			{
				Servicelet.Tracer.TraceError<DataValidationException>((long)this.GetHashCode(), "ActiveDirecotry permanent exception occurred during distribution: {0}", ex3);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_PermanentException, null, new object[]
				{
					ex3
				});
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000024D0 File Offset: 0x000006D0
		private void PerformDistribution(List<CertificateRecord> certsRequired)
		{
			Servicelet.Tracer.TraceDebug((long)this.GetHashCode(), "PerformDistribution(): Entering");
			List<CertificateRecord> list = new List<CertificateRecord>();
			foreach (CertificateRecord certificateRecord in certsRequired)
			{
				string thumbprint = certificateRecord.Thumbprint;
				Servicelet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Certificate Required: {0}", thumbprint);
				ExchangeCertificate exchangeCertificate;
				FederationTrustCertificateState federationTrustCertificateState = FederationCertificate.TestForCertificate(this.localServer.Name, thumbprint, out exchangeCertificate);
				Servicelet.Tracer.TraceDebug<FederationTrustCertificateState>((long)this.GetHashCode(), "Certificate State: {0}", federationTrustCertificateState);
				if (federationTrustCertificateState == FederationTrustCertificateState.NotInstalled)
				{
					list.Add(certificateRecord);
					if (this.IsCurrentOrNextCertificate(certificateRecord))
					{
						this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_NeedCertificate, null, new object[]
						{
							thumbprint
						});
					}
				}
				else if (federationTrustCertificateState == FederationTrustCertificateState.Installed)
				{
					if (this.IsCurrentOrNextCertificate(certificateRecord))
					{
						this.VerifyCertificateExpiration(exchangeCertificate);
					}
					if (!ManageExchangeCertificate.IsCertEnabledForNetworkService(exchangeCertificate))
					{
						Servicelet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Enabling for Network Service: {0}", thumbprint);
						try
						{
							FederationCertificate.EnableCertificateForNetworkService(this.localServer.Name, thumbprint);
						}
						catch (LocalizedException ex)
						{
							Servicelet.Tracer.TraceError<LocalizedException>((long)this.GetHashCode(), "Failed to Enable for Network Service: {0}", ex);
							this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_EnableNetworkServiceException, null, new object[]
							{
								thumbprint,
								ex
							});
						}
						catch (InvalidOperationException ex2)
						{
							Servicelet.Tracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "Failed to Enable for Network Service: {0}", ex2);
							this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_EnableNetworkServiceException, null, new object[]
							{
								thumbprint,
								ex2
							});
						}
					}
				}
			}
			if (list.Count != 0)
			{
				Dictionary<TopologySite, List<TopologyServer>> dictionary;
				TopologySite topologySite;
				FederationCertificate.DiscoverServers(this.session, true, out dictionary, out topologySite);
				if (topologySite == null)
				{
					Servicelet.Tracer.TraceError((long)this.GetHashCode(), "Server is not associated with a site");
					this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_CannotFindLocalSite, null, null);
					return;
				}
				List<TopologyServer> sourceServers;
				if (dictionary.TryGetValue(topologySite, out sourceServers))
				{
					this.PullCertificate(sourceServers, list, this.localServer);
				}
				if (list.Count != 0)
				{
					foreach (KeyValuePair<TopologySite, List<TopologyServer>> keyValuePair in dictionary)
					{
						if (!keyValuePair.Key.Equals(topologySite))
						{
							this.PullCertificate(keyValuePair.Value, list, this.localServer);
							if (list.Count == 0)
							{
								break;
							}
						}
					}
				}
			}
			foreach (CertificateRecord certificateRecord2 in list)
			{
				Servicelet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Certificate not found: {0}", certificateRecord2.Thumbprint);
				if (this.IsCurrentOrNextCertificate(certificateRecord2))
				{
					this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_CertificateNotFound, null, new object[]
					{
						certificateRecord2.Thumbprint
					});
				}
			}
			Servicelet.Tracer.TraceDebug((long)this.GetHashCode(), "PerformDistribution(): Exiting");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000285C File Offset: 0x00000A5C
		private bool IsCurrentOrNextCertificate(CertificateRecord cert)
		{
			return (cert.Type & FederationCertificateType.CurrentCertificate) == FederationCertificateType.CurrentCertificate || (cert.Type & FederationCertificateType.NextCertificate) == FederationCertificateType.NextCertificate;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002878 File Offset: 0x00000A78
		private List<CertificateRecord> GetFederationTrustCertificates()
		{
			IEnumerable<CertificateRecord> source = FederationCertificate.FederationCertificates(this.session);
			return source.ToList<CertificateRecord>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002898 File Offset: 0x00000A98
		private List<CertificateRecord> GetAuthCertificates()
		{
			List<CertificateRecord> list = new List<CertificateRecord>();
			AuthConfig authConfig = AuthConfig.Read(this.session);
			if (authConfig == null)
			{
				throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidAuthSettings(string.Empty));
			}
			if (!string.IsNullOrEmpty(authConfig.PreviousCertificateThumbprint))
			{
				CertificateRecord certificateRecord = new CertificateRecord
				{
					Type = FederationCertificateType.PreviousCertificate,
					Thumbprint = authConfig.PreviousCertificateThumbprint
				};
				CertificateRecord certificateRecord2 = list.Find(new Predicate<CertificateRecord>(certificateRecord.Equals));
				if (certificateRecord2 != null)
				{
					certificateRecord2.Type |= FederationCertificateType.PreviousCertificate;
				}
				else
				{
					list.Add(certificateRecord);
				}
			}
			if (!string.IsNullOrEmpty(authConfig.CurrentCertificateThumbprint))
			{
				CertificateRecord certificateRecord3 = new CertificateRecord
				{
					Type = FederationCertificateType.CurrentCertificate,
					Thumbprint = authConfig.CurrentCertificateThumbprint
				};
				CertificateRecord certificateRecord4 = list.Find(new Predicate<CertificateRecord>(certificateRecord3.Equals));
				if (certificateRecord4 != null)
				{
					certificateRecord4.Type |= FederationCertificateType.CurrentCertificate;
				}
				else
				{
					list.Add(certificateRecord3);
				}
			}
			if (!string.IsNullOrEmpty(authConfig.NextCertificateThumbprint))
			{
				CertificateRecord certificateRecord5 = new CertificateRecord
				{
					Type = FederationCertificateType.NextCertificate,
					Thumbprint = authConfig.NextCertificateThumbprint
				};
				CertificateRecord certificateRecord6 = list.Find(new Predicate<CertificateRecord>(certificateRecord5.Equals));
				if (certificateRecord6 != null)
				{
					certificateRecord6.Type |= FederationCertificateType.NextCertificate;
				}
				else
				{
					list.Add(certificateRecord5);
				}
			}
			return list;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000029E4 File Offset: 0x00000BE4
		private void PullCertificate(List<TopologyServer> sourceServers, List<CertificateRecord> certsNeeded, Server destinationServer)
		{
			foreach (TopologyServer topologyServer in sourceServers)
			{
				Servicelet.Tracer.TraceDebug<TopologyServer>((long)this.GetHashCode(), "Testing Server: {0}", topologyServer);
				if (!destinationServer.Id.Equals(topologyServer.Id))
				{
					List<CertificateRecord> list = new List<CertificateRecord>();
					foreach (CertificateRecord certificateRecord in certsNeeded)
					{
						Servicelet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Testing Cert: {0}", certificateRecord.Thumbprint);
						SecureString securePassword = FederationCertificate.GeneratePassword();
						try
						{
							string base64cert = FederationCertificate.ExportCertificate(topologyServer.Name, securePassword, certificateRecord.Thumbprint);
							FederationCertificate.ImportCertificate(destinationServer.Name, securePassword, base64cert);
							FederationCertificate.EnableCertificateForNetworkService(destinationServer.Name, certificateRecord.Thumbprint);
							list.Add(certificateRecord);
							this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_InstalledCertificate, null, new object[]
							{
								certificateRecord,
								topologyServer.Name
							});
						}
						catch (LocalizedException arg)
						{
							Servicelet.Tracer.TraceError<LocalizedException>((long)this.GetHashCode(), "Failed to Export/Import: {0}", arg);
						}
						catch (InvalidOperationException arg2)
						{
							Servicelet.Tracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "Failed to Export/Import: {0}", arg2);
						}
					}
					foreach (CertificateRecord certificateRecord2 in list)
					{
						Servicelet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Succesfully Retrieved: {0}", certificateRecord2.Thumbprint);
						certsNeeded.Remove(certificateRecord2);
					}
					if (certsNeeded.Count == 0)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002C20 File Offset: 0x00000E20
		private void VerifyCertificateExpiration(ExchangeCertificate certificate)
		{
			if (certificate.NotAfter.CompareTo(DateTime.UtcNow) <= 0)
			{
				string message = string.Format("Certificate {0} has expired.", certificate.Thumbprint);
				Servicelet.Tracer.TraceError((long)this.GetHashCode(), message);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_CertificateExpired, certificate.Thumbprint, new object[]
				{
					certificate.Thumbprint
				});
				return;
			}
			if (certificate.NotAfter.Subtract(DateTime.UtcNow).CompareTo(Servicelet.CertificateExpirationThreshold) <= 0)
			{
				string message2 = string.Format("Certificate {0} will expire in less than 15 days.", certificate.Thumbprint);
				Servicelet.Tracer.TraceWarning((long)this.GetHashCode(), message2);
				this.eventLogger.LogEvent(MSExchangeCertificateDeploymentEventLogConstants.Tuple_CertificateNearingExpiry, certificate.Thumbprint, new object[]
				{
					certificate.Thumbprint
				});
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly Trace Tracer = ExTraceGlobals.ServiceletTracer;

		// Token: 0x04000002 RID: 2
		private static readonly TimeSpan NotificationDelay = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000003 RID: 3
		private static readonly TimeSpan ReloadDelay = TimeSpan.FromHours(1.0);

		// Token: 0x04000004 RID: 4
		private static readonly TimeSpan CertificateExpirationThreshold = TimeSpan.FromDays(15.0);

		// Token: 0x04000005 RID: 5
		private readonly ExEventLog eventLogger = new ExEventLog(Servicelet.Tracer.Category, "MSExchange Certificate Deployment");

		// Token: 0x04000006 RID: 6
		private ITopologyConfigurationSession session;

		// Token: 0x04000007 RID: 7
		private Server localServer;

		// Token: 0x04000008 RID: 8
		private GuardedTimer distributionTimer;

		// Token: 0x04000009 RID: 9
		private int notificationHandlerCount;
	}
}

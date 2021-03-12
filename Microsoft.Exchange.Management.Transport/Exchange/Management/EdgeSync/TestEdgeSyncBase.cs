using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common.Internal;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.EdgeSync.Validation;
using Microsoft.Exchange.MessageSecurity.EdgeSync;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x0200003F RID: 63
	public abstract class TestEdgeSyncBase : Task
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007E2E File Offset: 0x0000602E
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00007E45 File Offset: 0x00006045
		[Parameter(Mandatory = false)]
		public Fqdn DomainController
		{
			get
			{
				return (Fqdn)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00007E58 File Offset: 0x00006058
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00007E79 File Offset: 0x00006079
		[Parameter(Mandatory = false, ParameterSetName = "Health")]
		public bool MonitoringContext
		{
			get
			{
				return (bool)(base.Fields["MonitoringContext"] ?? false);
			}
			set
			{
				base.Fields["MonitoringContext"] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001F0 RID: 496
		protected abstract string CmdletMonitoringEventSource { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001F1 RID: 497
		protected abstract string Service { get; }

		// Token: 0x060001F2 RID: 498
		internal abstract bool ReadConnectorLeasePath(IConfigurationSession session, ADObjectId rootId, out string primaryLeasePath, out string backupLeasePath, out bool hasOneConnectorEnabledInCurrentForest);

		// Token: 0x060001F3 RID: 499
		internal abstract ADObjectId GetCookieContainerId(IConfigurationSession session);

		// Token: 0x060001F4 RID: 500
		protected abstract EnhancedTimeSpan GetSyncInterval(EdgeSyncServiceConfig config);

		// Token: 0x060001F5 RID: 501 RVA: 0x00007E91 File Offset: 0x00006091
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || MonitoringHelper.IsKnownExceptionForMonitoring(exception);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007EAC File Offset: 0x000060AC
		protected void TestGeneralSyncHealth()
		{
			try
			{
				EdgeSyncRecord edgeSyncRecord = this.TestSyncHealth(this.DomainController);
				if (this.MonitoringContext)
				{
					this.ReportMomStatus(edgeSyncRecord);
				}
				base.WriteObject(edgeSyncRecord);
			}
			catch (TransientException exception)
			{
				this.WriteErrorAndMonitoringEvent(exception, ExchangeErrorCategory.ServerOperation, null, 1003, this.CmdletMonitoringEventSource);
			}
			catch (ADOperationException exception2)
			{
				this.WriteErrorAndMonitoringEvent(exception2, ExchangeErrorCategory.ServerOperation, null, 1003, this.CmdletMonitoringEventSource);
			}
			catch (IOException exception3)
			{
				this.WriteErrorAndMonitoringEvent(exception3, ExchangeErrorCategory.ServerOperation, null, 1003, this.CmdletMonitoringEventSource);
			}
			finally
			{
				if (this.MonitoringContext)
				{
					base.WriteObject(this.monitoringData);
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007F80 File Offset: 0x00006180
		private static LeaseToken GetLease(string primaryLeaseFilePath, string backupLeaseFilePath, out string additionalInfo)
		{
			additionalInfo = null;
			FileLeaseManager.LeaseOperationResult leaseOperationResult = FileLeaseManager.TryRunLeaseOperation(new FileLeaseManager.LeaseOperation(FileLeaseManager.GetLeaseOperation), new FileLeaseManager.LeaseOperationRequest(primaryLeaseFilePath));
			if (leaseOperationResult.Succeeded)
			{
				return leaseOperationResult.ResultToken;
			}
			Exception exception = leaseOperationResult.Exception;
			leaseOperationResult = FileLeaseManager.TryRunLeaseOperation(new FileLeaseManager.LeaseOperation(FileLeaseManager.GetLeaseOperation), new FileLeaseManager.LeaseOperationRequest(backupLeaseFilePath));
			if (leaseOperationResult.Succeeded)
			{
				return leaseOperationResult.ResultToken;
			}
			additionalInfo = string.Format("PrimaryLeaseException:{0}\n\nBackupLeaseException:{1}", exception, leaseOperationResult.Exception);
			return LeaseToken.Empty;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008038 File Offset: 0x00006238
		private bool TryReadCookie(IConfigurationSession session, out Cookie cookie)
		{
			ADObjectId cookieContainerId = null;
			Container cookieContainer = null;
			cookie = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				cookieContainerId = this.GetCookieContainerId(session);
				cookieContainer = session.Read<Container>(cookieContainerId);
			}, 3);
			if (adoperationResult.Succeeded)
			{
				using (MultiValuedProperty<byte[]>.Enumerator enumerator = cookieContainer.EdgeSyncCookies.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						byte[] bytes = enumerator.Current;
						cookie = Cookie.Deserialize(Encoding.ASCII.GetString(bytes));
					}
				}
			}
			return adoperationResult.Succeeded;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008150 File Offset: 0x00006350
		internal T FindSiteEdgeSyncConnector<T>(IConfigurationSession session, ADObjectId siteId, out bool hasOneConnectorEnabledInCurrentForest) where T : EdgeSyncConnector, new()
		{
			List<T> connectors = new List<T>();
			hasOneConnectorEnabledInCurrentForest = true;
			ADNotificationAdapter.ReadConfigurationPaged<T>(() => session.FindPaged<T>(siteId, QueryScope.SubTree, null, null, 0), delegate(T connector)
			{
				if (connector.Enabled)
				{
					connectors.Add(connector);
				}
			}, 3);
			if (connectors.Count == 0)
			{
				ADNotificationAdapter.ReadConfigurationPaged<T>(() => session.FindPaged<T>(null, QueryScope.SubTree, null, null, 0), delegate(T connector)
				{
					if (connector.Enabled)
					{
						connectors.Add(connector);
					}
				}, 3);
				hasOneConnectorEnabledInCurrentForest = (connectors.Count > 0);
				return default(T);
			}
			return connectors[0];
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000825C File Offset: 0x0000645C
		private EdgeSyncRecord TestSyncHealth(string domainController)
		{
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 383, "TestSyncHealth", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\TestEdgeSyncBase.cs");
			ADSite localSite = null;
			EdgeSyncServiceConfig config = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				localSite = session.GetLocalSite();
				if (localSite == null)
				{
					throw new TransientException(Strings.CannotGetLocalSite);
				}
				config = session.Read<EdgeSyncServiceConfig>(localSite.Id.GetChildId("EdgeSyncService"));
			}, 3);
			if (config == null)
			{
				return EdgeSyncRecord.GetEdgeSyncServiceNotConfiguredForCurrentSiteRecord(this.Service, localSite.Name);
			}
			bool flag = false;
			string primaryLeaseFilePath;
			string backupLeaseFilePath;
			if (!this.ReadConnectorLeasePath(session, config.Id, out primaryLeaseFilePath, out backupLeaseFilePath, out flag))
			{
				if (!flag)
				{
					return EdgeSyncRecord.GetEdgeSyncConnectorNotConfiguredForEntireForestRecord(this.Service);
				}
				return EdgeSyncRecord.GetEdgeSyncConnectorNotConfiguredForCurrentSiteRecord(this.Service, localSite.Name);
			}
			else
			{
				string additionalInfo = null;
				LeaseToken lease = TestEdgeSyncBase.GetLease(primaryLeaseFilePath, backupLeaseFilePath, out additionalInfo);
				if (lease.NotHeld)
				{
					return EdgeSyncRecord.GetFailedRecord(this.Service, base.MyInvocation.MyCommand.Name, "There is no lease file detected. It suggests synchronization has not started at all.", lease, null, additionalInfo);
				}
				Cookie cookie = null;
				string text = null;
				if (!this.TryGetNewestCookieFromAllDomainControllers(out cookie, out text))
				{
					throw new InvalidOperationException("Failed accessing all DCs: " + text);
				}
				if (cookie == null)
				{
					return EdgeSyncRecord.GetFailedRecord(this.Service, base.MyInvocation.MyCommand.Name, "There is no cookie detected. It suggests we haven't had a single successful synchronization.", lease, null, text);
				}
				EnhancedTimeSpan syncInterval = this.GetSyncInterval(config);
				switch (lease.Type)
				{
				case LeaseTokenType.Lock:
					if (DateTime.UtcNow > lease.AlertTime)
					{
						return EdgeSyncRecord.GetFailedRecord(this.Service, base.MyInvocation.MyCommand.Name, "Synchronization has completely stopped because lock has expired. It suggests the EdgeSync service died in the middle of the synchronization and no other service instance has taken over.", lease, cookie, text, true);
					}
					if (DateTime.UtcNow > cookie.LastUpdated + config.OptionDuration + 3L * syncInterval + TimeSpan.FromHours(1.0))
					{
						return EdgeSyncRecord.GetFailedRecord(this.Service, base.MyInvocation.MyCommand.Name, "Cookie has not been updated as expected. It might be caused by failure to synchronize some items which means that the sychronization might still be running but not efficiently. It might also be caused by a long full sync. Check EdgeSync log for further troubleshooting.", lease, cookie, text);
					}
					return EdgeSyncRecord.GetInconclusiveRecord(this.Service, base.MyInvocation.MyCommand.Name, "Synchronization status is inconclusive because EdgeSync is in the middle of synchronizing data. Try running this cmdlet again later.", lease, cookie, text);
				case LeaseTokenType.Option:
					if (DateTime.UtcNow > lease.AlertTime)
					{
						return EdgeSyncRecord.GetFailedRecord(this.Service, base.MyInvocation.MyCommand.Name, "Synchronization has completely stopped. We have failed to failover to another instance within the same AD site or to another AD site.", lease, cookie, text, true);
					}
					if (DateTime.UtcNow > cookie.LastUpdated + config.FailoverDCInterval + TimeSpan.FromMinutes(30.0))
					{
						return EdgeSyncRecord.GetFailedRecord(this.Service, base.MyInvocation.MyCommand.Name, "Cookie has not been updated as expected. It might be caused by failure to synchronize some items which means that the sychronization might still be running but not efficiently. It might also be caused by a long full sync. Check EdgeSync log for further troubleshooting.", lease, cookie, text);
					}
					return EdgeSyncRecord.GetNormalRecord(this.Service, "The synchronization is operating normally.", lease, cookie, text);
				default:
					throw new ArgumentException("Unknown lease type: " + lease.Type);
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008584 File Offset: 0x00006784
		private bool TryGetNewestCookieFromAllDomainControllers(out Cookie latestCookie, out string cookieTimeStampRecordInfo)
		{
			latestCookie = null;
			cookieTimeStampRecordInfo = null;
			bool result = false;
			ADForest localForest = ADForest.GetLocalForest();
			List<ADServer> list = localForest.FindAllGlobalCatalogsInLocalSite();
			if (list != null && list.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("List of cookie timestamp from all DCs:");
				foreach (ADServer adserver in list)
				{
					IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(adserver.DnsHostName, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 593, "TryGetNewestCookieFromAllDomainControllers", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\TestEdgeSyncBase.cs");
					Cookie cookie;
					if (this.TryReadCookie(session, out cookie))
					{
						result = true;
						if (cookie != null)
						{
							if (latestCookie == null || cookie.LastUpdated > latestCookie.LastUpdated)
							{
								latestCookie = cookie;
							}
							stringBuilder.AppendFormat("TimeStamp:{0}, SessionDC:{1}, CookieDC:{2};\r\n", cookie.LastUpdated, adserver.Name, cookie.DomainController);
						}
						else
						{
							stringBuilder.AppendFormat("Cookie Value Not Found On DC {0};\r\n", adserver.Name);
						}
					}
					else
					{
						stringBuilder.AppendFormat("Failed Accessing Domain Controller {0};\r\n", adserver.Name);
					}
				}
				cookieTimeStampRecordInfo = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000086C0 File Offset: 0x000068C0
		private void ReportMomStatus(EdgeSyncRecord record)
		{
			int eventIdentifier = 1000;
			EventTypeEnumeration eventType = EventTypeEnumeration.Success;
			switch (record.Status)
			{
			case ValidationStatus.NoSyncConfigured:
				eventIdentifier = 1005;
				eventType = EventTypeEnumeration.Warning;
				break;
			case ValidationStatus.Warning:
				eventIdentifier = 1001;
				eventType = EventTypeEnumeration.Warning;
				break;
			case ValidationStatus.Failed:
				eventIdentifier = 1002;
				eventType = EventTypeEnumeration.Error;
				break;
			case ValidationStatus.Inconclusive:
				eventIdentifier = 1004;
				eventType = EventTypeEnumeration.Information;
				break;
			case ValidationStatus.FailedUrgent:
				eventIdentifier = 1006;
				eventType = EventTypeEnumeration.Error;
				break;
			}
			MonitoringEvent item = new MonitoringEvent(this.CmdletMonitoringEventSource, eventIdentifier, eventType, record.ToString());
			this.monitoringData.Events.Add(item);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008751 File Offset: 0x00006951
		private void WriteErrorAndMonitoringEvent(Exception exception, ExchangeErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
			base.WriteError(exception, (ErrorCategory)errorCategory, target);
		}

		// Token: 0x0400009D RID: 157
		protected const string ParameterSetValidateAddress = "ValidateAddress";

		// Token: 0x0400009E RID: 158
		protected const string ParameterSetHealth = "Health";

		// Token: 0x0400009F RID: 159
		private const string NoLease = "There is no lease file detected. It suggests synchronization has not started at all.";

		// Token: 0x040000A0 RID: 160
		private const string NoCookie = "There is no cookie detected. It suggests we haven't had a single successful synchronization.";

		// Token: 0x040000A1 RID: 161
		private const string LockExpired = "Synchronization has completely stopped because lock has expired. It suggests the EdgeSync service died in the middle of the synchronization and no other service instance has taken over.";

		// Token: 0x040000A2 RID: 162
		private const string InterSiteFailoverFailed = "Synchronization has completely stopped. We have failed to failover to another instance within the same AD site or to another AD site.";

		// Token: 0x040000A3 RID: 163
		private const string StatusInconclusive = "Synchronization status is inconclusive because EdgeSync is in the middle of synchronizing data. Try running this cmdlet again later.";

		// Token: 0x040000A4 RID: 164
		private const string CookieNotUpdated = "Cookie has not been updated as expected. It might be caused by failure to synchronize some items which means that the sychronization might still be running but not efficiently. It might also be caused by a long full sync. Check EdgeSync log for further troubleshooting.";

		// Token: 0x040000A5 RID: 165
		private const string NormalSync = "The synchronization is operating normally.";

		// Token: 0x040000A6 RID: 166
		private MonitoringData monitoringData = new MonitoringData();

		// Token: 0x02000040 RID: 64
		private static class EventId
		{
			// Token: 0x040000A7 RID: 167
			public const int SyncNormal = 1000;

			// Token: 0x040000A8 RID: 168
			public const int SyncAbnormal = 1001;

			// Token: 0x040000A9 RID: 169
			public const int SyncFailed = 1002;

			// Token: 0x040000AA RID: 170
			public const int UnableToTestSyncHealth = 1003;

			// Token: 0x040000AB RID: 171
			public const int InconclusiveTestSyncHealth = 1004;

			// Token: 0x040000AC RID: 172
			public const int SyncNotConfigured = 1005;

			// Token: 0x040000AD RID: 173
			public const int SyncFailedUrgent = 1006;
		}
	}
}

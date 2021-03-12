using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync;
using Microsoft.Exchange.EdgeSync.Validation;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessageSecurity.EdgeSync;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000042 RID: 66
	[Cmdlet("Test", "EdgeSynchronization", SupportsShouldProcess = true, DefaultParameterSetName = "Default")]
	public sealed class TestEdgeSynchronization : Task
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00008DD7 File Offset: 0x00006FD7
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00008DEE File Offset: 0x00006FEE
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008E01 File Offset: 0x00007001
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00008E0E File Offset: 0x0000700E
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public SwitchParameter FullCompareMode
		{
			internal get
			{
				return new SwitchParameter(this.fullCompareMode);
			}
			set
			{
				this.fullCompareMode = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008E1C File Offset: 0x0000701C
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00008E3D File Offset: 0x0000703D
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008E55 File Offset: 0x00007055
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00008E7B File Offset: 0x0000707B
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public SwitchParameter ExcludeRecipientTest
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExcludeRecipientTest"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ExcludeRecipientTest"] = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008E93 File Offset: 0x00007093
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00008EBD File Offset: 0x000070BD
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public Unlimited<uint> MaxReportSize
		{
			get
			{
				return (Unlimited<uint>)(base.Fields["MaxReportSize"] ?? new Unlimited<uint>(1000U));
			}
			set
			{
				base.Fields["MaxReportSize"] = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008ED5 File Offset: 0x000070D5
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00008EEC File Offset: 0x000070EC
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string TargetServer
		{
			get
			{
				return (string)base.Fields["TargetServer"];
			}
			set
			{
				base.Fields["TargetServer"] = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008EFF File Offset: 0x000070FF
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00008F16 File Offset: 0x00007116
		[Parameter(Mandatory = true, ParameterSetName = "SingleValidation")]
		public ProxyAddress VerifyRecipient
		{
			get
			{
				return (ProxyAddress)base.Fields["VerifyRecipient"];
			}
			set
			{
				base.Fields["VerifyRecipient"] = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008F29 File Offset: 0x00007129
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestEdgeSynchronization;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008F30 File Offset: 0x00007130
		protected override bool IsKnownException(Exception e)
		{
			return base.IsKnownException(e) || MonitoringHelper.IsKnownExceptionForMonitoring(e);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008F43 File Offset: 0x00007143
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.monitoringData = new MonitoringData();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008F58 File Offset: 0x00007158
		protected override void InternalValidate()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 262, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\TestEdgeSynchronization.cs");
			try
			{
				Server server = topologyConfigurationSession.FindLocalServer();
				if (!server.IsHubTransportServer)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorTaskRunningLocationHubOnly), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (LocalServerNotFoundException)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTaskRunningLocationHubOnly), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008FDC File Offset: 0x000071DC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			Exception exception = null;
			if (!ReplicationTopology.TryLoadLocalSiteTopology(this.DomainController, out this.replicationTopology, out exception))
			{
				this.WriteErrorAndMonitoringEvent(exception, ExchangeErrorCategory.ServerOperation, null, 1002, "MSExchange Monitoring EdgeSynchronization");
				return;
			}
			if (this.replicationTopology.EdgeSyncServiceConfig == null)
			{
				this.ReportStatus(new EdgeSubscriptionStatus(string.Empty)
				{
					SyncStatus = ValidationStatus.NoSyncConfigured,
					FailureDetail = Strings.EdgeSyncServiceConfigMissing((this.replicationTopology.LocalSite != null) ? this.replicationTopology.LocalSite.Name : string.Empty)
				});
				return;
			}
			if (this.replicationTopology.SiteEdgeServers.Count == 0)
			{
				this.ReportStatus(new EdgeSubscriptionStatus(string.Empty)
				{
					SyncStatus = ValidationStatus.NoSyncConfigured,
					FailureDetail = Strings.NoSubscription((this.replicationTopology.LocalSite != null) ? this.replicationTopology.LocalSite.Name : string.Empty)
				});
				return;
			}
			List<EdgeSubscriptionStatus> list = new List<EdgeSubscriptionStatus>(this.replicationTopology.SiteEdgeServers.Count);
			foreach (Server server in this.replicationTopology.SiteEdgeServers)
			{
				if (string.IsNullOrEmpty(this.TargetServer) || this.TargetServer.Equals(server.Name, StringComparison.OrdinalIgnoreCase) || this.TargetServer.Equals(server.Fqdn))
				{
					EdgeSubscriptionStatus edgeSubscriptionStatus = new EdgeSubscriptionStatus(server.Name);
					EdgeConnectionInfo edgeConnectionInfo = new EdgeConnectionInfo(this.replicationTopology, server);
					if (edgeConnectionInfo.EdgeConnection == null)
					{
						goto IL_3D5;
					}
					edgeSubscriptionStatus.LeaseHolder = edgeConnectionInfo.LeaseHolder;
					edgeSubscriptionStatus.LeaseType = edgeConnectionInfo.LeaseType;
					edgeSubscriptionStatus.LeaseExpiryUtc = edgeConnectionInfo.LeaseExpiry;
					edgeSubscriptionStatus.LastSynchronizedUtc = edgeConnectionInfo.LastSynchronizedDate;
					edgeSubscriptionStatus.CredentialRecords.Records = CredentialRecordsLoader.Load(server);
					edgeSubscriptionStatus.CookieRecords.Load(edgeConnectionInfo.Cookies);
					if (this.VerifyRecipient != null)
					{
						RecipientValidator recipientValidator = new RecipientValidator(this.replicationTopology);
						edgeSubscriptionStatus.RecipientStatus = recipientValidator.ValidateOneRecipient(edgeConnectionInfo, this.VerifyRecipient.ProxyAddressString);
					}
					else if (this.InitializeSubscriptionStatus(edgeConnectionInfo, ref edgeSubscriptionStatus))
					{
						if (!(DateTime.UtcNow > edgeSubscriptionStatus.LeaseExpiryUtc + this.GetAlertPaddingTimeSpan()))
						{
							bool flag = false;
							if (edgeSubscriptionStatus.CookieRecords.Records.Count > 0)
							{
								using (MultiValuedProperty<CookieRecord>.Enumerator enumerator2 = edgeSubscriptionStatus.CookieRecords.Records.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										CookieRecord cookieRecord = enumerator2.Current;
										if (DateTime.UtcNow > cookieRecord.LastUpdated + this.GetAlertPaddingTimeSpan())
										{
											flag = true;
										}
									}
									goto IL_2EC;
								}
								goto IL_2E9;
							}
							goto IL_2E9;
							IL_2EC:
							if (!flag)
							{
								edgeSubscriptionStatus.SyncStatus = ValidationStatus.Normal;
								goto IL_313;
							}
							edgeSubscriptionStatus.SyncStatus = ValidationStatus.Failed;
							edgeSubscriptionStatus.FailureDetail = Strings.CookieNotUpdated;
							goto IL_313;
							IL_2E9:
							flag = true;
							goto IL_2EC;
						}
						edgeSubscriptionStatus.SyncStatus = ValidationStatus.Failed;
						edgeSubscriptionStatus.FailureDetail = Strings.LeaseExpired;
						IL_313:
						if (this.fullCompareMode)
						{
							try
							{
								this.LoadValidators();
								edgeSubscriptionStatus.TransportConfigStatus = this.transportConfigValidator.Validate(edgeConnectionInfo);
								edgeSubscriptionStatus.TransportServerStatus = this.transportServerValidator.Validate(edgeConnectionInfo);
								edgeSubscriptionStatus.AcceptedDomainStatus = this.acceptedDomainValidator.Validate(edgeConnectionInfo);
								edgeSubscriptionStatus.RemoteDomainStatus = this.remoteDomainValidator.Validate(edgeConnectionInfo);
								edgeSubscriptionStatus.MessageClassificationStatus = this.messageClassificationValidator.Validate(edgeConnectionInfo);
								edgeSubscriptionStatus.SendConnectorStatus = this.sendConnectorValidator.Validate(edgeConnectionInfo);
								if (!this.ExcludeRecipientTest.IsPresent)
								{
									edgeSubscriptionStatus.RecipientStatus = this.recipientValidator.Validate(edgeConnectionInfo);
								}
								goto IL_3F5;
							}
							catch (ExDirectoryException ex)
							{
								edgeSubscriptionStatus.FailureDetail = ex.Message;
								goto IL_3F5;
							}
							goto IL_3D5;
						}
					}
					IL_3F5:
					base.WriteObject(edgeSubscriptionStatus);
					list.Add(edgeSubscriptionStatus);
					continue;
					IL_3D5:
					edgeSubscriptionStatus.SyncStatus = ValidationStatus.Failed;
					edgeSubscriptionStatus.FailureDetail = Strings.SubscriptionConnectionError(edgeConnectionInfo.FailureDetail);
					goto IL_3F5;
				}
			}
			if (this.MonitoringContext)
			{
				this.ReportMomStatus(list);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000946C File Offset: 0x0000766C
		private EnhancedTimeSpan GetAlertPaddingTimeSpan()
		{
			EnhancedTimeSpan t = (this.replicationTopology.EdgeSyncServiceConfig.ConfigurationSyncInterval > this.replicationTopology.EdgeSyncServiceConfig.RecipientSyncInterval) ? this.replicationTopology.EdgeSyncServiceConfig.ConfigurationSyncInterval : this.replicationTopology.EdgeSyncServiceConfig.RecipientSyncInterval;
			return 2L * t + TimeSpan.FromMinutes(30.0);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000094E0 File Offset: 0x000076E0
		private void ReportMomStatus(List<EdgeSubscriptionStatus> statusList)
		{
			int num = 1000;
			EventTypeEnumeration eventType = EventTypeEnumeration.Success;
			StringBuilder stringBuilder = null;
			foreach (EdgeSubscriptionStatus edgeSubscriptionStatus in statusList)
			{
				switch (edgeSubscriptionStatus.SyncStatus)
				{
				case ValidationStatus.NoSyncConfigured:
					num = 1004;
					eventType = EventTypeEnumeration.Warning;
					break;
				case ValidationStatus.Failed:
					num = 1001;
					eventType = EventTypeEnumeration.Error;
					break;
				case ValidationStatus.Inconclusive:
					if (num == 1000)
					{
						num = 1003;
						eventType = EventTypeEnumeration.Information;
					}
					break;
				}
				if (stringBuilder == null)
				{
					string text = edgeSubscriptionStatus.ToStringForm();
					stringBuilder = new StringBuilder(text.Length * statusList.Count + 256);
					stringBuilder.Append(text);
				}
				else
				{
					stringBuilder.Append(edgeSubscriptionStatus.ToStringForm());
				}
			}
			MonitoringEvent item = new MonitoringEvent("MSExchange Monitoring EdgeSynchronization", num, eventType, stringBuilder.ToString());
			this.monitoringData.Events.Add(item);
			base.WriteObject(this.monitoringData);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000095F0 File Offset: 0x000077F0
		private void WriteErrorAndMonitoringEvent(Exception exception, ExchangeErrorCategory errorCategory, object target, int eventId, string eventSource)
		{
			if (this.MonitoringContext)
			{
				this.monitoringData.Events.Add(new MonitoringEvent(eventSource, eventId, EventTypeEnumeration.Error, exception.Message));
				base.WriteObject(this.monitoringData);
			}
			base.WriteError(exception, (ErrorCategory)errorCategory, target);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009630 File Offset: 0x00007830
		private void ReportStatus(EdgeSubscriptionStatus subscriptionStatus)
		{
			base.WriteObject(subscriptionStatus);
			if (this.MonitoringContext)
			{
				this.ReportMomStatus(new List<EdgeSubscriptionStatus>(1)
				{
					subscriptionStatus
				});
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009664 File Offset: 0x00007864
		private void LoadValidators()
		{
			if (this.acceptedDomainValidator == null)
			{
				this.acceptedDomainValidator = new AcceptedDomainValidator(this.replicationTopology);
				this.acceptedDomainValidator.MaxReportedLength = this.MaxReportSize;
				this.acceptedDomainValidator.UseChangedDate = this.MonitoringContext;
				this.acceptedDomainValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.acceptedDomainValidator.LoadValidationInfo();
			}
			if (this.remoteDomainValidator == null)
			{
				this.remoteDomainValidator = new RemoteDomainValidator(this.replicationTopology);
				this.remoteDomainValidator.MaxReportedLength = this.MaxReportSize;
				this.remoteDomainValidator.UseChangedDate = this.MonitoringContext;
				this.remoteDomainValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.remoteDomainValidator.LoadValidationInfo();
			}
			if (this.messageClassificationValidator == null)
			{
				this.messageClassificationValidator = new MessageClassificationValidator(this.replicationTopology);
				this.messageClassificationValidator.MaxReportedLength = this.MaxReportSize;
				this.messageClassificationValidator.UseChangedDate = this.MonitoringContext;
				this.messageClassificationValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.messageClassificationValidator.LoadValidationInfo();
			}
			if (this.sendConnectorValidator == null)
			{
				this.sendConnectorValidator = new SendConnectorValidator(this.replicationTopology);
				this.sendConnectorValidator.MaxReportedLength = this.MaxReportSize;
				this.sendConnectorValidator.UseChangedDate = this.MonitoringContext;
				this.sendConnectorValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.sendConnectorValidator.LoadValidationInfo();
			}
			if (this.transportConfigValidator == null)
			{
				this.transportConfigValidator = new TransportConfigValidator(this.replicationTopology);
				this.transportConfigValidator.MaxReportedLength = this.MaxReportSize;
				this.transportConfigValidator.UseChangedDate = this.MonitoringContext;
				this.transportConfigValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.transportConfigValidator.LoadValidationInfo();
			}
			if (this.transportServerValidator == null)
			{
				this.transportServerValidator = new TransportServerValidator(this.replicationTopology);
				this.transportServerValidator.MaxReportedLength = this.MaxReportSize;
				this.transportServerValidator.UseChangedDate = this.MonitoringContext;
				this.transportServerValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.transportServerValidator.LoadValidationInfo();
			}
			if (this.recipientValidator == null && !this.ExcludeRecipientTest.IsPresent)
			{
				this.recipientValidator = new RecipientValidator(this.replicationTopology);
				this.recipientValidator.MaxReportedLength = this.MaxReportSize;
				this.recipientValidator.UseChangedDate = this.MonitoringContext;
				this.recipientValidator.ProgressMethod = new EdgeSyncValidator.ProgressUpdate(this.ProgressUpdate);
				this.recipientValidator.LoadValidationInfo();
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000990C File Offset: 0x00007B0C
		private bool InitializeSubscriptionStatus(EdgeConnectionInfo subscription, ref EdgeSubscriptionStatus status)
		{
			switch (subscription.LeaseType)
			{
			case LeaseTokenType.Lock:
				status.SyncStatus = ValidationStatus.Inconclusive;
				status.AcceptedDomainStatus.SyncStatus = SyncStatus.InProgress;
				status.RemoteDomainStatus.SyncStatus = SyncStatus.InProgress;
				status.MessageClassificationStatus.SyncStatus = SyncStatus.InProgress;
				status.RecipientStatus.SyncStatus = SyncStatus.InProgress;
				status.SendConnectorStatus.SyncStatus = SyncStatus.InProgress;
				status.TransportConfigStatus.SyncStatus = SyncStatus.InProgress;
				status.TransportServerStatus.SyncStatus = SyncStatus.InProgress;
				return false;
			case LeaseTokenType.None:
				status.SyncStatus = ValidationStatus.Inconclusive;
				status.AcceptedDomainStatus.SyncStatus = SyncStatus.NotStarted;
				status.RemoteDomainStatus.SyncStatus = SyncStatus.NotStarted;
				status.MessageClassificationStatus.SyncStatus = SyncStatus.NotStarted;
				status.RecipientStatus.SyncStatus = SyncStatus.NotStarted;
				status.SendConnectorStatus.SyncStatus = SyncStatus.NotStarted;
				status.TransportConfigStatus.SyncStatus = SyncStatus.NotStarted;
				status.TransportServerStatus.SyncStatus = SyncStatus.NotStarted;
				return false;
			}
			return true;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009A04 File Offset: 0x00007C04
		private void ProgressUpdate(LocalizedString title, LocalizedString updateDescription)
		{
			ExProgressRecord record = new ExProgressRecord(0, title, updateDescription);
			base.WriteProgress(record);
		}

		// Token: 0x040000B4 RID: 180
		private const string CmdletNoun = "EdgeSynchronization";

		// Token: 0x040000B5 RID: 181
		private const string CmdletMonitoringEventSource = "MSExchange Monitoring EdgeSynchronization";

		// Token: 0x040000B6 RID: 182
		private const string ExcludeRecipientTestName = "ExcludeRecipientTest";

		// Token: 0x040000B7 RID: 183
		private const string MaxReportSizeName = "MaxReportSize";

		// Token: 0x040000B8 RID: 184
		private const string MonitoringContextName = "MonitoringContext";

		// Token: 0x040000B9 RID: 185
		private const uint DefaultReportSize = 1000U;

		// Token: 0x040000BA RID: 186
		private MonitoringData monitoringData = new MonitoringData();

		// Token: 0x040000BB RID: 187
		private bool fullCompareMode;

		// Token: 0x040000BC RID: 188
		private ReplicationTopology replicationTopology;

		// Token: 0x040000BD RID: 189
		private TransportConfigValidator transportConfigValidator;

		// Token: 0x040000BE RID: 190
		private TransportServerValidator transportServerValidator;

		// Token: 0x040000BF RID: 191
		private SendConnectorValidator sendConnectorValidator;

		// Token: 0x040000C0 RID: 192
		private RecipientValidator recipientValidator;

		// Token: 0x040000C1 RID: 193
		private MessageClassificationValidator messageClassificationValidator;

		// Token: 0x040000C2 RID: 194
		private AcceptedDomainValidator acceptedDomainValidator;

		// Token: 0x040000C3 RID: 195
		private RemoteDomainValidator remoteDomainValidator;

		// Token: 0x02000043 RID: 67
		private static class ParameterSet
		{
			// Token: 0x040000C4 RID: 196
			internal const string Default = "Default";

			// Token: 0x040000C5 RID: 197
			internal const string SingleValidation = "SingleValidation";
		}

		// Token: 0x02000044 RID: 68
		private static class EventId
		{
			// Token: 0x040000C6 RID: 198
			public const int SyncNormal = 1000;

			// Token: 0x040000C7 RID: 199
			public const int SyncFailed = 1001;

			// Token: 0x040000C8 RID: 200
			public const int UnableToTestSyncHealth = 1002;

			// Token: 0x040000C9 RID: 201
			public const int InconclusiveTestSyncHealth = 1003;

			// Token: 0x040000CA RID: 202
			public const int SyncNotConfigured = 1004;
		}
	}
}

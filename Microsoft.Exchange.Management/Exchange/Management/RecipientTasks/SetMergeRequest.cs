using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C87 RID: 3207
	[Cmdlet("Set", "MergeRequest", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMergeRequest : SetRequest<MergeRequestIdParameter>
	{
		// Token: 0x1700263A RID: 9786
		// (get) Token: 0x06007B60 RID: 31584 RVA: 0x001FA1B9 File Offset: 0x001F83B9
		// (set) Token: 0x06007B61 RID: 31585 RVA: 0x001FA1D0 File Offset: 0x001F83D0
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public string RemoteSourceMailboxServerLegacyDN
		{
			get
			{
				return (string)base.Fields["RemoteSourceMailboxServerLegacyDN"];
			}
			set
			{
				base.Fields["RemoteSourceMailboxServerLegacyDN"] = value;
			}
		}

		// Token: 0x1700263B RID: 9787
		// (get) Token: 0x06007B62 RID: 31586 RVA: 0x001FA1E3 File Offset: 0x001F83E3
		// (set) Token: 0x06007B63 RID: 31587 RVA: 0x001FA1FA File Offset: 0x001F83FA
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		[ValidateNotNull]
		public Fqdn OutlookAnywhereHostName
		{
			get
			{
				return (Fqdn)base.Fields["OutlookAnywhereHostName"];
			}
			set
			{
				base.Fields["OutlookAnywhereHostName"] = value;
			}
		}

		// Token: 0x1700263C RID: 9788
		// (get) Token: 0x06007B64 RID: 31588 RVA: 0x001FA20D File Offset: 0x001F840D
		// (set) Token: 0x06007B65 RID: 31589 RVA: 0x001FA22E File Offset: 0x001F842E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool IsAdministrativeCredential
		{
			get
			{
				return (bool)(base.Fields["IsAdministrativeCredential"] ?? false);
			}
			set
			{
				base.Fields["IsAdministrativeCredential"] = value;
			}
		}

		// Token: 0x1700263D RID: 9789
		// (get) Token: 0x06007B66 RID: 31590 RVA: 0x001FA246 File Offset: 0x001F8446
		// (set) Token: 0x06007B67 RID: 31591 RVA: 0x001FA25D File Offset: 0x001F845D
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public DateTime? StartAfter
		{
			get
			{
				return (DateTime?)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x1700263E RID: 9790
		// (get) Token: 0x06007B68 RID: 31592 RVA: 0x001FA275 File Offset: 0x001F8475
		// (set) Token: 0x06007B69 RID: 31593 RVA: 0x001FA29A File Offset: 0x001F849A
		[Parameter(Mandatory = false)]
		public TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)(base.Fields["IncrementalSyncInterval"] ?? TimeSpan.Zero);
			}
			set
			{
				base.Fields["IncrementalSyncInterval"] = value;
			}
		}

		// Token: 0x1700263F RID: 9791
		// (get) Token: 0x06007B6A RID: 31594 RVA: 0x001FA2B2 File Offset: 0x001F84B2
		// (set) Token: 0x06007B6B RID: 31595 RVA: 0x001FA2BA File Offset: 0x001F84BA
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public new PSCredential RemoteCredential
		{
			get
			{
				return base.RemoteCredential;
			}
			set
			{
				base.RemoteCredential = value;
			}
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x001FA2C4 File Offset: 0x001F84C4
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			DateTime? timestamp = requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter);
			bool flag = RequestTaskHelper.CompareUtcTimeWithLocalTime(timestamp, this.StartAfter);
			bool flag2 = base.IsFieldSet("StartAfter") && !flag;
			if (flag2)
			{
				this.CheckJobStatusInQueuedForStartAfterSet(requestJob);
			}
			DateTime utcNow = DateTime.UtcNow;
			if (flag2 && this.StartAfter != null)
			{
				RequestTaskHelper.ValidateStartAfterTime(this.StartAfter.Value.ToUniversalTime(), new Task.TaskErrorLoggingDelegate(base.WriteError), utcNow);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				if (requestJob.IncrementalSyncInterval == TimeSpan.Zero || requestJob.JobType < MRSJobType.RequestJobE15_AutoResume)
				{
					base.WriteError(new IncrementalSyncIntervalCannotBeSetOnNonIncrementalRequestsException(), ErrorCategory.InvalidArgument, this.Identity);
				}
				RequestTaskHelper.ValidateIncrementalSyncInterval(this.IncrementalSyncInterval, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.IsFieldSet("StartAfter") && flag)
			{
				this.WriteWarning(Strings.WarningScheduledTimeIsUnchanged("StartAfter"));
			}
			base.ValidateRequest(requestJob);
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x001FA3C8 File Offset: 0x001F85C8
		protected override void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
			if (base.IsFieldSet("RemoteSourceMailboxServerLegacyDN"))
			{
				changedValuesTracker.AppendLine(string.Format("RemoteMailboxServerLegacyDN: {0} -> {1}", requestJob.RemoteMailboxServerLegacyDN, this.RemoteSourceMailboxServerLegacyDN));
				requestJob.RemoteMailboxServerLegacyDN = this.RemoteSourceMailboxServerLegacyDN;
			}
			if (base.IsFieldSet("OutlookAnywhereHostName"))
			{
				changedValuesTracker.AppendLine(string.Format("OutlookAnywhereHostName: {0} -> {1}", requestJob.OutlookAnywhereHostName, this.OutlookAnywhereHostName));
				requestJob.OutlookAnywhereHostName = this.OutlookAnywhereHostName;
			}
			if (base.IsFieldSet("RemoteCredential"))
			{
				changedValuesTracker.AppendLine("RemoteCredential: <secure> -> <secure>");
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, requestJob.AuthenticationMethod);
			}
			if (base.IsFieldSet("IsAdministrativeCredential"))
			{
				changedValuesTracker.AppendLine(string.Format("IsAdministrativeCredential: {0} -> {1}", requestJob.IsAdministrativeCredential, this.IsAdministrativeCredential));
				requestJob.IsAdministrativeCredential = new bool?(this.IsAdministrativeCredential);
			}
			if (base.IsFieldSet("StartAfter") && !RequestTaskHelper.CompareUtcTimeWithLocalTime(requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.StartAfter), this.StartAfter))
			{
				RequestTaskHelper.SetStartAfter(this.StartAfter, requestJob, changedValuesTracker);
			}
			if (base.IsFieldSet("IncrementalSyncInterval"))
			{
				changedValuesTracker.AppendLine(string.Format("IncrementalSyncInterval: {0} -> {1}", requestJob.IncrementalSyncInterval, this.IncrementalSyncInterval));
				requestJob.IncrementalSyncInterval = this.IncrementalSyncInterval;
			}
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x001FA52F File Offset: 0x001F872F
		private void CheckJobStatusInQueuedForStartAfterSet(TransactionalRequestJob requestJob)
		{
			if (!RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.Queued))
			{
				base.WriteError(new ErrorStartAfterCanBeSetOnlyInQueuedException(), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x04003D45 RID: 15685
		public const string ParameterRemoteSourceMailboxServerLegacyDN = "RemoteSourceMailboxServerLegacyDN";

		// Token: 0x04003D46 RID: 15686
		public const string ParameterOutlookAnywhereHostName = "OutlookAnywhereHostName";

		// Token: 0x04003D47 RID: 15687
		public const string ParameterIsAdministrativeCredential = "IsAdministrativeCredential";

		// Token: 0x04003D48 RID: 15688
		public const string ParameterIncrementalSyncInterval = "IncrementalSyncInterval";
	}
}

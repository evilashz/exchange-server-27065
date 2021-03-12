using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C6C RID: 3180
	public abstract class SetRequest<TIdentity> : SetRequestBase<TIdentity> where TIdentity : MRSRequestIdParameter
	{
		// Token: 0x06007956 RID: 31062 RVA: 0x001EEB71 File Offset: 0x001ECD71
		public SetRequest()
		{
		}

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x06007957 RID: 31063 RVA: 0x001EEB79 File Offset: 0x001ECD79
		// (set) Token: 0x06007958 RID: 31064 RVA: 0x001EEB81 File Offset: 0x001ECD81
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNull]
		public override TIdentity Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x06007959 RID: 31065 RVA: 0x001EEB8A File Offset: 0x001ECD8A
		// (set) Token: 0x0600795A RID: 31066 RVA: 0x001EEBAF File Offset: 0x001ECDAF
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["BadItemLimit"] ?? RequestTaskHelper.UnlimitedZero);
			}
			set
			{
				base.Fields["BadItemLimit"] = value;
			}
		}

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x0600795B RID: 31067 RVA: 0x001EEBC7 File Offset: 0x001ECDC7
		// (set) Token: 0x0600795C RID: 31068 RVA: 0x001EEBEC File Offset: 0x001ECDEC
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)(base.Fields["LargeItemLimit"] ?? RequestTaskHelper.UnlimitedZero);
			}
			set
			{
				base.Fields["LargeItemLimit"] = value;
			}
		}

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x0600795D RID: 31069 RVA: 0x001EEC04 File Offset: 0x001ECE04
		// (set) Token: 0x0600795E RID: 31070 RVA: 0x001EEC2A File Offset: 0x001ECE2A
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter AcceptLargeDataLoss
		{
			get
			{
				return (SwitchParameter)(base.Fields["AcceptLargeDataLoss"] ?? false);
			}
			set
			{
				base.Fields["AcceptLargeDataLoss"] = value;
			}
		}

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x0600795F RID: 31071 RVA: 0x001EEC42 File Offset: 0x001ECE42
		// (set) Token: 0x06007960 RID: 31072 RVA: 0x001EEC59 File Offset: 0x001ECE59
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string BatchName
		{
			get
			{
				return (string)base.Fields["BatchName"];
			}
			set
			{
				base.Fields["BatchName"] = value;
			}
		}

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x06007961 RID: 31073 RVA: 0x001EEC6C File Offset: 0x001ECE6C
		// (set) Token: 0x06007962 RID: 31074 RVA: 0x001EEC8E File Offset: 0x001ECE8E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RequestPriority Priority
		{
			get
			{
				return (RequestPriority)(base.Fields["Priority"] ?? RequestPriority.Normal);
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x06007963 RID: 31075 RVA: 0x001EECA6 File Offset: 0x001ECEA6
		// (set) Token: 0x06007964 RID: 31076 RVA: 0x001EECCB File Offset: 0x001ECECB
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)(base.Fields["CompletedRequestAgeLimit"] ?? RequestTaskHelper.DefaultCompletedRequestAgeLimit);
			}
			set
			{
				base.Fields["CompletedRequestAgeLimit"] = value;
			}
		}

		// Token: 0x1700258F RID: 9615
		// (get) Token: 0x06007965 RID: 31077 RVA: 0x001EECE3 File Offset: 0x001ECEE3
		// (set) Token: 0x06007966 RID: 31078 RVA: 0x001EECFF File Offset: 0x001ECEFF
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SkippableMergeComponent[] SkipMerging
		{
			get
			{
				return (SkippableMergeComponent[])(base.Fields["SkipMerging"] ?? null);
			}
			set
			{
				base.Fields["SkipMerging"] = value;
			}
		}

		// Token: 0x17002590 RID: 9616
		// (get) Token: 0x06007967 RID: 31079 RVA: 0x001EED12 File Offset: 0x001ECF12
		// (set) Token: 0x06007968 RID: 31080 RVA: 0x001EED2E File Offset: 0x001ECF2E
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public InternalMrsFlag[] InternalFlags
		{
			get
			{
				return (InternalMrsFlag[])(base.Fields["InternalFlags"] ?? null);
			}
			set
			{
				base.Fields["InternalFlags"] = value;
			}
		}

		// Token: 0x17002591 RID: 9617
		// (get) Token: 0x06007969 RID: 31081 RVA: 0x001EED41 File Offset: 0x001ECF41
		// (set) Token: 0x0600796A RID: 31082 RVA: 0x001EED67 File Offset: 0x001ECF67
		[Parameter(Mandatory = true, ParameterSetName = "Rehome")]
		public SwitchParameter RehomeRequest
		{
			get
			{
				return (SwitchParameter)(base.Fields["RehomeRequest"] ?? false);
			}
			set
			{
				base.Fields["RehomeRequest"] = value;
			}
		}

		// Token: 0x17002592 RID: 9618
		// (get) Token: 0x0600796B RID: 31083 RVA: 0x001EED7F File Offset: 0x001ECF7F
		// (set) Token: 0x0600796C RID: 31084 RVA: 0x001EED96 File Offset: 0x001ECF96
		public Fqdn RemoteHostName
		{
			get
			{
				return (Fqdn)base.Fields["RemoteHostName"];
			}
			set
			{
				base.Fields["RemoteHostName"] = value;
			}
		}

		// Token: 0x17002593 RID: 9619
		// (get) Token: 0x0600796D RID: 31085 RVA: 0x001EEDA9 File Offset: 0x001ECFA9
		// (set) Token: 0x0600796E RID: 31086 RVA: 0x001EEDC0 File Offset: 0x001ECFC0
		public PSCredential RemoteCredential
		{
			get
			{
				return (PSCredential)base.Fields["RemoteCredential"];
			}
			set
			{
				base.Fields["RemoteCredential"] = value;
			}
		}

		// Token: 0x17002594 RID: 9620
		// (get) Token: 0x0600796F RID: 31087 RVA: 0x001EEDD3 File Offset: 0x001ECFD3
		// (set) Token: 0x06007970 RID: 31088 RVA: 0x001EEDDB File Offset: 0x001ECFDB
		internal Guid MdbGuid { get; private set; }

		// Token: 0x17002595 RID: 9621
		// (get) Token: 0x06007971 RID: 31089 RVA: 0x001EEDE4 File Offset: 0x001ECFE4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRequest(base.RequestName);
			}
		}

		// Token: 0x06007972 RID: 31090 RVA: 0x001EEDF4 File Offset: 0x001ECFF4
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			base.ValidateRequest(requestJob);
			base.ValidateRequestIsActive(requestJob);
			base.ValidateRequestProtectionStatus(requestJob);
			base.ValidateRequestIsNotCancelled(requestJob);
			if (!base.ParameterSetName.Equals("Rehome"))
			{
				base.ValidateRequestIsRunnable(requestJob);
			}
			if (base.IsFieldSet("BadItemLimit") && this.BadItemLimit < new Unlimited<int>(requestJob.BadItemsEncountered))
			{
				base.WriteError(new BadItemLimitAlreadyExceededPermanentException(requestJob.Name, requestJob.BadItemsEncountered, this.BadItemLimit.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (base.IsFieldSet("LargeItemLimit") && this.LargeItemLimit < new Unlimited<int>(requestJob.LargeItemsEncountered))
			{
				base.WriteError(new LargeItemLimitAlreadyExceededPermanentException(requestJob.Name, requestJob.LargeItemsEncountered, this.LargeItemLimit.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (this.RehomeRequest && requestJob.RequestQueue != null && requestJob.RequestQueue.Equals(requestJob.OptimalRequestQueue))
			{
				base.WriteError(new RequestJobAlreadyOnProperQueuePermanentException(requestJob.Name, requestJob.RequestQueue.ObjectGuid.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x001EEF4C File Offset: 0x001ED14C
		protected virtual void ModifyRequestInternal(TransactionalRequestJob requestJob, StringBuilder changedValuesTracker)
		{
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x001EEF50 File Offset: 0x001ED150
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			this.MdbGuid = requestJob.WorkItemQueueMdb.ObjectGuid;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("SetRequest changed values:");
			if (requestJob.TargetUser != null)
			{
				requestJob.DomainControllerToUpdate = requestJob.TargetUser.OriginatingServer;
			}
			else if (requestJob.SourceUser != null)
			{
				requestJob.DomainControllerToUpdate = requestJob.SourceUser.OriginatingServer;
			}
			if (base.IsFieldSet("BadItemLimit"))
			{
				stringBuilder.AppendLine(string.Format("BadItemLimit: {0} -> {1}", requestJob.BadItemLimit, this.BadItemLimit));
				requestJob.BadItemLimit = this.BadItemLimit;
			}
			if (base.IsFieldSet("LargeItemLimit"))
			{
				stringBuilder.AppendLine(string.Format("LargeItemLimit: {0} -> {1}", requestJob.LargeItemLimit, this.LargeItemLimit));
				requestJob.LargeItemLimit = this.LargeItemLimit;
			}
			if (base.IsFieldSet("BatchName"))
			{
				stringBuilder.AppendLine(string.Format("BatchName: {0} -> {1}", requestJob.BatchName, this.BatchName));
				requestJob.BatchName = (this.BatchName ?? string.Empty);
			}
			if (base.IsFieldSet("Priority"))
			{
				stringBuilder.AppendLine(string.Format("Priority: {0} -> {1}", requestJob.Priority, this.Priority));
				requestJob.Priority = this.Priority;
			}
			if (base.IsFieldSet("CompletedRequestAgeLimit"))
			{
				stringBuilder.AppendLine(string.Format("CompletedRequestAgeLimit: {0} -> {1}", requestJob.CompletedRequestAgeLimit, this.CompletedRequestAgeLimit));
				requestJob.CompletedRequestAgeLimit = this.CompletedRequestAgeLimit;
			}
			if (this.RehomeRequest)
			{
				stringBuilder.AppendLine(string.Format("RehomeRequest: {0} -> {1}", requestJob.RehomeRequest, this.RehomeRequest));
				requestJob.RehomeRequest = this.RehomeRequest;
			}
			if (base.IsFieldSet("SkipMerging"))
			{
				RequestJobInternalFlags requestJobInternalFlags = requestJob.RequestJobInternalFlags;
				RequestTaskHelper.SetSkipMerging(this.SkipMerging, requestJob, new Task.TaskErrorLoggingDelegate(base.WriteError));
				stringBuilder.AppendLine(string.Format("InternalFlags: {0} -> {1}", requestJobInternalFlags, requestJob.RequestJobInternalFlags));
			}
			if (base.IsFieldSet("InternalFlags"))
			{
				RequestJobInternalFlags requestJobInternalFlags2 = requestJob.RequestJobInternalFlags;
				RequestTaskHelper.SetInternalFlags(this.InternalFlags, requestJob, new Task.TaskErrorLoggingDelegate(base.WriteError));
				stringBuilder.AppendLine(string.Format("InternalFlags: {0} -> {1}", requestJobInternalFlags2, requestJob.RequestJobInternalFlags));
			}
			if (base.IsFieldSet("RemoteHostName"))
			{
				stringBuilder.AppendLine(string.Format("RemoteHostName: {0} -> {1}", requestJob.RemoteHostName, this.RemoteHostName));
				requestJob.RemoteHostName = this.RemoteHostName;
			}
			if (base.IsFieldSet("RemoteCredential"))
			{
				stringBuilder.AppendLine(string.Format("RemoteCredential: * -> *", new object[0]));
				requestJob.RemoteCredential = RequestTaskHelper.GetNetworkCredential(this.RemoteCredential, new AuthenticationMethod?(AuthenticationMethod.WindowsIntegrated));
			}
			this.ModifyRequestInternal(requestJob, stringBuilder);
			ReportData reportData = new ReportData(requestJob.RequestGuid, requestJob.ReportVersion);
			ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
			reportData.Append(MrsStrings.ReportRequestSet(base.ExecutingUserIdentity), connectivityRec);
			reportData.AppendDebug(stringBuilder.ToString());
			if (this.AcceptLargeDataLoss)
			{
				reportData.Append(MrsStrings.ReportLargeAmountOfDataLossAccepted2(requestJob.BadItemLimit.ToString(), requestJob.LargeItemLimit.ToString(), base.ExecutingUserIdentity));
			}
			reportData.Flush(base.RJProvider.SystemMailbox);
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x001EF2E9 File Offset: 0x001ED4E9
		protected override void PostSaveAction()
		{
			RequestTaskHelper.TickleMRS(this.DataObject, this.RehomeRequest ? MoveRequestNotification.SuspendResume : MoveRequestNotification.Updated, this.MdbGuid, base.ConfigSession, base.UnreachableMrsServers);
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x001EF31C File Offset: 0x001ED51C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			RequestTaskHelper.ValidateItemLimits(this.BadItemLimit, this.LargeItemLimit, this.AcceptLargeDataLoss, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), base.ExecutingUserIdentity);
			if (this.BatchName != null && this.BatchName.Length > 255)
			{
				base.WriteError(new ParameterLengthExceededPermanentException("BatchName", 255), ErrorCategory.InvalidArgument, this.BatchName);
			}
		}

		// Token: 0x04003C5B RID: 15451
		public const string ParameterSetRehome = "Rehome";
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C62 RID: 3170
	public abstract class NewRequest<TRequest> : NewTaskBase<TransactionalRequestJob> where TRequest : RequestBase, new()
	{
		// Token: 0x06007879 RID: 30841 RVA: 0x001EAFB0 File Offset: 0x001E91B0
		protected NewRequest()
		{
			this.MRSClient = null;
			this.RecipSession = null;
			this.GCSession = null;
			this.RJProvider = null;
			this.OrganizationId = null;
			this.MdbId = null;
			this.MdbServerSite = null;
			this.UnreachableMrsServers = new List<string>();
			this.NormalizedContentFilter = null;
			this.GeneralReportEntries = new List<ReportEntry>();
			this.PerRecordReportEntries = new List<ReportEntry>();
			this.ConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 95, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\NewRequest.cs");
		}

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x0600787A RID: 30842 RVA: 0x001EB041 File Offset: 0x001E9241
		// (set) Token: 0x0600787B RID: 30843 RVA: 0x001EB066 File Offset: 0x001E9266
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x0600787C RID: 30844 RVA: 0x001EB07E File Offset: 0x001E927E
		// (set) Token: 0x0600787D RID: 30845 RVA: 0x001EB0A3 File Offset: 0x001E92A3
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x0600787E RID: 30846 RVA: 0x001EB0BB File Offset: 0x001E92BB
		// (set) Token: 0x0600787F RID: 30847 RVA: 0x001EB0E1 File Offset: 0x001E92E1
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x06007880 RID: 30848 RVA: 0x001EB0F9 File Offset: 0x001E92F9
		// (set) Token: 0x06007881 RID: 30849 RVA: 0x001EB110 File Offset: 0x001E9310
		[Parameter(Mandatory = false)]
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

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x06007882 RID: 30850 RVA: 0x001EB123 File Offset: 0x001E9323
		// (set) Token: 0x06007883 RID: 30851 RVA: 0x001EB12B File Offset: 0x001E932B
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x06007884 RID: 30852 RVA: 0x001EB134 File Offset: 0x001E9334
		// (set) Token: 0x06007885 RID: 30853 RVA: 0x001EB15A File Offset: 0x001E935A
		[Parameter(Mandatory = false)]
		public SwitchParameter Suspend
		{
			get
			{
				return (SwitchParameter)(base.Fields["Suspend"] ?? false);
			}
			set
			{
				base.Fields["Suspend"] = value;
			}
		}

		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x06007886 RID: 30854 RVA: 0x001EB172 File Offset: 0x001E9372
		// (set) Token: 0x06007887 RID: 30855 RVA: 0x001EB189 File Offset: 0x001E9389
		[Parameter(Mandatory = false)]
		public string SuspendComment
		{
			get
			{
				return (string)base.Fields["SuspendComment"];
			}
			set
			{
				base.Fields["SuspendComment"] = value;
			}
		}

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x06007888 RID: 30856 RVA: 0x001EB19C File Offset: 0x001E939C
		// (set) Token: 0x06007889 RID: 30857 RVA: 0x001EB1B3 File Offset: 0x001E93B3
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x0600788A RID: 30858 RVA: 0x001EB1C6 File Offset: 0x001E93C6
		// (set) Token: 0x0600788B RID: 30859 RVA: 0x001EB1E8 File Offset: 0x001E93E8
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x0600788C RID: 30860 RVA: 0x001EB200 File Offset: 0x001E9400
		// (set) Token: 0x0600788D RID: 30861 RVA: 0x001EB221 File Offset: 0x001E9421
		[Parameter(Mandatory = false)]
		public RequestWorkloadType WorkloadType
		{
			get
			{
				return (RequestWorkloadType)(base.Fields["WorkloadType"] ?? RequestWorkloadType.None);
			}
			set
			{
				base.Fields["WorkloadType"] = value;
			}
		}

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x0600788E RID: 30862 RVA: 0x001EB239 File Offset: 0x001E9439
		// (set) Token: 0x0600788F RID: 30863 RVA: 0x001EB25E File Offset: 0x001E945E
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x06007890 RID: 30864 RVA: 0x001EB276 File Offset: 0x001E9476
		// (set) Token: 0x06007891 RID: 30865 RVA: 0x001EB292 File Offset: 0x001E9492
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x06007892 RID: 30866 RVA: 0x001EB2A5 File Offset: 0x001E94A5
		// (set) Token: 0x06007893 RID: 30867 RVA: 0x001EB2C1 File Offset: 0x001E94C1
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06007894 RID: 30868 RVA: 0x001EB2D4 File Offset: 0x001E94D4
		// (set) Token: 0x06007895 RID: 30869 RVA: 0x001EB2EB File Offset: 0x001E94EB
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

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x06007896 RID: 30870 RVA: 0x001EB2FE File Offset: 0x001E94FE
		// (set) Token: 0x06007897 RID: 30871 RVA: 0x001EB324 File Offset: 0x001E9524
		public SwitchParameter AllowLegacyDNMismatch
		{
			get
			{
				return (SwitchParameter)(base.Fields["AllowLegacyDNMismatch"] ?? false);
			}
			set
			{
				base.Fields["AllowLegacyDNMismatch"] = value;
			}
		}

		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x06007898 RID: 30872 RVA: 0x001EB33C File Offset: 0x001E953C
		// (set) Token: 0x06007899 RID: 30873 RVA: 0x001EB353 File Offset: 0x001E9553
		public string ContentFilter
		{
			get
			{
				return (string)base.Fields["ContentFilter"];
			}
			set
			{
				base.Fields["ContentFilter"] = value;
			}
		}

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x0600789A RID: 30874 RVA: 0x001EB366 File Offset: 0x001E9566
		// (set) Token: 0x0600789B RID: 30875 RVA: 0x001EB386 File Offset: 0x001E9586
		public CultureInfo ContentFilterLanguage
		{
			get
			{
				return (CultureInfo)(base.Fields["ContentFilterLanguage"] ?? CultureInfo.InvariantCulture);
			}
			set
			{
				base.Fields["ContentFilterLanguage"] = value;
			}
		}

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x0600789C RID: 30876 RVA: 0x001EB399 File Offset: 0x001E9599
		// (set) Token: 0x0600789D RID: 30877 RVA: 0x001EB3B0 File Offset: 0x001E95B0
		public string[] IncludeFolders
		{
			get
			{
				return (string[])base.Fields["IncludeFolders"];
			}
			set
			{
				base.Fields["IncludeFolders"] = value;
			}
		}

		// Token: 0x17002553 RID: 9555
		// (get) Token: 0x0600789E RID: 30878 RVA: 0x001EB3C3 File Offset: 0x001E95C3
		// (set) Token: 0x0600789F RID: 30879 RVA: 0x001EB3DA File Offset: 0x001E95DA
		public string[] ExcludeFolders
		{
			get
			{
				return (string[])base.Fields["ExcludeFolders"];
			}
			set
			{
				base.Fields["ExcludeFolders"] = value;
			}
		}

		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x060078A0 RID: 30880 RVA: 0x001EB3ED File Offset: 0x001E95ED
		// (set) Token: 0x060078A1 RID: 30881 RVA: 0x001EB413 File Offset: 0x001E9613
		public SwitchParameter ExcludeDumpster
		{
			get
			{
				return (SwitchParameter)(base.Fields["ExcludeDumpster"] ?? false);
			}
			set
			{
				base.Fields["ExcludeDumpster"] = value;
			}
		}

		// Token: 0x17002555 RID: 9557
		// (get) Token: 0x060078A2 RID: 30882 RVA: 0x001EB42B File Offset: 0x001E962B
		// (set) Token: 0x060078A3 RID: 30883 RVA: 0x001EB44C File Offset: 0x001E964C
		public ConflictResolutionOption ConflictResolutionOption
		{
			get
			{
				return (ConflictResolutionOption)(base.Fields["ConflictResolutionOption"] ?? ConflictResolutionOption.KeepSourceItem);
			}
			set
			{
				base.Fields["ConflictResolutionOption"] = value;
			}
		}

		// Token: 0x17002556 RID: 9558
		// (get) Token: 0x060078A4 RID: 30884 RVA: 0x001EB464 File Offset: 0x001E9664
		// (set) Token: 0x060078A5 RID: 30885 RVA: 0x001EB485 File Offset: 0x001E9685
		public FAICopyOption AssociatedMessagesCopyOption
		{
			get
			{
				return (FAICopyOption)(base.Fields["AssociatedMessagesCopyOption"] ?? FAICopyOption.Copy);
			}
			set
			{
				base.Fields["AssociatedMessagesCopyOption"] = value;
			}
		}

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x060078A6 RID: 30886 RVA: 0x001EB49D File Offset: 0x001E969D
		// (set) Token: 0x060078A7 RID: 30887 RVA: 0x001EB4B4 File Offset: 0x001E96B4
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

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x060078A8 RID: 30888 RVA: 0x001EB4C7 File Offset: 0x001E96C7
		// (set) Token: 0x060078A9 RID: 30889 RVA: 0x001EB4DE File Offset: 0x001E96DE
		public DateTime StartAfter
		{
			get
			{
				return (DateTime)base.Fields["StartAfter"];
			}
			set
			{
				base.Fields["StartAfter"] = value;
			}
		}

		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x060078AA RID: 30890 RVA: 0x001EB4F6 File Offset: 0x001E96F6
		// (set) Token: 0x060078AB RID: 30891 RVA: 0x001EB4FE File Offset: 0x001E96FE
		internal MailboxReplicationServiceClient MRSClient { get; private set; }

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x060078AC RID: 30892 RVA: 0x001EB507 File Offset: 0x001E9707
		// (set) Token: 0x060078AD RID: 30893 RVA: 0x001EB50F File Offset: 0x001E970F
		internal RequestJobProvider RJProvider { get; private set; }

		// Token: 0x1700255B RID: 9563
		// (get) Token: 0x060078AE RID: 30894 RVA: 0x001EB518 File Offset: 0x001E9718
		// (set) Token: 0x060078AF RID: 30895 RVA: 0x001EB520 File Offset: 0x001E9720
		internal List<string> UnreachableMrsServers { get; private set; }

		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x060078B0 RID: 30896 RVA: 0x001EB529 File Offset: 0x001E9729
		// (set) Token: 0x060078B1 RID: 30897 RVA: 0x001EB531 File Offset: 0x001E9731
		internal ADObjectId MdbId { get; set; }

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x060078B2 RID: 30898 RVA: 0x001EB53A File Offset: 0x001E973A
		// (set) Token: 0x060078B3 RID: 30899 RVA: 0x001EB542 File Offset: 0x001E9742
		internal ADObjectId MdbServerSite { get; set; }

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x060078B4 RID: 30900 RVA: 0x001EB54B File Offset: 0x001E974B
		// (set) Token: 0x060078B5 RID: 30901 RVA: 0x001EB553 File Offset: 0x001E9753
		internal IRecipientSession GCSession { get; set; }

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x060078B6 RID: 30902 RVA: 0x001EB55C File Offset: 0x001E975C
		// (set) Token: 0x060078B7 RID: 30903 RVA: 0x001EB564 File Offset: 0x001E9764
		internal IRecipientSession RecipSession { get; set; }

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x060078B8 RID: 30904 RVA: 0x001EB56D File Offset: 0x001E976D
		// (set) Token: 0x060078B9 RID: 30905 RVA: 0x001EB575 File Offset: 0x001E9775
		internal IConfigurationSession CurrentOrgConfigSession { get; set; }

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x060078BA RID: 30906 RVA: 0x001EB57E File Offset: 0x001E977E
		// (set) Token: 0x060078BB RID: 30907 RVA: 0x001EB586 File Offset: 0x001E9786
		internal ITopologyConfigurationSession ConfigSession { get; set; }

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x060078BC RID: 30908 RVA: 0x001EB58F File Offset: 0x001E978F
		// (set) Token: 0x060078BD RID: 30909 RVA: 0x001EB597 File Offset: 0x001E9797
		internal string NormalizedContentFilter { get; private set; }

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x060078BE RID: 30910 RVA: 0x001EB5A0 File Offset: 0x001E97A0
		internal string ExecutingUserIdentity
		{
			get
			{
				return base.ExecutingUserIdentityName;
			}
		}

		// Token: 0x17002564 RID: 9572
		// (get) Token: 0x060078BF RID: 30911 RVA: 0x001EB5A8 File Offset: 0x001E97A8
		// (set) Token: 0x060078C0 RID: 30912 RVA: 0x001EB5B0 File Offset: 0x001E97B0
		internal List<ReportEntry> GeneralReportEntries { get; private set; }

		// Token: 0x17002565 RID: 9573
		// (get) Token: 0x060078C1 RID: 30913 RVA: 0x001EB5B9 File Offset: 0x001E97B9
		// (set) Token: 0x060078C2 RID: 30914 RVA: 0x001EB5C1 File Offset: 0x001E97C1
		internal List<ReportEntry> PerRecordReportEntries { get; private set; }

		// Token: 0x17002566 RID: 9574
		// (get) Token: 0x060078C3 RID: 30915 RVA: 0x001EB5CA File Offset: 0x001E97CA
		// (set) Token: 0x060078C4 RID: 30916 RVA: 0x001EB5D2 File Offset: 0x001E97D2
		internal OrganizationId OrganizationId { get; set; }

		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x060078C5 RID: 30917 RVA: 0x001EB5DB File Offset: 0x001E97DB
		// (set) Token: 0x060078C6 RID: 30918 RVA: 0x001EB5E3 File Offset: 0x001E97E3
		internal RequestFlags Flags { get; set; }

		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x060078C7 RID: 30919 RVA: 0x001EB5EC File Offset: 0x001E97EC
		// (set) Token: 0x060078C8 RID: 30920 RVA: 0x001EB5F4 File Offset: 0x001E97F4
		internal string RequestName { get; set; }

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x060078C9 RID: 30921 RVA: 0x001EB5FD File Offset: 0x001E97FD
		protected virtual KeyValuePair<string, LocalizedString>[] ExtendedAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x001EB600 File Offset: 0x001E9800
		internal virtual RequestFlags LocateAndChooseMdb(ADObjectId sourceDatabaseId, ADObjectId targetDatabaseId, object sourceErrorObject, object targetErrorObject, object storageErrorObject, out ADObjectId mdb, out ADObjectId serverSite)
		{
			bool flag = false;
			bool flag2 = false;
			ADObjectId adobjectId = null;
			ADObjectId adobjectId2 = null;
			int num = 0;
			int num2 = 0;
			mdb = null;
			serverSite = null;
			if (sourceDatabaseId != null)
			{
				string text;
				string text2;
				this.CheckDatabase<MailboxDatabase>(new DatabaseIdParameter(sourceDatabaseId), NewRequest<TRequest>.DatabaseSide.Source, sourceErrorObject, out text, out text2, out adobjectId, out num);
				flag = this.IsSupportedDatabaseVersion(num, NewRequest<TRequest>.DatabaseSide.RequestStorage);
			}
			if (targetDatabaseId != null)
			{
				string text3;
				string text4;
				this.CheckDatabase<MailboxDatabase>(new DatabaseIdParameter(targetDatabaseId), NewRequest<TRequest>.DatabaseSide.Target, targetErrorObject, out text3, out text4, out adobjectId2, out num2);
				flag2 = this.IsSupportedDatabaseVersion(num2, NewRequest<TRequest>.DatabaseSide.RequestStorage);
			}
			if (!flag && !flag2)
			{
				base.WriteError(new MailboxDatabaseVersionUnsupportedPermanentException(Strings.ErrorStorageMailboxDatabaseVersionUnsupported), ErrorCategory.InvalidArgument, storageErrorObject);
				return RequestFlags.None;
			}
			if (flag && !flag2)
			{
				mdb = sourceDatabaseId;
				serverSite = adobjectId;
				return RequestFlags.Push;
			}
			if (!flag && flag2)
			{
				mdb = targetDatabaseId;
				serverSite = adobjectId2;
				return RequestFlags.Pull;
			}
			if (num > num2)
			{
				mdb = sourceDatabaseId;
				serverSite = adobjectId;
				return RequestFlags.Push;
			}
			mdb = targetDatabaseId;
			serverSite = adobjectId2;
			return RequestFlags.Pull;
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x001EB6C8 File Offset: 0x001E98C8
		protected virtual ADObjectId AutoSelectRequestQueueForPFRequest(OrganizationId orgId)
		{
			Guid guid = Guid.Empty;
			TenantPublicFolderConfigurationCache.Instance.RemoveValue(orgId);
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(orgId);
			PublicFolderInformation hierarchyMailboxInformation = value.GetHierarchyMailboxInformation();
			guid = hierarchyMailboxInformation.HierarchyMailboxGuid;
			if (guid == Guid.Empty)
			{
				base.WriteError(new RecipientTaskException(MrsStrings.PublicFolderMailboxesNotProvisionedForMigration), ExchangeErrorCategory.ServerOperation, null);
			}
			PublicFolderRecipient localMailboxRecipient = value.GetLocalMailboxRecipient(guid);
			return localMailboxRecipient.Database;
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x001EB731 File Offset: 0x001E9931
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.RJProvider != null)
				{
					this.RJProvider.Dispose();
					this.RJProvider = null;
				}
				if (this.MRSClient != null)
				{
					this.MRSClient.Dispose();
					this.MRSClient = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x001EB774 File Offset: 0x001E9974
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				ReportEntry reportEntry = new ReportEntry(MrsStrings.ReportRequestCreated(this.ExecutingUserIdentity));
				reportEntry.Connectivity = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				this.GeneralReportEntries.Insert(0, reportEntry);
				RequestTaskHelper.ValidateItemLimits(this.BadItemLimit, this.LargeItemLimit, this.AcceptLargeDataLoss, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning), this.ExecutingUserIdentity);
				if (this.SuspendComment != null && !this.Suspend)
				{
					base.WriteError(new SuspendCommentWithoutSuspendPermanentException(), ErrorCategory.InvalidArgument, this.SuspendComment);
				}
				if (!string.IsNullOrEmpty(this.SuspendComment) && this.SuspendComment.Length > 4096)
				{
					base.WriteError(new ParameterLengthExceededPermanentException("SuspendComment", 4096), ErrorCategory.InvalidArgument, this.SuspendComment);
				}
				if (!string.IsNullOrEmpty(this.Name) && this.Name.Length > 255)
				{
					base.WriteError(new ParameterLengthExceededPermanentException("Name", 255), ErrorCategory.InvalidArgument, this.Name);
				}
				if (!string.IsNullOrEmpty(this.BatchName) && this.BatchName.Length > 255)
				{
					base.WriteError(new ParameterLengthExceededPermanentException("BatchName", 255), ErrorCategory.InvalidArgument, this.BatchName);
				}
				if (!string.IsNullOrEmpty(this.ContentFilter))
				{
					this.NormalizedContentFilter = this.NormalizeContentFilter();
				}
				this.ValidateFolderFilter();
				base.InternalBeginProcessing();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x001EB90C File Offset: 0x001E9B0C
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null);
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			this.CurrentOrgConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 772, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\NewRequest.cs");
			sessionSettings = ADSessionSettings.RescopeToSubtree(sessionSettings);
			this.GCSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 783, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\NewRequest.cs");
			this.RecipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 791, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MailboxReplication\\RequestBase\\NewRequest.cs");
			if (this.RJProvider != null)
			{
				this.RJProvider.Dispose();
				this.RJProvider = null;
			}
			this.RJProvider = new RequestJobProvider(this.RecipSession, this.CurrentOrgConfigSession);
			return this.RJProvider;
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x001EBA0C File Offset: 0x001E9C0C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				this.RJProvider.AttachToMDB(this.MdbId.ObjectGuid);
				this.InitializeMRSClient();
				TransactionalRequestJob dataObject = this.DataObject;
				dataObject.TimeTracker.SetTimestamp(RequestJobTimestamp.Creation, new DateTime?(DateTime.UtcNow));
				dataObject.TimeTracker.CurrentState = RequestState.Queued;
				dataObject.JobType = MRSJobType.RequestJobE15_TenantHint;
				dataObject.RequestGuid = Guid.NewGuid();
				dataObject.AllowedToFinishMove = true;
				dataObject.BatchName = this.BatchName;
				dataObject.BadItemLimit = this.BadItemLimit;
				dataObject.LargeItemLimit = this.LargeItemLimit;
				dataObject.Status = RequestStatus.Queued;
				dataObject.RequestJobState = JobProcessingState.Ready;
				dataObject.Identity = new RequestJobObjectId(dataObject.RequestGuid, this.MdbId.ObjectGuid, null);
				dataObject.RequestQueue = ADObjectIdResolutionHelper.ResolveDN(this.MdbId);
				dataObject.RequestCreator = this.ExecutingUserIdentity;
				this.SetRequestProperties(dataObject);
				this.CreateIndexEntries(dataObject);
				dataObject.Suspend = this.Suspend;
				if (!string.IsNullOrEmpty(this.SuspendComment))
				{
					dataObject.Message = MrsStrings.MoveRequestMessageInformational(new LocalizedString(this.SuspendComment));
				}
				base.InternalValidate();
				if (this.MRSClient != null)
				{
					List<ReportEntry> entries = null;
					using (TransactionalRequestJob transactionalRequestJob = this.MRSClient.ValidateAndPopulateRequestJob(this.DataObject, out entries))
					{
						this.CopyDataToDataObject(transactionalRequestJob);
						RequestTaskHelper.WriteReportEntries(dataObject.Name, entries, dataObject.Identity, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x001EBBD8 File Offset: 0x001E9DD8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				TransactionalRequestJob dataObject = this.DataObject;
				if (!base.Stopping)
				{
					ReportData reportData = new ReportData(dataObject.RequestGuid, dataObject.ReportVersion);
					reportData.Delete(this.RJProvider.SystemMailbox);
					reportData.Append(this.GeneralReportEntries);
					reportData.Append(this.PerRecordReportEntries);
					reportData.Flush(this.RJProvider.SystemMailbox);
					base.InternalProcessRecord();
					RequestJobLog.Write(dataObject);
					if (this.MRSClient != null)
					{
						if (this.MRSClient.ServerVersion[3])
						{
							this.MRSClient.RefreshMoveRequest2(dataObject.RequestGuid, this.MdbId.ObjectGuid, (int)dataObject.Flags, MoveRequestNotification.Created);
						}
						else
						{
							this.MRSClient.RefreshMoveRequest(dataObject.RequestGuid, this.MdbId.ObjectGuid, MoveRequestNotification.Created);
						}
					}
					dataObject.CreateAsyncNotification((base.ExchangeRunspaceConfig != null) ? base.ExchangeRunspaceConfig.ExecutingUserAsRecipient : null, this.ExtendedAttributes);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060078D1 RID: 30929 RVA: 0x001EBD00 File Offset: 0x001E9F00
		protected override void InternalStateReset()
		{
			if (this.DataObject != null)
			{
				this.DataObject.Dispose();
				this.DataObject = null;
			}
			if (this.MRSClient != null)
			{
				this.MRSClient.Dispose();
				this.MRSClient = null;
			}
			this.PerRecordReportEntries.Clear();
			base.InternalStateReset();
			this.MdbId = null;
			this.OrganizationId = null;
			this.Flags = RequestFlags.None;
			this.RequestName = null;
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x001EBD6E File Offset: 0x001E9F6E
		protected override bool IsKnownException(Exception exception)
		{
			return RequestTaskHelper.IsKnownExceptionHandler(exception, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)) || base.IsKnownException(exception);
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x001EBD90 File Offset: 0x001E9F90
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			LocalizedException ex = RequestTaskHelper.TranslateExceptionHandler(e);
			if (ex == null)
			{
				ErrorCategory errorCategory;
				base.TranslateException(ref e, out errorCategory);
				category = errorCategory;
				return;
			}
			e = ex;
			category = ErrorCategory.ResourceUnavailable;
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x001EBDBC File Offset: 0x001E9FBC
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(this.DataObject.Identity, base.DataSession, typeof(TransactionalRequestJob)));
			IConfigurable configurable = null;
			try
			{
				try
				{
					configurable = base.DataSession.Read<TransactionalRequestJob>(this.DataObject.Identity);
				}
				finally
				{
					base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
				}
				if (configurable == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.DataObject.Identity.ToString(), typeof(TRequest).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, this.DataObject.Identity);
				}
				this.WriteResult(configurable);
			}
			finally
			{
				IDisposable disposable = configurable as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
					configurable = null;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060078D5 RID: 30933 RVA: 0x001EBED0 File Offset: 0x001EA0D0
		protected override void WriteResult(IConfigurable dataObject)
		{
			TRequest trequest = this.ConvertToPresentationObject(dataObject as TransactionalRequestJob);
			base.WriteResult(trequest);
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x001EBEF8 File Offset: 0x001EA0F8
		protected virtual void SetRequestProperties(TransactionalRequestJob dataObject)
		{
			dataObject.WorkloadType = this.WorkloadType;
			dataObject.IncludeFolders = this.IncludeFolders;
			dataObject.ExcludeFolders = this.ExcludeFolders;
			dataObject.ExcludeDumpster = this.ExcludeDumpster;
			if (!string.IsNullOrEmpty(this.NormalizedContentFilter))
			{
				dataObject.ContentFilter = this.NormalizedContentFilter;
				dataObject.ContentFilterLCID = this.ContentFilterLanguage.LCID;
			}
			dataObject.ConflictResolutionOption = new ConflictResolutionOption?(this.ConflictResolutionOption);
			dataObject.AssociatedMessagesCopyOption = new FAICopyOption?(this.AssociatedMessagesCopyOption);
			dataObject.Priority = this.Priority;
			dataObject.CompletedRequestAgeLimit = this.CompletedRequestAgeLimit;
			RequestTaskHelper.ApplyOrganization(dataObject, this.OrganizationId ?? OrganizationId.ForestWideOrgId);
			dataObject.Flags = this.Flags;
			dataObject.Name = this.RequestName;
			this.SetSkipMergingAndInternalFlags(dataObject);
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x001EBFD2 File Offset: 0x001EA1D2
		protected virtual void CreateIndexEntries(TransactionalRequestJob dataObject)
		{
			RequestIndexEntryProvider.CreateAndPopulateRequestIndexEntries(dataObject, this.CurrentOrgConfigSession);
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x001EBFE0 File Offset: 0x001EA1E0
		protected virtual void CopyDataToDataObject(TransactionalRequestJob requestJob)
		{
			if (this.DataObject != null)
			{
				this.DataObject.SourceVersion = requestJob.SourceVersion;
				this.DataObject.SourceServer = requestJob.SourceServer;
				this.DataObject.SourceArchiveVersion = requestJob.SourceArchiveVersion;
				this.DataObject.SourceArchiveServer = requestJob.SourceArchiveServer;
				this.DataObject.TargetVersion = requestJob.TargetVersion;
				this.DataObject.TargetServer = requestJob.TargetServer;
				this.DataObject.TargetArchiveVersion = requestJob.TargetArchiveVersion;
				this.DataObject.TargetArchiveServer = requestJob.TargetArchiveServer;
			}
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x001EC080 File Offset: 0x001EA280
		protected void ValidateLegacyDNMatch(string sourceDN, ADUser targetUser, object indicate)
		{
			if (!StringComparer.OrdinalIgnoreCase.Equals(sourceDN, targetUser.LegacyExchangeDN) && !targetUser.EmailAddresses.Contains(new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.X500, sourceDN, true)))
			{
				if (this.AllowLegacyDNMismatch)
				{
					this.PerRecordReportEntries.Add(new ReportEntry(MrsStrings.ReportRequestAllowedMismatch(this.ExecutingUserIdentity)));
					return;
				}
				base.WriteError(new NonMatchingLegacyDNPermanentException(sourceDN, targetUser.ToString(), "AllowLegacyDNMismatch"), ErrorCategory.InvalidArgument, indicate);
			}
		}

		// Token: 0x060078DA RID: 30938 RVA: 0x001EC100 File Offset: 0x001EA300
		protected bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x060078DB RID: 30939
		protected abstract TRequest ConvertToPresentationObject(TransactionalRequestJob dataObject);

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x060078DC RID: 30940 RVA: 0x001EC11E File Offset: 0x001EA31E
		protected virtual RequestIndexId DefaultRequestIndexId
		{
			get
			{
				return new RequestIndexId(RequestIndexLocation.AD);
			}
		}

		// Token: 0x060078DD RID: 30941 RVA: 0x001EC128 File Offset: 0x001EA328
		protected bool CheckRequestOfTypeExists(MRSRequestType requestType)
		{
			RequestIndexEntryQueryFilter filter = new RequestIndexEntryQueryFilter(null, null, requestType, this.DefaultRequestIndexId, false);
			ObjectId rootId = ADHandler.GetRootId(this.CurrentOrgConfigSession, requestType);
			IEnumerable<TRequest> enumerable = ((RequestJobProvider)base.DataSession).IndexProvider.FindPaged<TRequest>(filter, rootId, rootId == null, null, 10);
			foreach (TRequest trequest in enumerable)
			{
				if (trequest.Type == requestType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060078DE RID: 30942 RVA: 0x001EC1C4 File Offset: 0x001EA3C4
		protected virtual string CheckRequestNameAvailability(string name, ADObjectId identifyingObjectId, bool objectIsMailbox, MRSRequestType requestType, object errorObject, bool wildcardedSearch)
		{
			string text = string.Format("{0}*", name);
			RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = new RequestIndexEntryQueryFilter(wildcardedSearch ? text : name, identifyingObjectId, requestType, this.DefaultRequestIndexId, objectIsMailbox);
			requestIndexEntryQueryFilter.WildcardedNameSearch = wildcardedSearch;
			ObjectId rootId = ADHandler.GetRootId(this.CurrentOrgConfigSession, requestType);
			IEnumerable<TRequest> enumerable = ((RequestJobProvider)base.DataSession).IndexProvider.FindPaged<TRequest>(requestIndexEntryQueryFilter, rootId, rootId == null, null, 10);
			string result;
			using (IEnumerator<TRequest> enumerator = enumerable.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = name;
				}
				else if (!wildcardedSearch)
				{
					if (requestType.Equals(MRSRequestType.PublicFolderMigration) || requestType.Equals(MRSRequestType.PublicFolderMove))
					{
						base.WriteError(new MultipleSamePublicFolderMRSJobInstancesNotAllowedException(requestType.ToString()), ErrorCategory.InvalidArgument, errorObject);
					}
					else
					{
						base.WriteError(new NameMustBeUniquePermanentException(name, (identifyingObjectId == null) ? string.Empty : identifyingObjectId.ToString()), ErrorCategory.InvalidArgument, errorObject);
					}
					result = null;
				}
				else
				{
					Regex regex = new Regex(string.Format("^{0}(\\d)?$", name));
					List<uint> list = new List<uint>(10);
					for (uint num = 0U; num < 10U; num += 1U)
					{
						list.Add(num);
					}
					do
					{
						TRequest trequest = enumerator.Current;
						Match match = regex.Match(trequest.Name);
						if (match.Success)
						{
							string value = match.Groups[1].Value;
							uint num2;
							if (string.IsNullOrEmpty(value))
							{
								list.Remove(0U);
							}
							else if (uint.TryParse(value, out num2) && list.Contains(num2) && num2 != 0U)
							{
								list.Remove(num2);
							}
						}
					}
					while (enumerator.MoveNext() && list.Count > 0);
					if (list.Count == 0)
					{
						base.WriteError(new NoAvailableDefaultNamePermanentException(identifyingObjectId.ToString()), ErrorCategory.InvalidArgument, errorObject);
						result = null;
					}
					else if (list.Contains(0U))
					{
						result = name;
					}
					else
					{
						result = string.Format("{0}{1}", name, list[0]);
					}
				}
			}
			return result;
		}

		// Token: 0x060078DF RID: 30943 RVA: 0x001EC3EC File Offset: 0x001EA5EC
		protected virtual bool IsSupportedDatabaseVersion(int serverVersion, NewRequest<TRequest>.DatabaseSide databaseSide)
		{
			bool flag = serverVersion >= Server.E15MinVersion;
			bool flag2 = serverVersion >= Server.E14MinVersion && serverVersion < Server.E15MinVersion;
			bool flag3 = serverVersion >= Server.E2007SP2MinVersion && serverVersion < Server.E14MinVersion;
			bool flag4 = serverVersion >= Server.E2k3SP2MinVersion && serverVersion < Server.E2007MinVersion;
			switch (databaseSide)
			{
			case NewRequest<TRequest>.DatabaseSide.Source:
				return flag || flag2 || flag3 || flag4;
			case NewRequest<TRequest>.DatabaseSide.Target:
			case NewRequest<TRequest>.DatabaseSide.RequestStorage:
				return flag;
			default:
				return false;
			}
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x001EC46B File Offset: 0x001EA66B
		protected virtual void InitializeMRSClient()
		{
			this.MRSClient = MailboxReplicationServiceClient.Create(this.ConfigSession, MRSJobType.RequestJobE15_TenantHint, this.MdbId.ObjectGuid, this.UnreachableMrsServers);
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x001EC491 File Offset: 0x001EA691
		protected void ValidateName()
		{
			if (this.Name.Contains("\\"))
			{
				base.WriteError(new InvalidNameCharacterPermanentException(this.Name, "\\"), ErrorCategory.InvalidArgument, this.Name);
			}
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x001EC4C4 File Offset: 0x001EA6C4
		protected void ValidateRootFolders(string sourceRootFolderPath, string targetRootFolderPath)
		{
			if (sourceRootFolderPath != null)
			{
				try
				{
					WellKnownFolderType wellKnownFolderType;
					List<string> list;
					FolderMappingFlags folderMappingFlags;
					FolderFilterParser.Parse(sourceRootFolderPath, out wellKnownFolderType, out list, out folderMappingFlags);
				}
				catch (FolderFilterPermanentException ex)
				{
					base.WriteError(new RootFolderInvalidPermanentException(CommonUtils.FullExceptionMessage(ex), ex), ErrorCategory.InvalidArgument, sourceRootFolderPath);
				}
			}
			if (targetRootFolderPath != null)
			{
				try
				{
					WellKnownFolderType wellKnownFolderType;
					List<string> list;
					FolderMappingFlags folderMappingFlags;
					FolderFilterParser.Parse(targetRootFolderPath, out wellKnownFolderType, out list, out folderMappingFlags);
				}
				catch (FolderFilterPermanentException ex2)
				{
					base.WriteError(new RootFolderInvalidPermanentException(CommonUtils.FullExceptionMessage(ex2), ex2), ErrorCategory.InvalidArgument, targetRootFolderPath);
				}
			}
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x001EC540 File Offset: 0x001EA740
		protected T CheckDatabase<T>(DatabaseIdParameter databaseIdParameter, NewRequest<TRequest>.DatabaseSide databaseSide, object errorObject, out string serverName, out string serverDN, out ADObjectId serverSite, out int serverVersion) where T : Database, new()
		{
			T result = (T)((object)base.GetDataObject<T>(databaseIdParameter, this.ConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(databaseIdParameter.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(databaseIdParameter.ToString()))));
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(result.Id.ObjectGuid, null, null, FindServerFlags.None);
			serverName = databaseInformation.ServerFqdn;
			serverDN = databaseInformation.ServerDN;
			serverSite = databaseInformation.ServerSite;
			serverVersion = databaseInformation.ServerVersion;
			if (!this.IsSupportedDatabaseVersion(serverVersion, databaseSide))
			{
				base.WriteError(new DatabaseVersionUnsupportedPermanentException(result.Identity.ToString(), serverName, new ServerVersion(serverVersion).ToString()), ErrorCategory.InvalidArgument, errorObject);
			}
			return result;
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x001EC600 File Offset: 0x001EA800
		protected void RescopeToOrgId(OrganizationId orgId)
		{
			if (orgId != null)
			{
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.RecipSession, orgId))
				{
					this.RecipSession = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(this.RecipSession, orgId, true);
				}
				if (TaskHelper.ShouldUnderscopeDataSessionToOrganization(this.CurrentOrgConfigSession, orgId))
				{
					this.CurrentOrgConfigSession = (ITenantConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(this.CurrentOrgConfigSession, orgId, true);
					((RequestJobProvider)base.DataSession).IndexProvider.ConfigSession = this.CurrentOrgConfigSession;
				}
				this.OrganizationId = orgId;
			}
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x001EC684 File Offset: 0x001EA884
		private void SetSkipMergingAndInternalFlags(TransactionalRequestJob dataObject)
		{
			RequestTaskHelper.SetSkipMerging(this.SkipMerging, dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			RequestTaskHelper.SetInternalFlags(this.InternalFlags, dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (this.SkipMerging == null)
			{
				using (SettingsContextBase.ActivateContext(dataObject))
				{
					dataObject.SkipKnownCorruptions = ConfigBase<MRSConfigSchema>.GetConfig<bool>("SkipKnownCorruptionsDefault");
				}
			}
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x001EC6FC File Offset: 0x001EA8FC
		private string NormalizeContentFilter()
		{
			RestrictionData restrictionData = null;
			string result = null;
			try
			{
				ContentFilterBuilder.ProcessContentFilter(this.ContentFilter, this.ContentFilterLanguage.LCID, this, new NewRequest<TRequest>.FakeFilterMapper(), out restrictionData, out result);
			}
			catch (ContentFilterPermanentException ex)
			{
				base.WriteError(new ContentFilterInvalidPermanentException(CommonUtils.FullExceptionMessage(ex), ex), ErrorCategory.InvalidArgument, this.ContentFilter);
			}
			base.WriteVerbose(Strings.ContentFilterUsedVerbose(restrictionData.ToString()));
			return result;
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x001EC770 File Offset: 0x001EA970
		private void ValidateFolderFilter()
		{
			if (this.IncludeFolders != null)
			{
				try
				{
					foreach (string folderPath in this.IncludeFolders)
					{
						WellKnownFolderType wellKnownFolderType;
						List<string> list;
						FolderMappingFlags folderMappingFlags;
						FolderFilterParser.Parse(folderPath, out wellKnownFolderType, out list, out folderMappingFlags);
					}
				}
				catch (FolderFilterPermanentException ex)
				{
					base.WriteError(new FolderFilterInvalidPermanentException(CommonUtils.FullExceptionMessage(ex), ex), ErrorCategory.InvalidArgument, this.IncludeFolders);
				}
			}
			if (this.ExcludeFolders != null)
			{
				try
				{
					foreach (string folderPath2 in this.ExcludeFolders)
					{
						WellKnownFolderType wellKnownFolderType;
						List<string> list;
						FolderMappingFlags folderMappingFlags;
						FolderFilterParser.Parse(folderPath2, out wellKnownFolderType, out list, out folderMappingFlags);
					}
				}
				catch (FolderFilterPermanentException ex2)
				{
					base.WriteError(new FolderFilterInvalidPermanentException(CommonUtils.FullExceptionMessage(ex2), ex2), ErrorCategory.InvalidArgument, this.ExcludeFolders);
				}
			}
		}

		// Token: 0x02000C63 RID: 3171
		protected enum DatabaseSide
		{
			// Token: 0x04003C31 RID: 15409
			Source = 1,
			// Token: 0x04003C32 RID: 15410
			Target,
			// Token: 0x04003C33 RID: 15411
			RequestStorage
		}

		// Token: 0x02000C64 RID: 3172
		private class FakeFilterMapper : IFilterBuilderHelper
		{
			// Token: 0x060078E9 RID: 30953 RVA: 0x001EC850 File Offset: 0x001EAA50
			PropTag IFilterBuilderHelper.MapNamedProperty(NamedPropData npd, PropType propType)
			{
				return PropTag.Body;
			}

			// Token: 0x060078EA RID: 30954 RVA: 0x001EC858 File Offset: 0x001EAA58
			Guid[] IFilterBuilderHelper.MapPolicyTag(string policyTagStr)
			{
				return new Guid[]
				{
					Guid.NewGuid()
				};
			}

			// Token: 0x060078EB RID: 30955 RVA: 0x001EC880 File Offset: 0x001EAA80
			string[] IFilterBuilderHelper.MapRecipient(string recipientId)
			{
				return new string[]
				{
					"smtp:" + recipientId,
					"legDN:" + recipientId,
					"alias:" + recipientId
				};
			}
		}
	}
}

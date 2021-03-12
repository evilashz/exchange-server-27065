using System;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A86 RID: 2694
	[Serializable]
	public sealed class TeamMailboxDiagnosticsInfo : ConfigurableObject
	{
		// Token: 0x0600629D RID: 25245 RVA: 0x001A03F8 File Offset: 0x0019E5F8
		public TeamMailboxDiagnosticsInfo(string displayName) : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new SyncInfoId();
			this.propertyBag.ResetChangeTracking();
			this.DisplayName = displayName;
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x0600629E RID: 25246 RVA: 0x001A045A File Offset: 0x0019E65A
		// (set) Token: 0x0600629F RID: 25247 RVA: 0x001A046C File Offset: 0x0019E66C
		public SyncInfo HierarchySyncInfo
		{
			get
			{
				return (SyncInfo)this[TeamMailboxDiagnosticsInfoSchema.DocLibSyncInfo];
			}
			set
			{
				this[TeamMailboxDiagnosticsInfoSchema.DocLibSyncInfo] = value;
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x060062A0 RID: 25248 RVA: 0x001A047A File Offset: 0x0019E67A
		// (set) Token: 0x060062A1 RID: 25249 RVA: 0x001A048C File Offset: 0x0019E68C
		public SyncInfo MembershipSyncInfo
		{
			get
			{
				return (SyncInfo)this[TeamMailboxDiagnosticsInfoSchema.MembershipSyncInfo];
			}
			set
			{
				this[TeamMailboxDiagnosticsInfoSchema.MembershipSyncInfo] = value;
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x060062A2 RID: 25250 RVA: 0x001A049A File Offset: 0x0019E69A
		// (set) Token: 0x060062A3 RID: 25251 RVA: 0x001A04AC File Offset: 0x0019E6AC
		public SyncInfo MaintenanceSyncInfo
		{
			get
			{
				return (SyncInfo)this[TeamMailboxDiagnosticsInfoSchema.MaintenanceSyncInfo];
			}
			set
			{
				this[TeamMailboxDiagnosticsInfoSchema.MaintenanceSyncInfo] = value;
			}
		}

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x001A04BA File Offset: 0x0019E6BA
		// (set) Token: 0x060062A5 RID: 25253 RVA: 0x001A04C2 File Offset: 0x0019E6C2
		public MultiValuedProperty<SyncInfo> DocLibSyncInfos { get; set; }

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x060062A6 RID: 25254 RVA: 0x001A04CB File Offset: 0x0019E6CB
		// (set) Token: 0x060062A7 RID: 25255 RVA: 0x001A04DD File Offset: 0x0019E6DD
		public string DisplayName
		{
			get
			{
				return (string)this[TeamMailboxDiagnosticsInfoSchema.DisplayName];
			}
			private set
			{
				this[TeamMailboxDiagnosticsInfoSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x060062A8 RID: 25256 RVA: 0x001A04EB File Offset: 0x0019E6EB
		// (set) Token: 0x060062A9 RID: 25257 RVA: 0x001A04FD File Offset: 0x0019E6FD
		public TeamMailboxSyncStatus Status
		{
			get
			{
				return (TeamMailboxSyncStatus)this[TeamMailboxDiagnosticsInfoSchema.Status];
			}
			internal set
			{
				this[TeamMailboxDiagnosticsInfoSchema.Status] = value;
			}
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x060062AA RID: 25258 RVA: 0x001A0510 File Offset: 0x0019E710
		// (set) Token: 0x060062AB RID: 25259 RVA: 0x001A0522 File Offset: 0x0019E722
		public string LastDocumentSyncCycleLog
		{
			get
			{
				return (string)this[TeamMailboxDiagnosticsInfoSchema.LastDocumentSyncCycleLog];
			}
			internal set
			{
				this[TeamMailboxDiagnosticsInfoSchema.LastDocumentSyncCycleLog] = value;
			}
		}

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x060062AC RID: 25260 RVA: 0x001A0530 File Offset: 0x0019E730
		// (set) Token: 0x060062AD RID: 25261 RVA: 0x001A0542 File Offset: 0x0019E742
		public string LastMembershipSyncCycleLog
		{
			get
			{
				return (string)this[TeamMailboxDiagnosticsInfoSchema.LastMembershipSyncCycleLog];
			}
			internal set
			{
				this[TeamMailboxDiagnosticsInfoSchema.LastMembershipSyncCycleLog] = value;
			}
		}

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x060062AE RID: 25262 RVA: 0x001A0550 File Offset: 0x0019E750
		// (set) Token: 0x060062AF RID: 25263 RVA: 0x001A0562 File Offset: 0x0019E762
		public string LastMaintenanceSyncCycleLog
		{
			get
			{
				return (string)this[TeamMailboxDiagnosticsInfoSchema.LastMaintenanceSyncCycleLog];
			}
			internal set
			{
				this[TeamMailboxDiagnosticsInfoSchema.LastMaintenanceSyncCycleLog] = value;
			}
		}

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x001A0570 File Offset: 0x0019E770
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TeamMailboxDiagnosticsInfo.schema;
			}
		}

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x060062B1 RID: 25265 RVA: 0x001A0577 File Offset: 0x0019E777
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x001A0580 File Offset: 0x0019E780
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("==================== Diagnostic Information For Site Mailbox: \"{0}\" ====================", this.DisplayName);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("1. Hierarchy Synchronization Information:");
			stringBuilder.AppendLine();
			TeamMailboxDiagnosticsInfo.AppendSyncInfo(stringBuilder, this.HierarchySyncInfo);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("2. Document Library Synchronization Information:");
			stringBuilder.AppendLine();
			foreach (SyncInfo info in this.DocLibSyncInfos)
			{
				TeamMailboxDiagnosticsInfo.AppendSyncInfo(stringBuilder, info);
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("3. Membership Synchronization Information:");
			stringBuilder.AppendLine();
			TeamMailboxDiagnosticsInfo.AppendSyncInfo(stringBuilder, this.MembershipSyncInfo);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("4. Maintenance Synchronization Information:");
			stringBuilder.AppendLine();
			TeamMailboxDiagnosticsInfo.AppendSyncInfo(stringBuilder, this.MaintenanceSyncInfo);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("5. Document Synchronization Log:");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(this.LastDocumentSyncCycleLog ?? "N/A");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("6. Membership Synchronization Log:");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(this.LastMembershipSyncCycleLog ?? "N/A");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("7. Maintenance Synchronization Log:");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(this.LastMaintenanceSyncCycleLog ?? "N/A");
			return stringBuilder.ToString();
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x001A0740 File Offset: 0x0019E940
		private static void AppendSyncInfo(StringBuilder result, SyncInfo info)
		{
			if (info != null)
			{
				result.AppendFormat("DisplayName:{0}", info.DisplayName);
				result.AppendLine();
				result.AppendFormat("Url:{0}", info.Url);
				result.AppendLine();
				result.AppendFormat("LastFailedSyncTime:{0}", info.LastFailedSyncTime);
				result.AppendLine();
				result.AppendFormat("LastSyncFailure:{0}", info.LastSyncFailure);
				result.AppendLine();
				result.AppendFormat("FirstAttemptedSyncTime:{0}", info.FirstAttemptedSyncTime);
				result.AppendLine();
				result.AppendFormat("LastAttemptedSyncTime:{0}", info.LastAttemptedSyncTime);
				result.AppendLine();
				result.AppendFormat("LastSuccessfulSyncTime:{0}", info.LastSuccessfulSyncTime);
				result.AppendLine();
				result.AppendFormat("LastFailedSyncEmailTime:{0}", info.LastFailedSyncEmailTime);
				result.AppendLine();
				return;
			}
			result.AppendLine("N/A");
			result.AppendLine();
		}

		// Token: 0x040037CC RID: 14284
		private static readonly TeamMailboxDiagnosticsInfoSchema schema = ObjectSchema.GetInstance<TeamMailboxDiagnosticsInfoSchema>();
	}
}

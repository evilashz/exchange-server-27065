using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	public class UpgradeWorkItem : ConfigurableObject
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public UpgradeWorkItem() : base(new SimplePropertyBag(UpgradeWorkItem.UpgradeWorkItemSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.ResetChangeTracking();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000F518 File Offset: 0x0000D718
		public UpgradeWorkItem(WorkItemInfo workItemInfo) : this()
		{
			this.Identity = workItemInfo.WorkItemId.ToString();
			this.Created = new DateTime?(workItemInfo.Created);
			this.Modified = new DateTime?(workItemInfo.Modified);
			this.Scheduled = new DateTime?(workItemInfo.ScheduledDate);
			this.TenantID = new Guid?(workItemInfo.Tenant.TenantId);
			this.TenantPrimaryDomain = workItemInfo.Tenant.PrimaryDomain;
			this.TenantTier = workItemInfo.Tenant.Tier;
			this.StatusComment = workItemInfo.WorkItemStatus.Comment;
			this.StatusCompletedCount = new int?(workItemInfo.WorkItemStatus.CompletedCount);
			this.StatusDetails = workItemInfo.WorkItemStatus.StatusDetails;
			this.StatusHandlerState = workItemInfo.WorkItemStatus.HandlerState;
			this.StatusTotalCount = new int?(workItemInfo.WorkItemStatus.TotalCount);
			this.Status = workItemInfo.WorkItemStatus.Status.ToString();
			this.Type = workItemInfo.WorkItemType;
			this.TenantInitialDomain = workItemInfo.Tenant.InitialDomain;
			this.TenantScheduledUpgradeDate = workItemInfo.Tenant.ScheduledUpgradeDate;
			if (workItemInfo.PilotUser != null)
			{
				this.PilotUpn = workItemInfo.PilotUser.Upn;
				this.PilotUserID = new Guid?(workItemInfo.PilotUser.PilotUserId);
				return;
			}
			this.PilotUpn = null;
			this.PilotUserID = null;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000F695 File Offset: 0x0000D895
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000F6A7 File Offset: 0x0000D8A7
		public new string Identity
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.Identity];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.Identity] = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000F6B5 File Offset: 0x0000D8B5
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000F6C7 File Offset: 0x0000D8C7
		public DateTime? Created
		{
			get
			{
				return (DateTime?)this[UpgradeWorkItem.UpgradeWorkItemSchema.Created];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.Created] = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000F6DA File Offset: 0x0000D8DA
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public DateTime? Modified
		{
			get
			{
				return (DateTime?)this[UpgradeWorkItem.UpgradeWorkItemSchema.Modified];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.Modified] = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000F6FF File Offset: 0x0000D8FF
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000F711 File Offset: 0x0000D911
		public DateTime? Scheduled
		{
			get
			{
				return (DateTime?)this[UpgradeWorkItem.UpgradeWorkItemSchema.Scheduled];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.Scheduled] = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000F724 File Offset: 0x0000D924
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000F736 File Offset: 0x0000D936
		public Guid? TenantID
		{
			get
			{
				return (Guid?)this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantID];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantID] = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000F749 File Offset: 0x0000D949
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000F75B File Offset: 0x0000D95B
		public string TenantPrimaryDomain
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantPrimaryDomain];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantPrimaryDomain] = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000F769 File Offset: 0x0000D969
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000F77B File Offset: 0x0000D97B
		public string TenantInitialDomain
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantInitialDomain];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantInitialDomain] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000F789 File Offset: 0x0000D989
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000F79B File Offset: 0x0000D99B
		public DateTime? TenantScheduledUpgradeDate
		{
			get
			{
				return (DateTime?)this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantScheduledUpgradeDate];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantScheduledUpgradeDate] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		public string TenantTier
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantTier];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.TenantTier] = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000F7CE File Offset: 0x0000D9CE
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public string StatusComment
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusComment];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusComment] = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000F7EE File Offset: 0x0000D9EE
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000F800 File Offset: 0x0000DA00
		public int? StatusCompletedCount
		{
			get
			{
				return (int?)this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusCompletedCount];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusCompletedCount] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000F813 File Offset: 0x0000DA13
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000F825 File Offset: 0x0000DA25
		public Uri StatusDetails
		{
			get
			{
				return (Uri)this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusDetails];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusDetails] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000F833 File Offset: 0x0000DA33
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000F845 File Offset: 0x0000DA45
		public string StatusHandlerState
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusHandlerState];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusHandlerState] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000F853 File Offset: 0x0000DA53
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000F865 File Offset: 0x0000DA65
		public int? StatusTotalCount
		{
			get
			{
				return (int?)this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusTotalCount];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.StatusTotalCount] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F878 File Offset: 0x0000DA78
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000F88A File Offset: 0x0000DA8A
		public string Status
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.Status];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.Status] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000F898 File Offset: 0x0000DA98
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000F8AA File Offset: 0x0000DAAA
		public string Type
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.Type];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.Type] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000F8CA File Offset: 0x0000DACA
		public Guid? PilotUserID
		{
			get
			{
				return (Guid?)this[UpgradeWorkItem.UpgradeWorkItemSchema.PilotUserID];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.PilotUserID] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000F8DD File Offset: 0x0000DADD
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000F8EF File Offset: 0x0000DAEF
		public string PilotUpn
		{
			get
			{
				return (string)this[UpgradeWorkItem.UpgradeWorkItemSchema.PilotUpnField];
			}
			internal set
			{
				this[UpgradeWorkItem.UpgradeWorkItemSchema.PilotUpnField] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000F8FD File Offset: 0x0000DAFD
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UpgradeWorkItem.schema;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F904 File Offset: 0x0000DB04
		public override bool Equals(object obj)
		{
			UpgradeWorkItem upgradeWorkItem = obj as UpgradeWorkItem;
			return upgradeWorkItem != null && string.Equals(this.Identity.ToString(), upgradeWorkItem.Identity.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F939 File Offset: 0x0000DB39
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400014B RID: 331
		private static UpgradeWorkItem.UpgradeWorkItemSchema schema = ObjectSchema.GetInstance<UpgradeWorkItem.UpgradeWorkItemSchema>();

		// Token: 0x02000048 RID: 72
		internal class UpgradeWorkItemSchema : SimpleProviderObjectSchema
		{
			// Token: 0x0400014C RID: 332
			public new static readonly ProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400014D RID: 333
			public static readonly ProviderPropertyDefinition Created = new SimpleProviderPropertyDefinition("Created", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400014E RID: 334
			public static readonly ProviderPropertyDefinition Modified = new SimpleProviderPropertyDefinition("Modified", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400014F RID: 335
			public static readonly ProviderPropertyDefinition Scheduled = new SimpleProviderPropertyDefinition("Scheduled", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000150 RID: 336
			public static readonly ProviderPropertyDefinition TenantID = new SimpleProviderPropertyDefinition("TenantID", ExchangeObjectVersion.Exchange2010, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000151 RID: 337
			public static readonly ProviderPropertyDefinition TenantPrimaryDomain = new SimpleProviderPropertyDefinition("TenantPrimaryDomain", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000152 RID: 338
			public static readonly ProviderPropertyDefinition TenantTier = new SimpleProviderPropertyDefinition("TenantTier", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000153 RID: 339
			public static readonly ProviderPropertyDefinition StatusComment = new SimpleProviderPropertyDefinition("StatusComment", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000154 RID: 340
			public static readonly ProviderPropertyDefinition StatusCompletedCount = new SimpleProviderPropertyDefinition("StatusCompletedCount", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000155 RID: 341
			public static readonly ProviderPropertyDefinition StatusHandlerState = new SimpleProviderPropertyDefinition("StatusHandlerState", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000156 RID: 342
			public static readonly ProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000157 RID: 343
			public static readonly ProviderPropertyDefinition StatusDetails = new SimpleProviderPropertyDefinition("StatusDetails", ExchangeObjectVersion.Exchange2010, typeof(Uri), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000158 RID: 344
			public static readonly ProviderPropertyDefinition StatusTotalCount = new SimpleProviderPropertyDefinition("StatusTotalCount", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000159 RID: 345
			public static readonly ProviderPropertyDefinition Type = new SimpleProviderPropertyDefinition("Type", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400015A RID: 346
			public static readonly ProviderPropertyDefinition PilotUserID = new SimpleProviderPropertyDefinition("PilotUserID", ExchangeObjectVersion.Exchange2010, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400015B RID: 347
			public static readonly ProviderPropertyDefinition PilotUpnField = new SimpleProviderPropertyDefinition("PilotUpnField", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400015C RID: 348
			public static readonly ProviderPropertyDefinition TenantInitialDomain = new SimpleProviderPropertyDefinition("TenantInitialDomain", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400015D RID: 349
			public static readonly ProviderPropertyDefinition TenantScheduledUpgradeDate = new SimpleProviderPropertyDefinition("TenantScheduledUpgradeDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}

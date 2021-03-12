using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003AB RID: 939
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AttachmentFilteringConfig : ADConfigurationObject
	{
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000B3293 File Offset: 0x000B1493
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x000B329B File Offset: 0x000B149B
		internal override ADObjectSchema Schema
		{
			get
			{
				return AttachmentFilteringConfig.schema;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000B32A2 File Offset: 0x000B14A2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AttachmentFilteringConfig.mostDerivedClass;
			}
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000B32AC File Offset: 0x000B14AC
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(AttachmentFilteringConfigSchema.RejectResponse))
			{
				this[AttachmentFilteringConfigSchema.RejectResponse] = "Message rejected due to unacceptable attachments";
			}
			if (!base.IsModified(AttachmentFilteringConfigSchema.AdminMessage))
			{
				this[AttachmentFilteringConfigSchema.AdminMessage] = DirectoryStrings.AttachmentsWereRemovedMessage.ToString();
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000B330F File Offset: 0x000B150F
		// (set) Token: 0x06002B0E RID: 11022 RVA: 0x000B3321 File Offset: 0x000B1521
		[Parameter]
		public string RejectResponse
		{
			get
			{
				return (string)this[AttachmentFilteringConfigSchema.RejectResponse];
			}
			set
			{
				this[AttachmentFilteringConfigSchema.RejectResponse] = value;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000B332F File Offset: 0x000B152F
		// (set) Token: 0x06002B10 RID: 11024 RVA: 0x000B3341 File Offset: 0x000B1541
		[Parameter]
		public string AdminMessage
		{
			get
			{
				return (string)this[AttachmentFilteringConfigSchema.AdminMessage];
			}
			set
			{
				this[AttachmentFilteringConfigSchema.AdminMessage] = value;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000B334F File Offset: 0x000B154F
		// (set) Token: 0x06002B12 RID: 11026 RVA: 0x000B3361 File Offset: 0x000B1561
		[Parameter]
		public FilterActions Action
		{
			get
			{
				return (FilterActions)this[AttachmentFilteringConfigSchema.FilterAction];
			}
			set
			{
				this[AttachmentFilteringConfigSchema.FilterAction] = value;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000B3374 File Offset: 0x000B1574
		// (set) Token: 0x06002B14 RID: 11028 RVA: 0x000B3386 File Offset: 0x000B1586
		public MultiValuedProperty<ADObjectId> ExceptionConnectors
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[AttachmentFilteringConfigSchema.ExceptionConnectors];
			}
			set
			{
				this[AttachmentFilteringConfigSchema.ExceptionConnectors] = value;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000B3394 File Offset: 0x000B1594
		// (set) Token: 0x06002B16 RID: 11030 RVA: 0x000B33A6 File Offset: 0x000B15A6
		public MultiValuedProperty<string> AttachmentNames
		{
			get
			{
				return (MultiValuedProperty<string>)this[AttachmentFilteringConfigSchema.AttachmentNames];
			}
			internal set
			{
				this[AttachmentFilteringConfigSchema.AttachmentNames] = value;
			}
		}

		// Token: 0x040019DE RID: 6622
		private static AttachmentFilteringConfigSchema schema = ObjectSchema.GetInstance<AttachmentFilteringConfigSchema>();

		// Token: 0x040019DF RID: 6623
		private static string mostDerivedClass = "msExchTransportSettings";
	}
}

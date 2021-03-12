using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C7 RID: 199
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	internal class ADUserSetting : ADObject
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00015208 File Offset: 0x00013408
		public override ObjectId Identity
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00015210 File Offset: 0x00013410
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00015222 File Offset: 0x00013422
		public ObjectId ConfigurationId
		{
			get
			{
				return (ObjectId)this[ADUserSettingSchema.ConfigurationIdProp];
			}
			set
			{
				this[ADUserSettingSchema.ConfigurationIdProp] = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00015230 File Offset: 0x00013430
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00015242 File Offset: 0x00013442
		public UserSettingFlags Flags
		{
			get
			{
				return (UserSettingFlags)this[ADUserSettingSchema.FlagsProp];
			}
			set
			{
				this[ADUserSettingSchema.FlagsProp] = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00015255 File Offset: 0x00013455
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00015267 File Offset: 0x00013467
		public byte[] SafeSenders
		{
			get
			{
				return (byte[])this[ADUserSettingSchema.SafeSendersProp];
			}
			set
			{
				this[ADUserSettingSchema.SafeSendersProp] = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00015275 File Offset: 0x00013475
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00015287 File Offset: 0x00013487
		public byte[] BlockedSenders
		{
			get
			{
				return (byte[])this[ADUserSettingSchema.BlockedSendersProp];
			}
			set
			{
				this[ADUserSettingSchema.BlockedSendersProp] = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00015295 File Offset: 0x00013495
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x000152A7 File Offset: 0x000134A7
		public string DisplayName
		{
			get
			{
				return (string)this[ADUserSettingSchema.DisplayNameProp];
			}
			set
			{
				this[ADUserSettingSchema.DisplayNameProp] = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x000152B5 File Offset: 0x000134B5
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADUserSetting.schema;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000152BC File Offset: 0x000134BC
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADUserSetting.mostDerivedClass;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x000152C3 File Offset: 0x000134C3
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000410 RID: 1040
		private static readonly ADUserSettingSchema schema = ObjectSchema.GetInstance<ADUserSettingSchema>();

		// Token: 0x04000411 RID: 1041
		private static string mostDerivedClass = "ADUserSetting";
	}
}

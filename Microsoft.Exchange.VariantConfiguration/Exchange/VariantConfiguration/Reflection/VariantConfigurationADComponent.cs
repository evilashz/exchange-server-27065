using System;
using Microsoft.Exchange.Flighting;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000F9 RID: 249
	public sealed class VariantConfigurationADComponent : VariantConfigurationComponent
	{
		// Token: 0x06000AC9 RID: 2761 RVA: 0x000192DC File Offset: 0x000174DC
		internal VariantConfigurationADComponent() : base("AD")
		{
			base.Add(new VariantConfigurationSection("AD.settings.ini", "DelegatedSetupRoleGroupValue", typeof(IDelegatedSetupRoleGroupSettings), false));
			base.Add(new VariantConfigurationSection("AD.settings.ini", "DisplayNameMustContainReadableCharacter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("AD.settings.ini", "MailboxLocations", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("AD.settings.ini", "EnableUseIsDescendantOfForRecipientViewRoot", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("AD.settings.ini", "UseGlobalCatalogIsSetToFalse", typeof(IFeature), false));
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00019394 File Offset: 0x00017594
		public VariantConfigurationSection DelegatedSetupRoleGroupValue
		{
			get
			{
				return base["DelegatedSetupRoleGroupValue"];
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000193A1 File Offset: 0x000175A1
		public VariantConfigurationSection DisplayNameMustContainReadableCharacter
		{
			get
			{
				return base["DisplayNameMustContainReadableCharacter"];
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000193AE File Offset: 0x000175AE
		public VariantConfigurationSection MailboxLocations
		{
			get
			{
				return base["MailboxLocations"];
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000193BB File Offset: 0x000175BB
		public VariantConfigurationSection EnableUseIsDescendantOfForRecipientViewRoot
		{
			get
			{
				return base["EnableUseIsDescendantOfForRecipientViewRoot"];
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000193C8 File Offset: 0x000175C8
		public VariantConfigurationSection UseGlobalCatalogIsSetToFalse
		{
			get
			{
				return base["UseGlobalCatalogIsSetToFalse"];
			}
		}
	}
}

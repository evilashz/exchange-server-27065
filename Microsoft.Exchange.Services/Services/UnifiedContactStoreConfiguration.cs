using System;
using Microsoft.Exchange.Services.Core;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000029 RID: 41
	public class UnifiedContactStoreConfiguration : IUnifiedContactStoreConfiguration
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000A067 File Offset: 0x00008267
		internal UnifiedContactStoreConfiguration()
		{
			this.MaxImGroups = Global.GetAppSettingAsInt(UnifiedContactStoreConfiguration.MaxImGroupsAppSettingsName, 64);
			this.MaxImContacts = Global.GetAppSettingAsInt(UnifiedContactStoreConfiguration.MaxImContactsAppSettingsName, 1000);
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A096 File Offset: 0x00008296
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000A09E File Offset: 0x0000829E
		public int MaxImGroups { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A0A7 File Offset: 0x000082A7
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000A0AF File Offset: 0x000082AF
		public int MaxImContacts { get; private set; }

		// Token: 0x040001C2 RID: 450
		public static readonly string MaxImGroupsAppSettingsName = "MaxImGroups";

		// Token: 0x040001C3 RID: 451
		public static readonly string MaxImContactsAppSettingsName = "MaxImContacts";
	}
}

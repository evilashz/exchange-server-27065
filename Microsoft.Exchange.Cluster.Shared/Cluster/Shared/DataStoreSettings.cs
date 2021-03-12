using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000094 RID: 148
	public class DataStoreSettings
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00013F50 File Offset: 0x00012150
		public static bool IsRunningOnTestBox
		{
			get
			{
				return Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Setup", "BuildNumber", null) != null;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00013F68 File Offset: 0x00012168
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00013F70 File Offset: 0x00012170
		public StoreKind Primary { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00013F79 File Offset: 0x00012179
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00013F81 File Offset: 0x00012181
		public StoreKind Shadow { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00013F8A File Offset: 0x0001218A
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x00013F92 File Offset: 0x00012192
		public bool IsCompositeModeEnabled { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00013F9B File Offset: 0x0001219B
		public bool IsShadowConfigured
		{
			get
			{
				return this.Shadow != StoreKind.None;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013FAC File Offset: 0x000121AC
		public static DataStoreSettings GetStoreConfig()
		{
			IActiveManagerSettings settings = DxStoreSetting.Instance.GetSettings();
			DxStoreMode dxStoreRunMode = settings.DxStoreRunMode;
			DataStoreSettings dataStoreSettings = new DataStoreSettings();
			if (dxStoreRunMode == DxStoreMode.Shadow)
			{
				dataStoreSettings.Primary = StoreKind.Clusdb;
				dataStoreSettings.Shadow = StoreKind.DxStore;
				dataStoreSettings.IsCompositeModeEnabled = true;
			}
			else if (dxStoreRunMode == DxStoreMode.Primary)
			{
				dataStoreSettings.Primary = StoreKind.DxStore;
				dataStoreSettings.Shadow = StoreKind.None;
				dataStoreSettings.IsCompositeModeEnabled = true;
			}
			else
			{
				dataStoreSettings.Primary = StoreKind.Clusdb;
				dataStoreSettings.Shadow = StoreKind.None;
				dataStoreSettings.IsCompositeModeEnabled = false;
			}
			return dataStoreSettings;
		}
	}
}

using System;
using System.ComponentModel;
using System.Drawing;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001EF RID: 495
	[Serializable]
	public class FooteredFeatureLauncherListViewItem : FeatureLauncherListViewItem
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x0005C354 File Offset: 0x0005A554
		public FooteredFeatureLauncherListViewItem(string featureName, string statusPropertyName, Icon icon) : base(featureName, statusPropertyName, icon)
		{
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0005C35F File Offset: 0x0005A55F
		// (set) Token: 0x06001645 RID: 5701 RVA: 0x0005C367 File Offset: 0x0005A567
		[DefaultValue("")]
		public string FooterDescription { get; set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0005C370 File Offset: 0x0005A570
		// (set) Token: 0x06001647 RID: 5703 RVA: 0x0005C378 File Offset: 0x0005A578
		[DefaultValue(null)]
		public Bitmap FooterBitmap { get; set; }
	}
}

using System;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000634 RID: 1588
	[ClientScriptResource("PopupLauncher", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	[TargetControlType(typeof(Control))]
	public class PopupLauncher : ExtenderControlBase
	{
		// Token: 0x060045DE RID: 17886 RVA: 0x000D3640 File Offset: 0x000D1840
		protected override void OnPreRender(EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.NavigationUrl))
			{
				string path = base.TargetControl.ResolveClientUrl(this.NavigationUrl);
				if (!LoginUtil.CheckUrlAccess(path))
				{
					Util.MakeControlRbacDisabled(base.TargetControl);
					base.Enabled = false;
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x000D3690 File Offset: 0x000D1890
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddUrlProperty("NavigationUrl", this.NavigationUrl, this);
			descriptor.AddComponentProperty("OwnerControl", this.OwnerControlID, this);
			descriptor.AddProperty("Width", this.Width, 510);
			descriptor.AddProperty("Height", this.Height, 564);
		}

		// Token: 0x170026EE RID: 9966
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x000D36F4 File Offset: 0x000D18F4
		// (set) Token: 0x060045E1 RID: 17889 RVA: 0x000D3705 File Offset: 0x000D1905
		public string NavigationUrl
		{
			get
			{
				return this.navigationUrl ?? string.Empty;
			}
			set
			{
				this.navigationUrl = value;
			}
		}

		// Token: 0x170026EF RID: 9967
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x000D370E File Offset: 0x000D190E
		// (set) Token: 0x060045E3 RID: 17891 RVA: 0x000D371F File Offset: 0x000D191F
		public string OwnerControlID
		{
			get
			{
				return this.ownerControlID ?? string.Empty;
			}
			set
			{
				this.ownerControlID = value;
			}
		}

		// Token: 0x170026F0 RID: 9968
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x000D3728 File Offset: 0x000D1928
		// (set) Token: 0x060045E5 RID: 17893 RVA: 0x000D3730 File Offset: 0x000D1930
		[DefaultValue(510)]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170026F1 RID: 9969
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x000D3739 File Offset: 0x000D1939
		// (set) Token: 0x060045E7 RID: 17895 RVA: 0x000D3741 File Offset: 0x000D1941
		[DefaultValue(564)]
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x04002F4A RID: 12106
		private string navigationUrl;

		// Token: 0x04002F4B RID: 12107
		private string ownerControlID;

		// Token: 0x04002F4C RID: 12108
		private int width = 510;

		// Token: 0x04002F4D RID: 12109
		private int height = 564;
	}
}

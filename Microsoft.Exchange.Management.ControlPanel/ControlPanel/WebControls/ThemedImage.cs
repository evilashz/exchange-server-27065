using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200066E RID: 1646
	[ToolboxData("<{0}:ThemedImage runat=\"server\" />")]
	public class ThemedImage : Image
	{
		// Token: 0x17002769 RID: 10089
		// (get) Token: 0x0600475B RID: 18267 RVA: 0x000D86AE File Offset: 0x000D68AE
		// (set) Token: 0x0600475C RID: 18268 RVA: 0x000D86B6 File Offset: 0x000D68B6
		[DefaultValue("")]
		public string FileName { get; set; }

		// Token: 0x1700276A RID: 10090
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x000D86BF File Offset: 0x000D68BF
		// (set) Token: 0x0600475E RID: 18270 RVA: 0x000D86CD File Offset: 0x000D68CD
		public override string ImageUrl
		{
			get
			{
				return ThemeResource.GetThemeResource(this, this.FileName);
			}
			set
			{
				throw new NotSupportedException("When using the ThemedImage control, use the FileName property to specify the image.");
			}
		}
	}
}

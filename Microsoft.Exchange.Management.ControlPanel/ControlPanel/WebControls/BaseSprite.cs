using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000016 RID: 22
	public abstract class BaseSprite : Image
	{
		// Token: 0x04001855 RID: 6229
		public static bool IsDataCenter;

		// Token: 0x04001856 RID: 6230
		public static GetSpriteImageSrcDelegate GetSpriteImageSrc;
	}
}

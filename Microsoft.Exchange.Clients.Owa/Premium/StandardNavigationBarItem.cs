using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020003CB RID: 971
	internal class StandardNavigationBarItem : NavigationBarItemBase
	{
		// Token: 0x06002414 RID: 9236 RVA: 0x000D0243 File Offset: 0x000CE443
		public StandardNavigationBarItem(NavigationModule module, UserContext userContext, string text, string idSuffix, string onClickHandler, ThemeFileId largeIcon, ThemeFileId smallIcon) : base(userContext, text, idSuffix)
		{
			this.largeIcon = largeIcon;
			this.smallIcon = smallIcon;
			this.navigationModule = module;
			this.onClickHandler = onClickHandler;
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000D026E File Offset: 0x000CE46E
		protected override void RenderImageTag(TextWriter writer, bool useSmallIcons, bool isWunderBar)
		{
			base.UserContext.RenderThemeImage(writer, useSmallIcons ? this.smallIcon : this.largeIcon, isWunderBar ? (useSmallIcons ? "nbMnuImgWS" : "nbMnuImgWB") : "nbMnuImgN", new object[0]);
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000D02AC File Offset: 0x000CE4AC
		protected override void RenderOnClickHandler(TextWriter writer, NavigationModule currentModule)
		{
			Utilities.RenderScriptHandler(writer, "onclick", (currentModule == NavigationModule.Options) ? ("mnNav(" + (int)this.navigationModule + ");") : this.onClickHandler, false);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000D02E0 File Offset: 0x000CE4E0
		protected override bool IsCurrentModule(NavigationModule module)
		{
			return module == this.navigationModule;
		}

		// Token: 0x04001906 RID: 6406
		private const int Images = 6;

		// Token: 0x04001907 RID: 6407
		private readonly ThemeFileId largeIcon;

		// Token: 0x04001908 RID: 6408
		private readonly ThemeFileId smallIcon;

		// Token: 0x04001909 RID: 6409
		private readonly string onClickHandler;

		// Token: 0x0400190A RID: 6410
		private readonly NavigationModule navigationModule;
	}
}

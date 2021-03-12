using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200006C RID: 108
	public enum ThemeFileId
	{
		// Token: 0x0400023D RID: 573
		[ThemeFileInfo]
		None,
		// Token: 0x0400023E RID: 574
		[ThemeFileInfo("owafont.css", ThemeFileInfoFlags.Resource)]
		OwaFontCss,
		// Token: 0x0400023F RID: 575
		[ThemeFileInfo("logon.css", ThemeFileInfoFlags.Resource)]
		LogonCss,
		// Token: 0x04000240 RID: 576
		[ThemeFileInfo("errorFE.css", ThemeFileInfoFlags.Resource)]
		ErrorFECss,
		// Token: 0x04000241 RID: 577
		[ThemeFileInfo("icon_settings.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		OwaSettings,
		// Token: 0x04000242 RID: 578
		[ThemeFileInfo("olk_logo_white.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		OutlookLogoWhite,
		// Token: 0x04000243 RID: 579
		[ThemeFileInfo("olk_logo_white_cropped.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		OutlookLogoWhiteCropped,
		// Token: 0x04000244 RID: 580
		[ThemeFileInfo("olk_logo_white_small.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		OutlookLogoWhiteSmall,
		// Token: 0x04000245 RID: 581
		[ThemeFileInfo("owa_text_blue.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		OwaHeaderTextBlue,
		// Token: 0x04000246 RID: 582
		[ThemeFileInfo("bg_gradient.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		BackgroundGradient,
		// Token: 0x04000247 RID: 583
		[ThemeFileInfo("bg_gradient_login.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		BackgroundGradientLogin,
		// Token: 0x04000248 RID: 584
		[ThemeFileInfo("Sign_in_arrow.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		SignInArrow,
		// Token: 0x04000249 RID: 585
		[ThemeFileInfo("Sign_in_arrow_rtl.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		SignInArrowRtl,
		// Token: 0x0400024A RID: 586
		[ThemeFileInfo("warn.png")]
		Error,
		// Token: 0x0400024B RID: 587
		[ThemeFileInfo("lgntopl.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonTopLeft,
		// Token: 0x0400024C RID: 588
		[ThemeFileInfo("lgntopm.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonTopMiddle,
		// Token: 0x0400024D RID: 589
		[ThemeFileInfo("lgntopr.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonTopRight,
		// Token: 0x0400024E RID: 590
		[ThemeFileInfo("lgnbotl.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonBottomLeft,
		// Token: 0x0400024F RID: 591
		[ThemeFileInfo("lgnbotm.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonBottomMiddle,
		// Token: 0x04000250 RID: 592
		[ThemeFileInfo("lgnbotr.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonBottomRight,
		// Token: 0x04000251 RID: 593
		[ThemeFileInfo("lgnexlogo.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonExchangeLogo,
		// Token: 0x04000252 RID: 594
		[ThemeFileInfo("lgnleft.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonLeft,
		// Token: 0x04000253 RID: 595
		[ThemeFileInfo("lgnright.gif", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		LogonRight,
		// Token: 0x04000254 RID: 596
		[ThemeFileInfo("favicon.ico", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		FavoriteIcon,
		// Token: 0x04000255 RID: 597
		[ThemeFileInfo("favicon_office.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		FaviconOffice,
		// Token: 0x04000256 RID: 598
		[ThemeFileInfo("icp.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		ICPNum,
		// Token: 0x04000257 RID: 599
		[ThemeFileInfo("office365_cn.png", ThemeFileInfoFlags.LooseImage | ThemeFileInfoFlags.Resource)]
		Office365CnLogo,
		// Token: 0x04000258 RID: 600
		[ThemeFileInfo]
		Count
	}
}

using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000482 RID: 1154
	internal class ThemeStyleResource : StyleResource
	{
		// Token: 0x06002701 RID: 9985 RVA: 0x0008D653 File Offset: 0x0008B853
		public ThemeStyleResource(string resourceName, ResourceTarget.Filter targetFilter, string currentOwaVersion, bool skipThemeFolder) : base(resourceName, targetFilter, currentOwaVersion, true)
		{
			this.skipThemeFolder = skipThemeFolder;
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0008D667 File Offset: 0x0008B867
		public string ThemesPath
		{
			get
			{
				if (!this.skipThemeFolder)
				{
					return ResourcePathBuilderUtilities.GetThemeResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath);
				}
				return ResourcePathBuilderUtilities.GetStyleResourcesRelativeFolderPathWithSlash(base.ResourcesRelativeFolderPath);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x0008D688 File Offset: 0x0008B888
		public virtual string StyleDirectory
		{
			get
			{
				if (this.styleDirectory == null)
				{
					this.styleDirectory = (this.skipThemeFolder ? ResourcePathBuilderUtilities.GetImageResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath) : ResourcePathBuilderUtilities.GetThemeImageResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath));
				}
				string spriteDirectory = ThemeStyleResource.GetSpriteDirectory(Thread.CurrentThread.CurrentUICulture);
				return this.styleDirectory + spriteDirectory + "/";
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x0008D6E4 File Offset: 0x0008B8E4
		public static ThemeStyleResource FromSlabStyle(SlabStyleFile style, string owaVersion, bool shouldSkipThemeFolder)
		{
			ResourceTarget.Filter targetFilter = ResourceTarget.Any;
			if (style.IsHighResolutionSprite())
			{
				if (style.IsForLayout(LayoutType.TouchWide))
				{
					targetFilter = ResourceTarget.WideHighResolution;
				}
				if (style.IsForLayout(LayoutType.TouchNarrow))
				{
					targetFilter = ResourceTarget.NarrowHighResolution;
				}
			}
			else
			{
				if (style.IsForLayout(LayoutType.Mouse))
				{
					targetFilter = ResourceTarget.MouseOnly;
				}
				if (style.IsForLayout(LayoutType.TouchWide))
				{
					targetFilter = ResourceTarget.WideOnly;
				}
				if (style.IsForLayout(LayoutType.TouchNarrow))
				{
					targetFilter = ResourceTarget.NarrowOnly;
				}
			}
			if (style.IsSprite())
			{
				return new ThemeStyleResource(style.Name, targetFilter, owaVersion, shouldSkipThemeFolder);
			}
			return new LocalizedThemeStyleResource(style.Name, targetFilter, owaVersion, shouldSkipThemeFolder);
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0008D771 File Offset: 0x0008B971
		public static string GetSpriteDirectory(CultureInfo currentCulture)
		{
			if (!currentCulture.TextInfo.IsRightToLeft)
			{
				return "0";
			}
			return "rtl";
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0008D78B File Offset: 0x0008B98B
		protected override string GetStyleDirectory(IPageContext pageContext, string theme, bool isBootStylesDirectory)
		{
			return pageContext.FormatURIForCDN(string.Format(this.StyleDirectory, theme), isBootStylesDirectory);
		}

		// Token: 0x040016C7 RID: 5831
		private readonly bool skipThemeFolder;

		// Token: 0x040016C8 RID: 5832
		private string styleDirectory;
	}
}

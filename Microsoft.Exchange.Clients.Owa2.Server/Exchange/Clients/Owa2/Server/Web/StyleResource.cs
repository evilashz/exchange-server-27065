using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000481 RID: 1153
	internal class StyleResource : ResourceBase
	{
		// Token: 0x060026FE RID: 9982 RVA: 0x0008D5F4 File Offset: 0x0008B7F4
		public StyleResource(string resourceName, ResourceTarget.Filter targetFilter, string currentOwaVersion, bool hasUserSpecificData) : base(resourceName, targetFilter, currentOwaVersion, hasUserSpecificData)
		{
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0008D604 File Offset: 0x0008B804
		public override string GetResourcePath(IPageContext pageContext, bool isBootResource)
		{
			string text = ResourceBase.CombinePath(new string[]
			{
				this.GetStyleDirectory(pageContext, pageContext.Theme, isBootResource),
				this.ResourceName
			});
			return text.ToLowerInvariant();
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x0008D63F File Offset: 0x0008B83F
		protected virtual string GetStyleDirectory(IPageContext pageContext, string theme, bool isBootStylesDirectory)
		{
			return pageContext.FormatURIForCDN(ResourcePathBuilderUtilities.GetStyleResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath), isBootStylesDirectory);
		}
	}
}

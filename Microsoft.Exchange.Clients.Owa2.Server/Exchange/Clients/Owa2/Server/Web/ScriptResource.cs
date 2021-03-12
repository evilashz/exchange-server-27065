using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200047B RID: 1147
	public class ScriptResource : ResourceBase
	{
		// Token: 0x060026D3 RID: 9939 RVA: 0x0008CB00 File Offset: 0x0008AD00
		public ScriptResource(string resourceName, ResourceTarget.Filter targetFilter, string currentOwaVersion, bool hasUserSpecificData = false, bool isExternalDrop = false, bool isFullPath = false) : base(resourceName, targetFilter, currentOwaVersion, hasUserSpecificData)
		{
			this.isFullPath = isFullPath;
			this.isExternalDrop = isExternalDrop;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x0008CB1D File Offset: 0x0008AD1D
		public bool IsExternalDropped
		{
			get
			{
				return this.isExternalDrop;
			}
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0008CB28 File Offset: 0x0008AD28
		public override string GetResourcePath(IPageContext pageContext, bool isBootResourcePath)
		{
			string text = ResourceBase.CombinePath(new string[]
			{
				this.GetScriptDirectory(pageContext, this.ResourceName, isBootResourcePath),
				this.ResourceName
			});
			return text.ToLowerInvariant();
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0008CB63 File Offset: 0x0008AD63
		protected virtual string GetScriptDirectory(IPageContext pageContext, string resourceName, bool isBootScriptsDirectory)
		{
			if (resourceName.StartsWith("http://") || resourceName.StartsWith("https://") || this.isFullPath)
			{
				return string.Empty;
			}
			return pageContext.FormatURIForCDN(ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath), isBootScriptsDirectory);
		}

		// Token: 0x040016AF RID: 5807
		private readonly bool isFullPath;

		// Token: 0x040016B0 RID: 5808
		private readonly bool isExternalDrop;
	}
}

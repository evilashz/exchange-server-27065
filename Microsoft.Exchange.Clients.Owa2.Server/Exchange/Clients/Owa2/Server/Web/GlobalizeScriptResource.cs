using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200047C RID: 1148
	internal class GlobalizeScriptResource : ScriptResource
	{
		// Token: 0x060026D7 RID: 9943 RVA: 0x0008CB9F File Offset: 0x0008AD9F
		public GlobalizeScriptResource(string resourceName, ResourceTarget.Filter targetFilter, string currentVersion, bool hasUserSpecificData = false) : base(resourceName, targetFilter, currentVersion, hasUserSpecificData, true, false)
		{
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0008CBAE File Offset: 0x0008ADAE
		protected override string GetScriptDirectory(IPageContext pageContext, string resourceName, bool isBootScriptsDirectory)
		{
			if (this.scriptDirectory == null)
			{
				this.scriptDirectory = ResourcePathBuilderUtilities.GetGlobalizeScriptResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath);
			}
			return pageContext.FormatURIForCDN(this.scriptDirectory, isBootScriptsDirectory);
		}

		// Token: 0x040016B1 RID: 5809
		private string scriptDirectory;
	}
}

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200047F RID: 1151
	public class LocalizedExtensibilityStringsScriptResource : ScriptResource
	{
		// Token: 0x060026ED RID: 9965 RVA: 0x0008D2DF File Offset: 0x0008B4DF
		public LocalizedExtensibilityStringsScriptResource(string resourceName, ResourceTarget.Filter targetFilter, string currentOwaVersion) : base(resourceName, targetFilter, currentOwaVersion, true, false, false)
		{
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0008D2ED File Offset: 0x0008B4ED
		public string GetLocalizedCultureName()
		{
			return this.GetLocalizedCultureName(this.ResourceName);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x0008D2FC File Offset: 0x0008B4FC
		protected override string GetScriptDirectory(IPageContext pageContext, string resourceName, bool isBootScriptsDirectory)
		{
			string localizedCultureName = this.GetLocalizedCultureName(resourceName);
			return pageContext.FormatURIForCDN(ResourcePathBuilderUtilities.GetLocalizedScriptResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath, localizedCultureName), isBootScriptsDirectory);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0008D340 File Offset: 0x0008B540
		private string GetLocalizedCultureName(string resourceName)
		{
			return LocalizedStringsScriptResource.GetLocalizedCultureName(LocalizedExtensibilityStringsScriptResource.threadCultureToLocalizedCultureMap, Thread.CurrentThread.CurrentCulture, (string lang) => this.ResourceExists(resourceName, lang));
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0008D384 File Offset: 0x0008B584
		private bool ResourceExists(string resourceName, string languageName)
		{
			string path = Path.Combine(FolderConfiguration.Instance.RootPath, ResourcePathBuilderUtilities.GetLocalizedScriptResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath, languageName), resourceName);
			return File.Exists(path);
		}

		// Token: 0x040016C1 RID: 5825
		private static readonly ConcurrentDictionary<string, string> threadCultureToLocalizedCultureMap = new ConcurrentDictionary<string, string>();
	}
}

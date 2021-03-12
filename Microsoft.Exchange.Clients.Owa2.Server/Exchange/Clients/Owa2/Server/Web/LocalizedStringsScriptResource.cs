using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Core;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000480 RID: 1152
	public class LocalizedStringsScriptResource : ScriptResource
	{
		// Token: 0x060026F3 RID: 9971 RVA: 0x0008D3C0 File Offset: 0x0008B5C0
		public LocalizedStringsScriptResource(string resourceName, ResourceTarget.Filter targetFilter, string currentOwaVersion) : base(resourceName, targetFilter, currentOwaVersion, true, false, false)
		{
			this.resourceName = resourceName;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x0008D3D5 File Offset: 0x0008B5D5
		public string GetLocalizedCultureName()
		{
			return this.GetLocalizedCultureName(this.resourceName);
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x0008D3E3 File Offset: 0x0008B5E3
		public string GetResoucePath(IPageContext pageContext, string cultureName, bool isBootResourcePath)
		{
			return this.GetScriptDirectoryFromCultureName(pageContext, cultureName, isBootResourcePath) + "/" + this.resourceName.ToLowerInvariant();
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x0008D420 File Offset: 0x0008B620
		internal static string GetLocalizedCultureName(ConcurrentDictionary<string, string> cultureMap, CultureInfo culture, Func<string, bool> existsFilter)
		{
			return cultureMap.GetOrAdd(culture.Name, (string x) => LocalizedStringsScriptResource.GetLocalizedCultureName(culture, existsFilter));
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x0008D45E File Offset: 0x0008B65E
		protected override string GetScriptDirectory(IPageContext pageContext, string resourceName, bool isBootScriptsDirectory)
		{
			return pageContext.FormatURIForCDN(this.GetScriptDirectory(pageContext, Thread.CurrentThread.CurrentCulture, resourceName, isBootScriptsDirectory), isBootScriptsDirectory);
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0008D498 File Offset: 0x0008B698
		private string GetScriptDirectory(IPageContext pageContext, CultureInfo cultureInfo, string resourceName, bool isBootScriptsDirectory)
		{
			return this.GetScriptDirectoryFromCultureName(pageContext, LocalizedStringsScriptResource.GetLocalizedCultureName(LocalizedStringsScriptResource.threadCultureToLocalizedCultureMap, cultureInfo, (string lang) => this.ResourceExists(resourceName, lang)), isBootScriptsDirectory);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0008D4DC File Offset: 0x0008B6DC
		private static string GetLocalizedCultureName(CultureInfo culture, Func<string, bool> existsFilter)
		{
			string result = "en";
			CultureInfo cultureInfo = culture;
			while (cultureInfo != null && !string.IsNullOrEmpty(cultureInfo.Name))
			{
				string name = cultureInfo.Name;
				if (existsFilter(name))
				{
					result = name;
					break;
				}
				cultureInfo = cultureInfo.Parent;
			}
			return result;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008D51F File Offset: 0x0008B71F
		private string GetScriptDirectoryFromCultureName(IPageContext handler, string cultureName, bool isBootScriptsDirectory)
		{
			if (this.scriptDirectoryFormat == null)
			{
				this.scriptDirectoryFormat = ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath) + "/{0}";
			}
			return handler.FormatURIForCDN(string.Format(this.scriptDirectoryFormat, cultureName), isBootScriptsDirectory);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0008D574 File Offset: 0x0008B774
		private string GetLocalizedCultureName(string resourceName)
		{
			return LocalizedStringsScriptResource.GetLocalizedCultureName(LocalizedStringsScriptResource.threadCultureToLocalizedCultureMap, Thread.CurrentThread.CurrentUICulture, (string lang) => this.ResourceExists(resourceName, lang));
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0008D5B8 File Offset: 0x0008B7B8
		private bool ResourceExists(string resourceName, string languageName)
		{
			string path = Path.Combine(FolderConfiguration.Instance.RootPath, ResourcePathBuilderUtilities.GetScriptResourcesRelativeFolderPath(base.ResourcesRelativeFolderPath), languageName, resourceName);
			return File.Exists(path);
		}

		// Token: 0x040016C2 RID: 5826
		private const string LocalizedStringsScriptPath = "/{0}";

		// Token: 0x040016C3 RID: 5827
		private const string DefaultCulture = "en";

		// Token: 0x040016C4 RID: 5828
		private readonly string resourceName;

		// Token: 0x040016C5 RID: 5829
		private static ConcurrentDictionary<string, string> threadCultureToLocalizedCultureMap = new ConcurrentDictionary<string, string>();

		// Token: 0x040016C6 RID: 5830
		private string scriptDirectoryFormat;
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000434 RID: 1076
	internal sealed class ClientWatsonParameters : DisposeTrackableBase, IClientWatsonParameters
	{
		// Token: 0x060024B4 RID: 9396 RVA: 0x00085190 File Offset: 0x00083390
		public ClientWatsonParameters(string owaVersion)
		{
			this.symbolsFolderPath = AppConfigLoader.GetConfigStringValue("ClientWatsonSymbolsFolderPath", Path.Combine(ExchangeSetupContext.InstallPath, string.Format("ClientAccess\\Owa\\{0}\\ScriptSymbols", ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(owaVersion))));
			this.maxNumberOfWatsonsPerError = AppConfigLoader.GetConfigIntValue("ClientWatsonMaxReportsPerError", 1, int.MaxValue, 5);
			TimeSpan configTimeSpanValue = AppConfigLoader.GetConfigTimeSpanValue("ClientWatsonResetErrorCountInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromDays(1.0), TimeSpan.FromHours(1.0));
			this.resetErrorsReportedTimer = new Timer(delegate(object param0)
			{
				this.clientErrorsReported = new ConcurrentDictionary<int, int>();
			}, null, configTimeSpanValue, configTimeSpanValue);
			Version installedVersion = ExchangeSetupContext.InstalledVersion;
			this.ExchangeSourcesPath = string.Format("\\\\exsrc\\SOURCES\\ALL\\{0:D2}.{1:D2}.{2:D4}.{3:D3}\\", new object[]
			{
				installedVersion.Major,
				installedVersion.Minor,
				installedVersion.Build,
				installedVersion.Revision
			});
			this.owaVersion = owaVersion;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000852AD File Offset: 0x000834AD
		public IConsolidationSymbolsMap ConsolidationSymbolsMap
		{
			get
			{
				this.AssureSymbolsAreLoaded();
				return this.bootSlabSymbolsMap;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x000852BB File Offset: 0x000834BB
		public IJavaScriptSymbolsMap<AjaxMinSymbolForJavaScript> MinificationSymbolsMapForJavaScript
		{
			get
			{
				this.AssureSymbolsAreLoaded();
				return this.minificationSymbolsMapForJavaScript;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x000852C9 File Offset: 0x000834C9
		public IJavaScriptSymbolsMap<AjaxMinSymbolForScriptSharp> MinificationSymbolsMapForScriptSharp
		{
			get
			{
				this.AssureSymbolsAreLoaded();
				return this.minificationSymbolsMapForScriptSharp;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000852D7 File Offset: 0x000834D7
		public IJavaScriptSymbolsMap<ScriptSharpSymbolWrapper> ObfuscationSymbolsMap
		{
			get
			{
				this.AssureSymbolsAreLoaded();
				return this.obfuscationSymbolsMap;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x000852E5 File Offset: 0x000834E5
		public SendClientWatsonReportAction ReportAction
		{
			get
			{
				return new SendClientWatsonReportAction(ExWatson.SendClientWatsonReport);
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000852F3 File Offset: 0x000834F3
		public string OwaVersion
		{
			get
			{
				return this.owaVersion;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x000852FB File Offset: 0x000834FB
		// (set) Token: 0x060024BC RID: 9404 RVA: 0x00085303 File Offset: 0x00083503
		public string ExchangeSourcesPath { get; private set; }

		// Token: 0x060024BD RID: 9405 RVA: 0x0008530C File Offset: 0x0008350C
		private void AssureSymbolsAreLoaded()
		{
			if (this.isLoaded)
			{
				return;
			}
			lock (this.loadLock)
			{
				if (!this.isLoaded)
				{
					Exception ex = null;
					try
					{
						this.LoadSymbols();
					}
					catch (IOException ex2)
					{
						ex = ex2;
					}
					catch (UnauthorizedAccessException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						OwaServerLogger.AppendToLog(SymbolMapLoadLogEvent.CreateForError(ex));
					}
					this.isLoaded = true;
				}
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000853AC File Offset: 0x000835AC
		public bool IsErrorOverReportQuota(int hashCode)
		{
			int num = this.clientErrorsReported.AddOrUpdate(hashCode, 1, (int key, int oldValue) => oldValue + 1);
			return num > this.maxNumberOfWatsonsPerError;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000853ED File Offset: 0x000835ED
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ClientWatsonParameters>(this);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000853F5 File Offset: 0x000835F5
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && !base.IsDisposed)
			{
				this.resetErrorsReportedTimer.Dispose();
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00085410 File Offset: 0x00083610
		private void LoadSymbols()
		{
			this.bootSlabSymbolsMap = new ConsolidationSymbolsMap(this.symbolsFolderPath, this.OwaVersion);
			string[] files = Directory.GetFiles(this.symbolsFolderPath, "*_obfuscate.xml");
			ScriptSharpSourceMapLoader scriptSharpSourceMapLoader = new ScriptSharpSourceMapLoader(files);
			this.obfuscationSymbolsMap = scriptSharpSourceMapLoader.Load();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string[] files2 = Directory.GetFiles(this.symbolsFolderPath, "**_minify.xml");
			int length = "*_minify.xml".Length;
			foreach (string text in files2)
			{
				string fileName = Path.GetFileName(text);
				string scriptName = fileName.Substring(0, fileName.Length - length + 1);
				if (this.obfuscationSymbolsMap.HasSymbolsLoadedForScript(scriptName))
				{
					list.Add(text);
				}
				else
				{
					list2.Add(text);
				}
			}
			AjaxMinSourceMapLoader<AjaxMinSymbolForJavaScript> ajaxMinSourceMapLoader = new AjaxMinSourceMapLoader<AjaxMinSymbolForJavaScript>(list2, new AjaxMinSymbolParserForJavaScript());
			AjaxMinSourceMapLoader<AjaxMinSymbolForScriptSharp> ajaxMinSourceMapLoader2 = new AjaxMinSourceMapLoader<AjaxMinSymbolForScriptSharp>(list, new AjaxMinSymbolParserForScriptSharp());
			this.minificationSymbolsMapForJavaScript = ajaxMinSourceMapLoader.Load();
			this.minificationSymbolsMapForScriptSharp = ajaxMinSourceMapLoader2.Load();
		}

		// Token: 0x0400142B RID: 5163
		private readonly int maxNumberOfWatsonsPerError;

		// Token: 0x0400142C RID: 5164
		private readonly Timer resetErrorsReportedTimer;

		// Token: 0x0400142D RID: 5165
		private readonly string symbolsFolderPath;

		// Token: 0x0400142E RID: 5166
		private ConcurrentDictionary<int, int> clientErrorsReported = new ConcurrentDictionary<int, int>();

		// Token: 0x0400142F RID: 5167
		private readonly object loadLock = new object();

		// Token: 0x04001430 RID: 5168
		private readonly string owaVersion;

		// Token: 0x04001431 RID: 5169
		private volatile bool isLoaded;

		// Token: 0x04001432 RID: 5170
		public IConsolidationSymbolsMap bootSlabSymbolsMap;

		// Token: 0x04001433 RID: 5171
		public IJavaScriptSymbolsMap<AjaxMinSymbolForJavaScript> minificationSymbolsMapForJavaScript;

		// Token: 0x04001434 RID: 5172
		public IJavaScriptSymbolsMap<AjaxMinSymbolForScriptSharp> minificationSymbolsMapForScriptSharp;

		// Token: 0x04001435 RID: 5173
		public IJavaScriptSymbolsMap<ScriptSharpSymbolWrapper> obfuscationSymbolsMap;
	}
}

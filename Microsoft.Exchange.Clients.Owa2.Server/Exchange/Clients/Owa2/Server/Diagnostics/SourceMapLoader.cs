using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Xml;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000423 RID: 1059
	internal abstract class SourceMapLoader<T> where T : IJavaScriptSymbol
	{
		// Token: 0x06002436 RID: 9270 RVA: 0x00082F2D File Offset: 0x0008112D
		protected SourceMapLoader(IEnumerable<string> symbolMapFiles)
		{
			this.symbolMapFiles = symbolMapFiles;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00082F3C File Offset: 0x0008113C
		public JavaScriptSymbolsMap<T> Load()
		{
			Dictionary<string, List<T>> symbolMaps = new Dictionary<string, List<T>>(20, StringComparer.InvariantCultureIgnoreCase);
			Dictionary<uint, string> sourceFileIdMap = new Dictionary<uint, string>(1024);
			ClientWatsonFunctionNamePool clientWatsonFunctionNamePool = new ClientWatsonFunctionNamePool();
			foreach (string filePath in this.symbolMapFiles)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				Exception ex = null;
				try
				{
					this.LoadSymbolsMapFromFile(filePath, symbolMaps, sourceFileIdMap, clientWatsonFunctionNamePool);
				}
				catch (XmlException ex2)
				{
					ex = ex2;
				}
				catch (IOException ex3)
				{
					ex = ex3;
				}
				catch (SecurityException ex4)
				{
					ex = ex4;
				}
				finally
				{
					stopwatch.Stop();
				}
				SymbolMapLoadLogEvent logEvent;
				if (ex == null)
				{
					logEvent = SymbolMapLoadLogEvent.CreateForSuccess(filePath, stopwatch.Elapsed);
				}
				else
				{
					logEvent = SymbolMapLoadLogEvent.CreateForError(filePath, ex, stopwatch.Elapsed);
				}
				OwaServerLogger.AppendToLog(logEvent);
			}
			return new JavaScriptSymbolsMap<T>(symbolMaps, sourceFileIdMap, clientWatsonFunctionNamePool.ToArray());
		}

		// Token: 0x06002438 RID: 9272
		protected abstract void LoadSymbolsMapFromFile(string filePath, Dictionary<string, List<T>> symbolMaps, Dictionary<uint, string> sourceFileIdMap, ClientWatsonFunctionNamePool functionNamePool);

		// Token: 0x040013BB RID: 5051
		protected const int ScriptSymbolsInitialCapacity = 1024;

		// Token: 0x040013BC RID: 5052
		private const int ScriptMapFilesInitialCapacity = 20;

		// Token: 0x040013BD RID: 5053
		private const int SourceFileMapInitialCapacity = 1024;

		// Token: 0x040013BE RID: 5054
		private readonly IEnumerable<string> symbolMapFiles;
	}
}

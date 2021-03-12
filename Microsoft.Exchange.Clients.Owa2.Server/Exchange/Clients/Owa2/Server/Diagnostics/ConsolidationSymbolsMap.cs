using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000438 RID: 1080
	internal sealed class ConsolidationSymbolsMap : IConsolidationSymbolsMap
	{
		// Token: 0x060024CC RID: 9420 RVA: 0x000855E0 File Offset: 0x000837E0
		public ConsolidationSymbolsMap(string symbolsMapFolderPath, string owaVersion)
		{
			this.symbolsMapFolder = symbolsMapFolderPath;
			this.symbolMaps = new Dictionary<string, List<ConsolidationSymbolsMap.ConsolidationSymbol>>(21, StringComparer.InvariantCultureIgnoreCase);
			this.scriptsOutOfSync = new List<string>();
			this.sourceFileIds = new List<string>(40);
			this.owaVersion = owaVersion;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x00085636 File Offset: 0x00083836
		// (set) Token: 0x060024CE RID: 9422 RVA: 0x0008563E File Offset: 0x0008383E
		public bool SkipChecksumValidation { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x00085647 File Offset: 0x00083847
		private string ScriptsPath
		{
			get
			{
				if (this.scriptsPath == null)
				{
					this.scriptsPath = ResourcePathBuilderUtilities.GetScriptResourcesRootFolderPath(ExchangeSetupContext.InstallPath, ResourcePathBuilderUtilities.GetResourcesRelativeFolderPath(this.owaVersion));
				}
				return this.scriptsPath;
			}
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x00085674 File Offset: 0x00083874
		public bool Search(string scriptName, int line, int column, out string sourceFile, out Tuple<int, int> preConsolidationPosition)
		{
			this.AssureSymbolsAreLoaded();
			preConsolidationPosition = null;
			sourceFile = null;
			ConsolidationSymbolsMap.ConsolidationSymbol item = default(ConsolidationSymbolsMap.ConsolidationSymbol);
			item.ScriptEndLine = line;
			item.ScriptStartLine = line;
			item.ScriptEndColumn = column;
			item.ScriptStartColumn = column;
			List<ConsolidationSymbolsMap.ConsolidationSymbol> list;
			if (!this.symbolMaps.TryGetValue(scriptName, out list))
			{
				return false;
			}
			List<ConsolidationSymbolsMap.ConsolidationSymbol> list2 = this.symbolMaps[scriptName];
			int num = list2.BinarySearch(item, ConsolidationSymbolsMap.ConsolidationSymbolComparer.Instance);
			if (num < 0)
			{
				return false;
			}
			ConsolidationSymbolsMap.ConsolidationSymbol consolidationSymbol = list2[num];
			int num2 = line - consolidationSymbol.ScriptStartLine;
			int num3 = column - consolidationSymbol.ScriptStartColumn;
			preConsolidationPosition = new Tuple<int, int>(consolidationSymbol.SourceStartLine + num2, consolidationSymbol.SourceStartColumn + num3);
			sourceFile = this.sourceFileIds[consolidationSymbol.SourceFileId];
			return true;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00085742 File Offset: 0x00083942
		public bool HasSymbolsLoadedForScript(string scriptName)
		{
			this.AssureSymbolsAreLoaded();
			return this.symbolMaps.ContainsKey(scriptName) || this.scriptsOutOfSync.Contains(scriptName);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00085768 File Offset: 0x00083968
		private void AssureSymbolsAreLoaded()
		{
			if (this.symbolsLoaded)
			{
				return;
			}
			lock (this.loadLock)
			{
				try
				{
					this.UnsafeAssureSymbolsAreLoaded();
				}
				catch (IOException e)
				{
					OwaServerLogger.AppendToLog(SymbolMapLoadLogEvent.CreateForError(e));
				}
				finally
				{
					this.symbolsLoaded = true;
				}
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000857E4 File Offset: 0x000839E4
		private void UnsafeAssureSymbolsAreLoaded()
		{
			if (this.symbolsLoaded)
			{
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (string path in ConsolidationSymbolsMap.ConsolidationMapFileNames)
			{
				string text = Path.Combine(this.symbolsMapFolder, path);
				using (TextReader textReader = new StreamReader(text, Encoding.UTF8))
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(40, StringComparer.InvariantCultureIgnoreCase);
					string text2;
					while ((text2 = textReader.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(text2))
						{
							if (text2.StartsWith("#"))
							{
								string[] array = text2.Split(new char[]
								{
									' ',
									','
								});
								string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(array[1]);
								if (this.VerifyChecksum(fileNameWithoutExtension, array[2]))
								{
									this.symbolMaps.Add(fileNameWithoutExtension, new List<ConsolidationSymbolsMap.ConsolidationSymbol>(1024));
								}
								else
								{
									this.scriptsOutOfSync.Add(fileNameWithoutExtension);
								}
							}
							else
							{
								string[] array2 = text2.Split(new char[]
								{
									','
								});
								string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(array2[0]);
								int count;
								if (!dictionary.TryGetValue(fileNameWithoutExtension2, out count))
								{
									count = this.sourceFileIds.Count;
									dictionary.Add(fileNameWithoutExtension2, count);
									this.sourceFileIds.Add(fileNameWithoutExtension2);
								}
								string fileNameWithoutExtension3 = Path.GetFileNameWithoutExtension(array2[5]);
								if (this.symbolMaps.ContainsKey(fileNameWithoutExtension3))
								{
									ConsolidationSymbolsMap.ConsolidationSymbol item = new ConsolidationSymbolsMap.ConsolidationSymbol
									{
										SourceStartLine = int.Parse(array2[1]),
										SourceStartColumn = int.Parse(array2[2]),
										SourceEndLine = int.Parse(array2[3]),
										SourceEndColumn = int.Parse(array2[4]),
										ScriptStartLine = int.Parse(array2[6]),
										ScriptStartColumn = int.Parse(array2[7]),
										ScriptEndLine = int.Parse(array2[8]),
										ScriptEndColumn = int.Parse(array2[9]),
										SourceFileId = count
									};
									this.symbolMaps[fileNameWithoutExtension3].Add(item);
								}
							}
						}
					}
				}
				OwaServerLogger.AppendToLog(SymbolMapLoadLogEvent.CreateForSuccess(text, stopwatch.Elapsed));
			}
			foreach (List<ConsolidationSymbolsMap.ConsolidationSymbol> list in this.symbolMaps.Values)
			{
				list.TrimExcess();
			}
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00085A8C File Offset: 0x00083C8C
		private bool VerifyChecksum(string scriptName, string checksum)
		{
			if (this.SkipChecksumValidation)
			{
				return true;
			}
			string path = Path.Combine(this.ScriptsPath, scriptName + ".js");
			if (!File.Exists(path))
			{
				return false;
			}
			bool result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (HashAlgorithm hashAlgorithm = MD5.Create())
				{
					byte[] value = hashAlgorithm.ComputeHash(fileStream);
					result = BitConverter.ToString(value).Equals(checksum);
				}
			}
			return result;
		}

		// Token: 0x04001442 RID: 5186
		private const string ScriptExtension = ".js";

		// Token: 0x04001443 RID: 5187
		private const int EstimatedNumberOfPreConsolidationScripts = 40;

		// Token: 0x04001444 RID: 5188
		private const int EstimatedNumberOfPostConsolidationScripts = 21;

		// Token: 0x04001445 RID: 5189
		private const int EstimatedNumberOfSymbolsPerScript = 1024;

		// Token: 0x04001446 RID: 5190
		private static readonly string[] ConsolidationMapFileNames = new string[]
		{
			"preboot_slabmanifest_consolidation.csv",
			"slabmanifest_consolidation.csv"
		};

		// Token: 0x04001447 RID: 5191
		private readonly string symbolsMapFolder;

		// Token: 0x04001448 RID: 5192
		private readonly IDictionary<string, List<ConsolidationSymbolsMap.ConsolidationSymbol>> symbolMaps;

		// Token: 0x04001449 RID: 5193
		private readonly List<string> scriptsOutOfSync;

		// Token: 0x0400144A RID: 5194
		private readonly List<string> sourceFileIds;

		// Token: 0x0400144B RID: 5195
		private readonly object loadLock = new object();

		// Token: 0x0400144C RID: 5196
		private string scriptsPath;

		// Token: 0x0400144D RID: 5197
		private bool symbolsLoaded;

		// Token: 0x0400144E RID: 5198
		private readonly string owaVersion;

		// Token: 0x02000439 RID: 1081
		private enum ConsolidationSymbolData
		{
			// Token: 0x04001451 RID: 5201
			SourceFileName,
			// Token: 0x04001452 RID: 5202
			SourceStartLine,
			// Token: 0x04001453 RID: 5203
			SourceStartColumn,
			// Token: 0x04001454 RID: 5204
			SourceEndLine,
			// Token: 0x04001455 RID: 5205
			SourceEndColumn,
			// Token: 0x04001456 RID: 5206
			ScriptFileName,
			// Token: 0x04001457 RID: 5207
			ScriptStartLine,
			// Token: 0x04001458 RID: 5208
			ScriptStartColumn,
			// Token: 0x04001459 RID: 5209
			ScriptEndLine,
			// Token: 0x0400145A RID: 5210
			ScriptEndColumn
		}

		// Token: 0x0200043A RID: 1082
		private struct ConsolidationSymbol
		{
			// Token: 0x170009A1 RID: 2465
			// (get) Token: 0x060024D6 RID: 9430 RVA: 0x00085B4A File Offset: 0x00083D4A
			// (set) Token: 0x060024D7 RID: 9431 RVA: 0x00085B52 File Offset: 0x00083D52
			public int SourceStartLine { get; set; }

			// Token: 0x170009A2 RID: 2466
			// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00085B5B File Offset: 0x00083D5B
			// (set) Token: 0x060024D9 RID: 9433 RVA: 0x00085B63 File Offset: 0x00083D63
			public int SourceStartColumn { get; set; }

			// Token: 0x170009A3 RID: 2467
			// (get) Token: 0x060024DA RID: 9434 RVA: 0x00085B6C File Offset: 0x00083D6C
			// (set) Token: 0x060024DB RID: 9435 RVA: 0x00085B74 File Offset: 0x00083D74
			public int SourceEndLine { get; set; }

			// Token: 0x170009A4 RID: 2468
			// (get) Token: 0x060024DC RID: 9436 RVA: 0x00085B7D File Offset: 0x00083D7D
			// (set) Token: 0x060024DD RID: 9437 RVA: 0x00085B85 File Offset: 0x00083D85
			public int SourceEndColumn { get; set; }

			// Token: 0x170009A5 RID: 2469
			// (get) Token: 0x060024DE RID: 9438 RVA: 0x00085B8E File Offset: 0x00083D8E
			// (set) Token: 0x060024DF RID: 9439 RVA: 0x00085B96 File Offset: 0x00083D96
			public int ScriptStartColumn { get; set; }

			// Token: 0x170009A6 RID: 2470
			// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00085B9F File Offset: 0x00083D9F
			// (set) Token: 0x060024E1 RID: 9441 RVA: 0x00085BA7 File Offset: 0x00083DA7
			public int ScriptStartLine { get; set; }

			// Token: 0x170009A7 RID: 2471
			// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00085BB0 File Offset: 0x00083DB0
			// (set) Token: 0x060024E3 RID: 9443 RVA: 0x00085BB8 File Offset: 0x00083DB8
			public int ScriptEndColumn { get; set; }

			// Token: 0x170009A8 RID: 2472
			// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00085BC1 File Offset: 0x00083DC1
			// (set) Token: 0x060024E5 RID: 9445 RVA: 0x00085BC9 File Offset: 0x00083DC9
			public int ScriptEndLine { get; set; }

			// Token: 0x170009A9 RID: 2473
			// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00085BD2 File Offset: 0x00083DD2
			// (set) Token: 0x060024E7 RID: 9447 RVA: 0x00085BDA File Offset: 0x00083DDA
			public int SourceFileId { get; set; }
		}

		// Token: 0x0200043B RID: 1083
		private class ConsolidationSymbolComparer : IComparer<ConsolidationSymbolsMap.ConsolidationSymbol>
		{
			// Token: 0x060024E8 RID: 9448 RVA: 0x00085BE3 File Offset: 0x00083DE3
			private ConsolidationSymbolComparer()
			{
			}

			// Token: 0x170009AA RID: 2474
			// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00085BEB File Offset: 0x00083DEB
			public static ConsolidationSymbolsMap.ConsolidationSymbolComparer Instance
			{
				get
				{
					return ConsolidationSymbolsMap.ConsolidationSymbolComparer.instance;
				}
			}

			// Token: 0x060024EA RID: 9450 RVA: 0x00085BF4 File Offset: 0x00083DF4
			public int Compare(ConsolidationSymbolsMap.ConsolidationSymbol x, ConsolidationSymbolsMap.ConsolidationSymbol y)
			{
				if (x.ScriptStartLine > y.ScriptStartLine || (x.ScriptStartLine == y.ScriptStartLine && x.ScriptStartColumn > y.ScriptStartColumn))
				{
					return 1;
				}
				if (x.ScriptEndLine < y.ScriptEndLine || (x.ScriptEndLine == y.ScriptEndLine && x.ScriptEndColumn < y.ScriptEndColumn))
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x04001464 RID: 5220
			private const int Less = -1;

			// Token: 0x04001465 RID: 5221
			private const int Greater = 1;

			// Token: 0x04001466 RID: 5222
			private const int Contain = 0;

			// Token: 0x04001467 RID: 5223
			private static readonly ConsolidationSymbolsMap.ConsolidationSymbolComparer instance = new ConsolidationSymbolsMap.ConsolidationSymbolComparer();
		}
	}
}

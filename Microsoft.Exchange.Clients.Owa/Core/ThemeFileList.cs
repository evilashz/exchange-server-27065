using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000269 RID: 617
	internal static class ThemeFileList
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x0007DDE8 File Offset: 0x0007BFE8
		internal static int Add(string themeFileName, bool useCssSprites)
		{
			if (!ThemeFileList.idTable.ContainsKey(themeFileName))
			{
				ThemeFileList.ThemeFile item = new ThemeFileList.ThemeFile((ThemeFileId)ThemeFileList.nameTable.Count, themeFileName, useCssSprites);
				ThemeFileList.idTable[themeFileName] = ThemeFileList.nameTable.Count;
				ThemeFileList.nameTable.Add(item);
			}
			return ThemeFileList.idTable[themeFileName];
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0007DE40 File Offset: 0x0007C040
		internal static int GetIdFromName(string themeFileName)
		{
			int result = 0;
			ThemeFileList.idTable.TryGetValue(themeFileName, out result);
			return result;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0007DE5E File Offset: 0x0007C05E
		internal static string GetNameFromId(ThemeFileId themeFileId)
		{
			return ThemeFileList.nameTable[(int)themeFileId].Name;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0007DE70 File Offset: 0x0007C070
		internal static string GetClassNameFromId(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].ClassName;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0007DE82 File Offset: 0x0007C082
		internal static bool GetPhaseIIFromId(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].PhaseII;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0007DE94 File Offset: 0x0007C094
		internal static string GetNameFromId(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].Name;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0007DEA6 File Offset: 0x0007C0A6
		internal static bool CanUseCssSprites(ThemeFileId themeFileId)
		{
			return ThemeFileList.CanUseCssSprites((int)themeFileId);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0007DEAE File Offset: 0x0007C0AE
		internal static bool CanUseCssSprites(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].UseCssSprites;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0007DEC0 File Offset: 0x0007C0C0
		internal static bool IsResourceFile(ThemeFileId themeFileId)
		{
			return ThemeFileList.IsResourceFile((int)themeFileId);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0007DEC8 File Offset: 0x0007C0C8
		internal static bool IsResourceFile(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].IsResource;
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0007DEDA File Offset: 0x0007C0DA
		internal static int Count
		{
			get
			{
				return ThemeFileList.idTable.Count;
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0007DEE8 File Offset: 0x0007C0E8
		private static bool Initialize()
		{
			ThemeFileList.nameTable = new List<ThemeFileList.ThemeFile>(601);
			ThemeFileList.idTable = new Dictionary<string, int>(601);
			Type typeFromHandle = typeof(ThemeFileId);
			FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
			foreach (FieldInfo fieldInfo in fields)
			{
				ThemeFileId themeFileId = (ThemeFileId)fieldInfo.GetValue(null);
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(ThemeFileInfoAttribute), false);
				if (customAttributes == null || customAttributes.Length == 0)
				{
					ExTraceGlobals.CoreTracer.TraceError<ThemeFileId>(0L, "{0} doesn't define ThemeFileInfoAttribute", themeFileId);
				}
				else
				{
					ThemeFileInfoAttribute themeFileInfoAttribute = (ThemeFileInfoAttribute)customAttributes[0];
					ThemeFileList.idTable[themeFileInfoAttribute.Name] = (int)themeFileId;
					ThemeFileList.nameTable.Add(new ThemeFileList.ThemeFile(themeFileId, themeFileInfoAttribute.Name, themeFileInfoAttribute.UseCssSprites, themeFileInfoAttribute.IsResource, themeFileInfoAttribute.PhaseII));
				}
			}
			return true;
		}

		// Token: 0x04001089 RID: 4233
		private static List<ThemeFileList.ThemeFile> nameTable;

		// Token: 0x0400108A RID: 4234
		private static Dictionary<string, int> idTable;

		// Token: 0x0400108B RID: 4235
		private static bool isInitialized = ThemeFileList.Initialize();

		// Token: 0x0200026A RID: 618
		private struct ThemeFile
		{
			// Token: 0x060014AE RID: 5294 RVA: 0x0007DFE0 File Offset: 0x0007C1E0
			public ThemeFile(ThemeFileId id, string name)
			{
				this = new ThemeFileList.ThemeFile(id, name, true);
			}

			// Token: 0x060014AF RID: 5295 RVA: 0x0007DFEB File Offset: 0x0007C1EB
			public ThemeFile(ThemeFileId id, string name, bool useCssSprites)
			{
				this = new ThemeFileList.ThemeFile(id, name, useCssSprites, false);
			}

			// Token: 0x060014B0 RID: 5296 RVA: 0x0007DFF7 File Offset: 0x0007C1F7
			public ThemeFile(ThemeFileId id, string name, bool useCssSprites, bool isResource)
			{
				this = new ThemeFileList.ThemeFile(id, name, useCssSprites, isResource, false);
			}

			// Token: 0x060014B1 RID: 5297 RVA: 0x0007E008 File Offset: 0x0007C208
			public ThemeFile(ThemeFileId id, string name, bool useCssSprites, bool isResource, bool phaseII)
			{
				this.Id = id;
				this.Name = name;
				this.ClassName = (useCssSprites ? ("sprites-" + name.Replace(".", "-")) : string.Empty);
				this.UseCssSprites = useCssSprites;
				this.IsResource = isResource;
				this.PhaseII = phaseII;
			}

			// Token: 0x0400108C RID: 4236
			private const string SpritesClassPrefix = "sprites-";

			// Token: 0x0400108D RID: 4237
			public ThemeFileId Id;

			// Token: 0x0400108E RID: 4238
			public string Name;

			// Token: 0x0400108F RID: 4239
			public string ClassName;

			// Token: 0x04001090 RID: 4240
			public bool UseCssSprites;

			// Token: 0x04001091 RID: 4241
			public bool IsResource;

			// Token: 0x04001092 RID: 4242
			public bool PhaseII;
		}
	}
}

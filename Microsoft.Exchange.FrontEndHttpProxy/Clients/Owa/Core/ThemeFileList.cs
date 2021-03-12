using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200006F RID: 111
	internal static class ThemeFileList
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001413B File Offset: 0x0001233B
		internal static int Count
		{
			get
			{
				return ThemeFileList.idTable.Count;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00014148 File Offset: 0x00012348
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

		// Token: 0x06000360 RID: 864 RVA: 0x000141A0 File Offset: 0x000123A0
		internal static int GetIdFromName(string themeFileName)
		{
			int result = 0;
			ThemeFileList.idTable.TryGetValue(themeFileName, out result);
			return result;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000141BE File Offset: 0x000123BE
		internal static string GetNameFromId(ThemeFileId themeFileId)
		{
			return ThemeFileList.nameTable[(int)themeFileId].Name;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000141D0 File Offset: 0x000123D0
		internal static string GetClassNameFromId(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].ClassName;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000141E2 File Offset: 0x000123E2
		internal static bool GetPhaseIIFromId(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].PhaseII;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000141F4 File Offset: 0x000123F4
		internal static string GetNameFromId(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].Name;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00014206 File Offset: 0x00012406
		internal static bool CanUseCssSprites(ThemeFileId themeFileId)
		{
			return ThemeFileList.CanUseCssSprites((int)themeFileId);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001420E File Offset: 0x0001240E
		internal static bool CanUseCssSprites(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].UseCssSprites;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00014220 File Offset: 0x00012420
		internal static bool IsResourceFile(ThemeFileId themeFileId)
		{
			return ThemeFileList.IsResourceFile((int)themeFileId);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00014228 File Offset: 0x00012428
		internal static bool IsResourceFile(int themeFileIndex)
		{
			return ThemeFileList.nameTable[themeFileIndex].IsResource;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001423C File Offset: 0x0001243C
		private static bool Initialize()
		{
			ThemeFileList.nameTable = new List<ThemeFileList.ThemeFile>(27);
			ThemeFileList.idTable = new Dictionary<string, int>(27);
			Type typeFromHandle = typeof(ThemeFileId);
			FieldInfo[] fields = typeFromHandle.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
			foreach (FieldInfo fieldInfo in fields)
			{
				ThemeFileId themeFileId = (ThemeFileId)fieldInfo.GetValue(null);
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(ThemeFileInfoAttribute), false);
				if (customAttributes == null || customAttributes.Length == 0)
				{
					ExTraceGlobals.VerboseTracer.TraceError<ThemeFileId>(0L, "{0} doesn't define ThemeFileInfoAttribute", themeFileId);
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

		// Token: 0x04000261 RID: 609
		private static List<ThemeFileList.ThemeFile> nameTable;

		// Token: 0x04000262 RID: 610
		private static Dictionary<string, int> idTable;

		// Token: 0x04000263 RID: 611
		private static bool isInitialized = ThemeFileList.Initialize();

		// Token: 0x02000070 RID: 112
		private struct ThemeFile
		{
			// Token: 0x0600036B RID: 875 RVA: 0x0001432E File Offset: 0x0001252E
			public ThemeFile(ThemeFileId id, string name)
			{
				this = new ThemeFileList.ThemeFile(id, name, true);
			}

			// Token: 0x0600036C RID: 876 RVA: 0x00014339 File Offset: 0x00012539
			public ThemeFile(ThemeFileId id, string name, bool useCssSprites)
			{
				this = new ThemeFileList.ThemeFile(id, name, useCssSprites, false);
			}

			// Token: 0x0600036D RID: 877 RVA: 0x00014345 File Offset: 0x00012545
			public ThemeFile(ThemeFileId id, string name, bool useCssSprites, bool isResource)
			{
				this = new ThemeFileList.ThemeFile(id, name, useCssSprites, isResource, false);
			}

			// Token: 0x0600036E RID: 878 RVA: 0x00014354 File Offset: 0x00012554
			public ThemeFile(ThemeFileId id, string name, bool useCssSprites, bool isResource, bool phaseII)
			{
				this.Id = id;
				this.Name = name;
				this.ClassName = (useCssSprites ? ("sprites-" + name.Replace(".", "-")) : string.Empty);
				this.UseCssSprites = useCssSprites;
				this.IsResource = isResource;
				this.PhaseII = phaseII;
			}

			// Token: 0x04000264 RID: 612
			private const string SpritesClassPrefix = "sprites-";

			// Token: 0x04000265 RID: 613
			public ThemeFileId Id;

			// Token: 0x04000266 RID: 614
			public string Name;

			// Token: 0x04000267 RID: 615
			public string ClassName;

			// Token: 0x04000268 RID: 616
			public bool UseCssSprites;

			// Token: 0x04000269 RID: 617
			public bool IsResource;

			// Token: 0x0400026A RID: 618
			public bool PhaseII;
		}
	}
}

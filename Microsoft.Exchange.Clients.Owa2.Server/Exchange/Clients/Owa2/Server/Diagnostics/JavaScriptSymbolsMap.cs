using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000448 RID: 1096
	internal sealed class JavaScriptSymbolsMap<T> : IJavaScriptSymbolsMap<T> where T : IJavaScriptSymbol
	{
		// Token: 0x060024F7 RID: 9463 RVA: 0x00085E8E File Offset: 0x0008408E
		public JavaScriptSymbolsMap(IDictionary<string, List<T>> symbolMaps, IDictionary<uint, string> sourceFileIdMap, string[] functionNames)
		{
			this.symbolMaps = symbolMaps;
			this.sourceFileIdMap = sourceFileIdMap;
			this.functionNames = functionNames;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00085EAC File Offset: 0x000840AC
		public bool Search(string scriptName, T javaScriptSymbol, out T symbolFound)
		{
			symbolFound = default(T);
			List<T> list;
			if (!this.symbolMaps.TryGetValue(scriptName, out list))
			{
				return false;
			}
			int num = list.BinarySearch(javaScriptSymbol, JavaScriptSymbolComparer<T>.Instance);
			if (num < 0)
			{
				num = ~num;
			}
			if (num >= list.Count)
			{
				return false;
			}
			bool result = false;
			T t;
			for (;;)
			{
				t = list[num];
				if (t.ScriptStartLine < javaScriptSymbol.ScriptStartLine || (t.ScriptStartLine == javaScriptSymbol.ScriptStartLine && t.ScriptStartColumn <= javaScriptSymbol.ScriptStartColumn))
				{
					break;
				}
				num = t.ParentSymbolIndex;
				if (num < 0)
				{
					return result;
				}
			}
			result = true;
			symbolFound = t;
			return result;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00085F70 File Offset: 0x00084170
		public string GetSourceFilePathFromId(uint id)
		{
			string result;
			if (this.sourceFileIdMap.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00085F90 File Offset: 0x00084190
		public string GetFunctionName(int index)
		{
			return this.functionNames[index];
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00085F9A File Offset: 0x0008419A
		public bool HasSymbolsLoadedForScript(string scriptName)
		{
			return this.symbolMaps.ContainsKey(scriptName);
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00085FA8 File Offset: 0x000841A8
		internal HashSet<string> GetScriptNames()
		{
			return new HashSet<string>(this.symbolMaps.Keys, StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00086174 File Offset: 0x00084374
		internal IEnumerable<T> GetSymbolsLoadedForScript(string scriptName)
		{
			List<T> map;
			if (this.symbolMaps.TryGetValue(scriptName, out map))
			{
				foreach (T symbol in map)
				{
					yield return symbol;
				}
			}
			yield break;
		}

		// Token: 0x040014E2 RID: 5346
		private readonly IDictionary<string, List<T>> symbolMaps;

		// Token: 0x040014E3 RID: 5347
		private readonly IDictionary<uint, string> sourceFileIdMap;

		// Token: 0x040014E4 RID: 5348
		private readonly string[] functionNames;
	}
}

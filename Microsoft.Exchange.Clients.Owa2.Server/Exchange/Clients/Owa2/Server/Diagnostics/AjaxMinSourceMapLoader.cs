using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000424 RID: 1060
	internal class AjaxMinSourceMapLoader<T> : SourceMapLoader<T> where T : IJavaScriptSymbol
	{
		// Token: 0x06002439 RID: 9273 RVA: 0x00083048 File Offset: 0x00081248
		public AjaxMinSourceMapLoader(IEnumerable<string> symbolMapFiles, ClientWatsonSymbolParser<T> symbolParser) : base(symbolMapFiles)
		{
			this.symbolParser = symbolParser;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00083058 File Offset: 0x00081258
		protected override void LoadSymbolsMapFromFile(string filePath, Dictionary<string, List<T>> symbolMaps, Dictionary<uint, string> sourceFileIdMap, ClientWatsonFunctionNamePool functionNamePool)
		{
			uint count = (uint)sourceFileIdMap.Count;
			string text = null;
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, Stack<int>> dictionary = new Dictionary<string, Stack<int>>();
			using (XmlReader xmlReader = XmlReader.Create(filePath))
			{
				while (xmlReader.Read())
				{
					if (xmlReader.IsStartElement("s"))
					{
						T t;
						if (this.symbolParser.TryParseSymbolData(xmlReader.ReadString(), functionNamePool, out t))
						{
							if (text == null)
							{
								throw new XmlException("Found a <s> tag outside of a <scriptFile> section.");
							}
							t.SourceFileId += count;
							Stack<int> stack;
							if (!dictionary.TryGetValue(text, out stack))
							{
								stack = new Stack<int>();
								dictionary.Add(text, stack);
							}
							List<T> list = symbolMaps[text];
							int count2 = list.Count;
							while (stack.Count > 0)
							{
								int index = stack.Peek();
								T t2 = list[index];
								if (!AjaxMinSourceMapLoader<T>.CheckSymbolContainsAnother(t, t2))
								{
									break;
								}
								t2.ParentSymbolIndex = count2;
								list[index] = t2;
								stack.Pop();
							}
							t.ParentSymbolIndex = -1;
							stack.Push(count2);
							list.Add(t);
						}
					}
					else if (xmlReader.Name == "sourceFile")
					{
						string attribute = xmlReader.GetAttribute("id");
						if (attribute == null)
						{
							throw new XmlException("One of the source files had a null id");
						}
						sourceFileIdMap.Add(uint.Parse(attribute) + count, xmlReader.GetAttribute("path"));
					}
					else if (xmlReader.IsStartElement("scriptFile"))
					{
						string attribute2 = xmlReader.GetAttribute("path");
						if (!this.symbolParser.ExtractScriptPackageName(filePath, attribute2, out text))
						{
							throw new XmlException("Unable to determine the script package name: " + filePath);
						}
						symbolMaps[text] = new List<T>(1024);
						hashSet.Add(text);
					}
				}
			}
			foreach (string key in hashSet)
			{
				symbolMaps[key].TrimExcess();
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000832AC File Offset: 0x000814AC
		private static bool CheckSymbolContainsAnother(T parentCandidate, T symbol)
		{
			return parentCandidate.ScriptStartLine <= symbol.ScriptStartLine && (parentCandidate.ScriptStartLine != symbol.ScriptStartLine || parentCandidate.ScriptStartColumn <= symbol.ScriptStartColumn) && parentCandidate.ScriptEndLine >= symbol.ScriptEndLine && (parentCandidate.ScriptEndLine != symbol.ScriptEndLine || parentCandidate.ScriptEndColumn >= symbol.ScriptEndColumn);
		}

		// Token: 0x040013BF RID: 5055
		private const string ScriptFileTag = "scriptFile";

		// Token: 0x040013C0 RID: 5056
		private const string SourceFileTag = "sourceFile";

		// Token: 0x040013C1 RID: 5057
		private const string SymbolTag = "s";

		// Token: 0x040013C2 RID: 5058
		private const string FilePathAttribute = "path";

		// Token: 0x040013C3 RID: 5059
		private const string FileIdAttribute = "id";

		// Token: 0x040013C4 RID: 5060
		private readonly ClientWatsonSymbolParser<T> symbolParser;
	}
}

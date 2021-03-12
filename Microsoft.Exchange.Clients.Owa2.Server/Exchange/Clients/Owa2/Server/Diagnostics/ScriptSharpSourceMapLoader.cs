using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000459 RID: 1113
	internal class ScriptSharpSourceMapLoader : SourceMapLoader<ScriptSharpSymbolWrapper>
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x00087A9B File Offset: 0x00085C9B
		public ScriptSharpSourceMapLoader(IEnumerable<string> symbolMapFiles) : base(symbolMapFiles)
		{
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00087AA4 File Offset: 0x00085CA4
		protected override void LoadSymbolsMapFromFile(string filePath, Dictionary<string, List<ScriptSharpSymbolWrapper>> symbolMaps, Dictionary<uint, string> sourceFileIdMap, ClientWatsonFunctionNamePool functionNamePool)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<int, List<ScriptSharpSymbolWrapper>> dictionary2 = new Dictionary<int, List<ScriptSharpSymbolWrapper>>();
			Stack<ScriptSharpSourceMapLoader.ContainingMethodContext> stack = new Stack<ScriptSharpSourceMapLoader.ContainingMethodContext>();
			Stack<ScriptSharpSourceMapLoader.ParentContext> stack2 = new Stack<ScriptSharpSourceMapLoader.ParentContext>();
			string arg = null;
			string arg2 = null;
			int num = -1;
			int key = -1;
			int count = sourceFileIdMap.Count;
			using (XmlReader xmlReader = XmlReader.Create(filePath))
			{
				while (xmlReader.Read())
				{
					XmlNodeType xmlNodeType = xmlReader.MoveToContent();
					if (xmlNodeType == XmlNodeType.Element)
					{
						if (xmlReader.Name == "Statement")
						{
							ScriptSharpSourceMapLoader.ContainingMethodContext containingMethodContext = stack.Peek();
							ScriptSharpSymbol scriptSharpSymbol;
							if (ScriptSharpSourceMapLoader.TryParseSymbol(xmlReader, containingMethodContext.ScriptOffset, containingMethodContext.FunctionNameIndex, num, out scriptSharpSymbol))
							{
								ScriptSharpSourceMapLoader.PopNonParentSymbolsOut(stack2, scriptSharpSymbol, dictionary2[key]);
								stack2.Push(new ScriptSharpSourceMapLoader.ParentContext(scriptSharpSymbol));
							}
						}
						else if (xmlReader.Name == "Method")
						{
							string attribute = xmlReader.GetAttribute("SourceFile");
							if (string.IsNullOrEmpty(attribute))
							{
								ScriptSharpSourceMapLoader.SkipSubTree(xmlReader, "Method");
							}
							else
							{
								if (!dictionary.TryGetValue(attribute, out num))
								{
									num = dictionary.Count + count;
									dictionary.Add(attribute, num);
								}
								arg2 = xmlReader.GetAttribute("Name");
								string name = string.Format("{0}.{1}", arg, arg2);
								int orAddFunctionNameIndex = functionNamePool.GetOrAddFunctionNameIndex(name);
								ScriptSharpSymbol symbol;
								if (!ScriptSharpSourceMapLoader.TryParseSymbol(xmlReader, 0, orAddFunctionNameIndex, num, out symbol))
								{
									ScriptSharpSourceMapLoader.SkipSubTree(xmlReader, "Method");
								}
								else if (!xmlReader.IsEmptyElement)
								{
									stack.Push(new ScriptSharpSourceMapLoader.ContainingMethodContext(symbol.ScriptStartPosition, orAddFunctionNameIndex));
									stack2.Push(new ScriptSharpSourceMapLoader.ParentContext(symbol, true));
								}
								else
								{
									dictionary2[key].Add(new ScriptSharpSymbolWrapper(symbol));
								}
							}
						}
						else if (xmlReader.Name == "AnonymousMethod")
						{
							ScriptSharpSourceMapLoader.ContainingMethodContext containingMethodContext2 = stack.Peek();
							string name2 = string.Format("{0}.<{1}>anonymous", arg, arg2);
							int orAddFunctionNameIndex2 = functionNamePool.GetOrAddFunctionNameIndex(name2);
							ScriptSharpSymbol scriptSharpSymbol2;
							if (!ScriptSharpSourceMapLoader.TryParseSymbol(xmlReader, containingMethodContext2.ScriptOffset, orAddFunctionNameIndex2, num, out scriptSharpSymbol2))
							{
								ScriptSharpSourceMapLoader.SkipSubTree(xmlReader, "AnonymousMethod");
							}
							else
							{
								List<ScriptSharpSymbolWrapper> list = dictionary2[key];
								ScriptSharpSourceMapLoader.ParentContext parentContext = ScriptSharpSourceMapLoader.PopNonParentSymbolsOut(stack2, scriptSharpSymbol2, list);
								if (!xmlReader.IsEmptyElement)
								{
									stack.Push(new ScriptSharpSourceMapLoader.ContainingMethodContext(scriptSharpSymbol2.ScriptStartPosition, orAddFunctionNameIndex2));
									stack2.Push(new ScriptSharpSourceMapLoader.ParentContext(scriptSharpSymbol2, true));
								}
								else
								{
									parentContext.Children.Add(list.Count);
									list.Add(new ScriptSharpSymbolWrapper(scriptSharpSymbol2));
								}
							}
						}
						else if (xmlReader.Name == "Type")
						{
							arg = xmlReader.GetAttribute("Name");
							string attribute2 = xmlReader.GetAttribute("SegmentRef");
							if (string.IsNullOrEmpty(attribute2) || !int.TryParse(attribute2, out key))
							{
								ScriptSharpSourceMapLoader.SkipSubTree(xmlReader, "Type");
							}
							else if (!dictionary2.ContainsKey(key))
							{
								dictionary2.Add(key, new List<ScriptSharpSymbolWrapper>(1024));
							}
						}
						else if (xmlReader.Name == "Segments")
						{
							IL_3CE:
							while (xmlReader.Read())
							{
								if (xmlReader.IsStartElement("Segment"))
								{
									string attribute3 = xmlReader.GetAttribute("Id");
									int key2;
									List<ScriptSharpSymbolWrapper> list2;
									if (int.TryParse(attribute3, out key2) && dictionary2.TryGetValue(key2, out list2))
									{
										string packageName = ScriptSharpSourceMapLoader.GetPackageName(xmlReader, filePath);
										try
										{
											symbolMaps.Add(packageName, list2);
											list2.TrimExcess();
										}
										catch (ArgumentException exception)
										{
											string extraData = string.Format("Package: {0}, File name: {1}", packageName, filePath);
											ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps, extraData);
										}
									}
								}
							}
							goto IL_3E5;
						}
					}
					else if (xmlNodeType == XmlNodeType.EndElement && (xmlReader.Name == "Method" || xmlReader.Name == "AnonymousMethod"))
					{
						stack.Pop();
						List<ScriptSharpSymbolWrapper> symbolList = dictionary2[key];
						bool flag = false;
						while (stack2.Count > 0 && !flag)
						{
							ScriptSharpSourceMapLoader.ParentContext parentContext2 = ScriptSharpSourceMapLoader.PopItemAndAddToList(stack2, symbolList);
							flag = parentContext2.IsMethod;
						}
					}
				}
				goto IL_3CE;
			}
			IL_3E5:
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				sourceFileIdMap.Add((uint)keyValuePair.Value, keyValuePair.Key);
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00087F24 File Offset: 0x00086124
		private static string GetPackageName(XmlReader reader, string symbolFilePath)
		{
			string text = reader.GetAttribute("Package");
			string attribute = reader.GetAttribute("Slice");
			if (!string.IsNullOrEmpty(attribute))
			{
				text = string.Format("{0}.{1}", text, attribute);
			}
			if (text == "**SOURCE**")
			{
				text = Regex.Replace(symbolFilePath, Regex.Escape("_obfuscate.xml"), string.Empty, RegexOptions.IgnoreCase);
			}
			return text;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00087F84 File Offset: 0x00086184
		private static ScriptSharpSourceMapLoader.ParentContext PopNonParentSymbolsOut(Stack<ScriptSharpSourceMapLoader.ParentContext> parentCandidateStack, ScriptSharpSymbol newSymbol, List<ScriptSharpSymbolWrapper> symbolList)
		{
			while (parentCandidateStack.Count > 0)
			{
				ScriptSharpSourceMapLoader.ParentContext parentContext = parentCandidateStack.Peek();
				if (parentContext.Symbol.ScriptEndPosition >= newSymbol.ScriptEndPosition)
				{
					return parentContext;
				}
				ScriptSharpSourceMapLoader.PopItemAndAddToList(parentCandidateStack, symbolList);
			}
			return null;
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00087FC8 File Offset: 0x000861C8
		private static ScriptSharpSourceMapLoader.ParentContext PopItemAndAddToList(Stack<ScriptSharpSourceMapLoader.ParentContext> parentCandidateStack, List<ScriptSharpSymbolWrapper> symbolList)
		{
			ScriptSharpSourceMapLoader.ParentContext parentContext = parentCandidateStack.Pop();
			int count = symbolList.Count;
			if (parentCandidateStack.Count > 0)
			{
				parentCandidateStack.Peek().Children.Add(count);
			}
			foreach (int index in parentContext.Children)
			{
				ScriptSharpSymbolWrapper value = symbolList[index];
				value.ParentSymbolIndex = count;
				symbolList[index] = value;
			}
			symbolList.Add(new ScriptSharpSymbolWrapper(parentContext.Symbol));
			return parentContext;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00088068 File Offset: 0x00086268
		private static void SkipSubTree(XmlReader reader, string name)
		{
			if (!reader.IsEmptyElement)
			{
				int num = 0;
				while (reader.Read())
				{
					reader.MoveToContent();
					if (reader.Name == name)
					{
						if (reader.NodeType == XmlNodeType.Element && !reader.IsEmptyElement)
						{
							num++;
						}
						else if (reader.NodeType == XmlNodeType.EndElement)
						{
							if (num == 0)
							{
								return;
							}
							num--;
						}
					}
				}
			}
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000880C8 File Offset: 0x000862C8
		private static bool TryParseSymbol(XmlReader reader, int scriptOffset, int functionNameIndex, int sourceFileId, out ScriptSharpSymbol symbol)
		{
			symbol = default(ScriptSharpSymbol);
			try
			{
				int num = int.Parse(reader.GetAttribute("ScriptStartPosition"));
				int num2 = int.Parse(reader.GetAttribute("ScriptEndPosition"));
				symbol = new ScriptSharpSymbol
				{
					ScriptStartPosition = scriptOffset + num,
					ScriptEndPosition = scriptOffset + num2,
					FunctionNameIndex = functionNameIndex,
					ParentSymbol = -1,
					SourceStartLine = int.Parse(reader.GetAttribute("SourceStartLine")),
					SourceFileId = (uint)sourceFileId
				};
			}
			catch (ArgumentNullException)
			{
				return false;
			}
			catch (OverflowException)
			{
				return false;
			}
			catch (FormatException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400158B RID: 5515
		public const string ScriptSharpMapFilePattern = "*_obfuscate.xml";

		// Token: 0x0400158C RID: 5516
		private const string ScriptSharpMapFileSuffix = "_obfuscate.xml";

		// Token: 0x0400158D RID: 5517
		private const string MethodNameFormat = "{0}.{1}";

		// Token: 0x0400158E RID: 5518
		private const string AnonymousMethodNameFormat = "{0}.<{1}>anonymous";

		// Token: 0x0400158F RID: 5519
		private const string GenericPackageName = "**SOURCE**";

		// Token: 0x0200045A RID: 1114
		private class ContainingMethodContext
		{
			// Token: 0x170009D9 RID: 2521
			// (get) Token: 0x06002571 RID: 9585 RVA: 0x00088190 File Offset: 0x00086390
			// (set) Token: 0x06002572 RID: 9586 RVA: 0x00088198 File Offset: 0x00086398
			public int ScriptOffset { get; private set; }

			// Token: 0x170009DA RID: 2522
			// (get) Token: 0x06002573 RID: 9587 RVA: 0x000881A1 File Offset: 0x000863A1
			// (set) Token: 0x06002574 RID: 9588 RVA: 0x000881A9 File Offset: 0x000863A9
			public int FunctionNameIndex { get; private set; }

			// Token: 0x06002575 RID: 9589 RVA: 0x000881B2 File Offset: 0x000863B2
			public ContainingMethodContext(int scriptOffset, int functionNameIndex)
			{
				this.ScriptOffset = scriptOffset;
				this.FunctionNameIndex = functionNameIndex;
			}
		}

		// Token: 0x0200045B RID: 1115
		private class ParentContext
		{
			// Token: 0x170009DB RID: 2523
			// (get) Token: 0x06002576 RID: 9590 RVA: 0x000881C8 File Offset: 0x000863C8
			// (set) Token: 0x06002577 RID: 9591 RVA: 0x000881D0 File Offset: 0x000863D0
			public ScriptSharpSymbol Symbol { get; private set; }

			// Token: 0x170009DC RID: 2524
			// (get) Token: 0x06002578 RID: 9592 RVA: 0x000881D9 File Offset: 0x000863D9
			// (set) Token: 0x06002579 RID: 9593 RVA: 0x000881E1 File Offset: 0x000863E1
			public bool IsMethod { get; private set; }

			// Token: 0x170009DD RID: 2525
			// (get) Token: 0x0600257A RID: 9594 RVA: 0x000881EA File Offset: 0x000863EA
			// (set) Token: 0x0600257B RID: 9595 RVA: 0x000881F2 File Offset: 0x000863F2
			public List<int> Children { get; private set; }

			// Token: 0x0600257C RID: 9596 RVA: 0x000881FB File Offset: 0x000863FB
			public ParentContext(ScriptSharpSymbol symbol) : this(symbol, false)
			{
			}

			// Token: 0x0600257D RID: 9597 RVA: 0x00088205 File Offset: 0x00086405
			public ParentContext(ScriptSharpSymbol symbol, bool isMethod)
			{
				this.Symbol = symbol;
				this.IsMethod = isMethod;
				this.Children = new List<int>();
			}
		}

		// Token: 0x0200045C RID: 1116
		private static class XmlMapNames
		{
			// Token: 0x04001595 RID: 5525
			public const string TypeNode = "Type";

			// Token: 0x04001596 RID: 5526
			public const string MethodNode = "Method";

			// Token: 0x04001597 RID: 5527
			public const string StatementNode = "Statement";

			// Token: 0x04001598 RID: 5528
			public const string AnonymousMethodNode = "AnonymousMethod";

			// Token: 0x04001599 RID: 5529
			public const string SegmentsGroupNode = "Segments";

			// Token: 0x0400159A RID: 5530
			public const string SegmentNode = "Segment";

			// Token: 0x0400159B RID: 5531
			public const string SegmentRefAttribute = "SegmentRef";

			// Token: 0x0400159C RID: 5532
			public const string NameAttribute = "Name";

			// Token: 0x0400159D RID: 5533
			public const string SourceFileAttribute = "SourceFile";

			// Token: 0x0400159E RID: 5534
			public const string ScriptStartPositionAttribute = "ScriptStartPosition";

			// Token: 0x0400159F RID: 5535
			public const string ScriptEndPositionAttribute = "ScriptEndPosition";

			// Token: 0x040015A0 RID: 5536
			public const string SourceStartLineAttribute = "SourceStartLine";

			// Token: 0x040015A1 RID: 5537
			public const string SourceStartColumnAttribute = "SourceStartCharacter";

			// Token: 0x040015A2 RID: 5538
			public const string SourceEndLineAttribute = "SourceEndLine";

			// Token: 0x040015A3 RID: 5539
			public const string SourceEndColumnAttribute = "SourceEndCharacter";

			// Token: 0x040015A4 RID: 5540
			public const string IdAttribute = "Id";

			// Token: 0x040015A5 RID: 5541
			public const string PackageAttribute = "Package";

			// Token: 0x040015A6 RID: 5542
			public const string SliceAttribute = "Slice";
		}
	}
}

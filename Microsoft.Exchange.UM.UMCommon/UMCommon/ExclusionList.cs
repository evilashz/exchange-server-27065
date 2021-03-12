using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000096 RID: 150
	internal class ExclusionList
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x000145E8 File Offset: 0x000127E8
		private ExclusionList(string file)
		{
			ExclusionList.DebugTrace("ExclusionList::C'tor filename={0}", new object[]
			{
				file
			});
			this.pc = new ExclusionList.PatternCollection(file);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00014620 File Offset: 0x00012820
		internal static ExclusionList Instance
		{
			get
			{
				if (ExclusionList.exclusionListInstance == null)
				{
					lock (ExclusionList.locObj)
					{
						if (ExclusionList.exclusionListInstance == null)
						{
							ExclusionList.exclusionListInstance = ExclusionList.GetExclusionList();
						}
					}
				}
				return ExclusionList.exclusionListInstance;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00014678 File Offset: 0x00012878
		private static ExclusionList GetExclusionList()
		{
			ExclusionList result = null;
			string exchangeBinPath = Utils.GetExchangeBinPath();
			string text = Path.Combine(exchangeBinPath, "SpeechGrammarFilterList.xml");
			if (!File.Exists(text))
			{
				ExclusionList.ErrorTrace("Exclusion list specified file {0} doesnt exist", new object[]
				{
					text
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechGrammarFilterListInvalidWarning, null, new object[]
				{
					Strings.FileNotFound(text)
				});
			}
			else
			{
				try
				{
					ExclusionList.DebugTrace("ExclusionList::TryGetExclusionList() Initializing exclusion list file '{0}'", new object[]
					{
						text
					});
					result = new ExclusionList(text);
					ExclusionList.DebugTrace("ExclusionList::TryGetExclusionList() Exclusionlist file '{0}' initialized successfully", new object[]
					{
						text
					});
				}
				catch (IOException ex)
				{
					ExclusionList.ErrorTrace("ExclusionList::TryGetExclusionList() Error building exclusionlist file. Grammar generation will be aborted. Exception = {0}", new object[]
					{
						ex
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechGrammarFilterListInvalidWarning, null, new object[]
					{
						ex.Message
					});
				}
				catch (XmlSchemaValidationException ex2)
				{
					ExclusionList.ErrorTrace("ExclusionList::TryGetExclusionList() Error building exclusionlist file. Grammar generation will be aborted. Exception = {0}", new object[]
					{
						ex2
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechGrammarFilterListSchemaFailureWarning, null, new object[]
					{
						ex2.LineNumber,
						ex2.LinePosition,
						ex2.Message
					});
				}
				catch (InvalidSpeechGrammarFilterListException ex3)
				{
					ExclusionList.ErrorTrace("ExclusionList::TryGetExclusionList() Error building exclusionlist file. Grammar generation will be aborted. Exception = {0}", new object[]
					{
						ex3
					});
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SpeechGrammarFilterListInvalidWarning, null, new object[]
					{
						ex3.Message
					});
				}
				catch (Exception ex4)
				{
					throw new ExclusionListException(ex4.Message, ex4);
				}
			}
			return result;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001485C File Offset: 0x00012A5C
		private static void DebugTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, formatString, formatObjects);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001486B File Offset: 0x00012A6B
		private static void ErrorTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceError(ExTraceGlobals.UMGrammarGeneratorTracer, null, formatString, formatObjects);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001487C File Offset: 0x00012A7C
		internal MatchResult GetReplacementStrings(string name, RecipientType recipientType, out List<Replacement> replacementStrings)
		{
			replacementStrings = null;
			List<Replacement> list = new List<Replacement>();
			string className = recipientType.ToString();
			bool flag = false;
			List<ExclusionList.Pattern> list2 = null;
			if (!this.pc.GetPattern(className, out list2))
			{
				return MatchResult.NotFound;
			}
			MatchResult result = MatchResult.NotFound;
			foreach (ExclusionList.Pattern pattern in list2)
			{
				Regex regex = pattern.Regex;
				if (flag = regex.IsMatch(name))
				{
					using (List<ExclusionList.PatternReplacement>.Enumerator enumerator2 = pattern.Replacements.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ExclusionList.PatternReplacement patternReplacement = enumerator2.Current;
							string text = regex.Replace(name, patternReplacement.ReplacementString);
							if (text.Length > 0)
							{
								result = MatchResult.MatchWithReplacements;
								list.Add(new Replacement(text, patternReplacement.ShouldNormalize));
							}
							else
							{
								result = MatchResult.MatchWithNoReplacements;
							}
						}
						break;
					}
				}
			}
			if (!flag)
			{
				result = MatchResult.NoMatch;
			}
			replacementStrings = list;
			return result;
		}

		// Token: 0x0400033A RID: 826
		private const string ExclusionListFileName = "SpeechGrammarFilterList.xml";

		// Token: 0x0400033B RID: 827
		private const string GrammarFilterSchemaFile = "namemap.xsd";

		// Token: 0x0400033C RID: 828
		private static ExclusionList exclusionListInstance;

		// Token: 0x0400033D RID: 829
		private static object locObj = new object();

		// Token: 0x0400033E RID: 830
		private ExclusionList.PatternCollection pc;

		// Token: 0x02000097 RID: 151
		internal class PatternCollection
		{
			// Token: 0x06000544 RID: 1348 RVA: 0x0001499C File Offset: 0x00012B9C
			public PatternCollection(string fileName)
			{
				this.Parse(fileName);
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x000149B6 File Offset: 0x00012BB6
			public bool GetPattern(string className, out List<ExclusionList.Pattern> patterns)
			{
				return this.patternMap.TryGetValue(className, out patterns);
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x000149C8 File Offset: 0x00012BC8
			private static ExclusionList.Pattern ParsePatternNode(XmlNode root)
			{
				string text = null;
				List<ExclusionList.PatternReplacement> list = null;
				ExclusionList.Pattern pattern = null;
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!(xmlNode is XmlComment) && xmlNode.NodeType == XmlNodeType.Element)
					{
						if (string.Compare(xmlNode.Name, "Input", StringComparison.Ordinal) == 0)
						{
							text = xmlNode.InnerText;
						}
						else if (string.Compare(xmlNode.Name, "Output", StringComparison.Ordinal) == 0)
						{
							if (list == null)
							{
								list = new List<ExclusionList.PatternReplacement>();
							}
							string innerText = xmlNode.InnerText;
							bool shouldNormalize = true;
							XmlAttributeCollection attributes = xmlNode.Attributes;
							if (attributes != null)
							{
								foreach (object obj2 in attributes)
								{
									XmlAttribute xmlAttribute = (XmlAttribute)obj2;
									if (string.Compare(xmlAttribute.Name, "tn", StringComparison.Ordinal) == 0)
									{
										try
										{
											shouldNormalize = bool.Parse(xmlAttribute.Value);
										}
										catch (ArgumentNullException)
										{
										}
										catch (FormatException)
										{
										}
									}
								}
							}
							ExclusionList.PatternReplacement patternReplacement = new ExclusionList.PatternReplacement(innerText, shouldNormalize);
							if (list.Contains(patternReplacement))
							{
								throw new InvalidSpeechGrammarFilterListException(Strings.DuplicateReplacementStringError(patternReplacement.ReplacementString));
							}
							list.Add(patternReplacement);
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					pattern = new ExclusionList.Pattern();
					pattern.RegexString = text;
					foreach (ExclusionList.PatternReplacement replacement in list)
					{
						pattern.AddReplacement(replacement);
					}
				}
				return pattern;
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x00014BE4 File Offset: 0x00012DE4
			private void Parse(string filename)
			{
				Stream stream = null;
				XmlReader xmlReader = null;
				try
				{
					XmlDocument xmlDocument = new SafeXmlDocument();
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					stream = executingAssembly.GetManifestResourceStream("namemap.xsd");
					XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
					xmlSchemaSet.Add(null, SafeXmlFactory.CreateSafeXmlTextReader(stream));
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
					xmlReaderSettings.ValidationType = ValidationType.Schema;
					xmlReaderSettings.Schemas.Add(xmlSchemaSet);
					xmlReader = XmlReader.Create(filename, xmlReaderSettings);
					xmlDocument.Load(xmlReader);
					XmlNode documentElement = xmlDocument.DocumentElement;
					foreach (object obj in documentElement)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (!(xmlNode is XmlComment) && string.Compare(xmlNode.Name, "Rules", StringComparison.Ordinal) == 0)
						{
							this.ParseRulesNode(xmlNode);
						}
					}
				}
				finally
				{
					if (xmlReader != null)
					{
						xmlReader.Close();
					}
					if (stream != null)
					{
						stream.Close();
					}
				}
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x00014CC4 File Offset: 0x00012EC4
			private void ParseRulesNode(XmlNode root)
			{
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!(xmlNode is XmlComment) && xmlNode.NodeType == XmlNodeType.Element && string.Compare(xmlNode.Name, "Rule", StringComparison.Ordinal) == 0)
					{
						List<ExclusionList.Pattern> list = new List<ExclusionList.Pattern>();
						List<string> list2 = new List<string>();
						this.ParseRuleNode(xmlNode, list, list2);
						foreach (string key in list2)
						{
							this.patternMap[key] = list;
						}
					}
				}
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x00014DA4 File Offset: 0x00012FA4
			private void ParseRuleNode(XmlNode root, List<ExclusionList.Pattern> patterns, List<string> appliesTo)
			{
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!(xmlNode is XmlComment) && xmlNode.NodeType == XmlNodeType.Element)
					{
						if (string.Compare(xmlNode.Name, "Patterns", StringComparison.Ordinal) == 0)
						{
							this.ParsePatternsNode(xmlNode, patterns);
						}
						else if (string.Compare(xmlNode.Name, "Class", StringComparison.Ordinal) == 0)
						{
							string innerText = xmlNode.InnerText;
							if (appliesTo.Contains(innerText))
							{
								throw new InvalidSpeechGrammarFilterListException(Strings.DuplicateClassNameError(innerText));
							}
							List<ExclusionList.Pattern> list = null;
							if (this.patternMap.TryGetValue(innerText, out list))
							{
								throw new InvalidSpeechGrammarFilterListException(Strings.DuplicateClassNameError(innerText));
							}
							appliesTo.Add(innerText);
						}
					}
				}
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x00014E84 File Offset: 0x00013084
			private void ParsePatternsNode(XmlNode root, List<ExclusionList.Pattern> patterns)
			{
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!(xmlNode is XmlComment) && xmlNode.NodeType == XmlNodeType.Element && string.Compare(xmlNode.Name, "Pattern", StringComparison.Ordinal) == 0)
					{
						ExclusionList.Pattern pattern = ExclusionList.PatternCollection.ParsePatternNode(xmlNode);
						if (pattern != null)
						{
							patterns.Add(pattern);
						}
					}
				}
			}

			// Token: 0x0400033F RID: 831
			private Dictionary<string, List<ExclusionList.Pattern>> patternMap = new Dictionary<string, List<ExclusionList.Pattern>>();
		}

		// Token: 0x02000098 RID: 152
		internal class Pattern
		{
			// Token: 0x0600054B RID: 1355 RVA: 0x00014F0C File Offset: 0x0001310C
			internal Pattern()
			{
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600054C RID: 1356 RVA: 0x00014F1F File Offset: 0x0001311F
			// (set) Token: 0x0600054D RID: 1357 RVA: 0x00014F27 File Offset: 0x00013127
			internal string RegexString
			{
				get
				{
					return this.regexString;
				}
				set
				{
					this.regexString = value;
					this.regex = new Regex(this.regexString, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600054E RID: 1358 RVA: 0x00014F46 File Offset: 0x00013146
			internal Regex Regex
			{
				get
				{
					return this.regex;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x0600054F RID: 1359 RVA: 0x00014F4E File Offset: 0x0001314E
			internal List<ExclusionList.PatternReplacement> Replacements
			{
				get
				{
					return this.replacements;
				}
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x00014F56 File Offset: 0x00013156
			internal void AddReplacement(ExclusionList.PatternReplacement replacement)
			{
				this.replacements.Add(replacement);
			}

			// Token: 0x04000340 RID: 832
			private string regexString;

			// Token: 0x04000341 RID: 833
			private Regex regex;

			// Token: 0x04000342 RID: 834
			private List<ExclusionList.PatternReplacement> replacements = new List<ExclusionList.PatternReplacement>();
		}

		// Token: 0x02000099 RID: 153
		internal class PatternReplacement : IEquatable<ExclusionList.PatternReplacement>
		{
			// Token: 0x06000551 RID: 1361 RVA: 0x00014F64 File Offset: 0x00013164
			public PatternReplacement(string replacementString, bool shouldNormalize)
			{
				this.replacementString = replacementString;
				this.shouldNormalize = shouldNormalize;
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000552 RID: 1362 RVA: 0x00014F81 File Offset: 0x00013181
			public string ReplacementString
			{
				get
				{
					return this.replacementString;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x00014F89 File Offset: 0x00013189
			public bool ShouldNormalize
			{
				get
				{
					return this.shouldNormalize;
				}
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x00014F91 File Offset: 0x00013191
			public bool Equals(ExclusionList.PatternReplacement patternReplacement)
			{
				return string.Compare(this.replacementString, patternReplacement.ReplacementString, StringComparison.Ordinal) == 0;
			}

			// Token: 0x04000343 RID: 835
			private readonly string replacementString;

			// Token: 0x04000344 RID: 836
			private readonly bool shouldNormalize = true;
		}
	}
}

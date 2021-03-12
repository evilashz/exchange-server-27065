using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Microsoft.Ceres.DataLossPrevention
{
	// Token: 0x02000003 RID: 3
	internal class ClassificationRuleSet
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal ClassificationRuleSet(TextReader rulesXML)
		{
			XmlReader reader = XmlReader.Create(rulesXML);
			this.Load(reader);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		internal string RuleXML
		{
			get
			{
				return this.ruleXML;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
		internal string PackageId
		{
			get
			{
				return this.packageId;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002148 File Offset: 0x00000348
		internal string[] AllRuleIds
		{
			get
			{
				return this.ruleIdToCode.Keys.ToArray<string>();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		internal long? RuleIdToCode(string ruleId)
		{
			if (this.ruleIdToCode.ContainsKey(ruleId))
			{
				return new long?(this.ruleIdToCode[ruleId]);
			}
			return null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002192 File Offset: 0x00000392
		internal string RuleIdToQueryIdentifier(string ruleId)
		{
			return this.ruleIdToQueryIdentifier[ruleId];
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A0 File Offset: 0x000003A0
		internal string RuleIdToRuleName(string ruleId)
		{
			if (this.ruleIdToRuleName.ContainsKey(ruleId))
			{
				return this.ruleIdToRuleName[ruleId];
			}
			return null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021BE File Offset: 0x000003BE
		internal string RuleNameToRuleId(string ruleName)
		{
			if (this.ruleNameToRuleId.ContainsKey(ruleName))
			{
				return this.ruleNameToRuleId[ruleName];
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021DC File Offset: 0x000003DC
		private void Load(XmlReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = false
			}))
			{
				xmlWriter.WriteStartDocument();
				while (this.HandleRead(reader, xmlWriter))
				{
				}
				xmlWriter.WriteEndDocument();
				xmlWriter.Flush();
			}
			this.ruleXML = stringBuilder.ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002248 File Offset: 0x00000448
		private bool HandleRead(XmlReader reader, XmlWriter xw)
		{
			if (!reader.Read())
			{
				return false;
			}
			XmlNodeType nodeType = reader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				this.ProcessElement(reader);
				ClassificationRuleSet.WriteElement(reader, xw);
				break;
			case XmlNodeType.Attribute:
				break;
			case XmlNodeType.Text:
				this.ProcessTextNode(reader);
				ClassificationRuleSet.WriteTextNode(reader, xw);
				break;
			default:
				if (nodeType == XmlNodeType.EndElement)
				{
					this.ProcessEndElement(reader);
					ClassificationRuleSet.WriteEndElement(xw);
				}
				break;
			}
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022B0 File Offset: 0x000004B0
		private static void WriteElement(XmlReader reader, XmlWriter xw)
		{
			xw.WriteStartElement(reader.LocalName, reader.NamespaceURI);
			if (reader.HasAttributes && reader.MoveToFirstAttribute())
			{
				do
				{
					if (!"xmlns".Equals(reader.LocalName))
					{
						xw.WriteAttributeString(reader.LocalName, reader.Value);
					}
				}
				while (reader.MoveToNextAttribute());
				reader.MoveToElement();
			}
			if (reader.IsEmptyElement)
			{
				xw.WriteEndElement();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002320 File Offset: 0x00000520
		private static void WriteEndElement(XmlWriter xw)
		{
			xw.WriteEndElement();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002328 File Offset: 0x00000528
		private static void WriteTextNode(XmlReader reader, XmlWriter xw)
		{
			xw.WriteValue(reader.Value);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002338 File Offset: 0x00000538
		private void ProcessElement(XmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (!(localName == "RulePack"))
				{
					if (!(localName == "Entity"))
					{
						if (!(localName == "Resource"))
						{
							if (localName == "Name")
							{
								if (this.currentResource != null && reader.GetAttribute("default") != null)
								{
									this.isRuleName = true;
								}
							}
						}
						else
						{
							this.currentResource = reader.GetAttribute("idRef");
						}
					}
					else
					{
						string attribute = reader.GetAttribute("id");
						if (!string.IsNullOrEmpty(attribute))
						{
							if (this.ruleIdToCode.ContainsKey(attribute))
							{
								throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Rule XML Contains duplicate id : [{0}]", new object[]
								{
									attribute
								}));
							}
							long num = ClassificationRuleSet.CreateGuidHash(new Guid(attribute));
							if (this.codeToRuleId.ContainsKey(num))
							{
								throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Rule XML Contains rules ids with duplicate hash : [{0}]", new object[]
								{
									num
								}));
							}
							this.ruleIdToCode[attribute] = num;
							this.codeToRuleId[num] = attribute;
						}
					}
				}
				else
				{
					this.packageId = reader.GetAttribute("id");
				}
			}
			if (!reader.IsEmptyElement)
			{
				this.parseStack.Push(reader.LocalName);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002494 File Offset: 0x00000694
		private void ProcessEndElement(XmlReader reader)
		{
			if (this.parseStack.Count > 0)
			{
				if (!this.parseStack.Peek().Equals(reader.LocalName))
				{
					throw new InvalidDataException("Invalid XML");
				}
				string text = this.parseStack.Pop();
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Resource"))
					{
						return;
					}
					this.currentResource = null;
					return;
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024FC File Offset: 0x000006FC
		private void ProcessTextNode(XmlReader reader)
		{
			if (this.isRuleName)
			{
				string value = reader.Value;
				string value2 = ClassificationRuleSet.RemoveUnsafeCharacters(value);
				if (this.ruleNameToRuleId.ContainsKey(value))
				{
					throw new InvalidDataException(string.Format(CultureInfo.InvariantCulture, "Rule XML Contains rules with duplicate names : [{0}]", new object[]
					{
						value
					}));
				}
				this.ruleIdToRuleName[this.currentResource] = value;
				this.ruleNameToRuleId[value] = this.currentResource;
				this.ruleIdToQueryIdentifier[this.currentResource] = value2;
				this.isRuleName = false;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000258C File Offset: 0x0000078C
		private static string RemoveUnsafeCharacters(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				char c2 = c;
				if (c2 != ' ')
				{
					switch (c2)
					{
					case '\'':
					case '(':
					case ')':
					case '.':
					case '/':
						break;
					default:
						stringBuilder.Append(c);
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002600 File Offset: 0x00000800
		private static long CreateGuidHash(Guid id)
		{
			byte[] array = id.ToByteArray();
			uint num = 2166136261U;
			for (int i = 0; i < array.Length; i++)
			{
				num = (num ^ (uint)array[i]) * 16777619U;
			}
			long num2 = (long)((ulong)(num >> 1));
			return num2 << 32;
		}

		// Token: 0x04000008 RID: 8
		private const uint FnvPrime32 = 16777619U;

		// Token: 0x04000009 RID: 9
		private Stack<string> parseStack = new Stack<string>();

		// Token: 0x0400000A RID: 10
		private string currentResource;

		// Token: 0x0400000B RID: 11
		private bool isRuleName;

		// Token: 0x0400000C RID: 12
		private string ruleXML;

		// Token: 0x0400000D RID: 13
		private string packageId;

		// Token: 0x0400000E RID: 14
		private Dictionary<string, long> ruleIdToCode = new Dictionary<string, long>();

		// Token: 0x0400000F RID: 15
		private Dictionary<long, string> codeToRuleId = new Dictionary<long, string>();

		// Token: 0x04000010 RID: 16
		private Dictionary<string, string> ruleIdToQueryIdentifier = new Dictionary<string, string>();

		// Token: 0x04000011 RID: 17
		private Dictionary<string, string> ruleIdToRuleName = new Dictionary<string, string>();

		// Token: 0x04000012 RID: 18
		private Dictionary<string, string> ruleNameToRuleId = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}

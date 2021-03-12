using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200000F RID: 15
	public sealed class InMemoryClassificationRuleStore : IClassificationRuleStore, IRulePackageLoader
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002898 File Offset: 0x00000A98
		private InMemoryClassificationRuleStore()
		{
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(InMemoryClassificationRuleStore.classificationDefXmlResourceId))
			{
				using (XmlReader xmlReader = XmlReader.Create(manifestResourceStream))
				{
					this.ParseRuleDefinitionXml(xmlReader);
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002914 File Offset: 0x00000B14
		public static IClassificationRuleStore GetInstance()
		{
			return InMemoryClassificationRuleStore.instance;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000291C File Offset: 0x00000B1C
		public RULE_PACKAGE_DETAILS[] GetRulePackageDetails(IClassificationItem classificationItem = null)
		{
			return new RULE_PACKAGE_DETAILS[]
			{
				this.packageDetailsList
			};
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000297C File Offset: 0x00000B7C
		public RuleDefinitionDetails GetRuleDetails(string ruleId, string localeName = null)
		{
			RuleDefinitionDetails ruleDefinitionDetails = ClassificationUtils.ValidateRuleId(ruleId, this.ruleDefinitionDetailsTable);
			if (ruleDefinitionDetails == null)
			{
				throw new ClassificationRuleStorePermanentException(string.Format("rule name {0} can't be found!", ruleId), null, true);
			}
			if (string.IsNullOrWhiteSpace(localeName))
			{
				return ruleDefinitionDetails;
			}
			string text = ruleDefinitionDetails.LocalizableDetails.Keys.FirstOrDefault((string p) => p.Equals(localeName, StringComparison.InvariantCultureIgnoreCase));
			if (text == null)
			{
				text = ruleDefinitionDetails.LocalizableDetails.Keys.FirstOrDefault((string p) => p.StartsWith(localeName, StringComparison.InvariantCultureIgnoreCase) || localeName.StartsWith(p, StringComparison.InvariantCultureIgnoreCase));
			}
			if (text != null)
			{
				ruleDefinitionDetails.LocalizableDetails = new Dictionary<string, CLASSIFICATION_DEFINITION_DETAILS>
				{
					{
						text,
						ruleDefinitionDetails.LocalizableDetails[text]
					}
				};
			}
			else
			{
				ruleDefinitionDetails.LocalizableDetails = null;
			}
			return ruleDefinitionDetails;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A40 File Offset: 0x00000C40
		public IEnumerable<RuleDefinitionDetails> GetAllRuleDetails(bool loadLocalizableData = false)
		{
			List<RuleDefinitionDetails> list = new List<RuleDefinitionDetails>();
			if (this.ruleDefinitionDetailsTable.Any<KeyValuePair<Guid, RuleDefinitionDetails>>())
			{
				foreach (RuleDefinitionDetails ruleDefinitionDetails in this.ruleDefinitionDetailsTable.Values)
				{
					RuleDefinitionDetails ruleDefinitionDetails2 = ruleDefinitionDetails.Clone();
					if (!loadLocalizableData)
					{
						ruleDefinitionDetails2.LocalizableDetails = null;
					}
					list.Add(ruleDefinitionDetails2);
				}
			}
			return list;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public void GetRulePackages(uint ulRulePackageRequestDetailsSize, RULE_PACKAGE_REQUEST_DETAILS[] rulePackageRequestDetails)
		{
			throw new NotImplementedException("GetRulePackages is not implemented yet!");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void GetUpdatedRulePackageInfo(uint ulRulePackageTimestampDetailsSize, RULE_PACKAGE_TIMESTAMP_DETAILS[] rulePackageTimestampDetails)
		{
			throw new NotImplementedException("GetUpdatedRulePackageInfo is not implemented yet!");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private static string GetTextNodeValue(XmlReader reader)
		{
			while (reader.NodeType != XmlNodeType.Text)
			{
				reader.Read();
			}
			return reader.Value;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B90 File Offset: 0x00000D90
		private void ParseRuleDefinitionXml(XmlReader reader)
		{
			Dictionary<string, InMemoryClassificationRuleStore.SharedClassificationDefinitionDetails> dictionary = new Dictionary<string, InMemoryClassificationRuleStore.SharedClassificationDefinitionDetails>();
			while (reader.Read())
			{
				string localName;
				if (reader.NodeType == XmlNodeType.Element && (localName = reader.LocalName) != null)
				{
					if (!(localName == "RulePack"))
					{
						if (!(localName == "LocalizedDetails"))
						{
							if (!(localName == "Entity"))
							{
								if (localName == "Resource")
								{
									Guid key = Guid.Parse(reader.GetAttribute("idRef"));
									Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
									Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
									while (reader.LocalName != "Description")
									{
										reader.Read();
										if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Name")
										{
											string attribute = reader.GetAttribute("langcode");
											dictionary2[attribute] = InMemoryClassificationRuleStore.GetTextNodeValue(reader);
										}
									}
									while (reader.LocalName != "Resource")
									{
										if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Description")
										{
											string attribute2 = reader.GetAttribute("langcode");
											dictionary3[attribute2] = InMemoryClassificationRuleStore.GetTextNodeValue(reader);
										}
										reader.Read();
									}
									string key2 = dictionary2["en-us"];
									string text = dictionary3["en-us"];
									Dictionary<string, KeyValuePair<string, string>> dictionary4 = new Dictionary<string, KeyValuePair<string, string>>();
									using (Dictionary<string, string>.KeyCollection.Enumerator enumerator = dictionary2.Keys.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											string nameLang = enumerator.Current;
											string key3 = dictionary2[nameLang];
											string key4;
											string value;
											if (dictionary3.ContainsKey(nameLang))
											{
												key4 = nameLang;
												value = dictionary3[key4];
											}
											else
											{
												bool nameHasSubLang = false;
												string text2 = dictionary3.Keys.FirstOrDefault(delegate(string p)
												{
													if (nameLang.StartsWith(p, StringComparison.InvariantCultureIgnoreCase))
													{
														nameHasSubLang = true;
														return true;
													}
													if (p.StartsWith(nameLang, StringComparison.InvariantCultureIgnoreCase))
													{
														nameHasSubLang = false;
														return true;
													}
													return false;
												});
												if (text2 != null)
												{
													key4 = (nameHasSubLang ? nameLang : text2);
													value = dictionary3[text2];
												}
												else
												{
													key4 = nameLang;
													value = text;
												}
											}
											dictionary4[key4] = new KeyValuePair<string, string>(key3, value);
										}
									}
									using (Dictionary<string, string>.KeyCollection.Enumerator enumerator2 = dictionary3.Keys.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											string descLang = enumerator2.Current;
											if (!dictionary2.ContainsKey(descLang))
											{
												if (!dictionary2.Keys.Any((string p) => p.StartsWith(descLang, StringComparison.InvariantCultureIgnoreCase) || descLang.StartsWith(p, StringComparison.InvariantCultureIgnoreCase)))
												{
													dictionary4[descLang] = new KeyValuePair<string, string>(key2, dictionary3[descLang]);
												}
											}
										}
									}
									IDictionary<string, CLASSIFICATION_DEFINITION_DETAILS> dictionary5 = new Dictionary<string, CLASSIFICATION_DEFINITION_DETAILS>();
									using (Dictionary<string, KeyValuePair<string, string>>.KeyCollection.Enumerator enumerator3 = dictionary4.Keys.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											string lang = enumerator3.Current;
											string key5 = lang;
											if (!dictionary.ContainsKey(lang))
											{
												key5 = dictionary.Keys.FirstOrDefault((string p) => p.StartsWith(lang, StringComparison.InvariantCultureIgnoreCase) || lang.StartsWith(p, StringComparison.InvariantCultureIgnoreCase));
											}
											dictionary5[key5] = new CLASSIFICATION_DEFINITION_DETAILS
											{
												PublisherName = dictionary[key5].PublisherName,
												RulePackageName = dictionary[key5].RulePackageName,
												RulePackageDesc = dictionary[key5].RulePackageDesc,
												DefinitionName = dictionary4[lang].Key,
												Description = dictionary4[lang].Value
											};
										}
									}
									this.ruleDefinitionDetailsTable[key].LocalizableDetails = dictionary5;
								}
							}
							else
							{
								Guid guid = Guid.Parse(reader.GetAttribute("id"));
								int recommendedConfidence = int.Parse(reader.GetAttribute("recommendedConfidence"));
								this.ruleDefinitionDetailsTable[guid] = new RuleDefinitionDetails
								{
									RuleId = guid,
									RecommendedConfidence = recommendedConfidence
								};
							}
						}
						else
						{
							string attribute3 = reader.GetAttribute("langcode");
							dictionary.Add(attribute3, new InMemoryClassificationRuleStore.SharedClassificationDefinitionDetails());
							reader.ReadToFollowing("PublisherName");
							dictionary[attribute3].PublisherName = InMemoryClassificationRuleStore.GetTextNodeValue(reader);
							reader.ReadToFollowing("Name");
							dictionary[attribute3].RulePackageName = InMemoryClassificationRuleStore.GetTextNodeValue(reader);
							reader.ReadToFollowing("Description");
							dictionary[attribute3].RulePackageDesc = InMemoryClassificationRuleStore.GetTextNodeValue(reader);
						}
					}
					else
					{
						this.packageDetailsList.RulePackageID = reader.GetAttribute("id");
					}
				}
			}
			this.packageDetailsList.RuleIDs = new string[this.ruleDefinitionDetailsTable.Count];
			int num = 0;
			foreach (Guid guid2 in this.ruleDefinitionDetailsTable.Keys)
			{
				this.packageDetailsList.RuleIDs[num] = guid2.ToString();
				num++;
			}
		}

		// Token: 0x04000013 RID: 19
		private static string classificationDefXmlResourceId = "DefaultClassificationDefinitions.xml";

		// Token: 0x04000014 RID: 20
		private static InMemoryClassificationRuleStore instance = new InMemoryClassificationRuleStore();

		// Token: 0x04000015 RID: 21
		private RULE_PACKAGE_DETAILS packageDetailsList = default(RULE_PACKAGE_DETAILS);

		// Token: 0x04000016 RID: 22
		private Dictionary<Guid, RuleDefinitionDetails> ruleDefinitionDetailsTable = new Dictionary<Guid, RuleDefinitionDetails>();

		// Token: 0x02000010 RID: 16
		private class SharedClassificationDefinitionDetails
		{
			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000052 RID: 82 RVA: 0x0000314E File Offset: 0x0000134E
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00003156 File Offset: 0x00001356
			public string PublisherName { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000054 RID: 84 RVA: 0x0000315F File Offset: 0x0000135F
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00003167 File Offset: 0x00001367
			public string RulePackageName { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000056 RID: 86 RVA: 0x00003170 File Offset: 0x00001370
			// (set) Token: 0x06000057 RID: 87 RVA: 0x00003178 File Offset: 0x00001378
			public string RulePackageDesc { get; set; }
		}
	}
}

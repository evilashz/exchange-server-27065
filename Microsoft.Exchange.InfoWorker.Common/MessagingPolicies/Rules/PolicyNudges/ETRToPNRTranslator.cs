using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges
{
	// Token: 0x02000163 RID: 355
	internal class ETRToPNRTranslator
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x00029108 File Offset: 0x00027308
		internal ETRToPNRTranslator(string etrXml, ETRToPNRTranslator.IMessageStrings messageStrings, ETRToPNRTranslator.IDistributionListResolver distributionListResolver = null, ETRToPNRTranslator.IDataClassificationResolver dataClassificationResolver = null)
		{
			this.etrXml = etrXml;
			this.messageStrings = messageStrings;
			this.distributionListResolver = distributionListResolver;
			this.dataClassificationResolver = dataClassificationResolver;
			this.fullPnrXml = new Lazy<string>(new Func<string>(this.TryTransform));
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00029161 File Offset: 0x00027361
		internal RuleState Enabled
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidOperationException();
				}
				return this.enabled;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00029177 File Offset: 0x00027377
		internal DateTime? ActivationDate
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidOperationException();
				}
				return this.activationDate;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002918D File Offset: 0x0002738D
		internal DateTime? ExpiryDate
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidOperationException();
				}
				return this.expiryDate;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x000291A3 File Offset: 0x000273A3
		internal string PnrXml
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidOperationException();
				}
				return this.pnrXml;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000291B9 File Offset: 0x000273B9
		internal string FullPnrXml
		{
			get
			{
				if (!this.IsValid)
				{
					throw new InvalidOperationException();
				}
				return this.fullPnrXml.Value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x000291D4 File Offset: 0x000273D4
		internal bool IsValid
		{
			get
			{
				return this.fullPnrXml.Value != null;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000291E8 File Offset: 0x000273E8
		private string TryTransform()
		{
			if (string.IsNullOrEmpty(this.etrXml))
			{
				return null;
			}
			XDocument xdocument;
			try
			{
				xdocument = ETRToPNRTranslator.CreateXDocument(this.etrXml);
			}
			catch (XmlException)
			{
				return null;
			}
			XElement xelement = this.TransformRule(xdocument.Root);
			if (xelement == null || !this.hasSenderNotifyAction)
			{
				return null;
			}
			xdocument = new XDocument(new object[]
			{
				xelement
			});
			ETRToPNRTranslator.OptimizePredicates(xdocument);
			if (this.IsFalseRootCondition(xdocument))
			{
				return null;
			}
			string result = xdocument.Root.ToString();
			this.pnrXml = result;
			if (this.RemoveVersionedDataClassifications(xdocument))
			{
				ETRToPNRTranslator.OptimizePredicates(xdocument);
				this.pnrXml = (this.IsFalseRootCondition(xdocument) ? null : xdocument.Root.ToString());
			}
			return result;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000292AC File Offset: 0x000274AC
		private bool IsFalseRootCondition(XDocument xDocument)
		{
			XElement xelement = xDocument.Descendants("false").FirstOrDefault<XElement>();
			return xelement != null && xelement.Parent != null && xelement.Parent.Name.LocalName == "condition";
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002959C File Offset: 0x0002779C
		private bool RemoveVersionedDataClassifications(XDocument xDocument)
		{
			bool result = false;
			List<XElement> list = (from dataClassificationElement in xDocument.Descendants("classification")
			let id = dataClassificationElement.Attribute("id").Value
			let rulePackageId = dataClassificationElement.Attribute("rulePackId").Value
			where this.dataClassificationResolver != null && this.dataClassificationResolver.IsVersionedDataClassification(id, rulePackageId)
			select dataClassificationElement).ToList<XElement>();
			foreach (XElement xelement in list)
			{
				result = true;
				xelement.ReplaceWith(new XElement("false"));
			}
			return result;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002968C File Offset: 0x0002788C
		private bool CheckElement(XElement element, string name)
		{
			return element != null && element.Name.LocalName == name;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000296A4 File Offset: 0x000278A4
		private string GetAttributeValue(XElement element, string name)
		{
			string result;
			if (!this.TryGetAttributeValue(element, name, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000296C0 File Offset: 0x000278C0
		private bool TryGetAttributeValue(XElement element, string name, out string value)
		{
			XAttribute xattribute = element.Attribute(name);
			value = ((xattribute != null) ? xattribute.Value : null);
			return xattribute != null;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000296F0 File Offset: 0x000278F0
		private T TryParseAttribute<T>(XElement element, string name, ETRToPNRTranslator.TryParse<T> tryParse, T defaultValue)
		{
			T result;
			if (!tryParse(this.GetAttributeValue(element, name), out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00029714 File Offset: 0x00027914
		private static XElement CreateXElement(string name, params object[] content)
		{
			XElement xelement = new XElement(name);
			int i = 0;
			while (i < content.Length)
			{
				object obj = content[i];
				if (obj != null)
				{
					IEnumerable<XElement> enumerable = obj as IEnumerable<XElement>;
					if (enumerable != null)
					{
						using (IEnumerator<XElement> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								XElement xelement2 = enumerator.Current;
								if (xelement2 == null)
								{
									return null;
								}
								xelement.Add(xelement2);
							}
							goto IL_6B;
						}
						goto IL_64;
					}
					goto IL_64;
					IL_6B:
					i++;
					continue;
					IL_64:
					xelement.Add(obj);
					goto IL_6B;
				}
				return null;
			}
			return xelement;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000297B0 File Offset: 0x000279B0
		private XElement TransformRule(XElement ruleElement)
		{
			string value;
			if (!this.CheckElement(ruleElement, "rule") || !this.TryGetAttributeValue(ruleElement, "name", out value) || !RuleUtils.TryParseNullableDateTimeUtc(this.GetAttributeValue(ruleElement, "activationDate"), out this.activationDate) || !RuleUtils.TryParseNullableDateTimeUtc(this.GetAttributeValue(ruleElement, "expiryDate"), out this.expiryDate))
			{
				return null;
			}
			this.mode = this.TryParseAttribute<RuleMode>(ruleElement, "mode", new ETRToPNRTranslator.TryParse<RuleMode>(Enum.TryParse<RuleMode>), RuleMode.Enforce);
			if (this.mode == RuleMode.Audit)
			{
				return null;
			}
			string attributeValue = this.GetAttributeValue(ruleElement, "enabled");
			if (!string.IsNullOrEmpty(attributeValue) && !RuleConstants.TryParseEnabled(attributeValue, out this.enabled))
			{
				this.enabled = RuleState.Disabled;
			}
			XElement xelement = this.CheckElement(ruleElement.FirstElement(), "version") ? this.TransformVersion(ruleElement.FirstElement()) : ETRToPNRTranslator.CreateXElement("version", new object[]
			{
				new XAttribute("minRequiredVersion", "15.0.3225.3000"),
				this.TransformRuleContents(ruleElement)
			});
			return ETRToPNRTranslator.CreateXElement("rule", new object[]
			{
				new XAttribute("name", value),
				xelement
			});
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000298EC File Offset: 0x00027AEC
		private XElement TransformVersion(XElement versionElement)
		{
			Version v;
			if (!this.CheckElement(versionElement, "version") || !Version.TryParse(this.GetAttributeValue(versionElement, "requiredMinVersion"), out v))
			{
				return null;
			}
			if (v > ETRToPNRTranslator.HighestHonoredVersion)
			{
				return null;
			}
			return ETRToPNRTranslator.CreateXElement("version", new object[]
			{
				new XAttribute("minRequiredVersion", "15.0.3225.3000"),
				this.TransformRuleContents(versionElement)
			});
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00029C0C File Offset: 0x00027E0C
		private IEnumerable<XElement> TransformRuleContents(XElement ruleElement)
		{
			XElement childElement = ruleElement.FirstElement();
			while (this.CheckElement(childElement, "fork"))
			{
				if (!this.BufferFork(childElement))
				{
					yield return null;
					IL_210:
					yield break;
				}
				childElement = childElement.NextElement();
			}
			if (this.CheckElement(childElement, "tags"))
			{
				childElement = childElement.NextElement();
			}
			XElement conditionElementPnr = this.TransformCondition(childElement);
			yield return conditionElementPnr;
			if (conditionElementPnr == null)
			{
				goto IL_210;
			}
			childElement = childElement.NextElement();
			if (!this.CheckElement(childElement, "action"))
			{
				yield return null;
				goto IL_210;
			}
			XElement actionsElement = new XElement("actions");
			while (this.CheckElement(childElement, "action"))
			{
				XNode actionNodePnr = this.TransformAction(childElement);
				if (actionNodePnr == null)
				{
					yield return null;
					goto IL_210;
				}
				actionsElement.Add(actionNodePnr);
				childElement = childElement.NextElement();
			}
			yield return actionsElement;
			if (childElement != null)
			{
				yield return null;
				goto IL_210;
			}
			goto IL_210;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00029C30 File Offset: 0x00027E30
		private bool BufferFork(XElement forkElement)
		{
			if (!forkElement.Elements().Any<XElement>())
			{
				return false;
			}
			ETRToPNRTranslator.Fork fork = new ETRToPNRTranslator.Fork
			{
				IsException = this.TryParseAttribute<bool>(forkElement, "exception", new ETRToPNRTranslator.TryParse<bool>(bool.TryParse), false)
			};
			foreach (XElement xelement in forkElement.Elements())
			{
				string localName;
				if ((localName = xelement.Name.LocalName) != null)
				{
					if (!(localName == "recipient"))
					{
						if (localName == "external")
						{
							fork.IsExternal = true;
							continue;
						}
						if (localName == "internal")
						{
							fork.IsInternal = true;
							continue;
						}
						if (localName == "externalPartner")
						{
							fork.IsExternalPartner = true;
							continue;
						}
						if (localName == "externalNonPartner")
						{
							fork.IsExternalNonPartner = true;
							continue;
						}
					}
					else
					{
						string item;
						if (!this.TryGetAttributeValue(xelement, "address", out item))
						{
							return false;
						}
						fork.recipients.Add(item);
						continue;
					}
				}
				return false;
			}
			this.forks.Add(fork);
			return true;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00029F54 File Offset: 0x00028154
		private IEnumerable<XElement> TransformForks()
		{
			return (from fork in this.forks
			group fork by fork.IsException into forkGroup
			select new
			{
				forkGroup = forkGroup,
				isExceptionForksGroup = forkGroup.Key
			}).Select(delegate(<>h__TransparentIdentifier11)
			{
				if (<>h__TransparentIdentifier11.isExceptionForksGroup)
				{
					return new XElement("not", new XElement("or", from fork in <>h__TransparentIdentifier11.forkGroup
					select fork.TransformPredicates()));
				}
				return new XElement("and", from fork in <>h__TransparentIdentifier11.forkGroup
				select new XElement("or", fork.TransformPredicates()));
			});
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00029FD0 File Offset: 0x000281D0
		private XElement TransformCondition(XElement conditionElement)
		{
			if (!this.CheckElement(conditionElement, "condition") || conditionElement.Elements().Take(2).Count<XElement>() != 1)
			{
				return null;
			}
			return ETRToPNRTranslator.CreateXElement("condition", new object[]
			{
				ETRToPNRTranslator.CreateXElement("and", new object[]
				{
					this.TransformPredicate(conditionElement.FirstElement()),
					this.TransformForks()
				})
			});
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002A054 File Offset: 0x00028254
		private XElement TransformPredicate(XElement predicateElement)
		{
			if (predicateElement == null)
			{
				return null;
			}
			string localName;
			if ((localName = predicateElement.Name.LocalName) != null)
			{
				if (localName == "true")
				{
					return ETRToPNRTranslator.CreateXElement("true", new object[0]);
				}
				if (localName == "false")
				{
					return ETRToPNRTranslator.CreateXElement("false", new object[0]);
				}
				if (!(localName == "not"))
				{
					if (!(localName == "and"))
					{
						if (localName == "or")
						{
							if (!predicateElement.Elements().Any<XElement>())
							{
								return null;
							}
							string name = "or";
							object[] array = new object[1];
							array[0] = from element in predicateElement.Elements()
							select this.TransformPredicate(element);
							return ETRToPNRTranslator.CreateXElement(name, array);
						}
					}
					else
					{
						if (!predicateElement.Elements().Any<XElement>())
						{
							return null;
						}
						string name2 = "and";
						object[] array2 = new object[1];
						array2[0] = from element in predicateElement.Elements()
						select this.TransformPredicate(element);
						return ETRToPNRTranslator.CreateXElement(name2, array2);
					}
				}
				else
				{
					if (predicateElement.Elements().Take(2).Count<XElement>() != 1)
					{
						return null;
					}
					return ETRToPNRTranslator.CreateXElement("not", new object[]
					{
						this.TransformPredicate(predicateElement.FirstElement())
					});
				}
			}
			return this.TransformPedicate_Advanced(predicateElement);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002A1F0 File Offset: 0x000283F0
		private XElement TransformPedicate_Advanced(XElement predicateElement)
		{
			string a;
			if (!this.TryGetAttributeValue(predicateElement, "property", out a))
			{
				return null;
			}
			IList<string> list = null;
			IList<IList<KeyValuePair<string, string>>> list2 = null;
			XElement xelement = predicateElement.FirstElement();
			if (this.CheckElement(xelement, "keyValues"))
			{
				list2 = new List<IList<KeyValuePair<string, string>>>();
				while (this.CheckElement(xelement, "keyValues"))
				{
					IList<KeyValuePair<string, string>> list3 = new List<KeyValuePair<string, string>>();
					XElement xelement2 = xelement.FirstElement();
					while (this.CheckElement(xelement2, "keyValue"))
					{
						list3.Add(new KeyValuePair<string, string>(this.GetAttributeValue(xelement2, "key"), this.GetAttributeValue(xelement2, "value")));
						xelement2 = xelement2.NextElement();
					}
					if (xelement2 != null || !list3.Any<KeyValuePair<string, string>>())
					{
						return null;
					}
					list2.Add(list3);
					xelement = xelement.NextElement();
				}
			}
			else
			{
				list = new List<string>();
				while (this.CheckElement(xelement, "value"))
				{
					if (xelement.Elements().Any<XElement>())
					{
						return null;
					}
					list.Add(xelement.Value);
					xelement = xelement.NextElement();
				}
			}
			if (xelement != null)
			{
				return null;
			}
			string localName;
			if ((localName = predicateElement.Name.LocalName) != null)
			{
				if (!(localName == "containsDataClassification"))
				{
					if (!(localName == "isSameUser"))
					{
						if (!(localName == "isMemberOf"))
						{
							if (!(localName == "isInternal"))
							{
								if (localName == "is")
								{
									if (list == null || !list.Any<string>() || a != "Message.Auth")
									{
										return null;
									}
									if (list[0] != "<>")
									{
										return null;
									}
									return ETRToPNRTranslator.CreateXElement("false", new object[0]);
								}
							}
							else
							{
								if (list == null || a != "Message.From")
								{
									return null;
								}
								return ETRToPNRTranslator.CreateXElement("true", new object[0]);
							}
						}
						else
						{
							if (list == null || !list.Any<string>() || a != "Message.From")
							{
								return null;
							}
							if (this.distributionListResolver != null)
							{
								foreach (string distributionList in list)
								{
									this.distributionListResolver.Get(distributionList);
								}
							}
							string name = "or";
							object[] array = new object[1];
							array[0] = from value in list
							select new XElement("sender", new XAttribute("distributionGroup", value));
							return ETRToPNRTranslator.CreateXElement(name, array);
						}
					}
					else
					{
						if (list == null || !list.Any<string>() || a != "Message.From")
						{
							return null;
						}
						string name2 = "or";
						object[] array2 = new object[1];
						array2[0] = from value in list
						select new XElement("sender", new XAttribute("address", value));
						return ETRToPNRTranslator.CreateXElement(name2, array2);
					}
				}
				else
				{
					if (list2 == null || a != "Message.DataClassifications")
					{
						return null;
					}
					return new XElement("or", this.TransformPredicate_ContainsDataClassification(list2));
				}
			}
			return null;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002A9AC File Offset: 0x00028BAC
		private IEnumerable<XElement> TransformPredicate_ContainsDataClassification(IList<IList<KeyValuePair<string, string>>> keyValueCollections)
		{
			foreach (IList<KeyValuePair<string, string>> keyValueCollection in keyValueCollections)
			{
				string Id = null;
				string MinCount = null;
				string MaxCount = null;
				string MinConfidence = null;
				string MaxConfidence = null;
				string OpaqueData = null;
				foreach (KeyValuePair<string, string> keyValuePair in keyValueCollection)
				{
					if (string.Compare(keyValuePair.Key, "id", StringComparison.OrdinalIgnoreCase) == 0)
					{
						Id = keyValuePair.Value;
					}
					else if (string.Compare(keyValuePair.Key, "minCount", StringComparison.OrdinalIgnoreCase) == 0 && keyValuePair.Value != "1")
					{
						MinCount = keyValuePair.Value;
					}
					else if (string.Compare(keyValuePair.Key, "maxCount", StringComparison.OrdinalIgnoreCase) == 0 && keyValuePair.Value != "-1")
					{
						MaxCount = keyValuePair.Value;
					}
					else if (string.Compare(keyValuePair.Key, "minConfidence", StringComparison.OrdinalIgnoreCase) == 0 && keyValuePair.Value != "-1")
					{
						MinConfidence = keyValuePair.Value;
					}
					else if (string.Compare(keyValuePair.Key, "maxConfidence", StringComparison.OrdinalIgnoreCase) == 0 && keyValuePair.Value != "100")
					{
						MaxConfidence = keyValuePair.Value;
					}
					else if (string.Compare(keyValuePair.Key, "opaqueData", StringComparison.OrdinalIgnoreCase) == 0)
					{
						OpaqueData = keyValuePair.Value;
					}
				}
				int ignoredValue;
				if (string.IsNullOrEmpty(Id) || (MinCount != null && !int.TryParse(MinCount, out ignoredValue)) || (MaxCount != null && !int.TryParse(MaxCount, out ignoredValue)) || (MinConfidence != null && !int.TryParse(MinConfidence, out ignoredValue)) || (MaxConfidence != null && !int.TryParse(MaxConfidence, out ignoredValue)))
				{
					yield return null;
					yield break;
				}
				yield return new XElement("classification", new object[]
				{
					new XAttribute("id", Id),
					(MinCount != null) ? new XAttribute("minCount", MinCount) : null,
					(MaxCount != null) ? new XAttribute("maxCount", MaxCount) : null,
					(MinConfidence != null) ? new XAttribute("minConfidence", MinConfidence) : null,
					(MaxConfidence != null) ? new XAttribute("maxConfidence", MaxConfidence) : null,
					(OpaqueData != null) ? new XAttribute("rulePackId", OpaqueData) : null
				});
			}
			yield break;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002A9D0 File Offset: 0x00028BD0
		private XNode TransformAction(XElement actionElement)
		{
			string text;
			if (!this.TryGetAttributeValue(actionElement, "name", out text))
			{
				return null;
			}
			IList<string> list = new List<string>();
			XElement xelement = actionElement.FirstElement();
			while (this.CheckElement(xelement, "argument"))
			{
				string item;
				if (this.TryGetAttributeValue(xelement, "value", out item))
				{
					list.Add(item);
				}
				xelement = xelement.NextElement();
			}
			if (xelement != null)
			{
				return null;
			}
			string a;
			if ((a = text) != null && a == "SenderNotify")
			{
				return this.TransformAction_SenderNotify(list);
			}
			return new XComment("Ignored action: " + text);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002AA5C File Offset: 0x00028C5C
		private XElement TransformAction_SenderNotify(IList<string> arguments)
		{
			ETRToPNRTranslator.OutlookActionTypes outlookActionTypes;
			if (arguments.Count < 1 || !Enum.TryParse<ETRToPNRTranslator.OutlookActionTypes>(arguments[0], out outlookActionTypes))
			{
				return null;
			}
			if (this.mode == RuleMode.AuditAndNotify)
			{
				outlookActionTypes = ETRToPNRTranslator.OutlookActionTypes.NotifyOnly;
			}
			bool flag = outlookActionTypes == ETRToPNRTranslator.OutlookActionTypes.NotifyOnly;
			bool flag2 = outlookActionTypes == ETRToPNRTranslator.OutlookActionTypes.RejectUnlessSilentOverride || outlookActionTypes == ETRToPNRTranslator.OutlookActionTypes.RejectUnlessExplicitOverride;
			bool flag3 = outlookActionTypes == ETRToPNRTranslator.OutlookActionTypes.RejectUnlessExplicitOverride;
			this.hasSenderNotifyAction = true;
			return new XElement(flag ? "notify" : "block", new object[]
			{
				new XElement("message", new XElement("locale", new object[]
				{
					new XAttribute("name", this.messageStrings.OutlookCultureTag),
					new XElement("complianceNoteUrl", this.messageStrings.Url.Value),
					new XElement("text2", this.messageStrings.Get(outlookActionTypes).Value)
				})),
				new XElement("override", new object[]
				{
					new XAttribute("allow", flag2 ? "yes" : "no"),
					new XElement("justification", new XAttribute("type", flag3 ? "required" : "none"))
				}),
				new XElement("falsePositive", new XAttribute("allow", "yes"))
			});
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002ABF8 File Offset: 0x00028DF8
		private static XDocument CreateXDocument(string xml)
		{
			XmlReaderSettings settings = new XmlReaderSettings
			{
				ConformanceLevel = ConformanceLevel.Auto,
				IgnoreComments = true,
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
			XDocument result;
			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, settings))
				{
					result = XDocument.Load(xmlReader);
				}
			}
			return result;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002AEE4 File Offset: 0x000290E4
		public static string Evaluate(string policyNudgeRuleXml, ETRToPNRTranslator.IDistributionListResolver distributionListResolver)
		{
			XDocument xdocument = ETRToPNRTranslator.CreateXDocument(policyNudgeRuleXml);
			XElement xelement = xdocument.Root.Element("version").Element("condition");
			var source = from senderPredicate in xelement.Descendants("sender")
			let distributionGroupAttribute = senderPredicate.Attribute("distributionGroup")
			where distributionGroupAttribute != null
			select new
			{
				Element = senderPredicate,
				DistributionList = distributionGroupAttribute.Value
			};
			foreach (var <>f__AnonymousType in source.ToList())
			{
				bool flag = distributionListResolver.IsMemberOf(<>f__AnonymousType.DistributionList);
				<>f__AnonymousType.Element.ReplaceWith(new object[]
				{
					new XElement(flag ? "true" : "false"),
					new XComment("Replaced sender is member of distribution list \"" + <>f__AnonymousType.DistributionList + "\"")
				});
			}
			return xdocument.ToString();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002B054 File Offset: 0x00029254
		private static void OptimizePredicates(XDocument xdoc)
		{
			while (ETRToPNRTranslator.optimizations.Any((ETRToPNRTranslator.IRuleOptimizer optimization) => optimization.Optimize(xdoc)))
			{
			}
		}

		// Token: 0x04000763 RID: 1891
		private const string outlookVersionConstant = "15.0.3225.3000";

		// Token: 0x04000764 RID: 1892
		private static HashSet<string> knownPNRPredicatetNames = new HashSet<string>
		{
			"condition",
			"actions",
			"and",
			"classification",
			"false",
			"not",
			"or",
			"recipient",
			"sender",
			"true"
		};

		// Token: 0x04000765 RID: 1893
		public static readonly Version HighestHonoredVersion = new Version("15.00.0015.00");

		// Token: 0x04000766 RID: 1894
		private RuleMode mode = RuleMode.Enforce;

		// Token: 0x04000767 RID: 1895
		private List<ETRToPNRTranslator.Fork> forks = new List<ETRToPNRTranslator.Fork>();

		// Token: 0x04000768 RID: 1896
		private ETRToPNRTranslator.IMessageStrings messageStrings;

		// Token: 0x04000769 RID: 1897
		private ETRToPNRTranslator.IDistributionListResolver distributionListResolver;

		// Token: 0x0400076A RID: 1898
		private ETRToPNRTranslator.IDataClassificationResolver dataClassificationResolver;

		// Token: 0x0400076B RID: 1899
		private readonly string etrXml;

		// Token: 0x0400076C RID: 1900
		private RuleState enabled;

		// Token: 0x0400076D RID: 1901
		private DateTime? activationDate;

		// Token: 0x0400076E RID: 1902
		private DateTime? expiryDate;

		// Token: 0x0400076F RID: 1903
		private bool hasSenderNotifyAction;

		// Token: 0x04000770 RID: 1904
		private string pnrXml;

		// Token: 0x04000771 RID: 1905
		private Lazy<string> fullPnrXml;

		// Token: 0x04000772 RID: 1906
		private static readonly IEnumerable<ETRToPNRTranslator.IRuleOptimizer> optimizations = new ETRToPNRTranslator.IRuleOptimizer[]
		{
			new ETRToPNRTranslator.FalseElementUnderParentNotOptimizer(),
			new ETRToPNRTranslator.FalseElementUnderParentAndOptimizer(),
			new ETRToPNRTranslator.TrueElementUnderParentNotOptimizer(),
			new ETRToPNRTranslator.MergeAndOrElementsWithSameParentOptimizer(),
			new ETRToPNRTranslator.NotElementUnderParentNotOptimizer(),
			new ETRToPNRTranslator.FalseElementUnderParentOrOptimizer(),
			new ETRToPNRTranslator.TrueElementUnderParentOrOptimizer(),
			new ETRToPNRTranslator.TrueElementUnderParentAndOptimizer(),
			new ETRToPNRTranslator.RemoveExtraniousAndOrOptimizer()
		};

		// Token: 0x02000164 RID: 356
		public enum OutlookActionTypes
		{
			// Token: 0x04000781 RID: 1921
			NotifyOnly,
			// Token: 0x04000782 RID: 1922
			RejectMessage,
			// Token: 0x04000783 RID: 1923
			RejectUnlessFalsePositiveOverride,
			// Token: 0x04000784 RID: 1924
			RejectUnlessSilentOverride,
			// Token: 0x04000785 RID: 1925
			RejectUnlessExplicitOverride
		}

		// Token: 0x02000165 RID: 357
		private static class ETRConstants
		{
			// Token: 0x04000786 RID: 1926
			internal const string AttributeAddress = "address";

			// Token: 0x04000787 RID: 1927
			internal const string AttributeId = "id";

			// Token: 0x04000788 RID: 1928
			internal const string AttributeMinCount = "minCount";

			// Token: 0x04000789 RID: 1929
			internal const string AttributeMaxCount = "maxCount";

			// Token: 0x0400078A RID: 1930
			internal const string AttributeMinConfidence = "minConfidence";

			// Token: 0x0400078B RID: 1931
			internal const string AttributeMaxConfidence = "maxConfidence";

			// Token: 0x0400078C RID: 1932
			internal const string AttributeOpaqueData = "opaqueData";

			// Token: 0x0400078D RID: 1933
			internal const string AttributeValueSenderNotify = "SenderNotify";

			// Token: 0x0400078E RID: 1934
			internal const string TagActions = "actions";

			// Token: 0x0400078F RID: 1935
			internal const string TagAttachmentContainsWords = "attachmentContainsWords";

			// Token: 0x04000790 RID: 1936
			internal const string TagAttachmentIsUnsupported = "attachmentIsUnsupported";

			// Token: 0x04000791 RID: 1937
			internal const string TagContainsDataClassification = "containsDataClassification";

			// Token: 0x04000792 RID: 1938
			internal const string TagExternal = "external";

			// Token: 0x04000793 RID: 1939
			internal const string TagExternalNonPartner = "externalNonPartner";

			// Token: 0x04000794 RID: 1940
			internal const string TagExternalPartner = "externalPartner";

			// Token: 0x04000795 RID: 1941
			internal const string TagFork = "fork";

			// Token: 0x04000796 RID: 1942
			internal const string TagHasSenderOverride = "hasSenderOverride";

			// Token: 0x04000797 RID: 1943
			internal const string TagInternal = "internal";

			// Token: 0x04000798 RID: 1944
			internal const string TagIsInternal = "isInternal";

			// Token: 0x04000799 RID: 1945
			internal const string TagIsMemberOf = "isMemberOf";

			// Token: 0x0400079A RID: 1946
			internal const string TagIsMessageType = "isMessageType";

			// Token: 0x0400079B RID: 1947
			internal const string TagIsSameUser = "isSameUser";

			// Token: 0x0400079C RID: 1948
			internal const string TagRecipient = "recipient";

			// Token: 0x0400079D RID: 1949
			internal const string TagSender = "sender";

			// Token: 0x0400079E RID: 1950
			internal const string PropertyAuth = "Message.Auth";

			// Token: 0x0400079F RID: 1951
			internal const string PropertyFrom = "Message.From";

			// Token: 0x040007A0 RID: 1952
			internal const string PropertyDataClassifications = "Message.DataClassifications";
		}

		// Token: 0x02000166 RID: 358
		private static class PNRConstants
		{
			// Token: 0x040007A1 RID: 1953
			internal const string AttributeAddress = "address";

			// Token: 0x040007A2 RID: 1954
			internal const string AttributeDistributionGroup = "distributionGroup";

			// Token: 0x040007A3 RID: 1955
			internal const string AttributeId = "id";

			// Token: 0x040007A4 RID: 1956
			internal const string AttributeMinCount = "minCount";

			// Token: 0x040007A5 RID: 1957
			internal const string AttributeMinRequiredVersion = "minRequiredVersion";

			// Token: 0x040007A6 RID: 1958
			internal const string AttributeMaxCount = "maxCount";

			// Token: 0x040007A7 RID: 1959
			internal const string AttributeMinConfidence = "minConfidence";

			// Token: 0x040007A8 RID: 1960
			internal const string AttributeMaxConfidence = "maxConfidence";

			// Token: 0x040007A9 RID: 1961
			internal const string AttributeRulePackId = "rulePackId";

			// Token: 0x040007AA RID: 1962
			internal const string AttributeScope = "scope";

			// Token: 0x040007AB RID: 1963
			internal const string AttributeValueInternal = "Internal";

			// Token: 0x040007AC RID: 1964
			internal const string AttributeValueExternal = "External";

			// Token: 0x040007AD RID: 1965
			internal const string AttributeValueExternalPartner = "ExternalPartner";

			// Token: 0x040007AE RID: 1966
			internal const string AttributeValueExternalNonPartner = "ExternalNonPartner";

			// Token: 0x040007AF RID: 1967
			internal const string TagCondition = "condition";

			// Token: 0x040007B0 RID: 1968
			internal const string TagActions = "actions";

			// Token: 0x040007B1 RID: 1969
			internal const string TagAnd = "and";

			// Token: 0x040007B2 RID: 1970
			internal const string TagClassification = "classification";

			// Token: 0x040007B3 RID: 1971
			internal const string TagFalse = "false";

			// Token: 0x040007B4 RID: 1972
			internal const string TagNot = "not";

			// Token: 0x040007B5 RID: 1973
			internal const string TagOr = "or";

			// Token: 0x040007B6 RID: 1974
			internal const string TagRecipient = "recipient";

			// Token: 0x040007B7 RID: 1975
			internal const string TagSender = "sender";

			// Token: 0x040007B8 RID: 1976
			internal const string TagTrue = "true";

			// Token: 0x040007B9 RID: 1977
			internal const string TagNotify = "notify";

			// Token: 0x040007BA RID: 1978
			internal const string TagBlock = "block";

			// Token: 0x040007BB RID: 1979
			internal const string TagMessage = "message";

			// Token: 0x040007BC RID: 1980
			internal const string TagLocale = "locale";

			// Token: 0x040007BD RID: 1981
			internal const string TagOverride = "override";

			// Token: 0x040007BE RID: 1982
			internal const string AttributeAllow = "allow";

			// Token: 0x040007BF RID: 1983
			internal const string AttributeValueYes = "yes";

			// Token: 0x040007C0 RID: 1984
			internal const string AttributeValueNo = "no";

			// Token: 0x040007C1 RID: 1985
			internal const string TagJustification = "justification";

			// Token: 0x040007C2 RID: 1986
			internal const string AttributeType = "type";

			// Token: 0x040007C3 RID: 1987
			internal const string AttributeValueRequired = "required";

			// Token: 0x040007C4 RID: 1988
			internal const string AttributeValueNone = "none";

			// Token: 0x040007C5 RID: 1989
			internal const string TagFalsePositive = "falsePositive";
		}

		// Token: 0x02000167 RID: 359
		private class Fork
		{
			// Token: 0x060009F2 RID: 2546 RVA: 0x0002B494 File Offset: 0x00029694
			internal IEnumerable<XElement> TransformPredicates()
			{
				foreach (string recipient in this.recipients)
				{
					yield return ETRToPNRTranslator.CreateXElement("recipient", new object[]
					{
						new XAttribute("address", recipient)
					});
				}
				if (this.IsInternal)
				{
					yield return ETRToPNRTranslator.CreateXElement("recipient", new object[]
					{
						new XAttribute("scope", "Internal")
					});
				}
				if (this.IsExternal)
				{
					yield return ETRToPNRTranslator.CreateXElement("recipient", new object[]
					{
						new XAttribute("scope", "External")
					});
				}
				if (this.IsExternalPartner)
				{
					yield return ETRToPNRTranslator.CreateXElement("recipient", new object[]
					{
						new XAttribute("scope", "ExternalPartner")
					});
				}
				if (this.IsExternalNonPartner)
				{
					yield return ETRToPNRTranslator.CreateXElement("recipient", new object[]
					{
						new XAttribute("scope", "ExternalNonPartner")
					});
				}
				yield break;
			}

			// Token: 0x040007C6 RID: 1990
			internal bool IsException;

			// Token: 0x040007C7 RID: 1991
			internal List<string> recipients = new List<string>();

			// Token: 0x040007C8 RID: 1992
			internal bool IsInternal;

			// Token: 0x040007C9 RID: 1993
			internal bool IsExternal;

			// Token: 0x040007CA RID: 1994
			internal bool IsExternalPartner;

			// Token: 0x040007CB RID: 1995
			internal bool IsExternalNonPartner;
		}

		// Token: 0x02000168 RID: 360
		// (Invoke) Token: 0x060009F5 RID: 2549
		private delegate bool TryParse<T>(string stringValue, out T value);

		// Token: 0x02000169 RID: 361
		internal interface IDistributionListResolver
		{
			// Token: 0x060009F8 RID: 2552
			IEnumerable<IVersionedItem> Get(string distributionList);

			// Token: 0x060009F9 RID: 2553
			bool IsMemberOf(string distributionList);
		}

		// Token: 0x0200016A RID: 362
		internal class DistributionListResolverCallbackImpl : ETRToPNRTranslator.IDistributionListResolver
		{
			// Token: 0x060009FA RID: 2554 RVA: 0x0002B4C4 File Offset: 0x000296C4
			public DistributionListResolverCallbackImpl(Func<string, IEnumerable<IVersionedItem>> onGet, Func<string, bool> onIsMemberOf)
			{
				this.onGet = onGet;
				this.onIsMemberOf = onIsMemberOf;
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x0002B4DA File Offset: 0x000296DA
			public IEnumerable<IVersionedItem> Get(string distributionList)
			{
				return this.onGet(distributionList);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x0002B4E8 File Offset: 0x000296E8
			public bool IsMemberOf(string distributionList)
			{
				return this.onIsMemberOf(distributionList);
			}

			// Token: 0x040007CC RID: 1996
			private Func<string, IEnumerable<IVersionedItem>> onGet;

			// Token: 0x040007CD RID: 1997
			private Func<string, bool> onIsMemberOf;
		}

		// Token: 0x0200016B RID: 363
		internal interface IDataClassificationResolver
		{
			// Token: 0x060009FD RID: 2557
			bool IsVersionedDataClassification(string dataClassificationId, string rulePackageId);
		}

		// Token: 0x0200016C RID: 364
		public interface IMessageStrings
		{
			// Token: 0x1700027F RID: 639
			// (get) Token: 0x060009FE RID: 2558
			string OutlookCultureTag { get; }

			// Token: 0x060009FF RID: 2559
			PolicyTipMessage Get(ETRToPNRTranslator.OutlookActionTypes type);

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000A00 RID: 2560
			PolicyTipMessage Url { get; }
		}

		// Token: 0x0200016D RID: 365
		internal class MessageStringCallbackImpl : ETRToPNRTranslator.IMessageStrings
		{
			// Token: 0x06000A01 RID: 2561 RVA: 0x0002B4F6 File Offset: 0x000296F6
			public MessageStringCallbackImpl(string outlookCultureTag, Func<ETRToPNRTranslator.OutlookActionTypes, PolicyTipMessage> onGet, Func<PolicyTipMessage> onGetUrl)
			{
				this.OutlookCultureTag = outlookCultureTag;
				this.onGet = onGet;
				this.onGetUrl = onGetUrl;
			}

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002B513 File Offset: 0x00029713
			// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0002B51B File Offset: 0x0002971B
			public string OutlookCultureTag { get; private set; }

			// Token: 0x06000A04 RID: 2564 RVA: 0x0002B524 File Offset: 0x00029724
			public PolicyTipMessage Get(ETRToPNRTranslator.OutlookActionTypes type)
			{
				return this.onGet(type);
			}

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002B532 File Offset: 0x00029732
			public PolicyTipMessage Url
			{
				get
				{
					return this.onGetUrl();
				}
			}

			// Token: 0x040007CE RID: 1998
			private Func<ETRToPNRTranslator.OutlookActionTypes, PolicyTipMessage> onGet;

			// Token: 0x040007CF RID: 1999
			private Func<PolicyTipMessage> onGetUrl;
		}

		// Token: 0x0200016E RID: 366
		internal interface IRuleOptimizer
		{
			// Token: 0x06000A06 RID: 2566
			bool Optimize(XDocument xdoc);
		}

		// Token: 0x0200016F RID: 367
		internal class FalseElementUnderParentNotOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A07 RID: 2567 RVA: 0x0002B55C File Offset: 0x0002975C
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from el in xdoc.Descendants("false")
				where el.Parent.Name.LocalName == "not"
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Parent.ReplaceWith(new XElement("true"));
				return true;
			}
		}

		// Token: 0x02000170 RID: 368
		internal class FalseElementUnderParentAndOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A0A RID: 2570 RVA: 0x0002B5E8 File Offset: 0x000297E8
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from el in xdoc.Descendants("false")
				where el.Parent.Name.LocalName == "and"
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Parent.ReplaceWith(new XElement("false"));
				return true;
			}
		}

		// Token: 0x02000171 RID: 369
		internal class FalseElementUnderParentOrOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A0D RID: 2573 RVA: 0x0002B68C File Offset: 0x0002988C
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from el in xdoc.Descendants("false")
				where el.Parent.Name.LocalName == "or"
				where el.Parent.Elements().Take(2).Count<XElement>() > 1
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Remove();
				return true;
			}
		}

		// Token: 0x02000172 RID: 370
		internal class TrueElementUnderParentNotOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A11 RID: 2577 RVA: 0x0002B724 File Offset: 0x00029924
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from el in xdoc.Descendants("true")
				where el.Parent.Name.LocalName == "not"
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Parent.ReplaceWith(new XElement("false"));
				return true;
			}
		}

		// Token: 0x02000173 RID: 371
		internal class TrueElementUnderParentOrOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A14 RID: 2580 RVA: 0x0002B7C8 File Offset: 0x000299C8
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from el in xdoc.Descendants("true")
				where el.Parent.Name.LocalName == "or"
				where el.Parent.Elements().Take(2).Count<XElement>() > 1
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Remove();
				return true;
			}
		}

		// Token: 0x02000174 RID: 372
		internal class TrueElementUnderParentAndOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A18 RID: 2584 RVA: 0x0002B87C File Offset: 0x00029A7C
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from el in xdoc.Descendants("true")
				where el.Parent.Name.LocalName == "and"
				where el.Parent.Elements().Take(2).Count<XElement>() > 1
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Remove();
				return true;
			}
		}

		// Token: 0x02000175 RID: 373
		internal class MergeAndOrElementsWithSameParentOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A1C RID: 2588 RVA: 0x0002B928 File Offset: 0x00029B28
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from andOrElement in xdoc.Descendants("and").Concat(xdoc.Descendants("or"))
				where andOrElement.Parent != null
				where andOrElement.Name.LocalName == andOrElement.Parent.Name.LocalName
				select andOrElement).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Parent.Add(xelement.Elements());
				xelement.Remove();
				return true;
			}
		}

		// Token: 0x02000176 RID: 374
		internal class NotElementUnderParentNotOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A20 RID: 2592 RVA: 0x0002B9F4 File Offset: 0x00029BF4
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from notElement in xdoc.Descendants("not")
				where notElement.Parent != null
				where notElement.Parent.Name.LocalName == "not"
				select notElement).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.Parent.ReplaceWith(xelement.Elements());
				return true;
			}
		}

		// Token: 0x02000177 RID: 375
		internal class RemoveExtraniousAndOrOptimizer : ETRToPNRTranslator.IRuleOptimizer
		{
			// Token: 0x06000A24 RID: 2596 RVA: 0x0002BA90 File Offset: 0x00029C90
			public bool Optimize(XDocument xdoc)
			{
				XElement xelement = (from andOrElement in xdoc.Descendants("and").Concat(xdoc.Descendants("or"))
				where andOrElement.Elements().Take(2).Count<XElement>() == 1
				select andOrElement).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					return false;
				}
				xelement.ReplaceWith(xelement.Elements());
				return true;
			}
		}
	}
}

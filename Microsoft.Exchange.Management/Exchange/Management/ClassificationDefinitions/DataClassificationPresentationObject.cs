using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000833 RID: 2099
	[Serializable]
	public sealed class DataClassificationPresentationObject : IConfigurable
	{
		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x060048B1 RID: 18609 RVA: 0x0012A4FD File Offset: 0x001286FD
		// (set) Token: 0x060048B2 RID: 18610 RVA: 0x0012A505 File Offset: 0x00128705
		public ObjectId Identity { get; private set; }

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x060048B3 RID: 18611 RVA: 0x0012A50E File Offset: 0x0012870E
		public string Name
		{
			get
			{
				return this.defaultDetails.Name;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x0012A51B File Offset: 0x0012871B
		public string Description
		{
			get
			{
				return ClassificationDefinitionUtils.GetMatchingLocalizedInfo<string>(this.localizedDescriptions, this.defaultDetails.Description);
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x0012A533 File Offset: 0x00128733
		// (set) Token: 0x060048B6 RID: 18614 RVA: 0x0012A53C File Offset: 0x0012873C
		public MultiValuedProperty<Fingerprint> Fingerprints
		{
			get
			{
				return this.fingerprints;
			}
			set
			{
				if (object.ReferenceEquals(this.fingerprints, value))
				{
					return;
				}
				if (this.fingerprints != null && value != null && this.fingerprints.Count == value.Count && this.fingerprints.Intersect(value).ToList<Fingerprint>().Count == this.fingerprints.Count)
				{
					return;
				}
				this.fingerprints = value;
				this.IsDirty = true;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x060048B7 RID: 18615 RVA: 0x0012A5A8 File Offset: 0x001287A8
		public string LocalizedName
		{
			get
			{
				return ClassificationDefinitionUtils.GetMatchingLocalizedInfo<string>(this.localizedNames, this.defaultDetails.Name);
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x0012A5C0 File Offset: 0x001287C0
		public string Publisher
		{
			get
			{
				return this.ClassificationRuleCollection.Publisher;
			}
		}

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x0012A5CD File Offset: 0x001287CD
		// (set) Token: 0x060048BA RID: 18618 RVA: 0x0012A5D5 File Offset: 0x001287D5
		public ClassificationTypeEnum ClassificationType { get; private set; }

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x060048BB RID: 18619 RVA: 0x0012A5E0 File Offset: 0x001287E0
		public bool? IsEncrypted
		{
			get
			{
				if (this.ClassificationRuleCollection == null)
				{
					return null;
				}
				return new bool?(this.ClassificationRuleCollection.IsEncrypted);
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x060048BC RID: 18620 RVA: 0x0012A60F File Offset: 0x0012880F
		// (set) Token: 0x060048BD RID: 18621 RVA: 0x0012A617 File Offset: 0x00128817
		public uint? RecommendedConfidence { get; private set; }

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x060048BE RID: 18622 RVA: 0x0012A620 File Offset: 0x00128820
		// (set) Token: 0x060048BF RID: 18623 RVA: 0x0012A628 File Offset: 0x00128828
		public ClassificationRuleCollectionPresentationObject ClassificationRuleCollection { get; private set; }

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x0012A631 File Offset: 0x00128831
		// (set) Token: 0x060048C1 RID: 18625 RVA: 0x0012A639 File Offset: 0x00128839
		public ExchangeBuild MinEngineVersion { get; private set; }

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x060048C2 RID: 18626 RVA: 0x0012A644 File Offset: 0x00128844
		public DateTime? WhenChanged
		{
			get
			{
				if (this.ClassificationRuleCollection == null)
				{
					return null;
				}
				return this.ClassificationRuleCollection.WhenChanged;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x0012A66E File Offset: 0x0012886E
		public CultureInfo DefaultCulture
		{
			get
			{
				return this.defaultDetails.Culture;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x060048C4 RID: 18628 RVA: 0x0012A67B File Offset: 0x0012887B
		public Dictionary<CultureInfo, string> AllLocalizedNames
		{
			get
			{
				return this.localizedNames;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x060048C5 RID: 18629 RVA: 0x0012A683 File Offset: 0x00128883
		public Dictionary<CultureInfo, string> AllLocalizedDescriptions
		{
			get
			{
				return this.localizedDescriptions;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x0012A68B File Offset: 0x0012888B
		// (set) Token: 0x060048C7 RID: 18631 RVA: 0x0012A693 File Offset: 0x00128893
		internal bool IsDirty { get; private set; }

		// Token: 0x060048C8 RID: 18632 RVA: 0x0012A69C File Offset: 0x0012889C
		internal void SetLocalizedName(CultureInfo locale, string value)
		{
			ArgumentValidator.ThrowIfNull("locale", locale);
			if (locale.Equals(this.defaultDetails.Culture) && !string.Equals(this.defaultDetails.Name, value))
			{
				this.defaultDetails.Name = value;
				this.IsDirty = true;
			}
			if (!this.localizedNames.ContainsKey(locale) || !string.Equals(this.localizedNames[locale], value, StringComparison.Ordinal))
			{
				DataClassificationPresentationObject.SetLocalizedResource(this.localizedNames, locale, value);
				this.IsDirty = true;
			}
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x0012A724 File Offset: 0x00128924
		internal void SetLocalizedDescription(CultureInfo locale, string value)
		{
			ArgumentValidator.ThrowIfNull("locale", locale);
			if (locale.Equals(this.defaultDetails.Culture) && !string.Equals(this.defaultDetails.Description, value))
			{
				this.defaultDetails.Description = value;
				this.IsDirty = true;
			}
			if (!this.localizedDescriptions.ContainsKey(locale) || !string.Equals(this.localizedDescriptions[locale], value, StringComparison.Ordinal))
			{
				DataClassificationPresentationObject.SetLocalizedResource(this.localizedDescriptions, locale, value);
				this.IsDirty = true;
			}
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0012A7AC File Offset: 0x001289AC
		internal void SetDefaultResource(CultureInfo locale, string name, string description)
		{
			ArgumentValidator.ThrowIfNull("locale", locale);
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			if (this.defaultDetails != null && this.defaultDetails.Culture != null && this.defaultDetails.Culture.Equals(locale) && string.Equals(this.defaultDetails.Name, name, StringComparison.Ordinal) && string.Equals(this.defaultDetails.Description, description, StringComparison.Ordinal))
			{
				return;
			}
			this.SetLocalizedName(locale, name);
			this.SetLocalizedDescription(locale, description);
			this.defaultDetails = new DataClassificationLocalizableDetails
			{
				Culture = locale,
				Name = name,
				Description = description
			};
			this.IsDirty = true;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x0012A85C File Offset: 0x00128A5C
		private XElement GetRuleXElement()
		{
			List<XElement> list = new List<XElement>();
			foreach (Fingerprint fingerprint in this.Fingerprints)
			{
				list.Add(new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Match"), new XAttribute("idRef", fingerprint.Identity)));
			}
			XElement xelement = list[0];
			if (list.Count > 1)
			{
				xelement = new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Any"), new object[]
				{
					new XAttribute("minMatches", 1),
					list
				});
			}
			XElement xelement2 = new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Evidence"), new object[]
			{
				new XAttribute("confidenceLevel", 75),
				xelement
			});
			return new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Affinity"), new object[]
			{
				new XAttribute("id", ((DataClassificationObjectId)this.Identity).Name),
				new XAttribute("evidencesProximity", 300),
				new XAttribute("thresholdConfidenceLevel", 75),
				xelement2
			});
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x0012A9D4 File Offset: 0x00128BD4
		private XElement GetResourceXElement()
		{
			List<XElement> list = new List<XElement>();
			list.Add(new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name"), new object[]
			{
				new XAttribute("default", "true"),
				new XAttribute("langcode", this.defaultDetails.Culture.Name),
				this.defaultDetails.Name
			}));
			foreach (KeyValuePair<CultureInfo, string> keyValuePair in this.localizedNames)
			{
				if (!this.defaultDetails.Culture.Equals(keyValuePair.Key))
				{
					list.Add(new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name"), new object[]
					{
						new XAttribute("langcode", keyValuePair.Key.Name),
						keyValuePair.Value
					}));
				}
			}
			list.Add(new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description"), new object[]
			{
				new XAttribute("default", "true"),
				new XAttribute("langcode", this.defaultDetails.Culture.Name),
				this.defaultDetails.Description
			}));
			foreach (KeyValuePair<CultureInfo, string> keyValuePair2 in this.localizedDescriptions)
			{
				if (!this.defaultDetails.Culture.Equals(keyValuePair2.Key))
				{
					list.Add(new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description"), new object[]
					{
						new XAttribute("langcode", keyValuePair2.Key.Name),
						keyValuePair2.Value
					}));
				}
			}
			return new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource"), new object[]
			{
				new XAttribute("idRef", ((DataClassificationObjectId)this.Identity).Name),
				list
			});
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x0012AC38 File Offset: 0x00128E38
		private DataClassificationPresentationObject()
		{
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0012AC40 File Offset: 0x00128E40
		private static void SetLocalizedResource(Dictionary<CultureInfo, string> resourceDictionary, CultureInfo locale, string value)
		{
			ArgumentValidator.ThrowIfNull("resourceDictionary", resourceDictionary);
			ArgumentValidator.ThrowIfNull("locale", locale);
			if (string.IsNullOrEmpty(value))
			{
				if (resourceDictionary.ContainsKey(locale))
				{
					resourceDictionary.Remove(locale);
					return;
				}
			}
			else
			{
				if (resourceDictionary.ContainsKey(locale))
				{
					resourceDictionary[locale] = value;
					return;
				}
				resourceDictionary.Add(locale, value);
			}
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0012AC98 File Offset: 0x00128E98
		private static ClassificationTypeEnum Parse(string ruleElementName)
		{
			ExAssert.RetailAssert(!string.IsNullOrWhiteSpace(ruleElementName), "The rule element name being parsed into ClassificationTypeEnum cannot be null nor consists of white-spaces only");
			if (ruleElementName.Equals("Entity", StringComparison.Ordinal))
			{
				return ClassificationTypeEnum.Entity;
			}
			if (ruleElementName.Equals("Affinity", StringComparison.Ordinal))
			{
				return ClassificationTypeEnum.Affinity;
			}
			ExAssert.RetailAssert(false, "Invalid element name \"{0}\" being parsed into ClassificationTypeEnum", new object[]
			{
				ruleElementName
			});
			throw new ArgumentException(string.Empty, "ruleElementName");
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0012AD00 File Offset: 0x00128F00
		private static DataClassificationObjectId CreateDataClassificationIdentifier(string ruleIdentifier, ClassificationRuleCollectionPresentationObject rulePackPresentationObject)
		{
			if (rulePackPresentationObject != null && rulePackPresentationObject.Identity != null)
			{
				string text = rulePackPresentationObject.Identity.ToString();
				int num = text.LastIndexOf(ClassificationDefinitionConstants.HierarchicalIdentitySeparatorChar);
				if (num != -1)
				{
					string organizationHierarchy = text.Substring(0, num);
					return new DataClassificationObjectId(organizationHierarchy, ruleIdentifier);
				}
			}
			return new DataClassificationObjectId(ruleIdentifier);
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x0012AD4C File Offset: 0x00128F4C
		internal static DataClassificationPresentationObject Create(ClassificationRuleCollectionPresentationObject rulePackPresentationObject)
		{
			return new DataClassificationPresentationObject
			{
				defaultDetails = new DataClassificationLocalizableDetails(),
				localizedNames = new Dictionary<CultureInfo, string>(),
				localizedDescriptions = new Dictionary<CultureInfo, string>(),
				ClassificationRuleCollection = rulePackPresentationObject,
				Identity = DataClassificationPresentationObject.CreateDataClassificationIdentifier(Guid.NewGuid().ToString(), rulePackPresentationObject),
				ClassificationType = (rulePackPresentationObject.IsFingerprintRuleCollection ? ClassificationTypeEnum.Fingerprint : ClassificationTypeEnum.Affinity),
				RecommendedConfidence = new uint?(75U),
				MinEngineVersion = ClassificationDefinitionConstants.TextProcessorTypeToVersions[TextProcessorType.Fingerprint],
				IsDirty = true
			};
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x0012ADDE File Offset: 0x00128FDE
		internal static DataClassificationPresentationObject Create(string ruleIdentifier, XElement ruleElement, XElement resourceElement, ClassificationRuleCollectionPresentationObject rulePackPresentationObject)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ruleIdentifier", ruleIdentifier);
			ArgumentValidator.ThrowIfNull("ruleElement", ruleElement);
			ArgumentValidator.ThrowIfNull("resourceElement", resourceElement);
			return DataClassificationPresentationObject.Create(ruleIdentifier, XmlProcessingUtils.ReadDefaultRuleMetadata(resourceElement), ruleElement, resourceElement, rulePackPresentationObject);
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x0012AE10 File Offset: 0x00129010
		internal static DataClassificationPresentationObject Create(string ruleIdentifier, DataClassificationLocalizableDetails defaultRuleDetails, XElement ruleElement, XElement resourceElement, ClassificationRuleCollectionPresentationObject rulePackPresentationObject)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ruleIdentifier", ruleIdentifier);
			ArgumentValidator.ThrowIfNull("defaultRuleDetails", defaultRuleDetails);
			ArgumentValidator.ThrowIfNull("ruleElement", ruleElement);
			ArgumentValidator.ThrowIfNull("resourceElement", resourceElement);
			ArgumentValidator.ThrowIfNull("rulePackPresentationObject", rulePackPresentationObject);
			MultiValuedProperty<Fingerprint> multiValuedProperty = null;
			if (rulePackPresentationObject.IsFingerprintRuleCollection && ruleElement.Document != null)
			{
				multiValuedProperty = XmlProcessingUtils.ReadAllReferredFingerprints(ruleElement);
			}
			return new DataClassificationPresentationObject
			{
				defaultDetails = defaultRuleDetails,
				localizedNames = XmlProcessingUtils.ReadAllRuleNames(resourceElement),
				localizedDescriptions = XmlProcessingUtils.ReadAllRuleDescriptions(resourceElement),
				fingerprints = multiValuedProperty,
				Identity = DataClassificationPresentationObject.CreateDataClassificationIdentifier(ruleIdentifier, rulePackPresentationObject),
				ClassificationType = (rulePackPresentationObject.IsFingerprintRuleCollection ? ClassificationTypeEnum.Fingerprint : DataClassificationPresentationObject.Parse(ruleElement.Name.LocalName)),
				ClassificationRuleCollection = rulePackPresentationObject,
				RecommendedConfidence = XmlProcessingUtils.ReadRuleRecommendedConfidence(ruleElement),
				MinEngineVersion = XmlProcessingUtils.GetRulePackElementVersion(ruleElement)
			};
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x0012AEFC File Offset: 0x001290FC
		internal void Save(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			if (this.Fingerprints == null || this.Fingerprints.Count <= 0)
			{
				throw new DataClassificationFingerprintsMissingException(this.Name);
			}
			if (this.Fingerprints.Count((Fingerprint fingerprint) => string.IsNullOrEmpty(fingerprint.Description)) > 0)
			{
				throw new DataClassificationFingerprintsDescriptionMissingException(this.Name);
			}
			if (this.Fingerprints.Distinct(Fingerprint.Comparer).Count<Fingerprint>() != this.Fingerprints.Count)
			{
				throw new DataClassificationFingerprintsDuplicatedException(this.Name);
			}
			DataClassificationObjectId dataClassificationObjectId = this.Identity as DataClassificationObjectId;
			foreach (Fingerprint fingerprint2 in this.Fingerprints)
			{
				if (string.IsNullOrEmpty(fingerprint2.Identity))
				{
					fingerprint2.Identity = XmlProcessingUtils.AddFingerprintTextProcessor(rulePackXDoc, fingerprint2);
				}
			}
			XmlProcessingUtils.AddDataClassification(rulePackXDoc, dataClassificationObjectId.Name, this.MinEngineVersion.ToString(), this.GetRuleXElement());
			XmlProcessingUtils.AddLocalizedResource(rulePackXDoc, dataClassificationObjectId.Name, this.GetResourceXElement());
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x0012B038 File Offset: 0x00129238
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x0012B03B File Offset: 0x0012923B
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x0012B03E File Offset: 0x0012923E
		ValidationError[] IConfigurable.Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x0012B045 File Offset: 0x00129245
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x0012B047 File Offset: 0x00129247
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x04002C13 RID: 11283
		private Dictionary<CultureInfo, string> localizedNames;

		// Token: 0x04002C14 RID: 11284
		private Dictionary<CultureInfo, string> localizedDescriptions;

		// Token: 0x04002C15 RID: 11285
		private DataClassificationLocalizableDetails defaultDetails;

		// Token: 0x04002C16 RID: 11286
		private MultiValuedProperty<Fingerprint> fingerprints;
	}
}

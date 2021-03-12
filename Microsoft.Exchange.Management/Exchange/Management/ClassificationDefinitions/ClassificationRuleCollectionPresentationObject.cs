using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200082C RID: 2092
	[Serializable]
	public class ClassificationRuleCollectionPresentationObject : RulePresentationObjectBase
	{
		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x001296C1 File Offset: 0x001278C1
		public string RuleCollectionName
		{
			get
			{
				return this.defaultDetails.Name;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x001296CE File Offset: 0x001278CE
		public string LocalizedName
		{
			get
			{
				return ClassificationDefinitionUtils.GetMatchingLocalizedInfo<ClassificationRuleCollectionLocalizableDetails>(this.localizableDetails, this.defaultDetails).Name;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x001296E6 File Offset: 0x001278E6
		public string Description
		{
			get
			{
				return ClassificationDefinitionUtils.GetMatchingLocalizedInfo<ClassificationRuleCollectionLocalizableDetails>(this.localizableDetails, this.defaultDetails).Description;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x001296FE File Offset: 0x001278FE
		public string Publisher
		{
			get
			{
				return ClassificationDefinitionUtils.GetMatchingLocalizedInfo<ClassificationRuleCollectionLocalizableDetails>(this.localizableDetails, this.defaultDetails).PublisherName;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06004877 RID: 18551 RVA: 0x00129716 File Offset: 0x00127916
		// (set) Token: 0x06004878 RID: 18552 RVA: 0x0012971E File Offset: 0x0012791E
		public Version Version { get; private set; }

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x00129727 File Offset: 0x00127927
		// (set) Token: 0x0600487A RID: 18554 RVA: 0x0012972F File Offset: 0x0012792F
		public bool IsEncrypted { get; private set; }

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x00129738 File Offset: 0x00127938
		// (set) Token: 0x0600487C RID: 18556 RVA: 0x00129740 File Offset: 0x00127940
		public bool IsFingerprintRuleCollection { get; private set; }

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x00129749 File Offset: 0x00127949
		// (set) Token: 0x0600487E RID: 18558 RVA: 0x00129751 File Offset: 0x00127951
		private protected TransportRule StoredRuleCollection { protected get; private set; }

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x0012975A File Offset: 0x0012795A
		private new Guid Guid
		{
			get
			{
				return base.Guid;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x00129762 File Offset: 0x00127962
		private new Guid ImmutableId
		{
			get
			{
				return base.ImmutableId;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x0012976A File Offset: 0x0012796A
		internal CultureInfo DefaultCulture
		{
			get
			{
				return this.defaultDetails.Culture;
			}
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x00129777 File Offset: 0x00127977
		protected ClassificationRuleCollectionPresentationObject(TransportRule persistedRule) : base(persistedRule)
		{
			this.StoredRuleCollection = persistedRule;
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x00129788 File Offset: 0x00127988
		protected virtual void Initialize()
		{
			ExAssert.RetailAssert(this.StoredRuleCollection != null, "The stored transport rule instance for classification rule collection presentation object must not be null");
			XDocument ruleCollectionDocumentFromTransportRule = ClassificationDefinitionUtils.GetRuleCollectionDocumentFromTransportRule(this.StoredRuleCollection);
			this.Initialize(ruleCollectionDocumentFromTransportRule);
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x001297C0 File Offset: 0x001279C0
		protected virtual void Initialize(XDocument rulePackXDoc)
		{
			Version rulePackVersion = XmlProcessingUtils.GetRulePackVersion(rulePackXDoc);
			XElement rulePackageMetadataElement = XmlProcessingUtils.GetRulePackageMetadataElement(rulePackXDoc);
			bool isEncrypted = RulePackageDecrypter.IsRulePackageEncrypted(rulePackXDoc);
			this.Initialize(rulePackVersion, rulePackageMetadataElement, isEncrypted, XmlProcessingUtils.IsFingerprintRuleCollection(rulePackXDoc));
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x001297F1 File Offset: 0x001279F1
		protected virtual void Initialize(Version rulePackVersion, XElement rulePackageDetailsElement, bool isEncrypted, bool isFingerprintRuleCollection)
		{
			this.Version = rulePackVersion;
			this.defaultDetails = XmlProcessingUtils.ReadDefaultRulePackageMetadata(rulePackageDetailsElement);
			this.localizableDetails = XmlProcessingUtils.ReadAllRulePackageMetadata(rulePackageDetailsElement);
			this.IsEncrypted = isEncrypted;
			this.IsFingerprintRuleCollection = isFingerprintRuleCollection;
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x00129824 File Offset: 0x00127A24
		internal static ClassificationRuleCollectionPresentationObject Create(TransportRule transportRule, Version rulePackageVersion, XElement rulePackageDetailsElement, bool isEncrypted)
		{
			if (transportRule == null)
			{
				throw new ArgumentNullException("transportRule");
			}
			if (null == rulePackageVersion)
			{
				throw new ArgumentNullException("rulePackageVersion");
			}
			ClassificationRuleCollectionPresentationObject classificationRuleCollectionPresentationObject = new ClassificationRuleCollectionPresentationObject(transportRule);
			classificationRuleCollectionPresentationObject.Initialize(rulePackageVersion, rulePackageDetailsElement, isEncrypted, XmlProcessingUtils.IsFingerprintRuleCollection(rulePackageDetailsElement.Document));
			return classificationRuleCollectionPresentationObject;
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x00129870 File Offset: 0x00127A70
		internal static ClassificationRuleCollectionPresentationObject Create(TransportRule transportRule, XDocument rulePackXDoc)
		{
			if (transportRule == null)
			{
				throw new ArgumentNullException("transportRule");
			}
			ClassificationRuleCollectionPresentationObject classificationRuleCollectionPresentationObject = new ClassificationRuleCollectionPresentationObject(transportRule);
			classificationRuleCollectionPresentationObject.Initialize(rulePackXDoc);
			return classificationRuleCollectionPresentationObject;
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0012989C File Offset: 0x00127A9C
		internal static ClassificationRuleCollectionPresentationObject Create(TransportRule transportRule)
		{
			if (transportRule == null)
			{
				throw new ArgumentNullException("transportRule");
			}
			ClassificationRuleCollectionPresentationObject classificationRuleCollectionPresentationObject = new ClassificationRuleCollectionPresentationObject(transportRule);
			classificationRuleCollectionPresentationObject.Initialize();
			return classificationRuleCollectionPresentationObject;
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x001298C5 File Offset: 0x00127AC5
		public override ValidationError[] Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x001298CC File Offset: 0x00127ACC
		public override string ToString()
		{
			return this.RuleCollectionName;
		}

		// Token: 0x04002BFD RID: 11261
		private Dictionary<CultureInfo, ClassificationRuleCollectionLocalizableDetails> localizableDetails;

		// Token: 0x04002BFE RID: 11262
		private ClassificationRuleCollectionLocalizableDetails defaultDetails;
	}
}

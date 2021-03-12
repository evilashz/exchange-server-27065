using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Classification;
using Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C4 RID: 964
	internal class SerializerVisitor15 : Visitor15
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x0009B488 File Offset: 0x00099688
		private SerializerVisitor15()
		{
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0009B490 File Offset: 0x00099690
		internal static XmlElement Serialize(PolicyNudges15 policyNudges, bool hasChanged, CachedOrganizationConfiguration serverConfig, ADObjectId senderADObjectId, XmlDocument xmlDoc)
		{
			SerializerVisitor15 serializerVisitor = new SerializerVisitor15();
			serializerVisitor.serverConfig = serverConfig;
			serializerVisitor.senderADObjectId = senderADObjectId;
			serializerVisitor.xmlDoc = xmlDoc;
			serializerVisitor.isValidLocale = false;
			if (policyNudges != null)
			{
				serializerVisitor.outlookCultureTag = policyNudges.OutlookLocale;
				serializerVisitor.exchangeLocale = PolicyNudgeConfigurationUtils.GetExchangeLocaleFromOutlookCultureTag(serializerVisitor.outlookCultureTag);
				serializerVisitor.isValidLocale = (serializerVisitor.exchangeLocale != null);
				if (policyNudges.ClassificationItems != null && !string.IsNullOrEmpty(policyNudges.ClassificationItems.EngineVersion))
				{
					serializerVisitor.canOutlookSupportFullPnrXml = PolicyNudgeConfigurationUtils.CanOutlookSupportFullPnrXml(policyNudges.ClassificationItems.EngineVersion);
				}
			}
			serializerVisitor.hasChanged = (hasChanged || !serializerVisitor.isValidLocale);
			new PolicyNudges15().Accept(serializerVisitor);
			return serializerVisitor.returnElement;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0009B548 File Offset: 0x00099748
		internal override void Visit(PolicyNudges15 policyNudges)
		{
			this.returnElement = ServiceXml.CreateElement(this.xmlDoc, "PolicyNudgeRulesConfiguration", "http://schemas.microsoft.com/exchange/services/2006/messages");
			this.currentElements = new Stack<XmlElement>();
			this.currentElements.Push(this.returnElement);
			new PolicyNudgeRules15().Accept(this);
			new ClassificationItems15().Accept(this);
			this.currentElements.Pop();
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0009B5CC File Offset: 0x000997CC
		internal override void Visit(PolicyNudgeRules15 policyNudgeRules)
		{
			XmlElement xmlElement = this.xmlDoc.CreateElement("PolicyNudgeRules");
			if (this.hasChanged && this.isValidLocale)
			{
				foreach (PolicyNudgeRule policyNudgeRule in from rule in this.serverConfig.PolicyNudgeRules.Rules
				where rule.IsEnabled && (this.canOutlookSupportFullPnrXml || rule.IsPnrXmlValid)
				select rule)
				{
					PolicyNudgeConfigurationUtils.AdDistributionListResolver distributionListResolver = new PolicyNudgeConfigurationUtils.AdDistributionListResolver(this.serverConfig, this.senderADObjectId);
					PolicyNudgeConfigurationUtils.DataClassificationResolver dataClassificationResolver = new PolicyNudgeConfigurationUtils.DataClassificationResolver(this.serverConfig);
					PolicyNudgeRule.References references;
					string ruleXml = policyNudgeRule.GetRuleXml(this.exchangeLocale, new PolicyNudgeConfigurationUtils.AdMessageStrings(this.serverConfig, this.outlookCultureTag), distributionListResolver, dataClassificationResolver, this.canOutlookSupportFullPnrXml, out references);
					if (!string.IsNullOrEmpty(ruleXml))
					{
						XmlElement xmlElement2 = this.xmlDoc.CreateElement("PolicyNudgeRule");
						xmlElement2.SetAttribute("id", policyNudgeRule.ID);
						xmlElement2.SetAttribute("version", policyNudgeRule.Version.ToBinary().ToString());
						xmlElement2.InnerXml = ETRToPNRTranslator.Evaluate(ruleXml, distributionListResolver);
						OtherAttribtuesUtils.ApplyVersionedItems(xmlElement2, "messageString", references.Messages);
						OtherAttribtuesUtils.ApplyVersionedItems(xmlElement2, "distributionList", references.DistributionLists);
						xmlElement.AppendChild(xmlElement2);
					}
				}
			}
			PolicyNudgeConfigurationUtils.MarkElementAsApply(xmlElement, this.hasChanged);
			this.currentElements.Peek().AppendChild(xmlElement);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0009B764 File Offset: 0x00099964
		internal override void Visit(PolicyNudgeRule15 policyNudgeRule)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0009B76B File Offset: 0x0009996B
		internal override void Visit(ClassificationItems15 classificationItems)
		{
			new ClassificationDefinitions15().Accept(this);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0009B790 File Offset: 0x00099990
		internal override void Visit(ClassificationDefinitions15 classificationDefinitions)
		{
			XmlElement xmlElement = this.xmlDoc.CreateElement("ClassificationDefinitions");
			this.SerializeServerItems<ClassificationRulePackage>(xmlElement, "ClassificationDefinition", this.serverConfig.ClassificationDefinitions, (ClassificationRulePackage r) => r.ID, (ClassificationRulePackage r) => r.Version, (ClassificationRulePackage r) => r.RuleXml);
			this.currentElements.Peek().AppendChild(xmlElement);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0009B82E File Offset: 0x00099A2E
		internal override void Visit(ClassificationDefinition15 classificationDefinition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0009B838 File Offset: 0x00099A38
		private void SerializeServerItems<T>(XmlElement itemsElement, string itemElementName, IEnumerable<T> serverConfigItems, Func<T, string> id, Func<T, DateTime> version, Func<T, string> xml)
		{
			if (this.hasChanged && this.isValidLocale)
			{
				foreach (T arg in serverConfigItems)
				{
					XmlElement xmlElement = this.xmlDoc.CreateElement(itemElementName);
					xmlElement.SetAttribute("id", id(arg));
					xmlElement.SetAttribute("version", version(arg).ToBinary().ToString());
					xmlElement.InnerXml = xml(arg);
					itemsElement.AppendChild(xmlElement);
				}
			}
			PolicyNudgeConfigurationUtils.MarkElementAsApply(itemsElement, this.hasChanged);
		}

		// Token: 0x040011BF RID: 4543
		private bool hasChanged;

		// Token: 0x040011C0 RID: 4544
		private bool isValidLocale;

		// Token: 0x040011C1 RID: 4545
		private CachedOrganizationConfiguration serverConfig;

		// Token: 0x040011C2 RID: 4546
		private ADObjectId senderADObjectId;

		// Token: 0x040011C3 RID: 4547
		private string outlookCultureTag;

		// Token: 0x040011C4 RID: 4548
		private string exchangeLocale;

		// Token: 0x040011C5 RID: 4549
		private XmlDocument xmlDoc;

		// Token: 0x040011C6 RID: 4550
		private XmlElement returnElement;

		// Token: 0x040011C7 RID: 4551
		private Stack<XmlElement> currentElements;

		// Token: 0x040011C8 RID: 4552
		private bool canOutlookSupportFullPnrXml;
	}
}

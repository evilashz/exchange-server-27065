using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003C2 RID: 962
	internal class HasChangedVisitor15 : Visitor15
	{
		// Token: 0x06001B00 RID: 6912 RVA: 0x0009AF92 File Offset: 0x00099192
		private HasChangedVisitor15()
		{
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0009AF9C File Offset: 0x0009919C
		internal static bool HasChanged(PolicyNudges15 policyNudges, CachedOrganizationConfiguration serverConfig, ADObjectId senderADObjectId)
		{
			if (serverConfig == null || senderADObjectId == null)
			{
				throw new ArgumentNullException();
			}
			if (policyNudges == null)
			{
				return true;
			}
			HasChangedVisitor15 hasChangedVisitor = new HasChangedVisitor15();
			hasChangedVisitor.serverConfig = serverConfig;
			hasChangedVisitor.senderADObjectId = senderADObjectId;
			policyNudges.Accept(hasChangedVisitor);
			return hasChangedVisitor.hasChanged;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0009AFDC File Offset: 0x000991DC
		internal override void Visit(PolicyNudges15 policyNudges)
		{
			if (!this.EnsureValid(policyNudges.PolicyNudgeRules) || !this.EnsureValid(policyNudges.ClassificationItems))
			{
				return;
			}
			this.outlookCultureTag = policyNudges.OutlookLocale;
			this.exchangeLocale = PolicyNudgeConfigurationUtils.GetExchangeLocaleFromOutlookCultureTag(this.outlookCultureTag);
			if (policyNudges.ClassificationItems != null && !string.IsNullOrEmpty(policyNudges.ClassificationItems.EngineVersion))
			{
				this.canOutlookSupportFullPnrXml = PolicyNudgeConfigurationUtils.CanOutlookSupportFullPnrXml(policyNudges.ClassificationItems.EngineVersion);
			}
			if (!this.EnsureValid(this.exchangeLocale))
			{
				return;
			}
			policyNudges.PolicyNudgeRules.Accept(this);
			policyNudges.ClassificationItems.Accept(this);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0009B0FC File Offset: 0x000992FC
		internal override void Visit(PolicyNudgeRules15 policyNudgeRules)
		{
			if (this.hasChanged || !this.EnsureValid(policyNudgeRules.Rules))
			{
				return;
			}
			this.IdVersionItemsHasChanged<PolicyNudgeRule15>(policyNudgeRules.Rules, from r in this.serverConfig.PolicyNudgeRules.Rules
			where r.IsEnabled && (this.canOutlookSupportFullPnrXml || r.IsPnrXmlValid)
			select r);
			if (this.hasChanged)
			{
				return;
			}
			IList<IVersionedItem> clientMessages = new List<IVersionedItem>();
			IList<IVersionedItem> clientDistributionLists = new List<IVersionedItem>();
			if (!(from r in policyNudgeRules.Rules
			select OtherAttribtuesUtils.TryGetVersionedItemFromOtherAttributes(r, "messageString", clientMessages)).Any((bool v) => !v))
			{
				if (!(from r in policyNudgeRules.Rules
				select OtherAttribtuesUtils.TryGetVersionedItemFromOtherAttributes(r, "distributionList", clientDistributionLists)).Any((bool v) => !v))
				{
					List<PolicyNudgeRule.References> list = new List<PolicyNudgeRule.References>();
					foreach (PolicyNudgeRule policyNudgeRule in from r in this.serverConfig.PolicyNudgeRules.Rules
					where r.IsEnabled && (this.canOutlookSupportFullPnrXml || r.IsPnrXmlValid)
					select r)
					{
						PolicyNudgeRule.References item;
						policyNudgeRule.GetRuleXml(this.exchangeLocale, new PolicyNudgeConfigurationUtils.AdMessageStrings(this.serverConfig, this.outlookCultureTag), new PolicyNudgeConfigurationUtils.AdDistributionListResolver(this.serverConfig, this.senderADObjectId), new PolicyNudgeConfigurationUtils.DataClassificationResolver(this.serverConfig), this.canOutlookSupportFullPnrXml, out item);
						list.Add(item);
					}
					this.IdVersionItemsHasChanged<IVersionedItem>(clientMessages, list.SelectMany((PolicyNudgeRule.References r) => r.Messages));
					this.IdVersionItemsHasChanged<IVersionedItem>(clientDistributionLists, list.SelectMany((PolicyNudgeRule.References r) => r.DistributionLists));
					return;
				}
			}
			this.hasChanged = true;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0009B300 File Offset: 0x00099500
		internal override void Visit(PolicyNudgeRule15 policyNudgeRule)
		{
			this.EnsureValid(policyNudgeRule.ID);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0009B30F File Offset: 0x0009950F
		internal override void Visit(ClassificationItems15 classificationItems)
		{
			if (!this.EnsureValid(classificationItems.ClassificationDefinitions))
			{
				return;
			}
			classificationItems.ClassificationDefinitions.Accept(this);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0009B32C File Offset: 0x0009952C
		internal override void Visit(ClassificationDefinitions15 classificationDefinitions)
		{
			if (this.hasChanged || !this.EnsureValid(classificationDefinitions.ClassificationDefinitions))
			{
				return;
			}
			this.IdVersionItemsHasChanged<ClassificationDefinition15>(classificationDefinitions.ClassificationDefinitions, this.serverConfig.ClassificationDefinitions);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0009B35C File Offset: 0x0009955C
		internal override void Visit(ClassificationDefinition15 classificationDefinition)
		{
			this.EnsureValid(classificationDefinition.ID);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0009B3CC File Offset: 0x000995CC
		private void IdVersionItemsHasChanged<T>(IEnumerable<T> clientItems, IEnumerable<IVersionedItem> serverItems) where T : IVersionedItem
		{
			if (this.hasChanged)
			{
				return;
			}
			foreach (T t in clientItems)
			{
				if (t is IVisitee15)
				{
					((IVisitee15)((object)t)).Accept(this);
				}
			}
			IEnumerable<Tuple<T, IVersionedItem>> source = PolicyNudgeConfigurationUtils.OuterJoin<T, IVersionedItem, string>(clientItems, (T r) => r.ID, serverItems, (IVersionedItem r) => r.ID);
			this.hasChanged = source.Any(delegate(Tuple<T, IVersionedItem> i)
			{
				if (i.Item1 != null && i.Item2 != null)
				{
					T item = i.Item1;
					return item.Version != i.Item2.Version;
				}
				return true;
			});
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0009B46C File Offset: 0x0009966C
		private bool Ensure(bool b)
		{
			if (!b)
			{
				this.hasChanged = true;
			}
			return b;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0009B479 File Offset: 0x00099679
		private bool EnsureValid(object value)
		{
			return this.Ensure(value != null);
		}

		// Token: 0x040011B5 RID: 4533
		private CachedOrganizationConfiguration serverConfig;

		// Token: 0x040011B6 RID: 4534
		private ADObjectId senderADObjectId;

		// Token: 0x040011B7 RID: 4535
		private bool hasChanged;

		// Token: 0x040011B8 RID: 4536
		private string outlookCultureTag;

		// Token: 0x040011B9 RID: 4537
		private string exchangeLocale;

		// Token: 0x040011BA RID: 4538
		private bool canOutlookSupportFullPnrXml;
	}
}

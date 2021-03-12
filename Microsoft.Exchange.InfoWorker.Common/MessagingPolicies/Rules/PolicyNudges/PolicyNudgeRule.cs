using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges
{
	// Token: 0x02000179 RID: 377
	internal sealed class PolicyNudgeRule : IVersionedItem
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x0002BB6C File Offset: 0x00029D6C
		public PolicyNudgeRule(string etrXml, string id, DateTime version, RuleState state, DateTime? activationDate, DateTime? expiryDate)
		{
			this.etrXml = etrXml;
			this.ID = id;
			this.Version = version;
			this.state = state;
			this.activationDate = activationDate;
			this.expiryDate = expiryDate;
			this.IsPnrXmlValid = true;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002BC1C File Offset: 0x00029E1C
		public string GetRuleXml(string locale, ETRToPNRTranslator.IMessageStrings messageStrings, ETRToPNRTranslator.IDistributionListResolver distributionListResolver, ETRToPNRTranslator.IDataClassificationResolver dataClassificationResolver, bool needFullPnrXml, out PolicyNudgeRule.References references)
		{
			if (this.localeToPnrMap == null)
			{
				lock (this.etrXml)
				{
					if (this.localeToPnrMap == null)
					{
						this.localeToPnrMap = new Dictionary<string, PolicyNudgeRule.CacheEntry>();
					}
				}
			}
			PolicyNudgeRule.CacheEntry cacheEntry;
			if (this.localeToPnrMap.TryGetValue(locale, out cacheEntry))
			{
				references = new PolicyNudgeRule.References(cacheEntry.usedMessages, cacheEntry.usedDistributionLists.SelectMany((string dl) => distributionListResolver.Get(dl)));
				if (!needFullPnrXml)
				{
					return cacheEntry.PnrXml;
				}
				return cacheEntry.FullPnrXml;
			}
			else
			{
				List<PolicyTipMessage> usedMessagesList = new List<PolicyTipMessage>();
				List<string> usedDistributionListsList = new List<string>();
				ETRToPNRTranslator etrtoPNRTranslator = new ETRToPNRTranslator(this.etrXml, new ETRToPNRTranslator.MessageStringCallbackImpl(messageStrings.OutlookCultureTag, (ETRToPNRTranslator.OutlookActionTypes action) => PolicyNudgeRule.Track<PolicyTipMessage>(messageStrings.Get(action), usedMessagesList), () => PolicyNudgeRule.Track<PolicyTipMessage>(messageStrings.Url, usedMessagesList)), new ETRToPNRTranslator.DistributionListResolverCallbackImpl(delegate(string distributionList)
				{
					PolicyNudgeRule.Track<string>(distributionList, usedDistributionListsList);
					return null;
				}, (string distributionList) => distributionListResolver.IsMemberOf(distributionList)), dataClassificationResolver);
				string pnrXml = etrtoPNRTranslator.PnrXml;
				this.IsPnrXmlValid = !string.IsNullOrEmpty(pnrXml);
				string fullPnrXml = etrtoPNRTranslator.FullPnrXml;
				lock (this.localeToPnrMap)
				{
					if (!this.localeToPnrMap.ContainsKey(locale))
					{
						this.localeToPnrMap.Add(locale, new PolicyNudgeRule.CacheEntry
						{
							PnrXml = pnrXml,
							FullPnrXml = fullPnrXml,
							usedMessages = usedMessagesList.ToArray(),
							usedDistributionLists = usedDistributionListsList.ToArray()
						});
					}
				}
				references = new PolicyNudgeRule.References(usedMessagesList, usedDistributionListsList.SelectMany((string dl) => distributionListResolver.Get(dl)));
				if (!needFullPnrXml)
				{
					return pnrXml;
				}
				return fullPnrXml;
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002BE24 File Offset: 0x0002A024
		private static T Track<T>(T obj, IList<T> usedObjs)
		{
			usedObjs.Add(obj);
			return obj;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002BE2E File Offset: 0x0002A02E
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0002BE36 File Offset: 0x0002A036
		public string ID { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002BE3F File Offset: 0x0002A03F
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0002BE47 File Offset: 0x0002A047
		public DateTime Version { get; private set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002BE50 File Offset: 0x0002A050
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0002BE58 File Offset: 0x0002A058
		public bool IsPnrXmlValid { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002BE64 File Offset: 0x0002A064
		public bool IsEnabled
		{
			get
			{
				return this.state == RuleState.Enabled && (this.expiryDate == null || DateTime.UtcNow <= this.expiryDate.Value) && (this.activationDate == null || DateTime.UtcNow >= this.activationDate.Value);
			}
		}

		// Token: 0x040007DF RID: 2015
		private readonly string etrXml;

		// Token: 0x040007E0 RID: 2016
		private RuleState state;

		// Token: 0x040007E1 RID: 2017
		private DateTime? activationDate;

		// Token: 0x040007E2 RID: 2018
		private DateTime? expiryDate;

		// Token: 0x040007E3 RID: 2019
		private Dictionary<string, PolicyNudgeRule.CacheEntry> localeToPnrMap;

		// Token: 0x0200017A RID: 378
		private class CacheEntry
		{
			// Token: 0x040007E7 RID: 2023
			internal string PnrXml;

			// Token: 0x040007E8 RID: 2024
			internal string FullPnrXml;

			// Token: 0x040007E9 RID: 2025
			internal PolicyTipMessage[] usedMessages;

			// Token: 0x040007EA RID: 2026
			internal string[] usedDistributionLists;
		}

		// Token: 0x0200017B RID: 379
		internal sealed class References
		{
			// Token: 0x06000A34 RID: 2612 RVA: 0x0002BECB File Offset: 0x0002A0CB
			internal References(IEnumerable<PolicyTipMessage> messages, IEnumerable<IVersionedItem> distributionLists)
			{
				this.Messages = messages;
				this.DistributionLists = distributionLists;
			}

			// Token: 0x040007EB RID: 2027
			internal readonly IEnumerable<PolicyTipMessage> Messages;

			// Token: 0x040007EC RID: 2028
			internal readonly IEnumerable<IVersionedItem> DistributionLists;
		}
	}
}

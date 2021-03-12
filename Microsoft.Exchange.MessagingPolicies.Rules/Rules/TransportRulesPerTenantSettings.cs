using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000042 RID: 66
	internal class TransportRulesPerTenantSettings : TenantConfigurationCacheableItem<TransportRule>
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000E980 File Offset: 0x0000CB80
		protected virtual string RuleCollectionName
		{
			get
			{
				return "TransportVersioned";
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000E987 File Offset: 0x0000CB87
		protected virtual RuleParser Parser
		{
			get
			{
				return TransportRuleParser.Instance;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000E996 File Offset: 0x0000CB96
		public RuleCollection RuleCollection
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.ruleCollection;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000E9A5 File Offset: 0x0000CBA5
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.estimatedSize;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public override void ReadData(IConfigurationSession session, object state = null)
		{
			ADObjectId containerId = ADRuleStorageManager.GetContainerId(session, this.RuleCollectionName);
			IEnumerable<TransportRule> adTransportRules = session.FindPaged<TransportRule>(null, containerId, false, null, 0);
			RuleHealthMonitor ruleHealthMonitor = state as RuleHealthMonitor;
			this.ruleCollection = this.ParseRules(adTransportRules, ruleHealthMonitor);
			this.UpdateEstimatedSize();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		public override void ReadData(IConfigurationSession session)
		{
			this.ReadData(session, null);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E9FF File Offset: 0x0000CBFF
		private void UpdateEstimatedSize()
		{
			if (this.ruleCollection == null)
			{
				this.estimatedSize = 0L;
				return;
			}
			this.estimatedSize = (long)(18 + this.ruleCollection.GetEstimatedSize());
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000EA28 File Offset: 0x0000CC28
		protected virtual RuleCollection ParseRules(IEnumerable<TransportRule> adTransportRules, RuleHealthMonitor ruleHealthMonitor)
		{
			RuleCollection result;
			try
			{
				List<TransportRule> list = adTransportRules as List<TransportRule>;
				if (list == null)
				{
					list = new List<TransportRule>();
					foreach (TransportRule item in adTransportRules)
					{
						list.Add(item);
					}
				}
				list.Sort(new Comparison<TransportRule>(ADRuleStorageManager.CompareTransportRule));
				result = ADRuleStorageManager.ParseRuleCollection(this.RuleCollectionName, list, ruleHealthMonitor, this.Parser);
			}
			catch (ParserException ex)
			{
				throw new DataValidationException(new PropertyValidationError(ex.LocalizedString, TransportRuleSchema.Xml, null), ex);
			}
			return result;
		}

		// Token: 0x040001C0 RID: 448
		internal const int FixedObjectOverhead = 18;

		// Token: 0x040001C1 RID: 449
		protected RuleCollection ruleCollection;

		// Token: 0x040001C2 RID: 450
		private long estimatedSize;
	}
}

using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Compliance;
using Microsoft.Exchange.Data.Search.KqlParser;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200002F RID: 47
	internal sealed class InPlaceHoldConfiguration
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00009890 File Offset: 0x00007A90
		public InPlaceHoldConfiguration(MailboxDiscoverySearch searchObject)
		{
			this.Name = searchObject.Name;
			this.Identity = searchObject.InPlaceHoldIdentity;
			this.Enabled = searchObject.InPlaceHoldEnabled;
			this.IsValid = searchObject.IsValid;
			if (this.IsValid)
			{
				this.QueryString = searchObject.Query;
				this.QueryFilter = searchObject.InternalQueryFilter;
				if (!searchObject.ItemHoldPeriod.IsUnlimited)
				{
					QueryFilter retentionPeriodFilter = MailboxDiscoverySearch.GetRetentionPeriodFilter(searchObject.ItemHoldPeriod.Value);
					if (this.QueryFilter == null)
					{
						this.QueryFilter = retentionPeriodFilter;
						return;
					}
					this.QueryFilter = new AndFilter(new QueryFilter[]
					{
						retentionPeriodFilter,
						this.QueryFilter
					});
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009948 File Offset: 0x00007B48
		public InPlaceHoldConfiguration(PolicyDefinitionConfig definition, PolicyRuleConfig rule, RuleParser parser, Trace tracer)
		{
			this.Name = definition.Name;
			this.Identity = ExMailboxComplianceItemContainer.GetHoldId(definition.Identity);
			this.Enabled = (definition.Mode == Mode.Enforce);
			this.IsValid = false;
			PolicyRule rule2 = parser.GetRule(rule.RuleBlob);
			if (rule2 != null)
			{
				this.QueryString = string.Empty;
				string text = KqlHelpers.GenerateHoldKeepQuery(rule2, ExPropertyNameMapping.Mapping);
				if (string.IsNullOrEmpty(text))
				{
					this.QueryFilter = null;
				}
				else
				{
					this.QueryFilter = KqlParser.ParseAndBuildQuery(text, KqlParser.ParseOption.DisablePrefixMatch | KqlParser.ParseOption.AllowShortWildcards | KqlParser.ParseOption.EDiscoveryMode, CultureInfo.InvariantCulture, null, null);
				}
				this.IsValid = true;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000099E5 File Offset: 0x00007BE5
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000099ED File Offset: 0x00007BED
		public string Name { get; internal set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000099F6 File Offset: 0x00007BF6
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000099FE File Offset: 0x00007BFE
		public string Identity { get; internal set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00009A07 File Offset: 0x00007C07
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00009A0F File Offset: 0x00007C0F
		public bool Enabled { get; internal set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00009A18 File Offset: 0x00007C18
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00009A20 File Offset: 0x00007C20
		public string QueryString { get; internal set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00009A29 File Offset: 0x00007C29
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00009A31 File Offset: 0x00007C31
		public QueryFilter QueryFilter { get; internal set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00009A3A File Offset: 0x00007C3A
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00009A42 File Offset: 0x00007C42
		public bool IsValid { get; internal set; }
	}
}

using System;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000586 RID: 1414
	[XmlType(TypeName = "RuleType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class EwsRule
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000A7080 File Offset: 0x000A5280
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000A7088 File Offset: 0x000A5288
		[XmlElement(Order = 0)]
		public string RuleId { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000A7091 File Offset: 0x000A5291
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x000A7099 File Offset: 0x000A5299
		[XmlIgnore]
		public bool RuleIdSpecified { get; set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x000A70A2 File Offset: 0x000A52A2
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x000A70AA File Offset: 0x000A52AA
		[XmlElement(Order = 1)]
		public string DisplayName { get; set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x000A70B3 File Offset: 0x000A52B3
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x000A70BB File Offset: 0x000A52BB
		[XmlElement(Order = 2)]
		public int Priority { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x000A70C4 File Offset: 0x000A52C4
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x000A70CC File Offset: 0x000A52CC
		[XmlElement(Order = 3)]
		public bool IsEnabled { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000A70D5 File Offset: 0x000A52D5
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x000A70DD File Offset: 0x000A52DD
		[XmlElement(Order = 4)]
		public bool IsNotSupported { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x000A70E6 File Offset: 0x000A52E6
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x000A70EE File Offset: 0x000A52EE
		[XmlIgnore]
		public bool IsNotSupportedSpecified { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x000A70F7 File Offset: 0x000A52F7
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000A70FF File Offset: 0x000A52FF
		[XmlElement(Order = 5)]
		public bool IsInError { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000A7108 File Offset: 0x000A5308
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x000A7110 File Offset: 0x000A5310
		[XmlIgnore]
		public bool IsInErrorSpecified { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000A7119 File Offset: 0x000A5319
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x000A7121 File Offset: 0x000A5321
		[XmlElement(Order = 6)]
		public RulePredicates Conditions { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000A712A File Offset: 0x000A532A
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x000A7132 File Offset: 0x000A5332
		[XmlElement(Order = 7)]
		public RulePredicates Exceptions { get; set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000A713B File Offset: 0x000A533B
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x000A7143 File Offset: 0x000A5343
		[XmlElement(Order = 8)]
		public RuleActions Actions { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x000A714C File Offset: 0x000A534C
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x000A7154 File Offset: 0x000A5354
		[XmlIgnore]
		internal Rule ServerRule { get; set; }

		// Token: 0x0600274F RID: 10063 RVA: 0x000A7160 File Offset: 0x000A5360
		internal static EwsRule CreateFromServerRule(Rule serverRule, int hashCode, MailboxSession session, CultureInfo clientCulture)
		{
			EwsRule ewsRule = new EwsRule();
			ewsRule.RuleId = serverRule.Id.ToBase64String();
			ewsRule.RuleIdSpecified = true;
			ewsRule.DisplayName = serverRule.Name;
			ewsRule.Priority = serverRule.Sequence - 10 + 1;
			ewsRule.IsEnabled = serverRule.IsEnabled;
			ewsRule.IsNotSupported = serverRule.IsNotSupported;
			ewsRule.IsInError = serverRule.IsParameterInError;
			if (ewsRule.IsInError)
			{
				ewsRule.IsInErrorSpecified = true;
			}
			if (!ewsRule.IsNotSupported && 0 < serverRule.Conditions.Count)
			{
				ewsRule.Conditions = RulePredicates.CreateFromServerRuleConditions(serverRule.Conditions, ewsRule, hashCode, clientCulture);
			}
			if (!ewsRule.IsNotSupported && 0 < serverRule.Exceptions.Count)
			{
				ewsRule.Exceptions = RulePredicates.CreateFromServerRuleConditions(serverRule.Exceptions, ewsRule, hashCode, clientCulture);
			}
			if (!ewsRule.IsNotSupported && 0 < serverRule.Actions.Count)
			{
				ewsRule.Actions = RuleActions.CreateFromServerRuleActions(serverRule.Actions, ewsRule, hashCode, session);
			}
			if (ewsRule.IsNotSupported)
			{
				ewsRule.IsNotSupportedSpecified = true;
			}
			return ewsRule;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000A7269 File Offset: 0x000A5469
		internal bool HasActions()
		{
			return this.Actions != null && this.Actions.HasActions();
		}

		// Token: 0x040018D7 RID: 6359
		internal const int BaseSequence = 10;
	}
}

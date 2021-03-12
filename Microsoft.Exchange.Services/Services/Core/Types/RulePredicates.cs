using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000593 RID: 1427
	[XmlType(TypeName = "RulePredicatesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RulePredicates
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x000AABB3 File Offset: 0x000A8DB3
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x000AABBB File Offset: 0x000A8DBB
		[XmlArrayItem("String", Type = typeof(string))]
		[XmlArray(Order = 0)]
		public string[] Categories { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000AABC4 File Offset: 0x000A8DC4
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x000AABCC File Offset: 0x000A8DCC
		[XmlArray(Order = 1)]
		[XmlArrayItem("String", Type = typeof(string))]
		public string[] ContainsBodyStrings { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x000AABD5 File Offset: 0x000A8DD5
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x000AABDD File Offset: 0x000A8DDD
		[XmlArrayItem("String", Type = typeof(string))]
		[XmlArray(Order = 2)]
		public string[] ContainsHeaderStrings { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000AABE6 File Offset: 0x000A8DE6
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x000AABEE File Offset: 0x000A8DEE
		[XmlArrayItem("String", Type = typeof(string))]
		[XmlArray(Order = 3)]
		public string[] ContainsRecipientStrings { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000AABF7 File Offset: 0x000A8DF7
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x000AABFF File Offset: 0x000A8DFF
		[XmlArrayItem("String", Type = typeof(string))]
		[XmlArray(Order = 4)]
		public string[] ContainsSenderStrings { get; set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000AAC08 File Offset: 0x000A8E08
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x000AAC10 File Offset: 0x000A8E10
		[XmlArray(Order = 5)]
		[XmlArrayItem("String", Type = typeof(string))]
		public string[] ContainsSubjectOrBodyStrings { get; set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x000AAC19 File Offset: 0x000A8E19
		// (set) Token: 0x060027E3 RID: 10211 RVA: 0x000AAC21 File Offset: 0x000A8E21
		[XmlArray(Order = 6)]
		[XmlArrayItem("String", Type = typeof(string))]
		public string[] ContainsSubjectStrings { get; set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000AAC2A File Offset: 0x000A8E2A
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x000AAC32 File Offset: 0x000A8E32
		[XmlElement(Order = 7)]
		public FlaggedForAction FlaggedForAction { get; set; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000AAC3B File Offset: 0x000A8E3B
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x000AAC43 File Offset: 0x000A8E43
		[XmlIgnore]
		public bool FlaggedForActionSpecified { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000AAC4C File Offset: 0x000A8E4C
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x000AAC54 File Offset: 0x000A8E54
		[XmlArray(Order = 8)]
		[XmlArrayItem("Address", Type = typeof(EmailAddressWrapper))]
		public EmailAddressWrapper[] FromAddresses { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x000AAC5D File Offset: 0x000A8E5D
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x000AAC65 File Offset: 0x000A8E65
		[XmlArray(Order = 9)]
		[XmlArrayItem("String", Type = typeof(string))]
		public string[] FromConnectedAccounts { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000AAC6E File Offset: 0x000A8E6E
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x000AAC76 File Offset: 0x000A8E76
		[XmlElement(Order = 10)]
		public bool HasAttachments { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x000AAC7F File Offset: 0x000A8E7F
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x000AAC87 File Offset: 0x000A8E87
		[XmlIgnore]
		public bool HasAttachmentsSpecified { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x000AAC90 File Offset: 0x000A8E90
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x000AAC98 File Offset: 0x000A8E98
		[XmlElement(Order = 11)]
		public ImportanceType Importance { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x000AACA1 File Offset: 0x000A8EA1
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x000AACA9 File Offset: 0x000A8EA9
		[XmlIgnore]
		public bool ImportanceSpecified { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x000AACB2 File Offset: 0x000A8EB2
		// (set) Token: 0x060027F5 RID: 10229 RVA: 0x000AACBA File Offset: 0x000A8EBA
		[XmlElement(Order = 12)]
		public bool IsApprovalRequest { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000AACC3 File Offset: 0x000A8EC3
		// (set) Token: 0x060027F7 RID: 10231 RVA: 0x000AACCB File Offset: 0x000A8ECB
		[XmlIgnore]
		public bool IsApprovalRequestSpecified { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x000AACD4 File Offset: 0x000A8ED4
		// (set) Token: 0x060027F9 RID: 10233 RVA: 0x000AACDC File Offset: 0x000A8EDC
		[XmlElement(Order = 13)]
		public bool IsAutomaticForward { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x000AACE5 File Offset: 0x000A8EE5
		// (set) Token: 0x060027FB RID: 10235 RVA: 0x000AACED File Offset: 0x000A8EED
		[XmlIgnore]
		public bool IsAutomaticForwardSpecified { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000AACF6 File Offset: 0x000A8EF6
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x000AACFE File Offset: 0x000A8EFE
		[XmlElement(Order = 14)]
		public bool IsAutomaticReply { get; set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x000AAD07 File Offset: 0x000A8F07
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x000AAD0F File Offset: 0x000A8F0F
		[XmlIgnore]
		public bool IsAutomaticReplySpecified { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x000AAD18 File Offset: 0x000A8F18
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x000AAD20 File Offset: 0x000A8F20
		[XmlElement(Order = 15)]
		public bool IsEncrypted { get; set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000AAD29 File Offset: 0x000A8F29
		// (set) Token: 0x06002803 RID: 10243 RVA: 0x000AAD31 File Offset: 0x000A8F31
		[XmlIgnore]
		public bool IsEncryptedSpecified { get; set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x000AAD3A File Offset: 0x000A8F3A
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x000AAD42 File Offset: 0x000A8F42
		[XmlElement(Order = 16)]
		public bool IsMeetingRequest { get; set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x000AAD4B File Offset: 0x000A8F4B
		// (set) Token: 0x06002807 RID: 10247 RVA: 0x000AAD53 File Offset: 0x000A8F53
		[XmlIgnore]
		public bool IsMeetingRequestSpecified { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x000AAD5C File Offset: 0x000A8F5C
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x000AAD64 File Offset: 0x000A8F64
		[XmlElement(Order = 17)]
		public bool IsMeetingResponse { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x000AAD6D File Offset: 0x000A8F6D
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x000AAD75 File Offset: 0x000A8F75
		[XmlIgnore]
		public bool IsMeetingResponseSpecified { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x000AAD7E File Offset: 0x000A8F7E
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x000AAD86 File Offset: 0x000A8F86
		[XmlElement(Order = 18)]
		public bool IsNDR { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x000AAD8F File Offset: 0x000A8F8F
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x000AAD97 File Offset: 0x000A8F97
		[XmlIgnore]
		public bool IsNDRSpecified { get; set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000AADA0 File Offset: 0x000A8FA0
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x000AADA8 File Offset: 0x000A8FA8
		[XmlElement(Order = 19)]
		public bool IsPermissionControlled { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000AADB1 File Offset: 0x000A8FB1
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x000AADB9 File Offset: 0x000A8FB9
		[XmlIgnore]
		public bool IsPermissionControlledSpecified { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000AADC2 File Offset: 0x000A8FC2
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x000AADCA File Offset: 0x000A8FCA
		[XmlElement(Order = 20)]
		public bool IsReadReceipt { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000AADD3 File Offset: 0x000A8FD3
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x000AADDB File Offset: 0x000A8FDB
		[XmlIgnore]
		public bool IsReadReceiptSpecified { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x000AADE4 File Offset: 0x000A8FE4
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x000AADEC File Offset: 0x000A8FEC
		[XmlElement(Order = 21)]
		public bool IsSigned { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x000AADF5 File Offset: 0x000A8FF5
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x000AADFD File Offset: 0x000A8FFD
		[XmlIgnore]
		public bool IsSignedSpecified { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x000AAE06 File Offset: 0x000A9006
		// (set) Token: 0x0600281D RID: 10269 RVA: 0x000AAE0E File Offset: 0x000A900E
		[XmlElement(Order = 22)]
		public bool IsVoicemail { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x000AAE17 File Offset: 0x000A9017
		// (set) Token: 0x0600281F RID: 10271 RVA: 0x000AAE1F File Offset: 0x000A901F
		[XmlIgnore]
		public bool IsVoicemailSpecified { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x000AAE28 File Offset: 0x000A9028
		// (set) Token: 0x06002821 RID: 10273 RVA: 0x000AAE30 File Offset: 0x000A9030
		[XmlArrayItem("String", Type = typeof(string))]
		[XmlArray(Order = 23)]
		public string[] ItemClasses { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x000AAE39 File Offset: 0x000A9039
		// (set) Token: 0x06002823 RID: 10275 RVA: 0x000AAE41 File Offset: 0x000A9041
		[XmlArray(Order = 24)]
		[XmlArrayItem("String", Type = typeof(string))]
		public string[] MessageClassifications { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x000AAE4A File Offset: 0x000A904A
		// (set) Token: 0x06002825 RID: 10277 RVA: 0x000AAE52 File Offset: 0x000A9052
		[XmlElement(Order = 25)]
		public bool NotSentToMe { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x000AAE5B File Offset: 0x000A905B
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x000AAE63 File Offset: 0x000A9063
		[XmlIgnore]
		public bool NotSentToMeSpecified { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000AAE6C File Offset: 0x000A906C
		// (set) Token: 0x06002829 RID: 10281 RVA: 0x000AAE74 File Offset: 0x000A9074
		[XmlElement(Order = 26)]
		public bool SentCcMe { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000AAE7D File Offset: 0x000A907D
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x000AAE85 File Offset: 0x000A9085
		[XmlIgnore]
		public bool SentCcMeSpecified { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000AAE8E File Offset: 0x000A908E
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x000AAE96 File Offset: 0x000A9096
		[XmlElement(Order = 27)]
		public bool SentOnlyToMe { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000AAE9F File Offset: 0x000A909F
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x000AAEA7 File Offset: 0x000A90A7
		[XmlIgnore]
		public bool SentOnlyToMeSpecified { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000AAEB0 File Offset: 0x000A90B0
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x000AAEB8 File Offset: 0x000A90B8
		[XmlArrayItem("Address", Type = typeof(EmailAddressWrapper))]
		[XmlArray(Order = 28)]
		public EmailAddressWrapper[] SentToAddresses { get; set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x000AAEC1 File Offset: 0x000A90C1
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x000AAEC9 File Offset: 0x000A90C9
		[XmlElement(Order = 29)]
		public bool SentToMe { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x000AAED2 File Offset: 0x000A90D2
		// (set) Token: 0x06002835 RID: 10293 RVA: 0x000AAEDA File Offset: 0x000A90DA
		[XmlIgnore]
		public bool SentToMeSpecified { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000AAEE3 File Offset: 0x000A90E3
		// (set) Token: 0x06002837 RID: 10295 RVA: 0x000AAEEB File Offset: 0x000A90EB
		[XmlElement(Order = 30)]
		public bool SentToOrCcMe { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x000AAEF4 File Offset: 0x000A90F4
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x000AAEFC File Offset: 0x000A90FC
		[XmlIgnore]
		public bool SentToOrCcMeSpecified { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000AAF05 File Offset: 0x000A9105
		// (set) Token: 0x0600283B RID: 10299 RVA: 0x000AAF0D File Offset: 0x000A910D
		[XmlElement(Order = 31)]
		public SensitivityType Sensitivity { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600283C RID: 10300 RVA: 0x000AAF16 File Offset: 0x000A9116
		// (set) Token: 0x0600283D RID: 10301 RVA: 0x000AAF1E File Offset: 0x000A911E
		[XmlIgnore]
		public bool SensitivitySpecified { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x000AAF27 File Offset: 0x000A9127
		// (set) Token: 0x0600283F RID: 10303 RVA: 0x000AAF2F File Offset: 0x000A912F
		[XmlElement(Order = 32)]
		public RulePredicateDateRange WithinDateRange { get; set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000AAF38 File Offset: 0x000A9138
		// (set) Token: 0x06002841 RID: 10305 RVA: 0x000AAF40 File Offset: 0x000A9140
		[XmlElement(Order = 33)]
		public RulePredicateSizeRange WithinSizeRange { get; set; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000AAF49 File Offset: 0x000A9149
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x000AAF51 File Offset: 0x000A9151
		[XmlIgnore]
		internal EwsRule Rule { get; set; }

		// Token: 0x06002844 RID: 10308 RVA: 0x000AAF5A File Offset: 0x000A915A
		public RulePredicates()
		{
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000AAF62 File Offset: 0x000A9162
		private RulePredicates(EwsRule rule)
		{
			this.Rule = rule;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000AAF74 File Offset: 0x000A9174
		internal static RulePredicates CreateFromServerRuleConditions(IList<Condition> serverConditions, EwsRule rule, int hashCode, CultureInfo clientCulture)
		{
			RulePredicates rulePredicates = new RulePredicates(rule);
			foreach (Condition condition in serverConditions)
			{
				switch (condition.ConditionType)
				{
				case ConditionType.FromRecipientsCondition:
					rulePredicates.FromAddresses = ParticipantsAddressesConverter.ToAddresses(((FromRecipientsCondition)condition).Participants);
					continue;
				case ConditionType.ContainsSubjectStringCondition:
					rulePredicates.ContainsSubjectStrings = ((ContainsSubjectStringCondition)condition).Text;
					continue;
				case ConditionType.SentOnlyToMeCondition:
					rulePredicates.SentOnlyToMe = true;
					rulePredicates.SentOnlyToMeSpecified = true;
					continue;
				case ConditionType.SentToMeCondition:
					rulePredicates.SentToMe = true;
					rulePredicates.SentToMeSpecified = true;
					continue;
				case ConditionType.MarkedAsImportanceCondition:
					rulePredicates.Importance = (ImportanceType)((MarkedAsImportanceCondition)condition).Importance;
					rulePredicates.ImportanceSpecified = true;
					continue;
				case ConditionType.MarkedAsSensitivityCondition:
					rulePredicates.Sensitivity = (SensitivityType)((MarkedAsSensitivityCondition)condition).Sensitivity;
					rulePredicates.SensitivitySpecified = true;
					continue;
				case ConditionType.SentCcMeCondition:
					rulePredicates.SentCcMe = true;
					rulePredicates.SentCcMeSpecified = true;
					continue;
				case ConditionType.SentToOrCcMeCondition:
					rulePredicates.SentToOrCcMe = true;
					rulePredicates.SentToOrCcMeSpecified = true;
					continue;
				case ConditionType.NotSentToMeCondition:
					rulePredicates.NotSentToMe = true;
					rulePredicates.NotSentToMeSpecified = true;
					continue;
				case ConditionType.SentToRecipientsCondition:
					rulePredicates.SentToAddresses = ParticipantsAddressesConverter.ToAddresses(((SentToRecipientsCondition)condition).Participants);
					continue;
				case ConditionType.ContainsBodyStringCondition:
					rulePredicates.ContainsBodyStrings = ((ContainsBodyStringCondition)condition).Text;
					continue;
				case ConditionType.ContainsSubjectOrBodyStringCondition:
					rulePredicates.ContainsSubjectOrBodyStrings = ((ContainsSubjectOrBodyStringCondition)condition).Text;
					continue;
				case ConditionType.ContainsHeaderStringCondition:
					rulePredicates.ContainsHeaderStrings = ((ContainsHeaderStringCondition)condition).Text;
					continue;
				case ConditionType.ContainsSenderStringCondition:
					rulePredicates.ContainsSenderStrings = ((ContainsSenderStringCondition)condition).Text;
					continue;
				case ConditionType.MarkedAsOofCondition:
					rulePredicates.IsAutomaticReply = true;
					rulePredicates.IsAutomaticReplySpecified = true;
					continue;
				case ConditionType.HasAttachmentCondition:
					rulePredicates.HasAttachments = true;
					rulePredicates.HasAttachmentsSpecified = true;
					continue;
				case ConditionType.WithinSizeRangeCondition:
				{
					WithinSizeRangeCondition withinSizeRangeCondition = (WithinSizeRangeCondition)condition;
					rulePredicates.WithinSizeRange = new RulePredicateSizeRange(withinSizeRangeCondition.RangeLow, withinSizeRangeCondition.RangeHigh);
					continue;
				}
				case ConditionType.WithinDateRangeCondition:
				{
					WithinDateRangeCondition withinDateRangeCondition = (WithinDateRangeCondition)condition;
					rulePredicates.WithinDateRange = new RulePredicateDateRange(withinDateRangeCondition.RangeLow, withinDateRangeCondition.RangeHigh);
					continue;
				}
				case ConditionType.MeetingMessageCondition:
					rulePredicates.IsMeetingRequest = true;
					rulePredicates.IsMeetingRequestSpecified = true;
					continue;
				case ConditionType.ContainsRecipientStringCondition:
					rulePredicates.ContainsRecipientStrings = ((ContainsRecipientStringCondition)condition).Text;
					continue;
				case ConditionType.AssignedCategoriesCondition:
					rulePredicates.Categories = ((AssignedCategoriesCondition)condition).Text;
					continue;
				case ConditionType.FormsCondition:
					rulePredicates.ItemClasses = ((FormsCondition)condition).Text;
					continue;
				case ConditionType.MessageClassificationCondition:
					rulePredicates.MessageClassifications = ((MessageClassificationCondition)condition).Text;
					continue;
				case ConditionType.NdrCondition:
					rulePredicates.IsNDR = true;
					rulePredicates.IsNDRSpecified = true;
					continue;
				case ConditionType.AutomaticForwardCondition:
					rulePredicates.IsAutomaticForward = true;
					rulePredicates.IsAutomaticForwardSpecified = true;
					continue;
				case ConditionType.EncryptedCondition:
					rulePredicates.IsEncrypted = true;
					rulePredicates.IsEncryptedSpecified = true;
					continue;
				case ConditionType.SignedCondition:
					rulePredicates.IsSigned = true;
					rulePredicates.IsSignedSpecified = true;
					continue;
				case ConditionType.ReadReceiptCondition:
					rulePredicates.IsReadReceipt = true;
					rulePredicates.IsReadReceiptSpecified = true;
					continue;
				case ConditionType.MeetingResponseCondition:
					rulePredicates.IsMeetingResponse = true;
					rulePredicates.IsMeetingResponseSpecified = true;
					continue;
				case ConditionType.PermissionControlledCondition:
					rulePredicates.IsPermissionControlled = true;
					rulePredicates.IsPermissionControlledSpecified = true;
					continue;
				case ConditionType.ApprovalRequestCondition:
					rulePredicates.IsApprovalRequest = true;
					rulePredicates.IsApprovalRequestSpecified = true;
					continue;
				case ConditionType.VoicemailCondition:
					rulePredicates.IsVoicemail = true;
					rulePredicates.IsVoicemailSpecified = true;
					continue;
				case ConditionType.FlaggedForActionCondition:
				{
					string action = ((FlaggedForActionCondition)condition).Action;
					if (string.Equals(action, RequestedAction.Any.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						rulePredicates.FlaggedForAction = FlaggedForAction.Any;
						rulePredicates.FlaggedForActionSpecified = true;
						continue;
					}
					int num = (clientCulture != null) ? FlaggedForActionCondition.GetRequestedActionLocalizedStringEnumIndex(action, clientCulture) : FlaggedForActionCondition.GetRequestedActionLocalizedStringEnumIndex(action);
					if (num >= 0)
					{
						rulePredicates.FlaggedForAction = (FlaggedForAction)num;
						rulePredicates.FlaggedForActionSpecified = true;
						continue;
					}
					ExTraceGlobals.GetInboxRulesCallTracer.TraceError<ConditionType>((long)hashCode, "UnsupportedPredicateType={0};", condition.ConditionType);
					rule.IsNotSupported = true;
					return null;
				}
				case ConditionType.FromSubscriptionCondition:
				{
					Guid[] guids = ((FromSubscriptionCondition)condition).Guids;
					string[] array = new string[guids.Length];
					for (int i = 0; i < guids.Length; i++)
					{
						array[i] = GuidConverter.ToString(guids[i]);
					}
					rulePredicates.FromConnectedAccounts = array;
					continue;
				}
				}
				ExTraceGlobals.GetInboxRulesCallTracer.TraceError<ConditionType>((long)hashCode, "UnsupportedPredicateType={0};", condition.ConditionType);
				rule.IsNotSupported = true;
				return null;
			}
			return rulePredicates;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000AB440 File Offset: 0x000A9640
		internal bool SpecifiedPredicates()
		{
			return (this.Categories != null && 0 < this.Categories.Length) || (this.ContainsBodyStrings != null && 0 < this.ContainsBodyStrings.Length) || (this.ContainsHeaderStrings != null && 0 < this.ContainsHeaderStrings.Length) || (this.ContainsRecipientStrings != null && 0 < this.ContainsRecipientStrings.Length) || (this.ContainsSenderStrings != null && 0 < this.ContainsSenderStrings.Length) || (this.ContainsSubjectOrBodyStrings != null && 0 < this.ContainsSubjectOrBodyStrings.Length) || (this.ContainsSubjectStrings != null && 0 < this.ContainsSubjectStrings.Length) || this.FlaggedForActionSpecified || (this.FromAddresses != null && 0 < this.FromAddresses.Length) || (this.FromConnectedAccounts != null && 0 < this.FromConnectedAccounts.Length) || this.HasAttachmentsSpecified || this.ImportanceSpecified || this.IsApprovalRequestSpecified || this.IsAutomaticForwardSpecified || this.IsAutomaticReplySpecified || this.IsEncryptedSpecified || this.IsMeetingRequestSpecified || this.IsMeetingResponseSpecified || this.IsNDRSpecified || this.IsPermissionControlledSpecified || this.IsReadReceiptSpecified || this.IsSignedSpecified || this.IsVoicemailSpecified || (this.ItemClasses != null && 0 < this.ItemClasses.Length) || (this.MessageClassifications != null && 0 < this.MessageClassifications.Length) || this.NotSentToMeSpecified || this.SentCcMeSpecified || this.SentOnlyToMeSpecified || (this.SentToAddresses != null && 0 < this.SentToAddresses.Length) || this.SentToMeSpecified || this.SentToOrCcMeSpecified || this.SensitivitySpecified || this.WithinDateRange != null || this.WithinSizeRange != null;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public sealed class InboxRule : XsoMailboxConfigurationObject
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00023133 File Offset: 0x00021333
		public RuleDescription Description
		{
			get
			{
				return this.BuildDescription();
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002313B File Offset: 0x0002133B
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0002314D File Offset: 0x0002134D
		public bool Enabled
		{
			get
			{
				return (bool)this[InboxRuleSchema.Enabled];
			}
			internal set
			{
				this[InboxRuleSchema.Enabled] = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00023160 File Offset: 0x00021360
		public override ObjectId Identity
		{
			get
			{
				return (ObjectId)this[InboxRuleSchema.Identity];
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00023172 File Offset: 0x00021372
		public bool InError
		{
			get
			{
				return this.propertiesInError != null;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00023180 File Offset: 0x00021380
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00023192 File Offset: 0x00021392
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string Name
		{
			get
			{
				return (string)this[InboxRuleSchema.Name];
			}
			set
			{
				this[InboxRuleSchema.Name] = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000231A0 File Offset: 0x000213A0
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000231B2 File Offset: 0x000213B2
		[Parameter]
		public int Priority
		{
			get
			{
				return (int)this[InboxRuleSchema.Priority];
			}
			set
			{
				this[InboxRuleSchema.Priority] = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000231C5 File Offset: 0x000213C5
		public ulong? RuleIdentity
		{
			get
			{
				return (ulong?)this[InboxRuleSchema.RuleIdentity];
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000231D7 File Offset: 0x000213D7
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000231E9 File Offset: 0x000213E9
		public bool SupportedByTask
		{
			get
			{
				return (bool)this[InboxRuleSchema.SupportedByTask];
			}
			internal set
			{
				this[InboxRuleSchema.SupportedByTask] = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000231FC File Offset: 0x000213FC
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0002320E File Offset: 0x0002140E
		[Parameter]
		public MultiValuedProperty<string> BodyContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.BodyContainsWords];
			}
			set
			{
				this[InboxRuleSchema.BodyContainsWords] = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0002321C File Offset: 0x0002141C
		// (set) Token: 0x060007FE RID: 2046 RVA: 0x0002322E File Offset: 0x0002142E
		[Parameter]
		public MultiValuedProperty<string> ExceptIfBodyContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ExceptIfBodyContainsWords];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfBodyContainsWords] = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002323C File Offset: 0x0002143C
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x0002324E File Offset: 0x0002144E
		[Parameter]
		public string FlaggedForAction
		{
			get
			{
				return (string)this[InboxRuleSchema.FlaggedForAction];
			}
			set
			{
				this[InboxRuleSchema.FlaggedForAction] = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002325C File Offset: 0x0002145C
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x0002326E File Offset: 0x0002146E
		[Parameter]
		public string ExceptIfFlaggedForAction
		{
			get
			{
				return (string)this[InboxRuleSchema.ExceptIfFlaggedForAction];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfFlaggedForAction] = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002327C File Offset: 0x0002147C
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x0002328E File Offset: 0x0002148E
		[Parameter]
		public MultiValuedProperty<string> FromAddressContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.FromAddressContainsWords];
			}
			set
			{
				this[InboxRuleSchema.FromAddressContainsWords] = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0002329C File Offset: 0x0002149C
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x000232AE File Offset: 0x000214AE
		[Parameter]
		public MultiValuedProperty<string> ExceptIfFromAddressContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ExceptIfFromAddressContainsWords];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfFromAddressContainsWords] = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x000232BC File Offset: 0x000214BC
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x000232CE File Offset: 0x000214CE
		public ADRecipientOrAddress[] From
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.From];
			}
			set
			{
				this[InboxRuleSchema.From] = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000232DC File Offset: 0x000214DC
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x000232EE File Offset: 0x000214EE
		public ADRecipientOrAddress[] ExceptIfFrom
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.ExceptIfFrom];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfFrom] = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000232FC File Offset: 0x000214FC
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0002330E File Offset: 0x0002150E
		[Parameter]
		public bool HasAttachment
		{
			get
			{
				return (bool)this[InboxRuleSchema.HasAttachment];
			}
			set
			{
				this[InboxRuleSchema.HasAttachment] = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00023321 File Offset: 0x00021521
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00023333 File Offset: 0x00021533
		[Parameter]
		public bool ExceptIfHasAttachment
		{
			get
			{
				return (bool)this[InboxRuleSchema.ExceptIfHasAttachment];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfHasAttachment] = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00023346 File Offset: 0x00021546
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00023358 File Offset: 0x00021558
		public MessageClassification[] HasClassification
		{
			get
			{
				return (MessageClassification[])this[InboxRuleSchema.HasClassification];
			}
			set
			{
				this[InboxRuleSchema.HasClassification] = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00023366 File Offset: 0x00021566
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00023378 File Offset: 0x00021578
		public MessageClassification[] ExceptIfHasClassification
		{
			get
			{
				return (MessageClassification[])this[InboxRuleSchema.ExceptIfHasClassification];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfHasClassification] = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00023386 File Offset: 0x00021586
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00023398 File Offset: 0x00021598
		[Parameter]
		public MultiValuedProperty<string> HeaderContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.HeaderContainsWords];
			}
			set
			{
				this[InboxRuleSchema.HeaderContainsWords] = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000233A6 File Offset: 0x000215A6
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x000233B8 File Offset: 0x000215B8
		[Parameter]
		public MultiValuedProperty<string> ExceptIfHeaderContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ExceptIfHeaderContainsWords];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfHeaderContainsWords] = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000233C6 File Offset: 0x000215C6
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x000233D8 File Offset: 0x000215D8
		public AggregationSubscriptionIdentity[] FromSubscription
		{
			get
			{
				return (AggregationSubscriptionIdentity[])this[InboxRuleSchema.FromSubscription];
			}
			set
			{
				this[InboxRuleSchema.FromSubscription] = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000233E6 File Offset: 0x000215E6
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x000233F8 File Offset: 0x000215F8
		public AggregationSubscriptionIdentity[] ExceptIfFromSubscription
		{
			get
			{
				return (AggregationSubscriptionIdentity[])this[InboxRuleSchema.ExceptIfFromSubscription];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfFromSubscription] = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00023406 File Offset: 0x00021606
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00023418 File Offset: 0x00021618
		[Parameter]
		public InboxRuleMessageType? MessageTypeMatches
		{
			get
			{
				return (InboxRuleMessageType?)this[InboxRuleSchema.MessageTypeMatches];
			}
			set
			{
				this[InboxRuleSchema.MessageTypeMatches] = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002342B File Offset: 0x0002162B
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0002343D File Offset: 0x0002163D
		[Parameter]
		public InboxRuleMessageType? ExceptIfMessageTypeMatches
		{
			get
			{
				return (InboxRuleMessageType?)this[InboxRuleSchema.ExceptIfMessageTypeMatches];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfMessageTypeMatches] = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00023450 File Offset: 0x00021650
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00023462 File Offset: 0x00021662
		[Parameter]
		public bool MyNameInCcBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.MyNameInCcBox];
			}
			set
			{
				this[InboxRuleSchema.MyNameInCcBox] = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00023475 File Offset: 0x00021675
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00023487 File Offset: 0x00021687
		[Parameter]
		public bool ExceptIfMyNameInCcBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.ExceptIfMyNameInCcBox];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfMyNameInCcBox] = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0002349A File Offset: 0x0002169A
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x000234AC File Offset: 0x000216AC
		[Parameter]
		public bool MyNameInToBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.MyNameInToBox];
			}
			set
			{
				this[InboxRuleSchema.MyNameInToBox] = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000234BF File Offset: 0x000216BF
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x000234D1 File Offset: 0x000216D1
		[Parameter]
		public bool ExceptIfMyNameInToBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.ExceptIfMyNameInToBox];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfMyNameInToBox] = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x000234E4 File Offset: 0x000216E4
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x000234F6 File Offset: 0x000216F6
		[Parameter]
		public bool MyNameInToOrCcBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.MyNameInToOrCcBox];
			}
			set
			{
				this[InboxRuleSchema.MyNameInToOrCcBox] = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00023509 File Offset: 0x00021709
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x0002351B File Offset: 0x0002171B
		[Parameter]
		public bool ExceptIfMyNameInToOrCcBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.ExceptIfMyNameInToOrCcBox];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfMyNameInToOrCcBox] = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0002352E File Offset: 0x0002172E
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00023540 File Offset: 0x00021740
		[Parameter]
		public bool MyNameNotInToBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.MyNameNotInToBox];
			}
			set
			{
				this[InboxRuleSchema.MyNameNotInToBox] = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00023553 File Offset: 0x00021753
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00023565 File Offset: 0x00021765
		[Parameter]
		public bool ExceptIfMyNameNotInToBox
		{
			get
			{
				return (bool)this[InboxRuleSchema.ExceptIfMyNameNotInToBox];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfMyNameNotInToBox] = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00023578 File Offset: 0x00021778
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x0002358A File Offset: 0x0002178A
		[Parameter]
		public ExDateTime? ReceivedAfterDate
		{
			get
			{
				return (ExDateTime?)this[InboxRuleSchema.ReceivedAfterDate];
			}
			set
			{
				this[InboxRuleSchema.ReceivedAfterDate] = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0002359D File Offset: 0x0002179D
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x000235AF File Offset: 0x000217AF
		[Parameter]
		public ExDateTime? ExceptIfReceivedAfterDate
		{
			get
			{
				return (ExDateTime?)this[InboxRuleSchema.ExceptIfReceivedAfterDate];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfReceivedAfterDate] = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x000235C2 File Offset: 0x000217C2
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x000235D4 File Offset: 0x000217D4
		[Parameter]
		public ExDateTime? ReceivedBeforeDate
		{
			get
			{
				return (ExDateTime?)this[InboxRuleSchema.ReceivedBeforeDate];
			}
			set
			{
				this[InboxRuleSchema.ReceivedBeforeDate] = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000235E7 File Offset: 0x000217E7
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x000235F9 File Offset: 0x000217F9
		[Parameter]
		public ExDateTime? ExceptIfReceivedBeforeDate
		{
			get
			{
				return (ExDateTime?)this[InboxRuleSchema.ExceptIfReceivedBeforeDate];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfReceivedBeforeDate] = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0002360C File Offset: 0x0002180C
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0002361E File Offset: 0x0002181E
		[Parameter]
		public MultiValuedProperty<string> RecipientAddressContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.RecipientAddressContainsWords];
			}
			set
			{
				this[InboxRuleSchema.RecipientAddressContainsWords] = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0002362C File Offset: 0x0002182C
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0002363E File Offset: 0x0002183E
		[Parameter]
		public MultiValuedProperty<string> ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ExceptIfRecipientAddressContainsWords];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfRecipientAddressContainsWords] = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0002364C File Offset: 0x0002184C
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0002365E File Offset: 0x0002185E
		[Parameter]
		public bool SentOnlyToMe
		{
			get
			{
				return (bool)this[InboxRuleSchema.SentOnlyToMe];
			}
			set
			{
				this[InboxRuleSchema.SentOnlyToMe] = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00023671 File Offset: 0x00021871
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00023683 File Offset: 0x00021883
		[Parameter]
		public bool ExceptIfSentOnlyToMe
		{
			get
			{
				return (bool)this[InboxRuleSchema.ExceptIfSentOnlyToMe];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfSentOnlyToMe] = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00023696 File Offset: 0x00021896
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x000236A8 File Offset: 0x000218A8
		public ADRecipientOrAddress[] SentTo
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.SentTo];
			}
			set
			{
				this[InboxRuleSchema.SentTo] = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x000236B6 File Offset: 0x000218B6
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x000236C8 File Offset: 0x000218C8
		public ADRecipientOrAddress[] ExceptIfSentTo
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.ExceptIfSentTo];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfSentTo] = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000236D6 File Offset: 0x000218D6
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x000236E8 File Offset: 0x000218E8
		[Parameter]
		public MultiValuedProperty<string> SubjectContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.SubjectContainsWords];
			}
			set
			{
				this[InboxRuleSchema.SubjectContainsWords] = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000236F6 File Offset: 0x000218F6
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00023708 File Offset: 0x00021908
		[Parameter]
		public MultiValuedProperty<string> ExceptIfSubjectContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ExceptIfSubjectContainsWords];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfSubjectContainsWords] = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00023716 File Offset: 0x00021916
		// (set) Token: 0x06000848 RID: 2120 RVA: 0x00023728 File Offset: 0x00021928
		[Parameter]
		public MultiValuedProperty<string> SubjectOrBodyContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.SubjectOrBodyContainsWords];
			}
			set
			{
				this[InboxRuleSchema.SubjectOrBodyContainsWords] = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00023736 File Offset: 0x00021936
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x00023748 File Offset: 0x00021948
		[Parameter]
		public MultiValuedProperty<string> ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ExceptIfSubjectOrBodyContainsWords];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfSubjectOrBodyContainsWords] = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00023756 File Offset: 0x00021956
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x00023768 File Offset: 0x00021968
		[Parameter]
		public Microsoft.Exchange.Data.Storage.Importance? WithImportance
		{
			get
			{
				return (Microsoft.Exchange.Data.Storage.Importance?)this[InboxRuleSchema.WithImportance];
			}
			set
			{
				this[InboxRuleSchema.WithImportance] = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0002377B File Offset: 0x0002197B
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x0002378D File Offset: 0x0002198D
		[Parameter]
		public Microsoft.Exchange.Data.Storage.Importance? ExceptIfWithImportance
		{
			get
			{
				return (Microsoft.Exchange.Data.Storage.Importance?)this[InboxRuleSchema.ExceptIfWithImportance];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfWithImportance] = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000237A0 File Offset: 0x000219A0
		// (set) Token: 0x06000850 RID: 2128 RVA: 0x000237B2 File Offset: 0x000219B2
		[Parameter]
		public ByteQuantifiedSize? WithinSizeRangeMaximum
		{
			get
			{
				return (ByteQuantifiedSize?)this[InboxRuleSchema.WithinSizeRangeMaximum];
			}
			set
			{
				this[InboxRuleSchema.WithinSizeRangeMaximum] = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x000237C5 File Offset: 0x000219C5
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x000237D7 File Offset: 0x000219D7
		[Parameter]
		public ByteQuantifiedSize? ExceptIfWithinSizeRangeMaximum
		{
			get
			{
				return (ByteQuantifiedSize?)this[InboxRuleSchema.ExceptIfWithinSizeRangeMaximum];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfWithinSizeRangeMaximum] = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000237EA File Offset: 0x000219EA
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x000237FC File Offset: 0x000219FC
		[Parameter]
		public ByteQuantifiedSize? WithinSizeRangeMinimum
		{
			get
			{
				return (ByteQuantifiedSize?)this[InboxRuleSchema.WithinSizeRangeMinimum];
			}
			set
			{
				this[InboxRuleSchema.WithinSizeRangeMinimum] = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x0002380F File Offset: 0x00021A0F
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00023821 File Offset: 0x00021A21
		[Parameter]
		public ByteQuantifiedSize? ExceptIfWithinSizeRangeMinimum
		{
			get
			{
				return (ByteQuantifiedSize?)this[InboxRuleSchema.ExceptIfWithinSizeRangeMinimum];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfWithinSizeRangeMinimum] = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00023834 File Offset: 0x00021A34
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00023846 File Offset: 0x00021A46
		[Parameter]
		public Sensitivity? WithSensitivity
		{
			get
			{
				return (Sensitivity?)this[InboxRuleSchema.WithSensitivity];
			}
			set
			{
				this[InboxRuleSchema.WithSensitivity] = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00023859 File Offset: 0x00021A59
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0002386B File Offset: 0x00021A6B
		[Parameter]
		public Sensitivity? ExceptIfWithSensitivity
		{
			get
			{
				return (Sensitivity?)this[InboxRuleSchema.ExceptIfWithSensitivity];
			}
			set
			{
				this[InboxRuleSchema.ExceptIfWithSensitivity] = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0002387E File Offset: 0x00021A7E
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00023890 File Offset: 0x00021A90
		[Parameter]
		public MultiValuedProperty<string> ApplyCategory
		{
			get
			{
				return (MultiValuedProperty<string>)this[InboxRuleSchema.ApplyCategory];
			}
			set
			{
				this[InboxRuleSchema.ApplyCategory] = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0002389E File Offset: 0x00021A9E
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000238CE File Offset: 0x00021ACE
		public MailboxFolder CopyToFolder
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return this.RedactMailboxFolder((MailboxFolder)this[InboxRuleSchema.CopyToFolder]);
				}
				return (MailboxFolder)this[InboxRuleSchema.CopyToFolder];
			}
			internal set
			{
				this[InboxRuleSchema.CopyToFolder] = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000238DC File Offset: 0x00021ADC
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x000238EE File Offset: 0x00021AEE
		[Parameter]
		public bool DeleteMessage
		{
			get
			{
				return (bool)this[InboxRuleSchema.DeleteMessage];
			}
			set
			{
				this[InboxRuleSchema.DeleteMessage] = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00023901 File Offset: 0x00021B01
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00023913 File Offset: 0x00021B13
		public ADRecipientOrAddress[] ForwardAsAttachmentTo
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.ForwardAsAttachmentTo];
			}
			internal set
			{
				this[InboxRuleSchema.ForwardAsAttachmentTo] = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00023921 File Offset: 0x00021B21
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00023933 File Offset: 0x00021B33
		public ADRecipientOrAddress[] ForwardTo
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.ForwardTo];
			}
			internal set
			{
				this[InboxRuleSchema.ForwardTo] = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00023941 File Offset: 0x00021B41
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00023953 File Offset: 0x00021B53
		[Parameter]
		public bool MarkAsRead
		{
			get
			{
				return (bool)this[InboxRuleSchema.MarkAsRead];
			}
			set
			{
				this[InboxRuleSchema.MarkAsRead] = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00023966 File Offset: 0x00021B66
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00023978 File Offset: 0x00021B78
		[Parameter]
		public Microsoft.Exchange.Data.Storage.Importance? MarkImportance
		{
			get
			{
				return (Microsoft.Exchange.Data.Storage.Importance?)this[InboxRuleSchema.MarkImportance];
			}
			set
			{
				this[InboxRuleSchema.MarkImportance] = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0002398B File Offset: 0x00021B8B
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x000239BB File Offset: 0x00021BBB
		public MailboxFolder MoveToFolder
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return this.RedactMailboxFolder((MailboxFolder)this[InboxRuleSchema.MoveToFolder]);
				}
				return (MailboxFolder)this[InboxRuleSchema.MoveToFolder];
			}
			internal set
			{
				this[InboxRuleSchema.MoveToFolder] = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000239C9 File Offset: 0x00021BC9
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x000239DB File Offset: 0x00021BDB
		public ADRecipientOrAddress[] RedirectTo
		{
			get
			{
				return (ADRecipientOrAddress[])this[InboxRuleSchema.RedirectTo];
			}
			internal set
			{
				this[InboxRuleSchema.RedirectTo] = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000239E9 File Offset: 0x00021BE9
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x000239FB File Offset: 0x00021BFB
		[Parameter]
		public MultiValuedProperty<E164Number> SendTextMessageNotificationTo
		{
			get
			{
				return (MultiValuedProperty<E164Number>)this[InboxRuleSchema.SendTextMessageNotificationTo];
			}
			set
			{
				this[InboxRuleSchema.SendTextMessageNotificationTo] = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00023A09 File Offset: 0x00021C09
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00023A1B File Offset: 0x00021C1B
		[Parameter]
		public bool StopProcessingRules
		{
			get
			{
				return (bool)this[InboxRuleSchema.StopProcessingRules];
			}
			set
			{
				this[InboxRuleSchema.StopProcessingRules] = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00023A2E File Offset: 0x00021C2E
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return InboxRule.schema;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00023A35 File Offset: 0x00021C35
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x00023A47 File Offset: 0x00021C47
		internal RuleId RuleId
		{
			get
			{
				return (RuleId)this[InboxRuleSchema.RuleId];
			}
			set
			{
				this[InboxRuleSchema.RuleId] = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00023A55 File Offset: 0x00021C55
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00023A5D File Offset: 0x00021C5D
		internal XsoMailboxDataProviderBase Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
				this.culture = this.provider.MailboxSession.Culture;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00023A7C File Offset: 0x00021C7C
		internal CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00023A84 File Offset: 0x00021C84
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00023A8C File Offset: 0x00021C8C
		internal ExTimeZoneValue DescriptionTimeZone
		{
			get
			{
				return this.descriptionTimeZone;
			}
			set
			{
				if (value == null || value.ExTimeZone == null || value.ExTimeZone.Id == ExTimeZone.UtcTimeZone.Id)
				{
					this.descriptionTimeZone = null;
					return;
				}
				this.descriptionTimeZone = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00023ACA File Offset: 0x00021CCA
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00023AD2 File Offset: 0x00021CD2
		internal string DescriptionTimeFormat
		{
			get
			{
				return this.descriptionTimeFormat;
			}
			set
			{
				this.descriptionTimeFormat = value;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00023ADB File Offset: 0x00021CDB
		public InboxRule()
		{
			((SimplePropertyBag)this.propertyBag).SetObjectIdentityPropertyDefinition(InboxRuleSchema.Identity);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00023AF8 File Offset: 0x00021CF8
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00023B28 File Offset: 0x00021D28
		internal static object IdentityGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			RuleId ruleId = (RuleId)propertyBag[InboxRuleSchema.RuleId];
			string name = (string)propertyBag[InboxRuleSchema.Name];
			if (adobjectId != null)
			{
				return new InboxRuleId(adobjectId, name, ruleId);
			}
			return null;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00023B78 File Offset: 0x00021D78
		internal static object RuleIdentityGetter(IPropertyBag propertyBag)
		{
			RuleId ruleId = (RuleId)propertyBag[InboxRuleSchema.RuleId];
			if (ruleId != null)
			{
				return InboxRuleTaskHelper.GetRuleIdentity(ruleId);
			}
			return null;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00023BA6 File Offset: 0x00021DA6
		internal void SetPropertyInError(ProviderPropertyDefinition property)
		{
			if (this.propertiesInError == null)
			{
				this.propertiesInError = new HashSet<ProviderPropertyDefinition>();
			}
			this.propertiesInError.Add(property);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00023BC8 File Offset: 0x00021DC8
		internal bool IsPropertyInError(ProviderPropertyDefinition property)
		{
			return this.propertiesInError != null && this.propertiesInError.Contains(property);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00023BE0 File Offset: 0x00021DE0
		internal void ValidateInterdependentParameters(ManageInboxRule.ThrowTerminatingErrorDelegate writeError)
		{
			if (base.IsModified(InboxRuleSchema.ReceivedAfterDate) || base.IsModified(InboxRuleSchema.ReceivedBeforeDate))
			{
				ManageInboxRule.VerifyRange<ExDateTime?>(this.ReceivedAfterDate, this.ReceivedBeforeDate, false, writeError, Strings.ErrorInvalidDateRangeCondition);
			}
			if (base.IsModified(InboxRuleSchema.ExceptIfReceivedAfterDate) || base.IsModified(InboxRuleSchema.ExceptIfReceivedBeforeDate))
			{
				ManageInboxRule.VerifyRange<ExDateTime?>(this.ExceptIfReceivedAfterDate, this.ExceptIfReceivedBeforeDate, false, writeError, Strings.ErrorInvalidDateRangeException);
			}
			if (base.IsModified(InboxRuleSchema.WithinSizeRangeMinimum) || base.IsModified(InboxRuleSchema.WithinSizeRangeMaximum))
			{
				ManageInboxRule.VerifyRange<ByteQuantifiedSize?>(this.WithinSizeRangeMinimum, this.WithinSizeRangeMaximum, true, writeError, Strings.ErrorInvalidSizeRangeCondition);
			}
			if (base.IsModified(InboxRuleSchema.ExceptIfWithinSizeRangeMinimum) || base.IsModified(InboxRuleSchema.ExceptIfWithinSizeRangeMaximum))
			{
				ManageInboxRule.VerifyRange<ByteQuantifiedSize?>(this.ExceptIfWithinSizeRangeMinimum, this.ExceptIfWithinSizeRangeMaximum, true, writeError, Strings.ErrorInvalidSizeRangeException);
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00023CB8 File Offset: 0x00021EB8
		internal string GetDateString(ExDateTime dateTime)
		{
			ExDateTime exDateTime = dateTime;
			if (this.descriptionTimeZone != null && this.descriptionTimeZone.ExTimeZone != null)
			{
				exDateTime = this.descriptionTimeZone.ExTimeZone.ConvertDateTime(dateTime);
			}
			return exDateTime.ToString(this.descriptionTimeFormat);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00023D04 File Offset: 0x00021F04
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			bool flag = false;
			foreach (InboxRuleDataProvider.ActionMappingEntry actionMappingEntry in InboxRuleDataProvider.ActionMappings)
			{
				ProviderPropertyDefinition propertyDefinition = actionMappingEntry.PropertyDefinition;
				if (this[propertyDefinition] != null && (propertyDefinition.Type != typeof(bool) || (bool)this[propertyDefinition]))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				errors.Add(new ObjectValidationError(Strings.ErrorInboxRuleHasNoAction, this.Identity, string.Empty));
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00023D90 File Offset: 0x00021F90
		private RuleDescription BuildDescription()
		{
			RuleDescription ruleDescription = new RuleDescription();
			foreach (InboxRuleDataProvider.ConditionMappingEntry conditionMappingEntry in InboxRuleDataProvider.ConditionMappings)
			{
				ProviderPropertyDefinition propertyDefinition = conditionMappingEntry.PropertyDefinition;
				if (this[propertyDefinition] != null || this.IsPropertyInError(propertyDefinition))
				{
					ICollection collection = this[propertyDefinition] as ICollection;
					if (collection == null || collection.Count != 0 || this.IsPropertyInError(propertyDefinition))
					{
						string text = conditionMappingEntry.DescriptionStringDelegate(this);
						if (!string.IsNullOrEmpty(text))
						{
							if (conditionMappingEntry.PredicateType == InboxRuleDataProvider.PredicateType.Condition)
							{
								ruleDescription.ConditionDescriptions.Add(text);
							}
							else if (conditionMappingEntry.PredicateType == InboxRuleDataProvider.PredicateType.Exception)
							{
								ruleDescription.ExceptionDescriptions.Add(text);
							}
						}
					}
				}
			}
			foreach (InboxRuleDataProvider.ActionMappingEntry actionMappingEntry in InboxRuleDataProvider.ActionMappings)
			{
				ProviderPropertyDefinition propertyDefinition2 = actionMappingEntry.PropertyDefinition;
				if (this[propertyDefinition2] != null || this.IsPropertyInError(propertyDefinition2))
				{
					string text2 = actionMappingEntry.DescriptionStringDelegate(this);
					if (!string.IsNullOrEmpty(text2))
					{
						ruleDescription.ActionDescriptions.Add(text2);
					}
				}
			}
			return ruleDescription;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00023EB0 File Offset: 0x000220B0
		internal List<string> GetClassificationNames(IList<string> classificationGuids)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2087, "GetClassificationNames", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\InboxRule.cs");
			ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(base.MailboxOwnerId);
			IConfigurationSession systemConfigurationSession = this.provider.GetSystemConfigurationSession(adrecipient.OrganizationId);
			List<string> list = new List<string>();
			foreach (string text in classificationGuids)
			{
				if (!string.IsNullOrEmpty(text))
				{
					Guid guid;
					try
					{
						guid = new Guid(text);
					}
					catch (OverflowException)
					{
						continue;
					}
					catch (FormatException)
					{
						continue;
					}
					ADObjectId entryId = new ADObjectId(guid);
					MessageClassification messageClassification = systemConfigurationSession.Read<MessageClassification>(entryId);
					if (messageClassification != null)
					{
						list.Add(messageClassification.DisplayName);
					}
				}
			}
			return list;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00023F94 File Offset: 0x00022194
		internal IList<string> GetSubscriptionEmailAddresses(IList<AggregationSubscriptionIdentity> subscriptions)
		{
			IList<string> result;
			InboxRuleDataProvider.TryGetSubscriptionEmailAddresses(this.provider.MailboxOwner, subscriptions, out result);
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00023FB8 File Offset: 0x000221B8
		private MailboxFolder RedactMailboxFolder(MailboxFolder folder)
		{
			folder.Name = SuppressingPiiData.Redact(folder.Name);
			string text;
			string text2;
			folder.FolderPath = SuppressingPiiData.Redact(folder.FolderPath, out text, out text2);
			folder.MailboxOwnerId = SuppressingPiiData.Redact(folder.MailboxOwnerId, out text, out text2);
			return folder;
		}

		// Token: 0x04000424 RID: 1060
		private const string TabString = "\t";

		// Token: 0x04000425 RID: 1061
		private const string CrLfString = "\r\n";

		// Token: 0x04000426 RID: 1062
		private static InboxRuleSchema schema = ObjectSchema.GetInstance<InboxRuleSchema>();

		// Token: 0x04000427 RID: 1063
		[NonSerialized]
		private XsoMailboxDataProviderBase provider;

		// Token: 0x04000428 RID: 1064
		private CultureInfo culture;

		// Token: 0x04000429 RID: 1065
		private ExTimeZoneValue descriptionTimeZone;

		// Token: 0x0400042A RID: 1066
		private string descriptionTimeFormat;

		// Token: 0x0400042B RID: 1067
		private HashSet<ProviderPropertyDefinition> propertiesInError;
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200042D RID: 1069
	[DataContract]
	[KnownType(typeof(RuleCondition))]
	public class RulePhrase
	{
		// Token: 0x06003580 RID: 13696 RVA: 0x000A6AC4 File Offset: 0x000A4CC4
		public RulePhrase(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, bool isDisplayedInSimpleMode) : this(name, displayText, ruleParameters, additionalRoles, LocalizedString.Empty, LocalizedString.Empty, LocalizedString.Empty, isDisplayedInSimpleMode, false)
		{
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
		public RulePhrase(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString groupText, LocalizedString flyOutText, bool isDisplayedInSimpleMode, bool stopProcessingRulesByDefault) : this(name, displayText, ruleParameters, additionalRoles, groupText, flyOutText, LocalizedString.Empty, isDisplayedInSimpleMode, stopProcessingRulesByDefault)
		{
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000A6B18 File Offset: 0x000A4D18
		public RulePhrase(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString groupText, LocalizedString flyOutText, bool isDisplayedInSimpleMode) : this(name, displayText, ruleParameters, additionalRoles, groupText, flyOutText, LocalizedString.Empty, isDisplayedInSimpleMode, false)
		{
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000A6B3C File Offset: 0x000A4D3C
		public RulePhrase(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString groupText, LocalizedString flyOutText, LocalizedString explanationText, bool isDisplayedInSimpleMode) : this(name, displayText, ruleParameters, additionalRoles, groupText, flyOutText, explanationText, isDisplayedInSimpleMode, false)
		{
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000A6B60 File Offset: 0x000A4D60
		public RulePhrase(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString groupText, LocalizedString flyOutText, LocalizedString explanationText, bool isDisplayedInSimpleMode, bool stopProcessingRulesByDefault)
		{
			this.Name = name;
			this.displayText = displayText;
			this.Parameters = ruleParameters;
			this.AdditionalRoles = additionalRoles;
			this.groupText = groupText;
			this.flyOutText = flyOutText;
			this.DisplayedInSimpleMode = isDisplayedInSimpleMode;
			this.explanationText = explanationText;
			this.StopProcessingRulesByDefault = stopProcessingRulesByDefault;
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000A6BB8 File Offset: 0x000A4DB8
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x000A6BC0 File Offset: 0x000A4DC0
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000A6BC9 File Offset: 0x000A4DC9
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x000A6BDC File Offset: 0x000A4DDC
		[DataMember]
		public string DisplayText
		{
			get
			{
				return this.displayText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000A6BE3 File Offset: 0x000A4DE3
		// (set) Token: 0x0600358A RID: 13706 RVA: 0x000A6BF6 File Offset: 0x000A4DF6
		[DataMember]
		public string GroupText
		{
			get
			{
				return this.groupText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000A6BFD File Offset: 0x000A4DFD
		// (set) Token: 0x0600358C RID: 13708 RVA: 0x000A6C10 File Offset: 0x000A4E10
		[DataMember]
		public string FlyOutText
		{
			get
			{
				return this.flyOutText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000A6C17 File Offset: 0x000A4E17
		// (set) Token: 0x0600358E RID: 13710 RVA: 0x000A6C2A File Offset: 0x000A4E2A
		[DataMember]
		public string ExplanationText
		{
			get
			{
				return this.explanationText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000A6C31 File Offset: 0x000A4E31
		// (set) Token: 0x06003590 RID: 13712 RVA: 0x000A6C39 File Offset: 0x000A4E39
		[DataMember]
		public FormletParameter[] Parameters { get; private set; }

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000A6C42 File Offset: 0x000A4E42
		// (set) Token: 0x06003592 RID: 13714 RVA: 0x000A6C4A File Offset: 0x000A4E4A
		[DataMember]
		public bool DisplayedInSimpleMode { get; internal set; }

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000A6C53 File Offset: 0x000A4E53
		// (set) Token: 0x06003594 RID: 13716 RVA: 0x000A6C5B File Offset: 0x000A4E5B
		[DataMember]
		public bool StopProcessingRulesByDefault { get; private set; }

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000A6C64 File Offset: 0x000A4E64
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000A6C6C File Offset: 0x000A4E6C
		public string AdditionalRoles { get; private set; }

		// Token: 0x04002597 RID: 9623
		private LocalizedString displayText;

		// Token: 0x04002598 RID: 9624
		private LocalizedString groupText;

		// Token: 0x04002599 RID: 9625
		private LocalizedString flyOutText;

		// Token: 0x0400259A RID: 9626
		private LocalizedString explanationText;
	}
}

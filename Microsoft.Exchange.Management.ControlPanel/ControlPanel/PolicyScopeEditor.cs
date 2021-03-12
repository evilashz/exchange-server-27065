using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000633 RID: 1587
	public class PolicyScopeEditor : RecipientConditionEditorBase
	{
		// Token: 0x060045DA RID: 17882 RVA: 0x000D3521 File Offset: 0x000D1721
		public PolicyScopeEditor()
		{
			this.UseExceptions = false;
		}

		// Token: 0x170026EC RID: 9964
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x000D3530 File Offset: 0x000D1730
		protected override RulePhrase[] SupportedConditions
		{
			get
			{
				string name = this.UseExceptions ? "ExceptIfSentTo" : "SentTo";
				string name2 = this.UseExceptions ? "ExceptIfRecipientDomainIs" : "RecipientDomainIs";
				string name3 = this.UseExceptions ? "ExceptIfSentToMemberOf" : "SentToMemberOf";
				return new RulePhrase[]
				{
					new RulePhrase(name, Strings.ConditionalRecipientIs, new FormletParameter[]
					{
						new PeopleParameter(name, PickerType.PickTo)
					}, null, false),
					new RulePhrase(name2, Strings.ConditionalRecipientDomain, new FormletParameter[]
					{
						new ObjectsParameter(name2, Strings.DomainDialogTitle, Strings.StringArrayDialogLabel, Strings.TransportRuleRecipientDomainContainsWordsPredicateText, "~/pickers/AcceptedDomainPicker.aspx")
						{
							ValueProperty = "DisplayName",
							DialogWidth = 445,
							DialogHeight = 530
						}
					}, null, false),
					new RulePhrase(name3, Strings.ConditionalRecipientMemberOf, new FormletParameter[]
					{
						new PeopleParameter(name3, PickerType.PickTo)
					}, null, false)
				};
			}
		}

		// Token: 0x170026ED RID: 9965
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x000D362E File Offset: 0x000D182E
		// (set) Token: 0x060045DD RID: 17885 RVA: 0x000D3636 File Offset: 0x000D1836
		public bool UseExceptions { get; set; }
	}
}

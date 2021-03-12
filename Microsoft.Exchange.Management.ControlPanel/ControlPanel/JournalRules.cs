using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000416 RID: 1046
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class JournalRules : RuleDataService, IJournalRules, IDataSourceService<JournalRuleFilter, JournalRuleRow, JournalRule, SetJournalRule, NewJournalRule>, IDataSourceService<JournalRuleFilter, JournalRuleRow, JournalRule, SetJournalRule, NewJournalRule, BaseWebServiceParameters>, IEditListService<JournalRuleFilter, JournalRuleRow, JournalRule, NewJournalRule, BaseWebServiceParameters>, IGetListService<JournalRuleFilter, JournalRuleRow>, INewObjectService<JournalRuleRow, NewJournalRule>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<JournalRule, SetJournalRule, JournalRuleRow>, IGetObjectService<JournalRule>, IGetObjectForListService<JournalRuleRow>
	{
		// Token: 0x0600351E RID: 13598 RVA: 0x000A5559 File Offset: 0x000A3759
		public JournalRules() : base("JournalRule")
		{
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x000A5566 File Offset: 0x000A3766
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-JournalRule@R:Organization")]
		public PowerShellResults<JournalRuleRow> GetList(JournalRuleFilter filter, SortOptions sort)
		{
			return base.GetList<JournalRuleRow, JournalRuleFilter>("Get-JournalRule", filter, sort);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x000A5575 File Offset: 0x000A3775
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-JournalRule?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-JournalRule", identities, parameters);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000A5584 File Offset: 0x000A3784
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-JournalRule?Identity@R:Organization")]
		public PowerShellResults<JournalRule> GetObject(Identity identity)
		{
			return base.GetObject<JournalRule>("Get-JournalRule", identity);
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000A5592 File Offset: 0x000A3792
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-JournalRule?Identity@R:Organization")]
		public PowerShellResults<JournalRuleRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<JournalRuleRow>("Get-JournalRule", identity);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000A55A0 File Offset: 0x000A37A0
		[PrincipalPermission(SecurityAction.Demand, Role = "New-JournalRule@W:Organization")]
		public PowerShellResults<JournalRuleRow> NewObject(NewJournalRule properties)
		{
			properties.FaultIfNull();
			properties.Enabled = new bool?(true);
			properties.Name = base.GetUniqueRuleName(properties.Name, this.GetList(null, null).Output);
			PowerShellResults<JournalRuleRow> powerShellResults = base.NewObject<JournalRuleRow, NewJournalRule>("New-JournalRule", properties);
			powerShellResults.Output = null;
			return powerShellResults;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000A55F3 File Offset: 0x000A37F3
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-JournalRule?Identity@R:Organization+Set-JournalRule?Identity@W:Organization")]
		public PowerShellResults<JournalRuleRow> SetObject(Identity identity, SetJournalRule properties)
		{
			properties.FaultIfNull();
			if (properties.Name != null)
			{
				properties.Name = base.GetUniqueRuleName(properties.Name, this.GetList(null, null).Output);
			}
			return base.SetObject<JournalRule, SetJournalRule, JournalRuleRow>("Set-JournalRule", identity, properties);
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000A562F File Offset: 0x000A382F
		[PrincipalPermission(SecurityAction.Demand, Role = "Disable-JournalRule?Identity@W:Organization")]
		public PowerShellResults<JournalRuleRow> DisableRule(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.InvokeAndGetObject<JournalRuleRow>(new PSCommand().AddCommand("Disable-JournalRule"), identities, parameters);
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000A5648 File Offset: 0x000A3848
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-JournalRule?Identity@W:Organization")]
		public PowerShellResults<JournalRuleRow> EnableRule(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.InvokeAndGetObject<JournalRuleRow>(new PSCommand().AddCommand("Enable-JournalRule"), identities, parameters);
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000A5661 File Offset: 0x000A3861
		public override int RuleNameMaxLength
		{
			get
			{
				return JournalRules.ruleNameMaxLength;
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000A5668 File Offset: 0x000A3868
		public override RulePhrase[] SupportedConditions
		{
			get
			{
				return JournalRules.supportedConditions;
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000A566F File Offset: 0x000A386F
		public override RulePhrase[] SupportedActions
		{
			get
			{
				return JournalRules.supportedActions;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000A5676 File Offset: 0x000A3876
		public override RulePhrase[] SupportedExceptions
		{
			get
			{
				return new RulePhrase[0];
			}
		}

		// Token: 0x0400255A RID: 9562
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400255B RID: 9563
		internal const string WriteScope = "@W:Organization";

		// Token: 0x0400255C RID: 9564
		internal const string NewJournalRule = "New-JournalRule";

		// Token: 0x0400255D RID: 9565
		internal const string GetJournalRule = "Get-JournalRule";

		// Token: 0x0400255E RID: 9566
		internal const string SetJournalRule = "Set-JournalRule";

		// Token: 0x0400255F RID: 9567
		internal const string RemoveJournalRule = "Remove-JournalRule";

		// Token: 0x04002560 RID: 9568
		internal const string DisableJournalRule = "Disable-JournalRule";

		// Token: 0x04002561 RID: 9569
		internal const string EnableJournalRule = "Enable-JournalRule";

		// Token: 0x04002562 RID: 9570
		internal const string JournalEmailAddressParameterName = "JournalEmailAddress";

		// Token: 0x04002563 RID: 9571
		internal const string RecipientParameterName = "Recipient";

		// Token: 0x04002564 RID: 9572
		internal const string ScopeParameterName = "Scope";

		// Token: 0x04002565 RID: 9573
		internal const string NameParameterName = "Name";

		// Token: 0x04002566 RID: 9574
		internal const string EnabledParameterName = "Enabled";

		// Token: 0x04002567 RID: 9575
		internal const string JournalRuleScopeName = "JournalRuleScope";

		// Token: 0x04002568 RID: 9576
		internal const string GetListRole = "Get-JournalRule@R:Organization";

		// Token: 0x04002569 RID: 9577
		internal const string RemoveObjectsRole = "Remove-JournalRule?Identity@W:Organization";

		// Token: 0x0400256A RID: 9578
		internal const string GetObjectRole = "Get-JournalRule?Identity@R:Organization";

		// Token: 0x0400256B RID: 9579
		internal const string NewObjectRole = "New-JournalRule@W:Organization";

		// Token: 0x0400256C RID: 9580
		internal const string SetObjectRole = "Get-JournalRule?Identity@R:Organization+Set-JournalRule?Identity@W:Organization";

		// Token: 0x0400256D RID: 9581
		internal const string DisableRuleRole = "Disable-JournalRule?Identity@W:Organization";

		// Token: 0x0400256E RID: 9582
		internal const string EnableRuleRole = "Enable-JournalRule?Identity@W:Organization";

		// Token: 0x0400256F RID: 9583
		private static int ruleNameMaxLength = Util.GetMaxLengthFromDefinition(ADObjectSchema.Name);

		// Token: 0x04002570 RID: 9584
		private static RulePhrase[] supportedConditions = new RulePhrase[]
		{
			new RuleCondition("Recipient", Strings.JournalRecipientParameterLabel, new FormletParameter[]
			{
				new PeopleParameter("Recipient", PickerType.PickSelectMailbox)
			}, null, Strings.JournalRuleAutoNameFormatString, Strings.TransportRuleSenderGroupText, Strings.TransportRuleFromFlyOutText, LocalizedString.Empty, true)
		};

		// Token: 0x04002571 RID: 9585
		private static RulePhrase[] supportedActions = new RulePhrase[]
		{
			new RulePhrase("JournalRuleScope", Strings.JournalRuleAllMessagesLabel, new FormletParameter[]
			{
				new JournalRuleScopeParameter("Global")
			}, null, true),
			new RulePhrase("JournalRuleScope", Strings.JournalRuleInternalMessagesLabel, new FormletParameter[]
			{
				new JournalRuleScopeParameter("Internal")
			}, null, true),
			new RulePhrase("JournalRuleScope", Strings.JournalRuleExternalMessagesLabel, new FormletParameter[]
			{
				new JournalRuleScopeParameter("External")
			}, null, true)
		};
	}
}

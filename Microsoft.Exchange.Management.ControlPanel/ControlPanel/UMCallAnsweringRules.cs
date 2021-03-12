using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200046C RID: 1132
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class UMCallAnsweringRules : RuleDataService, IUMCallAnsweringRules, IDataSourceService<UMCallAnsweringRuleFilter, RuleRow, UMCallAnsweringRule, SetUMCallAnsweringRule, NewUMCallAnsweringRule, RemoveUMCallAnsweringRule>, IEditListService<UMCallAnsweringRuleFilter, RuleRow, UMCallAnsweringRule, NewUMCallAnsweringRule, RemoveUMCallAnsweringRule>, IGetListService<UMCallAnsweringRuleFilter, RuleRow>, INewObjectService<RuleRow, NewUMCallAnsweringRule>, IRemoveObjectsService<RemoveUMCallAnsweringRule>, IEditObjectForListService<UMCallAnsweringRule, SetUMCallAnsweringRule, RuleRow>, IGetObjectService<UMCallAnsweringRule>, IGetObjectForListService<RuleRow>
	{
		// Token: 0x0600393A RID: 14650 RVA: 0x000AE21A File Offset: 0x000AC41A
		public UMCallAnsweringRules() : base("UMCallAnsweringRule")
		{
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000AE227 File Offset: 0x000AC427
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallAnsweringRule@R:Self")]
		public PowerShellResults<RuleRow> GetList(UMCallAnsweringRuleFilter filter, SortOptions sort)
		{
			return base.GetList<RuleRow, UMCallAnsweringRuleFilter>("Get-UMCallAnsweringRule", filter, sort);
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000AE236 File Offset: 0x000AC436
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-UMCallAnsweringRule?Identity@W:Self")]
		public PowerShellResults RemoveObjects(Identity[] identities, RemoveUMCallAnsweringRule parameters)
		{
			parameters = (parameters ?? new RemoveUMCallAnsweringRule());
			return base.Invoke(new PSCommand().AddCommand("Remove-UMCallAnsweringRule"), identities, parameters);
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000AE25C File Offset: 0x000AC45C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallAnsweringRule?Identity@R:Self")]
		public PowerShellResults<UMCallAnsweringRule> GetObject(Identity identity)
		{
			PSCommand psCommand = new PSCommand().AddCommand("Get-UMCallAnsweringRule");
			return base.GetObject<UMCallAnsweringRule>(psCommand, identity);
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000AE281 File Offset: 0x000AC481
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallAnsweringRule?Identity@R:Self")]
		public PowerShellResults<RuleRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<RuleRow>("Get-UMCallAnsweringRule", identity);
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000AE290 File Offset: 0x000AC490
		[PrincipalPermission(SecurityAction.Demand, Role = "New-UMCallAnsweringRule@W:Self")]
		public PowerShellResults<RuleRow> NewObject(NewUMCallAnsweringRule properties)
		{
			properties.FaultIfNull();
			properties.Name = base.GetUniqueRuleName(properties.Name, this.GetList(null, null).Output);
			PowerShellResults<RuleRow> powerShellResults = base.NewObject<RuleRow, NewUMCallAnsweringRule>("New-UMCallAnsweringRule", properties);
			powerShellResults.Output = null;
			return powerShellResults;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000AE2D7 File Offset: 0x000AC4D7
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallAnsweringRule?Identity@R:Self+Set-UMCallAnsweringRule?Identity@W:Self")]
		public PowerShellResults<RuleRow> SetObject(Identity identity, SetUMCallAnsweringRule properties)
		{
			properties.FaultIfNull();
			if (properties.Name != null)
			{
				properties.Name = base.GetUniqueRuleName(properties.Name, this.GetList(null, null).Output);
			}
			return base.SetObject<UMCallAnsweringRule, SetUMCallAnsweringRule, RuleRow>("Set-UMCallAnsweringRule", identity, properties);
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000AE313 File Offset: 0x000AC513
		[PrincipalPermission(SecurityAction.Demand, Role = "Disable-UMCallAnsweringRule?Identity@W:Self")]
		public PowerShellResults<RuleRow> DisableRule(Identity[] identities, DisableUMCallAnsweringRule parameters)
		{
			parameters = (parameters ?? new DisableUMCallAnsweringRule());
			return base.InvokeAndGetObject<RuleRow>(new PSCommand().AddCommand("Disable-UMCallAnsweringRule"), identities, parameters);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000AE338 File Offset: 0x000AC538
		[PrincipalPermission(SecurityAction.Demand, Role = "Enable-UMCallAnsweringRule?Identity@W:Self")]
		public PowerShellResults<RuleRow> EnableRule(Identity[] identities, EnableUMCallAnsweringRule parameters)
		{
			parameters = (parameters ?? new EnableUMCallAnsweringRule());
			return base.InvokeAndGetObject<RuleRow>(new PSCommand().AddCommand("Enable-UMCallAnsweringRule"), identities, parameters);
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000AE35D File Offset: 0x000AC55D
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallAnsweringRule?Identity@R:Self+Set-UMCallAnsweringRule?Identity&Priority@W:Self")]
		public PowerShellResults IncreasePriority(Identity[] identities, ChangeUMCallAnsweringRule parameters)
		{
			parameters = (parameters ?? new ChangeUMCallAnsweringRule());
			return base.ChangePriority<UMCallAnsweringRule>(identities, -1, parameters);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000AE374 File Offset: 0x000AC574
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMCallAnsweringRule?Identity@R:Self+Set-UMCallAnsweringRule?Identity&Priority@W:Self")]
		public PowerShellResults DecreasePriority(Identity[] identities, ChangeUMCallAnsweringRule parameters)
		{
			parameters = (parameters ?? new ChangeUMCallAnsweringRule());
			return base.ChangePriority<UMCallAnsweringRule>(identities, 1, parameters);
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x000AE38B File Offset: 0x000AC58B
		public override int RuleNameMaxLength
		{
			get
			{
				return UMCallAnsweringRules.ruleNameMaxLength;
			}
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000AE392 File Offset: 0x000AC592
		public override RulePhrase[] SupportedConditions
		{
			get
			{
				return UMCallAnsweringRules.supportedConditions;
			}
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000AE399 File Offset: 0x000AC599
		public override RulePhrase[] SupportedActions
		{
			get
			{
				return UMCallAnsweringRules.supportedActions;
			}
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x000AE3A0 File Offset: 0x000AC5A0
		public override RulePhrase[] SupportedExceptions
		{
			get
			{
				return UMCallAnsweringRules.supportedExceptions;
			}
		}

		// Token: 0x04002688 RID: 9864
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002689 RID: 9865
		internal const string WriteScope = "@W:Self";

		// Token: 0x0400268A RID: 9866
		internal const string NewUMCallAnsweringRule = "New-UMCallAnsweringRule";

		// Token: 0x0400268B RID: 9867
		internal const string GetUMCallAnsweringRule = "Get-UMCallAnsweringRule";

		// Token: 0x0400268C RID: 9868
		internal const string SetUMCallAnsweringRule = "Set-UMCallAnsweringRule";

		// Token: 0x0400268D RID: 9869
		internal const string RemoveUMCallAnsweringRule = "Remove-UMCallAnsweringRule";

		// Token: 0x0400268E RID: 9870
		internal const string DisableUMCallAnsweringRule = "Disable-UMCallAnsweringRule";

		// Token: 0x0400268F RID: 9871
		internal const string EnableUMCallAnsweringRule = "Enable-UMCallAnsweringRule";

		// Token: 0x04002690 RID: 9872
		internal const string GetListRole = "Get-UMCallAnsweringRule@R:Self";

		// Token: 0x04002691 RID: 9873
		internal const string RemoveObjectsRole = "Remove-UMCallAnsweringRule?Identity@W:Self";

		// Token: 0x04002692 RID: 9874
		internal const string GetObjectRole = "Get-UMCallAnsweringRule?Identity@R:Self";

		// Token: 0x04002693 RID: 9875
		internal const string NewObjectRole = "New-UMCallAnsweringRule@W:Self";

		// Token: 0x04002694 RID: 9876
		internal const string SetObjectRole = "Get-UMCallAnsweringRule?Identity@R:Self+Set-UMCallAnsweringRule?Identity@W:Self";

		// Token: 0x04002695 RID: 9877
		internal const string DisableRuleRole = "Disable-UMCallAnsweringRule?Identity@W:Self";

		// Token: 0x04002696 RID: 9878
		internal const string EnableRuleRole = "Enable-UMCallAnsweringRule?Identity@W:Self";

		// Token: 0x04002697 RID: 9879
		internal const string ChangePriorityRole = "Get-UMCallAnsweringRule?Identity@R:Self+Set-UMCallAnsweringRule?Identity&Priority@W:Self";

		// Token: 0x04002698 RID: 9880
		private static int ruleNameMaxLength = Util.GetMaxLengthFromDefinition(UMCallAnsweringRuleSchema.Name);

		// Token: 0x04002699 RID: 9881
		private static RulePhrase[] supportedConditions = new RulePhrase[]
		{
			new RuleCondition("CheckAutomaticReplies", Strings.CallAnsweringRuleCheckAutomaticRepliesText, new FormletParameter[]
			{
				new BooleanParameter("CheckAutomaticReplies")
			}, null, Strings.CheckAutomaticRepliesFormat, Strings.CallAnsweringRuleCheckAutomaticRepliesGroupText, Strings.CallAnsweringRuleCheckAutomaticRepliesFlyoutText, Strings.CallAnsweringRuleCheckAutomaticPreCannedText, true),
			new RuleCondition("ScheduleStatus", Strings.CallAnsweringRuleScheduleStatusText, new FormletParameter[]
			{
				new EnumParameter("ScheduleStatus", LocalizedString.Empty, LocalizedString.Empty, typeof(FreeBusyStatusEnum), null)
				{
					Values = new EnumValue[]
					{
						new EnumValue(Strings.ScheduleStatusFreeText, FreeBusyStatusEnum.Free.ToString()),
						new EnumValue(Strings.ScheduleStatusTentativeText, FreeBusyStatusEnum.Tentative.ToString()),
						new EnumValue(Strings.ScheduleStatusBusyText, FreeBusyStatusEnum.Busy.ToString()),
						new EnumValue(Strings.ScheduleStatusAwayText, FreeBusyStatusEnum.OutOfOffice.ToString())
					}
				}
			}, null, Strings.ScheduleStatusFormat, Strings.CallAnsweringRuleScheduleStatusGroupText, Strings.CallAnsweringRuleScheduleStatusFlyoutText, Strings.CallAnsweringRuleScheduleStatusPreCannedText, true),
			new RuleCondition("ExtensionsDialed", Strings.CallAnsweringRuleExtensionsDialedText, new FormletParameter[]
			{
				new ExtensionsDialedParameter("ExtensionsDialed")
			}, null, Strings.ExtensionsDialedFormat, Strings.CallAnsweringRuleExtensionsDialedGroupText, Strings.CallAnsweringRuleExtensionsDialedFlyoutText, Strings.CallAnsweringRuleExtensionsDialedPreCannedText, true),
			new RuleCondition("TimeOfDay", Strings.CallAnsweringRuleTimePeriodText, new FormletParameter[]
			{
				new TimePeriodParameter("TimeOfDay")
			}, null, Strings.TimeOfDayFormat, Strings.CallAnsweringRuleTimePeriodGroupText, Strings.CallAnsweringRuleTimePeriodFlyoutText, LocalizedString.Empty, true),
			new RuleCondition("CallerIds", Strings.CallAnsweringRuleCallerIdsText, new FormletParameter[]
			{
				new CallerIdsParameter("CallerIds")
			}, null, Strings.CallerIdsFormat, Strings.CallAnsweringRuleCallerIdsGroupText, Strings.CallAnsweringRuleCallerIdsFlyoutText, LocalizedString.Empty, true)
		};

		// Token: 0x0400269A RID: 9882
		private static RulePhrase[] supportedActions = new RulePhrase[]
		{
			new RulePhrase("KeyMappings", Strings.CallAnsweringRuleKeyMappingsText, new FormletParameter[]
			{
				new KeyMappingsParameter("KeyMappings")
			}, null, Strings.CallAnsweringRuleKeyMappingsGroupText, Strings.CallAnsweringRuleKeyMappingsFlyoutText, true, true)
		};

		// Token: 0x0400269B RID: 9883
		private static RulePhrase[] supportedExceptions = new RulePhrase[0];
	}
}

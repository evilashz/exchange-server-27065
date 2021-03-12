using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A43 RID: 2627
	[Cmdlet("Set", "HostedContentFilterPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHostedContentFilterPolicy : SetSystemConfigurationObjectTask<HostedContentFilterPolicyIdParameter, HostedContentFilterPolicy>
	{
		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06005E18 RID: 24088 RVA: 0x0018A9BB File Offset: 0x00188BBB
		// (set) Token: 0x06005E19 RID: 24089 RVA: 0x0018A9C3 File Offset: 0x00188BC3
		[Parameter]
		public SwitchParameter MakeDefault { get; set; }

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x0018A9CC File Offset: 0x00188BCC
		// (set) Token: 0x06005E1B RID: 24091 RVA: 0x0018A9D4 File Offset: 0x00188BD4
		[Parameter]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06005E1C RID: 24092 RVA: 0x0018A9DD File Offset: 0x00188BDD
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetHostedContentFilterPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x06005E1D RID: 24093 RVA: 0x0018A9EF File Offset: 0x00188BEF
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x0018AA04 File Offset: 0x00188C04
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.DataObject.LanguageBlockList != null)
			{
				foreach (string text in this.DataObject.LanguageBlockList)
				{
					if (!HygieneUtils.IsAntispamFilterableLanguage(text))
					{
						base.WriteError(new ArgumentException(Strings.ErrorUnsupportedBlockLanguage(text)), ErrorCategory.InvalidArgument, text);
					}
				}
			}
			if (this.DataObject.RegionBlockList != null)
			{
				foreach (string text2 in this.DataObject.RegionBlockList)
				{
					if (!HygieneUtils.IsValidIso3166Alpha2Code(text2))
					{
						base.WriteError(new ArgumentException(Strings.ErrorInvalidIso3166Alpha2Code(text2)), ErrorCategory.InvalidArgument, text2);
					}
				}
			}
			if (this.DataObject.IsModified(HostedContentFilterPolicySchema.EnableEndUserSpamNotifications) && this.DataObject.EnableEndUserSpamNotifications)
			{
				HostedContentFilterRule policyScopingRule = this.GetPolicyScopingRule();
				if (policyScopingRule != null && !policyScopingRule.IsEsnCompatible)
				{
					base.WriteError(new OperationNotAllowedException(Strings.ErrorEsnIncompatibleRule(policyScopingRule.Name)), ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0018AB84 File Offset: 0x00188D84
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			HostedContentFilterPolicy hostedContentFilterPolicy = null;
			if (this.MakeDefault && !this.DataObject.IsDefault)
			{
				this.DataObject.IsDefault = true;
				hostedContentFilterPolicy = ((ITenantConfigurationSession)base.DataSession).GetDefaultFilteringConfiguration<HostedContentFilterPolicy>();
				if (hostedContentFilterPolicy != null && hostedContentFilterPolicy.IsDefault)
				{
					hostedContentFilterPolicy.IsDefault = false;
					base.DataSession.Save(hostedContentFilterPolicy);
					FfoDualWriter.SaveToFfo<HostedContentFilterPolicy>(this, hostedContentFilterPolicy, null);
				}
			}
			else if (base.Fields.Contains("MakeDefault") && !this.MakeDefault && this.DataObject.IsDefault)
			{
				base.WriteError(new OperationNotAllowedException(Strings.OperationNotAllowed), ErrorCategory.InvalidOperation, this.MakeDefault);
			}
			try
			{
				base.InternalProcessRecord();
				FfoDualWriter.SaveToFfo<HostedContentFilterPolicy>(this, this.DataObject, null);
				hostedContentFilterPolicy = null;
			}
			finally
			{
				if (hostedContentFilterPolicy != null)
				{
					hostedContentFilterPolicy.IsDefault = true;
					base.DataSession.Save(hostedContentFilterPolicy);
					FfoDualWriter.SaveToFfo<HostedContentFilterPolicy>(this, hostedContentFilterPolicy, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x0018ACC4 File Offset: 0x00188EC4
		private HostedContentFilterRule GetPolicyScopingRule()
		{
			TransportRule transportRule = HygieneUtils.ResolvePolicyRuleObject<HostedContentFilterPolicy>(this.DataObject, this.ConfigurationSession, "HostedContentFilterVersioned");
			if (transportRule != null)
			{
				TransportRule transportRule2 = this.GetTransportRule(transportRule.Name);
				return HostedContentFilterRule.CreateFromInternalRule(transportRule, -1, transportRule2);
			}
			return null;
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x0018AD04 File Offset: 0x00188F04
		private TransportRule GetTransportRule(string ruleName)
		{
			ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager("HostedContentFilterVersioned", base.DataSession);
			adruleStorageManager.LoadRuleCollectionWithoutParsing(new TextFilter(ADObjectSchema.Name, ruleName, MatchOptions.FullString, MatchFlags.Default));
			TransportRule result = null;
			if (adruleStorageManager.Count > 0)
			{
				adruleStorageManager.GetRuleWithoutParsing(0, out result);
			}
			return result;
		}
	}
}

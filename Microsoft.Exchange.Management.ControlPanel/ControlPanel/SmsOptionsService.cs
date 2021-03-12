using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000486 RID: 1158
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SmsOptionsService : DataSourceService, ISmsOptionsService, IEditObjectService<SmsOptions, SetSmsOptions>, IGetObjectService<SmsOptions>
	{
		// Token: 0x060039F2 RID: 14834 RVA: 0x000AFA3C File Offset: 0x000ADC3C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self")]
		public PowerShellResults<SmsOptions> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			return base.GetObject<SmsOptions>("Get-TextMessagingAccount", identity);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000AFA60 File Offset: 0x000ADC60
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self+Clear-TextMessagingAccount?Identity@W:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self")]
		public PowerShellResults<SmsOptions> DisableObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("Clear-TextMessagingAccount");
			PowerShellResults results = base.Invoke(pscommand, new Identity[]
			{
				identity
			}, null);
			PowerShellResults<SmsOptions> @object = this.GetObject(identity);
			@object.MergeErrors(results);
			return @object;
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000AFAAC File Offset: 0x000ADCAC
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self+Compare-TextMessagingVerificationCode?Identity@W:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self")]
		public PowerShellResults<SmsOptions> SetObject(Identity identity, SetSmsOptions properties)
		{
			properties.FaultIfNull();
			identity = Identity.FromExecutingUserId();
			if (!string.IsNullOrEmpty(properties.VerificationCode))
			{
				PSCommand pscommand = new PSCommand();
				pscommand.AddCommand("Compare-TextMessagingVerificationCode");
				pscommand.AddParameter("VerificationCode", properties.VerificationCode);
				PowerShellResults results = base.Invoke(pscommand, new Identity[]
				{
					identity
				}, new BaseWebServiceParameters
				{
					ShouldContinue = properties.ShouldContinue
				});
				PowerShellResults<SmsOptions> @object = this.GetObject(identity);
				@object.MergeErrors(results);
				return @object;
			}
			if (!string.IsNullOrEmpty(properties.CountryCode) && !string.IsNullOrEmpty(properties.NotificationPhoneNumber) && !properties.NotificationPhoneNumber.StartsWith(properties.CountryCode))
			{
				properties.NotificationPhoneNumber = properties.CountryCode + properties.NotificationPhoneNumber;
			}
			return base.SetObject<SmsOptions, SetSmsOptions>("Set-TextMessagingAccount", identity, properties);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000AFB84 File Offset: 0x000ADD84
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self+Send-TextMessagingVerificationCode?Identity@W:Self")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self")]
		public PowerShellResults<SmsOptions> SendVerificationCode(Identity identity, SetSmsOptions properties)
		{
			properties.FaultIfNull();
			identity = Identity.FromExecutingUserId();
			properties.VerificationCode = null;
			PowerShellResults<SmsOptions> powerShellResults = this.SetObject(identity, properties);
			if (!powerShellResults.Failed)
			{
				PSCommand pscommand = new PSCommand();
				pscommand.AddCommand("Send-TextMessagingVerificationCode");
				PowerShellResults results = base.Invoke(pscommand, new Identity[]
				{
					identity
				}, new BaseWebServiceParameters
				{
					ShouldContinue = properties.ShouldContinue
				});
				powerShellResults.MergeErrors(results);
			}
			return powerShellResults;
		}

		// Token: 0x040026C7 RID: 9927
		internal const string DisableCmdlet = "Clear-TextMessagingAccount";

		// Token: 0x040026C8 RID: 9928
		internal const string GetCmdlet = "Get-TextMessagingAccount";

		// Token: 0x040026C9 RID: 9929
		internal const string SetCmdlet = "Set-TextMessagingAccount";

		// Token: 0x040026CA RID: 9930
		internal const string SendVerificationCodeCmdlet = "Send-TextMessagingVerificationCode";

		// Token: 0x040026CB RID: 9931
		internal const string CompareVerificationCodeCmdlet = "Compare-TextMessagingVerificationCode";

		// Token: 0x040026CC RID: 9932
		internal const string ReadScope = "@R:Self";

		// Token: 0x040026CD RID: 9933
		internal const string WriteScope = "@W:Self";

		// Token: 0x040026CE RID: 9934
		internal const string GetObjectRole = "Get-TextMessagingAccount?Identity@R:Self";

		// Token: 0x040026CF RID: 9935
		private const string DisableObjectRole = "Get-TextMessagingAccount?Identity@R:Self+Clear-TextMessagingAccount?Identity@W:Self";

		// Token: 0x040026D0 RID: 9936
		private const string SetObjectRole = "Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self";

		// Token: 0x040026D1 RID: 9937
		private const string CompareVerificationCodeRole = "Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self+Compare-TextMessagingVerificationCode?Identity@W:Self";

		// Token: 0x040026D2 RID: 9938
		private const string SendVerificationCodeRole = "Get-TextMessagingAccount?Identity@R:Self+Send-TextMessagingVerificationCode?Identity@W:Self";
	}
}

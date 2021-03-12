using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000CB RID: 203
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class VoiceMail : DataSourceService, IVoiceMail, IEditObjectService<GetVoiceMailConfiguration, SetVoiceMailConfiguration>, IGetObjectService<GetVoiceMailConfiguration>
	{
		// Token: 0x06001D3A RID: 7482 RVA: 0x00059A6C File Offset: 0x00057C6C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self")]
		public PowerShellResults<GetVoiceMailConfiguration> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<GetVoiceMailConfiguration> @object = base.GetObject<GetVoiceMailConfiguration>("Get-UMMailbox", identity);
			if (@object.SucceededWithValue)
			{
				PowerShellResults<SmsOptions> object2 = base.GetObject<SmsOptions>("Get-TextMessagingAccount", identity);
				@object.MergeErrors<SmsOptions>(object2);
				if (object2.SucceededWithValue)
				{
					@object.Value.SmsOptions = object2.Value;
				}
			}
			else if (@object.Failed && @object.ErrorRecords[0].Exception is ManagementObjectNotFoundException)
			{
				@object.ErrorRecords = new ErrorRecord[0];
			}
			return @object;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00059AF0 File Offset: 0x00057CF0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailbox?Identity@W:Self+Set-UMMailboxPIN?Identity@W:Self")]
		public PowerShellResults<GetVoiceMailConfiguration> SetObject(Identity identity, SetVoiceMailConfiguration properties)
		{
			properties.FaultIfNull();
			identity = Identity.FromExecutingUserId();
			PowerShellResults<GetVoiceMailConfiguration> powerShellResults = new PowerShellResults<GetVoiceMailConfiguration>();
			PowerShellResults<GetVoiceMailConfiguration> @object = this.GetObject(identity);
			powerShellResults.MergeErrors<GetVoiceMailConfiguration>(@object);
			if (powerShellResults.Failed)
			{
				return powerShellResults;
			}
			powerShellResults.MergeErrors<UMMailboxPin>(base.SetObject<UMMailboxPin, SetVoiceMailPIN>("Set-UMMailboxPIN", identity, properties.SetVoiceMailPIN));
			if (powerShellResults.Failed)
			{
				return powerShellResults;
			}
			properties.ReturnObjectType = ReturnObjectTypes.Full;
			powerShellResults.MergeAll(base.SetObject<GetVoiceMailConfiguration, SetVoiceMailConfiguration>("Set-UMMailbox", identity, properties));
			if (powerShellResults.SucceededWithValue)
			{
				GetVoiceMailConfiguration value = powerShellResults.Value;
				RbacPrincipal.Current.RbacConfiguration.ExecutingUserIsUmConfigured = value.IsConfigured;
				if (this.IsPhoneVerified(value.PhoneNumber, value) && !string.IsNullOrEmpty(properties.PhoneProviderId))
				{
					PowerShellResults<SmsOptions> results = this.SetTextMessagingAccount(identity, value.PhoneNumber, value.PhoneProviderId, value);
					powerShellResults.MergeErrors<SmsOptions>(results);
				}
			}
			return powerShellResults;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00059BC8 File Offset: 0x00057DC8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailbox?Identity@W:Self")]
		public PowerShellResults<GetVoiceMailConfiguration> ClearSettings(Identity identity)
		{
			return this.SetObject(identity, new SetVoiceMailConfiguration
			{
				PhoneNumber = string.Empty,
				PhoneProviderId = string.Empty,
				SMSNotificationOption = "None",
				PinlessAccessToVoiceMailEnabled = false
			});
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00059C0C File Offset: 0x00057E0C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self+Send-TextMessagingVerificationCode?Identity@W:Self+Compare-TextMessagingVerificationCode?Identity@W:Self")]
		public PowerShellResults<GetVoiceMailConfiguration> RegisterPhone(Identity identity, SetVoiceMailConfiguration properties)
		{
			properties.FaultIfNull();
			identity = Identity.FromExecutingUserId();
			PowerShellResults<GetVoiceMailConfiguration> @object = this.GetObject(identity);
			if (@object.Failed || string.IsNullOrEmpty(properties.PhoneNumber))
			{
				return @object;
			}
			GetVoiceMailConfiguration value = @object.Value;
			value.VerificationCodeRequired = !this.IsPhoneVerified(properties.PhoneNumber, value);
			if (string.IsNullOrEmpty(properties.PhoneProviderId))
			{
				properties.PhoneProviderId = value.PhoneProviderId;
			}
			if (value.VerificationCodeRequired)
			{
				if (!string.IsNullOrEmpty(properties.VerificationCode))
				{
					@object.MergeErrors(this.ComparePasscode(identity, properties.VerificationCode));
					value.VerificationCodeRequired = !@object.Succeeded;
				}
				else
				{
					PowerShellResults<SmsOptions> powerShellResults = this.SetTextMessagingAccount(identity, properties.PhoneNumber, properties.PhoneProviderId, value);
					@object.MergeErrors<SmsOptions>(powerShellResults);
					if (powerShellResults.SucceededWithValue)
					{
						value.SmsOptions = powerShellResults.Value;
						value.VerificationCodeRequired = !powerShellResults.Value.NotificationPhoneNumberVerified;
						if (value.VerificationCodeRequired)
						{
							@object.MergeErrors(this.SendVerificationCode(identity));
						}
					}
				}
			}
			return this.ClearOutputOnFailure(@object);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00059D1C File Offset: 0x00057F1C
		private PowerShellResults ComparePasscode(Identity identity, string passcode)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Compare-TextMessagingVerificationCode");
			pscommand.AddParameter("Identity", identity);
			pscommand.AddParameter("VerificationCode", passcode);
			return base.Invoke(pscommand);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00059D5C File Offset: 0x00057F5C
		private PowerShellResults<SmsOptions> SetTextMessagingAccount(Identity identity, string phoneNumber, string phoneProviderId, GetVoiceMailConfiguration voiceMailConfig)
		{
			SetSmsOptions setSmsOptions = new SetSmsOptions();
			setSmsOptions.CountryCode = "+" + voiceMailConfig.CountryOrRegionCode;
			setSmsOptions.CountryRegionId = voiceMailConfig.CountryOrRegionId;
			setSmsOptions.MobileOperatorId = phoneProviderId;
			setSmsOptions.NotificationPhoneNumber = setSmsOptions.CountryCode + phoneNumber;
			setSmsOptions.VerificationCode = null;
			PowerShellResults<SmsOptions> powerShellResults = new PowerShellResults<SmsOptions>();
			PowerShellResults results = base.SetObject<SmsOptions, SetSmsOptions>("Set-TextMessagingAccount", identity, setSmsOptions);
			powerShellResults.MergeErrors(results);
			if (powerShellResults.Succeeded)
			{
				powerShellResults.MergeAll(base.GetObject<SmsOptions>("Get-TextMessagingAccount", identity));
			}
			return powerShellResults;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00059DEC File Offset: 0x00057FEC
		private PowerShellResults SendVerificationCode(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Send-TextMessagingVerificationCode");
			pscommand.AddParameter("Identity", identity);
			return base.Invoke(pscommand);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00059E1D File Offset: 0x0005801D
		private bool IsPhoneVerified(string phoneNumber, GetVoiceMailConfiguration config)
		{
			return config.SmsOptions.NotificationPhoneNumberVerified && string.Equals(phoneNumber, config.SMSNotificationPhoneNumber, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00059E3B File Offset: 0x0005803B
		private PowerShellResults<GetVoiceMailConfiguration> ClearOutputOnFailure(PowerShellResults<GetVoiceMailConfiguration> results)
		{
			if (results.Failed)
			{
				results.Output = new GetVoiceMailConfiguration[0];
			}
			return results;
		}

		// Token: 0x04001BC0 RID: 7104
		internal const string GetCmdlet = "Get-UMMailbox";

		// Token: 0x04001BC1 RID: 7105
		internal const string SetCmdlet = "Set-UMMailbox";

		// Token: 0x04001BC2 RID: 7106
		internal const string GetSmsAccountCmdlet = "Get-TextMessagingAccount";

		// Token: 0x04001BC3 RID: 7107
		internal const string SetSmsAccountCmdlet = "Set-TextMessagingAccount";

		// Token: 0x04001BC4 RID: 7108
		internal const string SetUMMailboxPINCmdlet = "Set-UMMailboxPIN";

		// Token: 0x04001BC5 RID: 7109
		internal const string SendSmsPasscodeCmdlet = "Send-TextMessagingVerificationCode";

		// Token: 0x04001BC6 RID: 7110
		internal const string CompareSmsPasscodeCmdlet = "Compare-TextMessagingVerificationCode";

		// Token: 0x04001BC7 RID: 7111
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001BC8 RID: 7112
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001BC9 RID: 7113
		private const string GetObjectRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self";

		// Token: 0x04001BCA RID: 7114
		private const string SetObjectRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailbox?Identity@W:Self+Set-UMMailboxPIN?Identity@W:Self";

		// Token: 0x04001BCB RID: 7115
		private const string ClearSettingsRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailbox?Identity@W:Self";

		// Token: 0x04001BCC RID: 7116
		private const string RegisterPhoneRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-TextMessagingAccount?Identity@W:Self+Send-TextMessagingVerificationCode?Identity@W:Self+Compare-TextMessagingVerificationCode?Identity@W:Self";
	}
}

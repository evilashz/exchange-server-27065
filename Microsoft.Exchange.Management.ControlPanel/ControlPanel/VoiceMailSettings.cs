using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D4 RID: 212
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class VoiceMailSettings : DataSourceService, IVoiceMailSettings, IEditObjectService<GetVoiceMailSettings, SetVoiceMailSettings>, IGetObjectService<GetVoiceMailSettings>
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x0005A34C File Offset: 0x0005854C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self")]
		public PowerShellResults<GetVoiceMailSettings> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<GetVoiceMailSettings> @object = base.GetObject<GetVoiceMailSettings>("Get-UMMailbox", identity);
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

		// Token: 0x06001D69 RID: 7529 RVA: 0x0005A3CE File Offset: 0x000585CE
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailbox?Identity@W:Self")]
		public PowerShellResults<GetVoiceMailSettings> SetObject(Identity identity, SetVoiceMailSettings properties)
		{
			identity = Identity.FromExecutingUserId();
			return base.SetObject<GetVoiceMailSettings, SetVoiceMailSettings>("Set-UMMailbox", identity, properties);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0005A3E4 File Offset: 0x000585E4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailboxPIN?Identity@W:Self")]
		public PowerShellResults ResetPIN(Identity identity)
		{
			PSCommand pscommand = new PSCommand().AddCommand("Set-UMMailboxPIN");
			pscommand.AddParameter("Identity", Identity.FromExecutingUserId());
			pscommand.AddParameter("PinExpired", true);
			return base.Invoke(pscommand);
		}

		// Token: 0x04001BD6 RID: 7126
		internal const string GetCmdlet = "Get-UMMailbox";

		// Token: 0x04001BD7 RID: 7127
		internal const string GetSmsAccountCmdlet = "Get-TextMessagingAccount";

		// Token: 0x04001BD8 RID: 7128
		internal const string SetCmdlet = "Set-UMMailbox";

		// Token: 0x04001BD9 RID: 7129
		internal const string SetPINCmdlet = "Set-UMMailboxPIN";

		// Token: 0x04001BDA RID: 7130
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001BDB RID: 7131
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001BDC RID: 7132
		private const string GetObjectRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self";

		// Token: 0x04001BDD RID: 7133
		private const string SetObjectRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailbox?Identity@W:Self";

		// Token: 0x04001BDE RID: 7134
		private const string ResetPINRole = "Get-UMMailbox?Identity@R:Self+Get-TextMessagingAccount?Identity@R:Self+Set-UMMailboxPIN?Identity@W:Self";
	}
}

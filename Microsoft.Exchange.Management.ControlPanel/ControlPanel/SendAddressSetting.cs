using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002EB RID: 747
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class SendAddressSetting : DataSourceService, ISendAddressSetting, IEditObjectService<SendAddressConfiguration, SetSendAddressDefaultConfiguration>, IGetObjectService<SendAddressConfiguration>
	{
		// Token: 0x06002D14 RID: 11540 RVA: 0x0008A260 File Offset: 0x00088460
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<SendAddressConfiguration> GetObject(Identity identity)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<SendAddressConfiguration> @object = base.GetObject<SendAddressConfiguration>("Get-MailboxMessageConfiguration", identity);
			if (@object.Failed)
			{
				return @object;
			}
			SendAddress sendAddress = new SendAddress();
			PowerShellResults<SendAddressRow> list = sendAddress.GetList(new SendAddressFilter
			{
				AddressId = (@object.Value.SendAddressDefault ?? string.Empty),
				IgnoreNullOrEmpty = false
			}, null);
			if (list.Failed)
			{
				@object.MergeErrors<SendAddressRow>(list);
				return @object;
			}
			@object.Value.SendAddressDefault = list.Value.Value;
			return @object;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0008A2EC File Offset: 0x000884EC
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-MailboxMessageConfiguration?Identity@R:Self+MultiTenant+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<SendAddressConfiguration> SetObject(Identity identity, SetSendAddressDefaultConfiguration properties)
		{
			identity = Identity.FromExecutingUserId();
			PowerShellResults<SendAddressConfiguration> powerShellResults;
			lock (RbacPrincipal.Current.OwaOptionsLock)
			{
				powerShellResults = base.SetObject<SendAddressConfiguration, SetSendAddressDefaultConfiguration>("Set-MailboxMessageConfiguration", identity, properties);
			}
			if (powerShellResults.Succeeded)
			{
				Util.NotifyOWAUserSettingsChanged(UserSettings.Mail);
			}
			return powerShellResults;
		}

		// Token: 0x04002235 RID: 8757
		internal const string GetCmdlet = "Get-MailboxMessageConfiguration";

		// Token: 0x04002236 RID: 8758
		internal const string SetCmdlet = "Set-MailboxMessageConfiguration";

		// Token: 0x04002237 RID: 8759
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002238 RID: 8760
		internal const string WriteScope = "@W:Self";

		// Token: 0x04002239 RID: 8761
		private const string GetObjectRole = "MultiTenant+Get-MailboxMessageConfiguration?Identity@R:Self";

		// Token: 0x0400223A RID: 8762
		private const string SetObjectRole = "MultiTenant+Get-MailboxMessageConfiguration?Identity@R:Self+MultiTenant+Set-MailboxMessageConfiguration?Identity@W:Self";
	}
}

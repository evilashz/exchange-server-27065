using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A5 RID: 165
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ReadReceipts : MessagingBase, IReadReceipts, IMessagingBase<ReadReceiptsConfiguration, SetReadReceiptsConfiguration>, IEditObjectService<ReadReceiptsConfiguration, SetReadReceiptsConfiguration>, IGetObjectService<ReadReceiptsConfiguration>
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x00057D6A File Offset: 0x00055F6A
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<ReadReceiptsConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<ReadReceiptsConfiguration>(identity);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00057D73 File Offset: 0x00055F73
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<ReadReceiptsConfiguration> SetObject(Identity identity, SetReadReceiptsConfiguration properties)
		{
			return base.SetObject<ReadReceiptsConfiguration, SetReadReceiptsConfiguration>(identity, properties);
		}
	}
}

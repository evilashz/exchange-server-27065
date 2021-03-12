using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A0 RID: 160
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class ReadingPane : MessagingBase, IReadingPane, IMessagingBase<ReadingPaneConfiguration, SetReadingPaneConfiguration>, IEditObjectService<ReadingPaneConfiguration, SetReadingPaneConfiguration>, IGetObjectService<ReadingPaneConfiguration>
	{
		// Token: 0x06001C03 RID: 7171 RVA: 0x00057CCB File Offset: 0x00055ECB
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<ReadingPaneConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<ReadingPaneConfiguration>(identity);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00057CD4 File Offset: 0x00055ED4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<ReadingPaneConfiguration> SetObject(Identity identity, SetReadingPaneConfiguration properties)
		{
			return base.SetObject<ReadingPaneConfiguration, SetReadingPaneConfiguration>(identity, properties);
		}
	}
}

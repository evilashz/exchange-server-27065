using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000093 RID: 147
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class EMailSignature : MessagingBase, IEMailSignature, IMessagingBase<EMailSignatureConfiguration, SetEMailSignatureConfiguration>, IEditObjectService<EMailSignatureConfiguration, SetEMailSignatureConfiguration>, IGetObjectService<EMailSignatureConfiguration>
	{
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0005776C File Offset: 0x0005596C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self")]
		public PowerShellResults<EMailSignatureConfiguration> GetObject(Identity identity)
		{
			return base.GetObject<EMailSignatureConfiguration>(identity);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00057775 File Offset: 0x00055975
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxMessageConfiguration?Identity@R:Self+Set-MailboxMessageConfiguration?Identity@W:Self")]
		public PowerShellResults<EMailSignatureConfiguration> SetObject(Identity identity, SetEMailSignatureConfiguration properties)
		{
			return base.SetObject<EMailSignatureConfiguration, SetEMailSignatureConfiguration>(identity, properties);
		}
	}
}

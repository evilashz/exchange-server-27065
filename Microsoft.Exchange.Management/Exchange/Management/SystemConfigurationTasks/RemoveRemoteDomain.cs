using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD2 RID: 2770
	[Cmdlet("Remove", "RemoteDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class RemoveRemoteDomain : RemoveSystemConfigurationObjectTask<RemoteDomainIdParameter, DomainContentConfig>
	{
		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x0600626D RID: 25197 RVA: 0x0019AD34 File Offset: 0x00198F34
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRemoteDomain(this.Identity.ToString());
			}
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x0019AD46 File Offset: 0x00198F46
		protected override void InternalValidate()
		{
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x0019AD68 File Offset: 0x00198F68
		protected override void InternalProcessRecord()
		{
			if (base.DataObject.DomainName != null && base.DataObject.DomainName.Equals(SmtpDomainWithSubdomains.StarDomain))
			{
				base.WriteError(new CannotRemoveDefaultRemoteDomainException(), ErrorCategory.InvalidOperation, this.Identity);
				return;
			}
			base.InternalProcessRecord();
			FfoDualWriter.DeleteFromFfo<DomainContentConfig>(this, base.DataObject);
		}
	}
}

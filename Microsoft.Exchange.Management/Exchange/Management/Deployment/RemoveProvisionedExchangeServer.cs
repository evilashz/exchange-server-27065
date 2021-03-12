using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000224 RID: 548
	[Cmdlet("Remove", "ProvisionedExchangeServer")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class RemoveProvisionedExchangeServer : RemoveSystemConfigurationObjectTask<ServerIdParameter, Server>
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x00052228 File Offset: 0x00050428
		protected override void InternalProcessRecord()
		{
			if (base.DataObject.IsProvisionedServer)
			{
				((IConfigurationSession)base.DataSession).DeleteTree(base.DataObject, delegate(ADTreeDeleteNotFinishedException de)
				{
					base.WriteVerbose(de.LocalizedString);
				});
				return;
			}
			base.WriteError(new CannotRemoveNonProvisionedServerException(this.Identity.ToString()), ErrorCategory.InvalidOperation, null);
		}
	}
}

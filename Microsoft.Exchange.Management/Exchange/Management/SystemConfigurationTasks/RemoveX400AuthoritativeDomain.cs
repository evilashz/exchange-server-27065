using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB4 RID: 2740
	[Cmdlet("Remove", "X400AuthoritativeDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveX400AuthoritativeDomain : RemoveSystemConfigurationObjectTask<X400AuthoritativeDomainIdParameter, X400AuthoritativeDomain>
	{
		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x060060F1 RID: 24817 RVA: 0x00194A11 File Offset: 0x00192C11
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAcceptedDomain(this.Identity.ToString());
			}
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x00194A23 File Offset: 0x00192C23
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (TopologyProvider.IsAdamTopology())
			{
				base.WriteError(new CannotRunOnEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}
	}
}

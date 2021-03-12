using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB5 RID: 2741
	[Cmdlet("Set", "X400AuthoritativeDomain", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetX400AuthoritativeDomain : SetSystemConfigurationObjectTask<X400AuthoritativeDomainIdParameter, X400AuthoritativeDomain>
	{
		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x060060F4 RID: 24820 RVA: 0x00194A51 File Offset: 0x00192C51
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAcceptedDomain(this.Identity.ToString());
			}
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x00194A63 File Offset: 0x00192C63
		protected override void InternalValidate()
		{
			if (TopologyProvider.IsAdamTopology())
			{
				base.WriteError(new CannotRunOnEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			NewX400AuthoritativeDomain.ValidateNoDuplicates(this.DataObject, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
		}
	}
}

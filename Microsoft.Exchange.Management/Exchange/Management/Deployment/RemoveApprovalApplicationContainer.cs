using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200021D RID: 541
	[Cmdlet("Remove", "ApprovalApplicationContainer")]
	public sealed class RemoveApprovalApplicationContainer : RemoveSystemConfigurationObjectTask<ApprovalApplicationContainerIdParameter, ApprovalApplicationContainer>
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00051598 File Offset: 0x0004F798
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x000515AF File Offset: 0x0004F7AF
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override ApprovalApplicationContainerIdParameter Identity
		{
			get
			{
				return (ApprovalApplicationContainerIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000515C4 File Offset: 0x0004F7C4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.Identity == null)
				{
					this.Identity = ApprovalApplicationContainerIdParameter.Parse(ApprovalApplicationContainer.DefaultName);
				}
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}
	}
}

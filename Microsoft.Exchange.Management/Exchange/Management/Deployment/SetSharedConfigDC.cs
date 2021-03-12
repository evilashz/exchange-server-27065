using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000255 RID: 597
	[Cmdlet("Set", "SharedConfigDC")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class SetSharedConfigDC : Task
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001676 RID: 5750 RVA: 0x0005DC2C File Offset: 0x0005BE2C
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x0005DC43 File Offset: 0x0005BE43
		[LocDescription(Strings.IDs.DomainControllerName)]
		[Parameter(Mandatory = true)]
		public string DomainController
		{
			get
			{
				return (string)base.Fields["DomainController"];
			}
			set
			{
				base.Fields["DomainController"] = value;
			}
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0005DC58 File Offset: 0x0005BE58
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			Exception ex = null;
			try
			{
				ADSession.SetSharedConfigDC((base.CurrentOrganizationId.PartitionId != null) ? base.CurrentOrganizationId.PartitionId.ForestFQDN : TopologyProvider.LocalForestFqdn, this.DomainController, 389);
			}
			catch (DataSourceOperationException ex2)
			{
				ex = ex2;
			}
			catch (DataSourceTransientException ex3)
			{
				ex = ex3;
			}
			catch (DataValidationException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, null);
				}
				TaskLogger.LogExit();
			}
		}
	}
}

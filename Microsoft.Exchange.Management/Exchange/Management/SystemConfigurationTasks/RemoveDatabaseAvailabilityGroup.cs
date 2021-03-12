using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B9 RID: 2233
	[Cmdlet("Remove", "DatabaseAvailabilityGroup", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveDatabaseAvailabilityGroup : RemoveSystemConfigurationObjectTask<DatabaseAvailabilityGroupIdParameter, DatabaseAvailabilityGroup>
	{
		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x0014850E File Offset: 0x0014670E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDatabaseAvailabilityGroup(this.Identity.ToString());
			}
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x00148520 File Offset: 0x00146720
		private void CheckServerDagRemovalMembership(DatabaseAvailabilityGroup dag)
		{
			int count = dag.Servers.Count;
			if (count > 0)
			{
				base.WriteError(new RemoveDagNeedsZeroServersException(count), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x0014854C File Offset: 0x0014674C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			DatabaseAvailabilityGroup dataObject = base.DataObject;
			DagTaskHelper.VerifyDagIsWithinScopes<DatabaseAvailabilityGroup>(this, dataObject, true);
			this.CheckServerDagRemovalMembership(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x0014857F File Offset: 0x0014677F
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<DatabaseAvailabilityGroup>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorDagNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorDagNotUnique(this.Identity.ToString())));
		}
	}
}

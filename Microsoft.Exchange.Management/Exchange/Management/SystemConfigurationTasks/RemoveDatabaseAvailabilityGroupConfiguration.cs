using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008BA RID: 2234
	[Cmdlet("Remove", "DatabaseAvailabilityGroupConfiguration", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveDatabaseAvailabilityGroupConfiguration : RemoveSystemConfigurationObjectTask<DatabaseAvailabilityGroupConfigurationIdParameter, DatabaseAvailabilityGroupConfiguration>
	{
		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06004F13 RID: 20243 RVA: 0x001485C6 File Offset: 0x001467C6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDatabaseAvailabilityGroupConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x001485D8 File Offset: 0x001467D8
		private void CheckDagConfigurationRemovalMembership(DatabaseAvailabilityGroupConfiguration dagConfig)
		{
			int count = dagConfig.Dags.Count;
			if (count > 0)
			{
				base.WriteError(new RemoveDagConfigurationNeedsZeroDagsException(count), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x00148604 File Offset: 0x00146804
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			DatabaseAvailabilityGroupConfiguration dataObject = base.DataObject;
			this.CheckDagConfigurationRemovalMembership(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x0014862F File Offset: 0x0014682F
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<DatabaseAvailabilityGroupConfiguration>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorDagNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorDagNotUnique(this.Identity.ToString())));
		}
	}
}

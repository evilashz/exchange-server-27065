using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008BD RID: 2237
	[Cmdlet("Remove", "StampGroup", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveStampGroup : RemoveSystemConfigurationObjectTask<StampGroupIdParameter, StampGroup>
	{
		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x00149F74 File Offset: 0x00148174
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveDatabaseAvailabilityGroup(this.Identity.ToString());
			}
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00149F88 File Offset: 0x00148188
		private void CheckServerDagRemovalMembership(StampGroup stampGroup)
		{
			int count = stampGroup.Servers.Count;
			if (count > 0)
			{
				base.WriteError(new RemoveDagNeedsZeroServersException(count), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x00149FB4 File Offset: 0x001481B4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			StampGroup dataObject = base.DataObject;
			this.CheckServerDagRemovalMembership(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00149FDF File Offset: 0x001481DF
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<StampGroup>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorDagNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorDagNotUnique(this.Identity.ToString())));
		}
	}
}

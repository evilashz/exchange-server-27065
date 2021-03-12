using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000373 RID: 883
	[Cmdlet("Remove", "SyncServiceInstance", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncServiceInstance : RemoveSystemConfigurationObjectTask<ServiceInstanceIdParameter, SyncServiceInstance>
	{
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00085F52 File Offset: 0x00084152
		protected override ObjectId RootId
		{
			get
			{
				return SyncServiceInstance.GetMsoSyncRootContainer();
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00085F59 File Offset: 0x00084159
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveSyncServiceInstance(base.DataObject.Name);
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00085F6B File Offset: 0x0008416B
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x00085F73 File Offset: 0x00084173
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x06001F05 RID: 7941 RVA: 0x00085F7C File Offset: 0x0008417C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!this.Force && !SetSyncServiceInstance.IsServiceInstanceEmpty(base.DataObject))
			{
				base.WriteError(new InvalidOperationException(Strings.CannotRemoveServiceInstanceError(base.DataObject.Name)), ErrorCategory.InvalidOperation, base.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00085FE0 File Offset: 0x000841E0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity,
				base.DataObject
			});
			try
			{
				((ITopologyConfigurationSession)base.DataSession).DeleteTree(base.DataObject, null);
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, ExchangeErrorCategory.ServerTransient, base.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A1 RID: 2465
	[Cmdlet("Remove", "ClientAccessArray", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveClientAccessArray : RemoveSystemConfigurationObjectTask<ClientAccessArrayIdParameter, ClientAccessArray>
	{
		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x0017007F File Offset: 0x0016E27F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveClientAccessArray(this.Identity.ToString());
			}
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x00170094 File Offset: 0x0016E294
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.DataObject.ExchangeVersion.IsOlderThan(ClientAccessArray.MinimumSupportedExchangeObjectVersion))
			{
				base.WriteError(new TaskException(Strings.ErrorCannotChangeBecauseTooOld(base.DataObject.ExchangeVersion.ToString(), ClientAccessArray.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
			ClientAccessArray dataObject = base.DataObject;
			this.CheckArrayRemovalMembership(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x0017010B File Offset: 0x0016E30B
		protected override IConfigurable ResolveDataObject()
		{
			return base.GetDataObject<ClientAccessArray>(this.Identity, base.DataSession, null, new LocalizedString?(Strings.ErrorClientAccessArrayNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorClientAccessArrayNotUnique(this.Identity.ToString())));
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x0017014A File Offset: 0x0016E34A
		private void CheckArrayRemovalMembership(ClientAccessArray array)
		{
			if (array.Servers.Count > 0)
			{
				base.WriteError(new TaskException(Strings.ErrorArrayRemovalMembership(base.DataObject.Identity.ToString(), array.Servers.Count)), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}

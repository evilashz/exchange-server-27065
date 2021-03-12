using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A2 RID: 2466
	[Cmdlet("Remove", "RPCClientAccess", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRpcClientAccess : RemoveSingletonSystemConfigurationObjectTask<ExchangeRpcClientAccess>
	{
		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06005832 RID: 22578 RVA: 0x0017018F File Offset: 0x0016E38F
		// (set) Token: 0x06005833 RID: 22579 RVA: 0x001701A6 File Offset: 0x0016E3A6
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x001701B9 File Offset: 0x0016E3B9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveExchangeRpcClientAccess(this.casServer.Name);
			}
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x06005835 RID: 22581 RVA: 0x001701CC File Offset: 0x0016E3CC
		protected override ObjectId RootId
		{
			get
			{
				if (this.rootId == null)
				{
					this.casServer = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
					this.rootId = ExchangeRpcClientAccess.FromServerId(this.casServer.Id);
				}
				return this.rootId;
			}
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x00170245 File Offset: 0x0016E445
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.DataObject == null)
			{
				base.WriteError(new LocalizedException(Strings.ClientAccessRoleAbsent(this.casServer.Name)), ErrorCategory.ObjectNotFound, this.casServer);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x040032B6 RID: 12982
		private ObjectId rootId;

		// Token: 0x040032B7 RID: 12983
		private Server casServer;
	}
}

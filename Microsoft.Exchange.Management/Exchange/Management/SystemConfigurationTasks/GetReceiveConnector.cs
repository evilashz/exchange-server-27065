using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B2F RID: 2863
	[Cmdlet("Get", "ReceiveConnector", DefaultParameterSetName = "Identity")]
	public sealed class GetReceiveConnector : GetSystemConfigurationObjectTask<ReceiveConnectorIdParameter, ReceiveConnector>
	{
		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x060066ED RID: 26349 RVA: 0x001A9538 File Offset: 0x001A7738
		// (set) Token: 0x060066EE RID: 26350 RVA: 0x001A954F File Offset: 0x001A774F
		[Parameter(Mandatory = false, ParameterSetName = "Server", ValueFromPipeline = true)]
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

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x060066EF RID: 26351 RVA: 0x001A9562 File Offset: 0x001A7762
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x060066F0 RID: 26352 RVA: 0x001A9565 File Offset: 0x001A7765
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x001A9570 File Offset: 0x001A7770
		protected override void WriteResult(IConfigurable dataObject)
		{
			ReceiveConnector receiveConnector = dataObject as ReceiveConnector;
			if (receiveConnector != null && !receiveConnector.IsReadOnly)
			{
				Server permissionGroupsBasedOnSecurityDescriptor = (Server)base.GetDataObject<Server>(new ServerIdParameter(receiveConnector.Server), base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(receiveConnector.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(receiveConnector.Server.ToString())));
				if (base.HasErrors)
				{
					return;
				}
				try
				{
					receiveConnector.SetPermissionGroupsBasedOnSecurityDescriptor(permissionGroupsBasedOnSecurityDescriptor);
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidOperation, dataObject);
					return;
				}
				receiveConnector.ResetChangeTracking();
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x001A9618 File Offset: 0x001A7818
		protected override void InternalValidate()
		{
			if (this.Server != null)
			{
				Server server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				if (base.HasErrors)
				{
					return;
				}
				this.rootId = (ADObjectId)server.Identity;
			}
			base.InternalValidate();
		}

		// Token: 0x04003649 RID: 13897
		private ADObjectId rootId;
	}
}

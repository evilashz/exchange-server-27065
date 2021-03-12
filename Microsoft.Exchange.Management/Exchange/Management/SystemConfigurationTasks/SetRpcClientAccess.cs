using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A4 RID: 2468
	[Cmdlet("Set", "RpcClientAccess", SupportsShouldProcess = true, DefaultParameterSetName = "Server")]
	public sealed class SetRpcClientAccess : SetSingletonSystemConfigurationObjectTask<ExchangeRpcClientAccess>
	{
		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06005843 RID: 22595 RVA: 0x0017049C File Offset: 0x0016E69C
		// (set) Token: 0x06005844 RID: 22596 RVA: 0x001704B3 File Offset: 0x0016E6B3
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

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06005845 RID: 22597 RVA: 0x001704C6 File Offset: 0x0016E6C6
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x001704C9 File Offset: 0x0016E6C9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetExchangeRpcClientAccessServer(this.Server.ToString());
			}
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06005847 RID: 22599 RVA: 0x001704DC File Offset: 0x0016E6DC
		protected override ObjectId RootId
		{
			get
			{
				if (this.Server != null)
				{
					Server server = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
					return ExchangeRpcClientAccess.FromServerId(server.Id);
				}
				return null;
			}
		}
	}
}

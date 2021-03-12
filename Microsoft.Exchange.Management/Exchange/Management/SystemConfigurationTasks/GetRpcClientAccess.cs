using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200099E RID: 2462
	[Cmdlet("Get", "RpcClientAccess")]
	public sealed class GetRpcClientAccess : GetSingletonSystemConfigurationObjectTask<ExchangeRpcClientAccess>
	{
		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x0600580A RID: 22538 RVA: 0x0016FB07 File Offset: 0x0016DD07
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x0016FB0A File Offset: 0x0016DD0A
		// (set) Token: 0x0600580C RID: 22540 RVA: 0x0016FB21 File Offset: 0x0016DD21
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0016FB34 File Offset: 0x0016DD34
		protected override ObjectId RootId
		{
			get
			{
				if (this.casServer == null)
				{
					return null;
				}
				return ExchangeRpcClientAccess.FromServerId(this.casServer.Id);
			}
		}

		// Token: 0x0600580E RID: 22542 RVA: 0x0016FB50 File Offset: 0x0016DD50
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.Server != null)
			{
				this.casServer = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
				return;
			}
			this.casServer = null;
		}

		// Token: 0x0600580F RID: 22543 RVA: 0x0016FBBC File Offset: 0x0016DDBC
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ExchangeRpcClientAccess exchangeRpcClientAccess = (ExchangeRpcClientAccess)dataObject;
			exchangeRpcClientAccess.CompleteAllCalculatedProperties(this.casServer ?? this.ConfigurationSession.Read<Server>(exchangeRpcClientAccess.Server));
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x040032B2 RID: 12978
		private Server casServer;
	}
}

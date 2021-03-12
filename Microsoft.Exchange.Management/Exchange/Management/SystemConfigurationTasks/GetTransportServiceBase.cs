using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B6 RID: 2486
	public class GetTransportServiceBase : GetSystemConfigurationObjectTask<TransportServerIdParameter, Server>
	{
		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x060058AA RID: 22698 RVA: 0x001720C9 File Offset: 0x001702C9
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new BitMaskOrFilter(ServerSchema.CurrentServerRole, 96UL);
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x060058AB RID: 22699 RVA: 0x001720D8 File Offset: 0x001702D8
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060058AC RID: 22700 RVA: 0x001720DC File Offset: 0x001702DC
		protected override void InternalValidate()
		{
			Server server;
			try
			{
				server = ((ITopologyConfigurationSession)base.DataSession).ReadLocalServer();
			}
			catch (TransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ResourceUnavailable, null);
				return;
			}
			if (server != null)
			{
				this.isTaskOnEdge = server.IsEdgeServer;
				return;
			}
		}

		// Token: 0x060058AD RID: 22701 RVA: 0x0017212C File Offset: 0x0017032C
		protected override void WriteResult(IConfigurable dataObject)
		{
			Server server = (Server)dataObject;
			if (this.isTaskOnEdge && server.IsHubTransportServer)
			{
				return;
			}
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			base.WriteResult(new TransportServer(server));
			TaskLogger.LogExit();
		}

		// Token: 0x040032D7 RID: 13015
		private bool isTaskOnEdge;
	}
}

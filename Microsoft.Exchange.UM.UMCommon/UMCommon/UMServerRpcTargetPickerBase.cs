using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000031 RID: 49
	internal abstract class UMServerRpcTargetPickerBase<RpcTargetType> : ServerPickerBase<RpcTargetType, Guid> where RpcTargetType : class, IRpcTarget
	{
		// Token: 0x060002AE RID: 686
		protected abstract RpcTargetType CreateTarget(Server server);

		// Token: 0x060002AF RID: 687 RVA: 0x0000AA48 File Offset: 0x00008C48
		protected override List<RpcTargetType> LoadConfiguration()
		{
			string name = base.GetType().Name;
			CallIdTracer.TraceDebug(base.Tracer, this.GetHashCode(), "{0}.LoadConfiguration()", new object[]
			{
				name
			});
			List<RpcTargetType> list = new List<RpcTargetType>();
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			Server localServer = adtopologyLookup.GetLocalServer();
			if (localServer != null && localServer.IsUnifiedMessagingServer)
			{
				RpcTargetType rpcTargetType = this.CreateTarget(localServer);
				CallIdTracer.TraceDebug(base.Tracer, this.GetHashCode(), "{0}.LoadConfiguration() - Adding {1} to the target list", new object[]
				{
					name,
					rpcTargetType
				});
				list.Add(rpcTargetType);
			}
			return list;
		}
	}
}

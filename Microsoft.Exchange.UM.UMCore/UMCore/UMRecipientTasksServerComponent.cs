using System;
using System.DirectoryServices;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000223 RID: 547
	internal sealed class UMRecipientTasksServerComponent : UMRPCComponentBase
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00047369 File Offset: 0x00045569
		internal static UMRecipientTasksServerComponent Instance
		{
			get
			{
				return UMRecipientTasksServerComponent.instance;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00047370 File Offset: 0x00045570
		internal override void RegisterServer()
		{
			ActiveDirectorySecurity sd = null;
			Util.GetServerSecurityDescriptor(ref sd);
			RpcServerBase.RegisterServer(typeof(UMRecipientTasksServerComponent.UMRecipientTasksServer), sd, 131220);
		}

		// Token: 0x04000B5B RID: 2907
		private static UMRecipientTasksServerComponent instance = new UMRecipientTasksServerComponent();

		// Token: 0x02000224 RID: 548
		internal sealed class UMRecipientTasksServer : UMVersionedRpcServer
		{
			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x000473B0 File Offset: 0x000455B0
			protected override UMRPCComponentBase Component
			{
				get
				{
					return UMRecipientTasksServerComponent.Instance;
				}
			}

			// Token: 0x06000FD5 RID: 4053 RVA: 0x000473B7 File Offset: 0x000455B7
			protected override void PrepareRequest(UMVersionedRpcRequest request)
			{
			}

			// Token: 0x04000B5C RID: 2908
			public static IntPtr RpcIntfHandle = UMVersionedRpcServerBase.UMRecipientTasksRpcIntfHandle;
		}
	}
}

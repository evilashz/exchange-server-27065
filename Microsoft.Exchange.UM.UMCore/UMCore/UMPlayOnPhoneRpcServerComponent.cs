using System;
using System.DirectoryServices;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021F RID: 543
	internal sealed class UMPlayOnPhoneRpcServerComponent : UMRPCComponentBase
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00047268 File Offset: 0x00045468
		internal static UMPlayOnPhoneRpcServerComponent Instance
		{
			get
			{
				return UMPlayOnPhoneRpcServerComponent.instance;
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00047270 File Offset: 0x00045470
		internal override void RegisterServer()
		{
			ActiveDirectorySecurity sd = null;
			Util.GetServerSecurityDescriptor(ref sd);
			uint accessMask = 131220U;
			RpcServerBase.RegisterServer(typeof(UMPlayOnPhoneRpcServerComponent.UMPlayOnPhoneRpcServer), sd, accessMask);
		}

		// Token: 0x04000B57 RID: 2903
		private static UMPlayOnPhoneRpcServerComponent instance = new UMPlayOnPhoneRpcServerComponent();

		// Token: 0x02000220 RID: 544
		internal sealed class UMPlayOnPhoneRpcServer : UMVersionedRpcServer
		{
			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x000472B2 File Offset: 0x000454B2
			protected override UMRPCComponentBase Component
			{
				get
				{
					return UMPlayOnPhoneRpcServerComponent.Instance;
				}
			}

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000472B9 File Offset: 0x000454B9
			protected override bool ResponseIsMandatory
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000FC4 RID: 4036 RVA: 0x000472BC File Offset: 0x000454BC
			protected override void PrepareRequest(UMVersionedRpcRequest request)
			{
				((RequestBase)request).ProcessRequest = new ProcessRequestDelegate(RequestHandler.ProcessRequest);
			}

			// Token: 0x04000B58 RID: 2904
			public static IntPtr RpcIntfHandle = UMVersionedRpcServerBase.UMPlayOnPhoneRpcIntfHandle;
		}
	}
}

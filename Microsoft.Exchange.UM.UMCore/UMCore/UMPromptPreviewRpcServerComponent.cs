using System;
using System.DirectoryServices;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000221 RID: 545
	internal sealed class UMPromptPreviewRpcServerComponent : UMRPCComponentBase
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000472E9 File Offset: 0x000454E9
		internal static UMPromptPreviewRpcServerComponent Instance
		{
			get
			{
				return UMPromptPreviewRpcServerComponent.instance;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000472F0 File Offset: 0x000454F0
		internal override void RegisterServer()
		{
			ActiveDirectorySecurity sd = null;
			Util.GetServerSecurityDescriptor(ref sd);
			uint accessMask = 131220U;
			RpcServerBase.RegisterServer(typeof(UMPromptPreviewRpcServerComponent.UMPromptPreviewRpcServer), sd, accessMask);
		}

		// Token: 0x04000B59 RID: 2905
		private static UMPromptPreviewRpcServerComponent instance = new UMPromptPreviewRpcServerComponent();

		// Token: 0x02000222 RID: 546
		internal sealed class UMPromptPreviewRpcServer : UMVersionedRpcServer
		{
			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00047332 File Offset: 0x00045532
			protected override UMRPCComponentBase Component
			{
				get
				{
					return UMPromptPreviewRpcServerComponent.Instance;
				}
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00047339 File Offset: 0x00045539
			protected override bool ResponseIsMandatory
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000FCD RID: 4045 RVA: 0x0004733C File Offset: 0x0004553C
			protected override void PrepareRequest(UMVersionedRpcRequest request)
			{
				((RequestBase)request).ProcessRequest = new ProcessRequestDelegate(RequestHandler.ProcessRequest);
			}

			// Token: 0x04000B5A RID: 2906
			public static IntPtr RpcIntfHandle = UMVersionedRpcServerBase.UMPromptPreviewRpcIntfHandle;
		}
	}
}

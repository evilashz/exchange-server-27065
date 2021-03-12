using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000D9 RID: 217
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetAttachmentTableResultFactory : StandardResultFactory
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F10D File Offset: 0x0000D30D
		internal GetAttachmentTableResultFactory(ServerObjectHandle serverObjectHandle) : base(RopId.GetAttachmentTable)
		{
			this.ServerObjectHandle = serverObjectHandle;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000F11E File Offset: 0x0000D31E
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0000F126 File Offset: 0x0000D326
		public ServerObjectHandle ServerObjectHandle { get; private set; }

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F12F File Offset: 0x0000D32F
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulGetAttachmentTableResult(serverObject);
		}
	}
}

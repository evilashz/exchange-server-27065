using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200015C RID: 348
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferDownloadDelimitedObject : BaseObject, IFastTransferProcessor<FastTransferDownloadContext>, IDisposable
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x00012C3B File Offset: 0x00010E3B
		public FastTransferDownloadDelimitedObject(IFastTransferProcessor<FastTransferDownloadContext> enclosedObject, PropertyTag startMarker, PropertyTag endMarker)
		{
			this.enclosedObject = enclosedObject;
			this.startMarker = startMarker;
			this.endMarker = endMarker;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00012C58 File Offset: 0x00010E58
		protected override void InternalDispose()
		{
			this.enclosedObject.Dispose();
			base.InternalDispose();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00012C6B File Offset: 0x00010E6B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferDownloadDelimitedObject>(this);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00012D5C File Offset: 0x00010F5C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			context.DataInterface.PutMarker(this.startMarker);
			yield return null;
			yield return new FastTransferStateMachine?(context.CreateStateMachine(this.enclosedObject));
			context.DataInterface.PutMarker(this.endMarker);
			yield break;
		}

		// Token: 0x04000346 RID: 838
		private readonly IFastTransferProcessor<FastTransferDownloadContext> enclosedObject;

		// Token: 0x04000347 RID: 839
		private readonly PropertyTag startMarker;

		// Token: 0x04000348 RID: 840
		private readonly PropertyTag endMarker;
	}
}

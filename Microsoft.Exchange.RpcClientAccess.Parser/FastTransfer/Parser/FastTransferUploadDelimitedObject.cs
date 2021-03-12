using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200015D RID: 349
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferUploadDelimitedObject : BaseObject, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x00012D7F File Offset: 0x00010F7F
		public FastTransferUploadDelimitedObject(IFastTransferProcessor<FastTransferUploadContext> enclosedObject, PropertyTag startMarker, PropertyTag endMarker)
		{
			this.enclosedObject = enclosedObject;
			this.startMarker = startMarker;
			this.endMarker = endMarker;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00012D9C File Offset: 0x00010F9C
		protected override void InternalDispose()
		{
			this.enclosedObject.Dispose();
			base.InternalDispose();
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00012DAF File Offset: 0x00010FAF
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferUploadDelimitedObject>(this);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00012EA0 File Offset: 0x000110A0
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			context.DataInterface.ReadMarker(this.startMarker);
			yield return null;
			yield return new FastTransferStateMachine?(context.CreateStateMachine(this.enclosedObject));
			context.DataInterface.ReadMarker(this.endMarker);
			yield break;
		}

		// Token: 0x04000349 RID: 841
		private readonly IFastTransferProcessor<FastTransferUploadContext> enclosedObject;

		// Token: 0x0400034A RID: 842
		private readonly PropertyTag startMarker;

		// Token: 0x0400034B RID: 843
		private readonly PropertyTag endMarker;
	}
}

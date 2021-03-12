using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000173 RID: 371
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferSkipDnPrefix : BaseObject, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x00019883 File Offset: 0x00017A83
		public FastTransferSkipDnPrefix(IFastTransferProcessor<FastTransferUploadContext> enclosedObject)
		{
			this.enclosedObject = enclosedObject;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00019892 File Offset: 0x00017A92
		protected override void InternalDispose()
		{
			this.enclosedObject.Dispose();
			base.InternalDispose();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000198A5 File Offset: 0x00017AA5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferSkipDnPrefix>(this);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001996C File Offset: 0x00017B6C
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			yield return new FastTransferStateMachine?(FastTransferPropertyValue.SkipPropertyIfExists(context, PropertyTag.DNPrefix));
			yield return new FastTransferStateMachine?(context.CreateStateMachine(this.enclosedObject));
			yield break;
		}

		// Token: 0x04000393 RID: 915
		private readonly IFastTransferProcessor<FastTransferUploadContext> enclosedObject;
	}
}

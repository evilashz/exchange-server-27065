using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferInjectFailure : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IDisposable
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0001E09C File Offset: 0x0001C29C
		public FastTransferInjectFailure(Exception failureException) : base(true)
		{
			this.failureException = failureException;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001E128 File Offset: 0x0001C328
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			yield return this.ThrowFailureException();
			yield break;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001E144 File Offset: 0x0001C344
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferInjectFailure>(this);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001E14C File Offset: 0x0001C34C
		private FastTransferStateMachine? ThrowFailureException()
		{
			throw this.failureException;
		}

		// Token: 0x0400016B RID: 363
		private readonly Exception failureException;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ManifestCallbackQueue<TCallback> where TCallback : class
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C2D5 File Offset: 0x0000A4D5
		public int Count
		{
			get
			{
				return this.callbackInvocations.Count;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
		public void Enqueue(ManifestCallbackQueue<TCallback>.CallbackInvocation callbackInvocation)
		{
			this.callbackInvocations.Enqueue(callbackInvocation);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public ManifestCallbackStatus Execute(TCallback callback)
		{
			return ManifestCallbackQueue<TCallback>.Execute(callback, this.DequeueCallbackInvocations());
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C2FE File Offset: 0x0000A4FE
		public ManifestCallbackStatus ExecuteNoDequeue(TCallback callback)
		{
			return ManifestCallbackQueue<TCallback>.Execute(callback, this.callbackInvocations);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		private IEnumerable<ManifestCallbackQueue<TCallback>.CallbackInvocation> DequeueCallbackInvocations()
		{
			while (this.callbackInvocations.Count > 0)
			{
				yield return this.callbackInvocations.Dequeue();
			}
			yield break;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C414 File Offset: 0x0000A614
		private static ManifestCallbackStatus Execute(TCallback callback, IEnumerable<ManifestCallbackQueue<TCallback>.CallbackInvocation> callbackInvocations)
		{
			ManifestCallbackStatus manifestCallbackStatus = ManifestCallbackStatus.Continue;
			foreach (ManifestCallbackQueue<TCallback>.CallbackInvocation callbackInvocation in callbackInvocations)
			{
				manifestCallbackStatus = callbackInvocation(callback);
				if (manifestCallbackStatus != ManifestCallbackStatus.Continue)
				{
					break;
				}
			}
			return manifestCallbackStatus;
		}

		// Token: 0x0400049C RID: 1180
		private readonly Queue<ManifestCallbackQueue<TCallback>.CallbackInvocation> callbackInvocations = new Queue<ManifestCallbackQueue<TCallback>.CallbackInvocation>();

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x060002D9 RID: 729
		public delegate ManifestCallbackStatus CallbackInvocation(TCallback callback);
	}
}

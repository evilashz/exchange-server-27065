using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ManifestCallbackHelperBase<TCallback> where TCallback : class
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		protected ManifestCallbackHelperBase()
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(32))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(15538, 32, (long)this.GetHashCode(), "ManifestCallbackHelper.ManifestCallbackHelper: this={0}", TraceUtils.MakeHash(this));
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000BD40 File Offset: 0x00009F40
		public ManifestCallbackQueue<TCallback> Changes
		{
			get
			{
				return this.changeList;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000BD48 File Offset: 0x00009F48
		public ManifestCallbackQueue<TCallback> Deletes
		{
			get
			{
				return this.deleteList;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000BD50 File Offset: 0x00009F50
		public ManifestStatus DoCallbacks(TCallback callback, params ManifestCallbackQueue<TCallback>[] callbackQueues)
		{
			ManifestCallbackStatus manifestCallbackStatus = ManifestCallbackStatus.Continue;
			foreach (ManifestCallbackQueue<TCallback> manifestCallbackQueue in callbackQueues)
			{
				manifestCallbackStatus = manifestCallbackQueue.Execute(callback);
				if (manifestCallbackStatus != ManifestCallbackStatus.Continue)
				{
					break;
				}
			}
			switch (manifestCallbackStatus)
			{
			case ManifestCallbackStatus.Continue:
				return ManifestStatus.Done;
			case ManifestCallbackStatus.Stop:
				return ManifestStatus.Stopped;
			case ManifestCallbackStatus.Yield:
				return ManifestStatus.Yielded;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0400046E RID: 1134
		private readonly ManifestCallbackQueue<TCallback> changeList = new ManifestCallbackQueue<TCallback>();

		// Token: 0x0400046F RID: 1135
		private readonly ManifestCallbackQueue<TCallback> deleteList = new ManifestCallbackQueue<TCallback>();
	}
}

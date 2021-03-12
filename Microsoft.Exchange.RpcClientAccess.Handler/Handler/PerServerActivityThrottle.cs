using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PerServerActivityThrottle<TContext>
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00006ECC File Offset: 0x000050CC
		public static PerServerActivityThrottle<TContext> GetPerServerActivityThrottle(string server)
		{
			PerServerActivityThrottle<TContext> result;
			lock (PerServerActivityThrottle<TContext>.cacheLock)
			{
				PerServerActivityThrottle<TContext> perServerActivityThrottle = null;
				if (!PerServerActivityThrottle<TContext>.cache.TryGetValue(server, out perServerActivityThrottle))
				{
					perServerActivityThrottle = new PerServerActivityThrottle<TContext>();
					PerServerActivityThrottle<TContext>.cache.Add(server, perServerActivityThrottle);
				}
				result = perServerActivityThrottle;
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006F2C File Offset: 0x0000512C
		public bool TryGetActivityLock(bool force, int maxActivity, out IDisposable activityLock)
		{
			activityLock = null;
			bool result;
			lock (this.activityCounterLock)
			{
				if (!force && this.activityCounter >= maxActivity)
				{
					result = false;
				}
				else
				{
					activityLock = new PerServerActivityThrottle<TContext>.ActivityLock(this);
					this.activityCounter++;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006F98 File Offset: 0x00005198
		private void DecreaseActivityCount()
		{
			lock (this.activityCounterLock)
			{
				if (this.activityCounter == 0)
				{
					throw new InvalidOperationException("Activity counter already zero and cannot be decreased.");
				}
				this.activityCounter--;
			}
		}

		// Token: 0x04000058 RID: 88
		private static readonly Dictionary<string, PerServerActivityThrottle<TContext>> cache = new Dictionary<string, PerServerActivityThrottle<TContext>>();

		// Token: 0x04000059 RID: 89
		private static readonly object cacheLock = new object();

		// Token: 0x0400005A RID: 90
		private int activityCounter;

		// Token: 0x0400005B RID: 91
		private readonly object activityCounterLock = new object();

		// Token: 0x02000016 RID: 22
		private struct ActivityLock : IDisposeTrackable, IDisposable
		{
			// Token: 0x060000BA RID: 186 RVA: 0x0000701D File Offset: 0x0000521D
			internal ActivityLock(PerServerActivityThrottle<TContext> perServerActivityThrottle)
			{
				this.perServerActivityThrottle = perServerActivityThrottle;
				this.isDisposed = false;
				this.disposeTracker = null;
				this.disposeTracker = DisposeTracker.Get<PerServerActivityThrottle<TContext>.ActivityLock>(this);
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00007045 File Offset: 0x00005245
			public void Dispose()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					if (this.perServerActivityThrottle != null)
					{
						this.perServerActivityThrottle.DecreaseActivityCount();
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
			}

			// Token: 0x060000BC RID: 188 RVA: 0x0000707C File Offset: 0x0000527C
			DisposeTracker IDisposeTrackable.GetDisposeTracker()
			{
				return DisposeTracker.Get<PerServerActivityThrottle<TContext>.ActivityLock>(this);
			}

			// Token: 0x060000BD RID: 189 RVA: 0x00007089 File Offset: 0x00005289
			void IDisposeTrackable.SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x0400005C RID: 92
			private readonly DisposeTracker disposeTracker;

			// Token: 0x0400005D RID: 93
			private readonly PerServerActivityThrottle<TContext> perServerActivityThrottle;

			// Token: 0x0400005E RID: 94
			private bool isDisposed;
		}
	}
}

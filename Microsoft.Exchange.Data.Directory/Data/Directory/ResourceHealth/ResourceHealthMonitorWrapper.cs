using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F7 RID: 2551
	internal class ResourceHealthMonitorWrapper : IResourceLoadMonitor
	{
		// Token: 0x06007657 RID: 30295 RVA: 0x001858C9 File Offset: 0x00183AC9
		internal ResourceHealthMonitorWrapper(CacheableResourceHealthMonitor monitor)
		{
			if (monitor == null)
			{
				throw new ArgumentNullException("monitor");
			}
			this.wrappedMonitor = monitor;
		}

		// Token: 0x17002A62 RID: 10850
		// (get) Token: 0x06007658 RID: 30296 RVA: 0x001858F1 File Offset: 0x00183AF1
		public ResourceKey Key
		{
			get
			{
				this.CheckExpired();
				return this.wrappedMonitor.Key;
			}
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x00185904 File Offset: 0x00183B04
		public int GetWrappedHashCode()
		{
			this.CheckExpired();
			return this.wrappedMonitor.GetHashCode();
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x00185917 File Offset: 0x00183B17
		internal T GetWrappedMonitor<T>() where T : CacheableResourceHealthMonitor
		{
			this.CheckExpired();
			return this.wrappedMonitor as T;
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x00185930 File Offset: 0x00183B30
		protected internal void CheckExpired()
		{
			while (this.wrappedMonitor.Expired)
			{
				lock (this.instanceLock)
				{
					if (this.wrappedMonitor.Expired)
					{
						IResourceLoadMonitor resourceLoadMonitor = ResourceHealthMonitorManager.Singleton.Get(this.wrappedMonitor.Key);
						this.wrappedMonitor = (resourceLoadMonitor as ResourceHealthMonitorWrapper).wrappedMonitor;
					}
				}
				if (this.wrappedMonitor.Expired)
				{
					ExTraceGlobals.ClientThrottlingTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorWrapper.CheckExpired] Retrieved expired monitor from cache '{0}'.  Will try again.", this.wrappedMonitor.Key);
					Thread.Yield();
				}
			}
		}

		// Token: 0x17002A63 RID: 10851
		// (get) Token: 0x0600765C RID: 30300 RVA: 0x001859E8 File Offset: 0x00183BE8
		public DateTime LastUpdateUtc
		{
			get
			{
				this.CheckExpired();
				return this.wrappedMonitor.LastUpdateUtc;
			}
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x001859FB File Offset: 0x00183BFB
		public ResourceLoad GetResourceLoad(WorkloadType type, bool raw = false, object optionalData = null)
		{
			this.CheckExpired();
			return this.wrappedMonitor.GetResourceLoad(type, raw, optionalData);
		}

		// Token: 0x0600765E RID: 30302 RVA: 0x00185A11 File Offset: 0x00183C11
		public ResourceLoad GetResourceLoad(WorkloadClassification classification, bool raw = false, object optionalData = null)
		{
			this.CheckExpired();
			return this.wrappedMonitor.GetResourceLoad(classification, raw, optionalData);
		}

		// Token: 0x04004BBC RID: 19388
		private CacheableResourceHealthMonitor wrappedMonitor;

		// Token: 0x04004BBD RID: 19389
		private object instanceLock = new object();
	}
}

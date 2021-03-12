using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000034 RID: 52
	internal class ResourceReservation : DisposeTrackableBase
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00008104 File Offset: 0x00006304
		internal ResourceReservation(WorkloadClassification classification, IEnumerable<IResourceAdmissionControl> reserved, double delayFactor)
		{
			if (reserved == null)
			{
				throw new ArgumentNullException("reserved");
			}
			if (delayFactor < 0.0)
			{
				throw new ArgumentOutOfRangeException("delayFactor", "Delay factor must be greater or equal to 0.");
			}
			this.Classification = classification;
			this.reserved = reserved;
			this.DelayFactor = delayFactor;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008161 File Offset: 0x00006361
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00008169 File Offset: 0x00006369
		public WorkloadClassification Classification { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008174 File Offset: 0x00006374
		public IEnumerable<ResourceKey> Resources
		{
			get
			{
				if (this.resources == null)
				{
					lock (this.lockObject)
					{
						if (this.resources == null)
						{
							this.resources = new List<ResourceKey>();
							foreach (IResourceAdmissionControl resourceAdmissionControl in this.reserved)
							{
								this.resources.Add(resourceAdmissionControl.ResourceKey);
							}
						}
					}
				}
				return this.resources;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008218 File Offset: 0x00006418
		public bool IsActive
		{
			get
			{
				return !base.IsDisposed;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008223 File Offset: 0x00006423
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000822B File Offset: 0x0000642B
		public double DelayFactor { get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x00008234 File Offset: 0x00006434
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.reserved != null)
			{
				lock (this.lockObject)
				{
					foreach (IResourceAdmissionControl resourceAdmissionControl in this.reserved)
					{
						resourceAdmissionControl.Release(this.Classification);
					}
					this.reserved = ResourceReservation.emptyReserved;
				}
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000082C8 File Offset: 0x000064C8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ResourceReservation>(this);
		}

		// Token: 0x040000F1 RID: 241
		private static readonly IEnumerable<IResourceAdmissionControl> emptyReserved = new IResourceAdmissionControl[0];

		// Token: 0x040000F2 RID: 242
		private IEnumerable<IResourceAdmissionControl> reserved;

		// Token: 0x040000F3 RID: 243
		private List<ResourceKey> resources;

		// Token: 0x040000F4 RID: 244
		private object lockObject = new object();
	}
}

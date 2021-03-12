using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000035 RID: 53
	internal class ResourceReservationContext : DisposeTrackableBase
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x000082DD File Offset: 0x000064DD
		public ResourceReservationContext() : this(false)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000082E6 File Offset: 0x000064E6
		internal ResourceReservationContext(bool ignoreImplicitLocalCpuResource) : this(new ResourceAdmissionControlManager(), ignoreImplicitLocalCpuResource)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000082F4 File Offset: 0x000064F4
		internal ResourceReservationContext(ResourceAdmissionControlManager admissionManager) : this(admissionManager, false)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000082FE File Offset: 0x000064FE
		internal ResourceReservationContext(ResourceAdmissionControlManager admissionManager, bool ignoreImplicitLocalCpuResource)
		{
			this.admissionManager = admissionManager;
			this.ignoreImplicitLocalCpuResource = ignoreImplicitLocalCpuResource;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00008314 File Offset: 0x00006514
		internal ResourceAdmissionControlManager AdmissionControlManager
		{
			get
			{
				return this.admissionManager;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000831C File Offset: 0x0000651C
		public ResourceReservation GetUnthrottledReservation(SystemWorkloadBase workload)
		{
			if (workload.WorkloadType != WorkloadType.MailboxReplicationServiceHighPriority && workload.WorkloadType != WorkloadType.MailboxReplicationServiceInteractive && workload.WorkloadType != WorkloadType.MailboxReplicationServiceInternalMaintenance)
			{
				WorkloadType workloadType = workload.WorkloadType;
			}
			return new ResourceReservation(workload.Classification, ResourceReservationContext.emptyReservation, 0.0);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000835C File Offset: 0x0000655C
		public ResourceReservation GetReservation(SystemWorkloadBase workload, IEnumerable<ResourceKey> resources)
		{
			return this.GetReservation(workload.Classification, resources);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000836B File Offset: 0x0000656B
		public ResourceReservation GetReservation(SystemWorkloadBase workload, IEnumerable<ResourceKey> resources, out ResourceKey throttledResource)
		{
			return this.GetReservation(workload.Classification, resources, out throttledResource);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000837C File Offset: 0x0000657C
		public ResourceReservation GetReservation(WorkloadClassification classification, IEnumerable<ResourceKey> resources)
		{
			ResourceKey resourceKey = null;
			return this.GetReservation(classification, resources, out resourceKey);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008398 File Offset: 0x00006598
		public ResourceReservation GetReservation(WorkloadClassification classification, IEnumerable<ResourceKey> resources, out ResourceKey throttledResource)
		{
			if (resources == null)
			{
				throw new ArgumentNullException("resources");
			}
			List<IResourceAdmissionControl> list = null;
			ResourceReservation resourceReservation = null;
			double delayFactor = 0.0;
			bool flag = false;
			throttledResource = null;
			lock (this.admissionManager)
			{
				try
				{
					foreach (ResourceKey resourceKey in resources)
					{
						if (!this.GetReservationForResource(classification, resourceKey, ref list, ref delayFactor, ref throttledResource))
						{
							return null;
						}
						if (!flag)
						{
							flag = resourceKey.Equals(ProcessorResourceKey.Local);
						}
					}
					if (!flag && !this.ignoreImplicitLocalCpuResource && !this.GetReservationForResource(classification, ProcessorResourceKey.Local, ref list, ref delayFactor, ref throttledResource))
					{
						return null;
					}
					resourceReservation = new ResourceReservation(classification, list ?? ResourceReservationContext.emptyReservation, delayFactor);
				}
				finally
				{
					if (resourceReservation == null && list != null)
					{
						foreach (IResourceAdmissionControl resourceAdmissionControl in list)
						{
							resourceAdmissionControl.Release(classification);
						}
					}
				}
			}
			return resourceReservation;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000084E4 File Offset: 0x000066E4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.admissionManager.Dispose();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000084F4 File Offset: 0x000066F4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ResourceReservationContext>(this);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000084FC File Offset: 0x000066FC
		private bool GetReservationForResource(WorkloadClassification classification, ResourceKey resource, ref List<IResourceAdmissionControl> reservedAdmissions, ref double delayRatio, ref ResourceKey throttledResource)
		{
			try
			{
				IResourceAdmissionControl resourceAdmissionControl = this.admissionManager.Get(resource);
				double num = 0.0;
				if (!resourceAdmissionControl.TryAcquire(classification, out num))
				{
					ExTraceGlobals.SchedulerTracer.TraceDebug<ResourceKey, WorkloadClassification>((long)this.GetHashCode(), "[ResourceReservationContext.GetReservationForResource] Unable to reserve resource {0}. Classification: {1}", resource, classification);
					throttledResource = resource;
					return false;
				}
				if (reservedAdmissions == null)
				{
					reservedAdmissions = new List<IResourceAdmissionControl>();
				}
				reservedAdmissions.Add(resourceAdmissionControl);
				if (num > delayRatio)
				{
					delayRatio = num;
					throttledResource = resource;
				}
			}
			catch (NonOperationalAdmissionControlException arg)
			{
				ExTraceGlobals.SchedulerTracer.TraceError<NonOperationalAdmissionControlException>((long)this.GetHashCode(), "[ResourceReservationContext.GetReservationForResource] Encountered exception while reserving resources: {1}", arg);
				return false;
			}
			return true;
		}

		// Token: 0x040000F7 RID: 247
		private static readonly IEnumerable<IResourceAdmissionControl> emptyReservation = new IResourceAdmissionControl[0];

		// Token: 0x040000F8 RID: 248
		private readonly bool ignoreImplicitLocalCpuResource;

		// Token: 0x040000F9 RID: 249
		private ResourceAdmissionControlManager admissionManager;
	}
}

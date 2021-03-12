using System;
using System.Text;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000194 RID: 404
	internal class PrioritizedLatencyPerformanceCounter : ILatencyPerformanceCounter
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x000483EE File Offset: 0x000465EE
		public LatencyPerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000483F6 File Offset: 0x000465F6
		private PrioritizedLatencyPerformanceCounter(LatencyPerformanceCounterType type)
		{
			this.counterType = type;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00048405 File Offset: 0x00046605
		public static ILatencyPerformanceCounter CreateInstance(string instanceName, TimeSpan expiryInterval, long infinitySeconds)
		{
			return PrioritizedLatencyPerformanceCounter.CreateInstance(instanceName, expiryInterval, infinitySeconds, LatencyPerformanceCounterType.Local);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00048410 File Offset: 0x00046610
		public static ILatencyPerformanceCounter CreateInstance(string instanceName, TimeSpan expiryInterval, long infinitySeconds, LatencyPerformanceCounterType type)
		{
			PrioritizedLatencyPerformanceCounter prioritizedLatencyPerformanceCounter = new PrioritizedLatencyPerformanceCounter(type);
			prioritizedLatencyPerformanceCounter.perfCounters = new LatencyPerformanceCounter[PrioritizedLatencyPerformanceCounter.priorityValues.Length];
			bool flag = true;
			foreach (DeliveryPriority deliveryPriority in PrioritizedLatencyPerformanceCounter.priorityValues)
			{
				prioritizedLatencyPerformanceCounter.perfCounters[(int)deliveryPriority] = LatencyPerformanceCounter.CreateInstance(PrioritizedLatencyPerformanceCounter.GetInstanceName(instanceName, deliveryPriority), expiryInterval, infinitySeconds, false, type);
				if (prioritizedLatencyPerformanceCounter.perfCounters[(int)deliveryPriority] == null)
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				return null;
			}
			return prioritizedLatencyPerformanceCounter;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00048481 File Offset: 0x00046681
		public void AddValue(long latencySeconds)
		{
			this.AddValue(latencySeconds, DeliveryPriority.Normal);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004848C File Offset: 0x0004668C
		public void AddValue(long latencySeconds, DeliveryPriority priority)
		{
			if (priority < DeliveryPriority.High || priority > (DeliveryPriority)PrioritizedLatencyPerformanceCounter.priorityValues.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Provided priority '{0}' is outside valid range", priority));
			}
			if (latencySeconds >= 0L)
			{
				this.perfCounters[(int)priority].AddValue(latencySeconds);
				this.perfCounters[(int)priority].Update();
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000484E0 File Offset: 0x000466E0
		public void Update()
		{
			foreach (DeliveryPriority deliveryPriority in PrioritizedLatencyPerformanceCounter.priorityValues)
			{
				this.perfCounters[(int)deliveryPriority].Update();
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00048514 File Offset: 0x00046714
		public void Reset()
		{
			foreach (DeliveryPriority deliveryPriority in PrioritizedLatencyPerformanceCounter.priorityValues)
			{
				this.perfCounters[(int)deliveryPriority].Reset();
			}
			this.Update();
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004854C File Offset: 0x0004674C
		private static string GetInstanceName(string instanceName, DeliveryPriority priority)
		{
			StringBuilder stringBuilder = new StringBuilder(instanceName);
			stringBuilder.Append(" - ");
			stringBuilder.Append(priority.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04000961 RID: 2401
		private static readonly DeliveryPriority[] priorityValues = (DeliveryPriority[])Enum.GetValues(typeof(DeliveryPriority));

		// Token: 0x04000962 RID: 2402
		private ILatencyPerformanceCounter[] perfCounters;

		// Token: 0x04000963 RID: 2403
		private LatencyPerformanceCounterType counterType;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000025 RID: 37
	internal class PriorityDistributedQueue : ISchedulerQueue
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00004188 File Offset: 0x00002388
		public PriorityDistributedQueue(IDictionary<DeliveryPriority, int> distributions)
		{
			this.priorityDistributions = PriorityDistributedQueue.ValidateAndGetDistributionArray(distributions);
			this.perPrioritySubQueues = new ISchedulerQueue[this.priorityDistributions.Length];
			for (int i = 0; i < this.perPrioritySubQueues.Length; i++)
			{
				this.perPrioritySubQueues[i] = new ConcurrentQueueWrapper();
			}
			this.selectedPriorityIndex = 0;
			this.selectedPriorityItemCount = this.priorityDistributions[this.selectedPriorityIndex];
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000041F4 File Offset: 0x000023F4
		public bool IsEmpty
		{
			get
			{
				foreach (ISchedulerQueue schedulerQueue in this.perPrioritySubQueues)
				{
					if (!schedulerQueue.IsEmpty)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000422C File Offset: 0x0000242C
		public long Count
		{
			get
			{
				long num = 0L;
				foreach (ISchedulerQueue schedulerQueue in this.perPrioritySubQueues)
				{
					num += schedulerQueue.Count;
				}
				return num;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000426C File Offset: 0x0000246C
		public void Enqueue(ISchedulableMessage message)
		{
			ArgumentValidator.ThrowIfNull("message", message);
			IMessageScope messageScope = null;
			if (message.Scopes != null)
			{
				messageScope = message.Scopes.FirstOrDefault((IMessageScope x) => x.Type == MessageScopeType.Priority);
			}
			if (messageScope == null)
			{
				throw new ArgumentException(string.Format("Message does not have a priority scope associated with it:{0}", message));
			}
			int num = (int)((DeliveryPriority)messageScope.Value);
			this.perPrioritySubQueues[num].Enqueue(message);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000042E8 File Offset: 0x000024E8
		public bool TryDequeue(out ISchedulableMessage message)
		{
			int num = this.selectedPriorityIndex;
			while (!this.perPrioritySubQueues[this.selectedPriorityIndex].TryDequeue(out message))
			{
				this.RolloverToNextPriority();
				if (num == this.selectedPriorityIndex)
				{
					message = null;
					return false;
				}
			}
			this.selectedPriorityItemCount--;
			if (this.selectedPriorityItemCount == 0)
			{
				this.RolloverToNextPriority();
			}
			return true;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004344 File Offset: 0x00002544
		public bool TryPeek(out ISchedulableMessage message)
		{
			int num = this.selectedPriorityIndex;
			while (!this.perPrioritySubQueues[this.selectedPriorityIndex].TryPeek(out message))
			{
				this.RolloverToNextPriority();
				if (num == this.selectedPriorityIndex)
				{
					message = null;
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000439C File Offset: 0x0000259C
		private static int[] ValidateAndGetDistributionArray(IDictionary<DeliveryPriority, int> distributions)
		{
			ArgumentValidator.ThrowIfNull("distributions", distributions);
			int priorityTypesLength = Enum.GetNames(typeof(DeliveryPriority)).Count<string>();
			ArgumentValidator.ThrowIfInvalidValue<IDictionary<DeliveryPriority, int>>("distributions", distributions, (IDictionary<DeliveryPriority, int> input) => input.Count == priorityTypesLength);
			foreach (KeyValuePair<DeliveryPriority, int> keyValuePair in distributions)
			{
				if (keyValuePair.Value <= 0)
				{
					throw new ArgumentException(string.Format("Delivery Priority {0} has invalid value of {1} specified. Only positive value allowed", keyValuePair.Key, keyValuePair.Value));
				}
			}
			int[] array = new int[priorityTypesLength];
			foreach (object obj in Enum.GetValues(typeof(DeliveryPriority)))
			{
				DeliveryPriority deliveryPriority = (DeliveryPriority)obj;
				int num;
				if (!distributions.TryGetValue(deliveryPriority, out num))
				{
					throw new ArgumentException(string.Format("Could not find expected entry for Priority {0}. Input Set {1} has unknown values", deliveryPriority, distributions));
				}
				array[(int)deliveryPriority] = num;
			}
			return array;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000044E0 File Offset: 0x000026E0
		private void RolloverToNextPriority()
		{
			this.selectedPriorityIndex = (this.selectedPriorityIndex + 1) % this.perPrioritySubQueues.Length;
			this.selectedPriorityItemCount = this.priorityDistributions[this.selectedPriorityIndex];
		}

		// Token: 0x04000068 RID: 104
		private readonly int[] priorityDistributions;

		// Token: 0x04000069 RID: 105
		private readonly ISchedulerQueue[] perPrioritySubQueues;

		// Token: 0x0400006A RID: 106
		private int selectedPriorityIndex;

		// Token: 0x0400006B RID: 107
		private int selectedPriorityItemCount;
	}
}

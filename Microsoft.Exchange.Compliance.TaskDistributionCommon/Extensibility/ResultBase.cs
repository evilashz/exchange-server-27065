using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility
{
	// Token: 0x02000047 RID: 71
	public abstract class ResultBase : WorkDefinition
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A522 File Offset: 0x00008722
		public IProducerConsumerCollection<FaultRecord> Faults
		{
			get
			{
				return this.faults;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A9 RID: 425
		public abstract int SerializationVersion { get; }

		// Token: 0x060001AA RID: 426
		public abstract byte[] GetSerializedResult();

		// Token: 0x060001AB RID: 427 RVA: 0x0000A52C File Offset: 0x0000872C
		public virtual void MergeFaults(ResultBase source)
		{
			if (source != null)
			{
				FaultRecord item;
				while (source.Faults.TryTake(out item))
				{
					this.Faults.TryAdd(item);
				}
			}
		}

		// Token: 0x04000137 RID: 311
		private ConcurrentBag<FaultRecord> faults = new ConcurrentBag<FaultRecord>();
	}
}

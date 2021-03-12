using System;
using System.Threading;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000355 RID: 853
	internal class QueueQuotaTrackingBits
	{
		// Token: 0x17000B50 RID: 2896
		public bool this[QueueQuotaEntity entity, QueueQuotaResources resources]
		{
			get
			{
				return this.data != null && (this.data[(int)entity] & resources) == resources;
			}
			set
			{
				if (value)
				{
					if (this.data == null)
					{
						Interlocked.CompareExchange<QueueQuotaResources[]>(ref this.data, new QueueQuotaResources[Enum.GetValues(typeof(QueueQuotaEntity)).Length], null);
					}
					QueueQuotaResources[] array = this.data;
					array[(int)entity] = (array[(int)entity] | resources);
					return;
				}
				if (this.data != null)
				{
					QueueQuotaResources[] array2 = this.data;
					array2[(int)entity] = (array2[(int)entity] & ~resources);
				}
			}
		}

		// Token: 0x04001331 RID: 4913
		private QueueQuotaResources[] data;
	}
}

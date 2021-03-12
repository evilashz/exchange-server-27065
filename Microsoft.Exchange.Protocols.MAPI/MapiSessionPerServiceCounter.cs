using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000075 RID: 117
	internal class MapiSessionPerServiceCounter : IMapiObjectCounter
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0001BC0F File Offset: 0x00019E0F
		private MapiSessionPerServiceCounter(MapiServiceType serviceType)
		{
			this.serviceType = serviceType;
			this.objectCounter = 0L;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001BC26 File Offset: 0x00019E26
		public long GetCount()
		{
			return Interlocked.Read(ref this.objectCounter);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001BC33 File Offset: 0x00019E33
		public void IncrementCount()
		{
			Interlocked.Increment(ref this.objectCounter);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001BC41 File Offset: 0x00019E41
		public void DecrementCount()
		{
			Interlocked.Decrement(ref this.objectCounter);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001BC50 File Offset: 0x00019E50
		public void CheckObjectQuota(bool mustBeStrictlyUnderQuota)
		{
			long num = Interlocked.Read(ref this.objectCounter);
			long num2 = ActiveObjectLimits.EffectiveLimitation(this.serviceType);
			if (num2 == -1L)
			{
				return;
			}
			bool flag = mustBeStrictlyUnderQuota ? (num >= num2) : (num > num2);
			if (flag)
			{
				throw new StoreException((LID)53288U, ErrorCodeValue.MaxObjectsExceeded, "Exceeded object size limitation, service type=" + this.serviceType.ToString());
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001BCBE File Offset: 0x00019EBE
		internal static IMapiObjectCounter GetObjectCounter(MapiServiceType serviceType)
		{
			if (serviceType >= MapiServiceType.UnknownServiceType)
			{
				return UnlimitedObjectCounter.Instance;
			}
			return MapiSessionPerServiceCounter.serviceCounters[(int)serviceType];
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		internal static void Initialize()
		{
			for (int i = 0; i < 8; i++)
			{
				MapiSessionPerServiceCounter.serviceCounters[i] = new MapiSessionPerServiceCounter((MapiServiceType)i);
			}
		}

		// Token: 0x04000250 RID: 592
		private static TimeSpan interval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000251 RID: 593
		private static MapiSessionPerServiceCounter[] serviceCounters = new MapiSessionPerServiceCounter[8];

		// Token: 0x04000252 RID: 594
		private readonly MapiServiceType serviceType;

		// Token: 0x04000253 RID: 595
		private long objectCounter;
	}
}

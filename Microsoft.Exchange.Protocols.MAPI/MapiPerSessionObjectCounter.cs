using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000076 RID: 118
	public sealed class MapiPerSessionObjectCounter : IMapiObjectCounter
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0001BD1C File Offset: 0x00019F1C
		public MapiPerSessionObjectCounter(MapiObjectTrackedType objectType, MapiSession session)
		{
			this.trackedObjectType = objectType;
			this.session = session;
			this.objectCounter = 0L;
			this.lastReportTime = DateTime.UtcNow.AddMonths(-1).ToBinary();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001BD61 File Offset: 0x00019F61
		public MapiObjectTrackedType TrackedObjectType
		{
			get
			{
				return this.trackedObjectType;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0001BD6C File Offset: 0x00019F6C
		public DateTime LastReportTime
		{
			get
			{
				long dateData = Interlocked.Read(ref this.lastReportTime);
				return DateTime.FromBinary(dateData);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001BD8B File Offset: 0x00019F8B
		public long GetCount()
		{
			return Interlocked.Read(ref this.objectCounter);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001BD98 File Offset: 0x00019F98
		public void IncrementCount()
		{
			Interlocked.Increment(ref this.objectCounter);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001BDA6 File Offset: 0x00019FA6
		public void DecrementCount()
		{
			Interlocked.Decrement(ref this.objectCounter);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		public void CheckObjectQuota(bool mustBeStrictlyUnderQuota)
		{
			long num = Interlocked.Read(ref this.objectCounter);
			long num2 = ActiveObjectLimits.EffectiveLimitation(this.trackedObjectType);
			if (num2 == -1L)
			{
				return;
			}
			bool flag = mustBeStrictlyUnderQuota ? (num >= num2) : (num > num2);
			if (flag)
			{
				DateTime utcNow = DateTime.UtcNow;
				DateTime d = this.LastReportTime;
				if (utcNow - d > MapiPerSessionObjectCounter.interval)
				{
					long num3 = d.ToBinary();
					if (num3 == Interlocked.CompareExchange(ref this.lastReportTime, num3, utcNow.ToBinary()))
					{
						string periodicKey = this.session.UserDN + this.session.InternalClientType + this.trackedObjectType;
						Globals.LogPeriodicEvent(periodicKey, MSExchangeISEventLogConstants.Tuple_MaxObjectsExceeded, new object[]
						{
							this.session.UserDN,
							this.session.InternalClientType,
							num2,
							this.trackedObjectType
						});
					}
				}
				DiagnosticContext.TraceDwordAndString((LID)53840U, (uint)(num2 & (long)((ulong)-1)), this.trackedObjectType.ToString());
				DiagnosticContext.TraceDword((LID)41552U, (uint)num);
				throw new StoreException((LID)45096U, ErrorCodeValue.MaxObjectsExceeded, "Exceeded object size limitation, type=" + this.trackedObjectType.ToString());
			}
		}

		// Token: 0x04000254 RID: 596
		private static TimeSpan interval = TimeSpan.FromMinutes(30.0);

		// Token: 0x04000255 RID: 597
		private readonly MapiObjectTrackedType trackedObjectType;

		// Token: 0x04000256 RID: 598
		private long objectCounter;

		// Token: 0x04000257 RID: 599
		private long lastReportTime;

		// Token: 0x04000258 RID: 600
		private MapiSession session;
	}
}

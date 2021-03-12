using System;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000090 RID: 144
	public struct LockAcquisitionTracker : IOperationExecutionTrackable
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0001DBA9 File Offset: 0x0001BDA9
		private LockAcquisitionTracker(LockAcquisitionTracker.Key key, LockAcquisitionTracker.Data data)
		{
			this.key = key;
			this.data = data;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001DBBC File Offset: 0x0001BDBC
		public static LockAcquisitionTracker Create(LockManager.LockType lockType, bool locked, bool contested, byte ownerClientType, byte ownerOperation, TimeSpan timeSpentWaiting)
		{
			LockAcquisitionTracker.Key key = LockAcquisitionTracker.Key.Create(lockType);
			LockAcquisitionTracker.Data data = new LockAcquisitionTracker.Data
			{
				Count = 1,
				NumberSucceeded = (locked ? 1 : 0),
				NumberContested = (contested ? 1 : 0),
				LockOwnerClient = ownerClientType,
				LockOwnerOperation = ownerOperation,
				TimeSpentWaiting = timeSpentWaiting
			};
			return new LockAcquisitionTracker(key, data);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001DC17 File Offset: 0x0001BE17
		public LockAcquisitionTracker.Data Tracked
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001DC1F File Offset: 0x0001BE1F
		public IExecutionPlanner GetExecutionPlanner()
		{
			return null;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001DC22 File Offset: 0x0001BE22
		public IOperationExecutionTrackingKey GetTrackingKey()
		{
			return this.key;
		}

		// Token: 0x040003D0 RID: 976
		private readonly LockAcquisitionTracker.Key key;

		// Token: 0x040003D1 RID: 977
		private readonly LockAcquisitionTracker.Data data;

		// Token: 0x02000091 RID: 145
		internal class Key : IOperationExecutionTrackingKey
		{
			// Token: 0x06000513 RID: 1299 RVA: 0x0001DC2A File Offset: 0x0001BE2A
			private Key(LockManager.LockType lockType)
			{
				this.key = lockType.GetHashCode();
				this.lockType = lockType;
				this.description = null;
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0001DC51 File Offset: 0x0001BE51
			public static LockAcquisitionTracker.Key Create(LockManager.LockType lockType)
			{
				return new LockAcquisitionTracker.Key(lockType);
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001DC59 File Offset: 0x0001BE59
			public LockManager.LockType LockType
			{
				get
				{
					return this.lockType;
				}
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0001DC61 File Offset: 0x0001BE61
			public int GetTrackingKeyHashValue()
			{
				return this.key;
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x0001DC69 File Offset: 0x0001BE69
			public int GetSimpleHashValue()
			{
				return this.key;
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x0001DC74 File Offset: 0x0001BE74
			public bool IsTrackingKeyEqualTo(IOperationExecutionTrackingKey other)
			{
				return other.GetTrackingKeyHashValue().Equals(this.key);
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0001DC95 File Offset: 0x0001BE95
			public string TrackingKeyToString()
			{
				if (this.description == null)
				{
					this.description = this.lockType.ToString();
				}
				return this.description;
			}

			// Token: 0x040003D2 RID: 978
			private readonly int key;

			// Token: 0x040003D3 RID: 979
			private readonly LockManager.LockType lockType;

			// Token: 0x040003D4 RID: 980
			private string description;
		}

		// Token: 0x02000092 RID: 146
		public class Data : IExecutionTrackingData<LockAcquisitionTracker.Data>
		{
			// Token: 0x17000155 RID: 341
			// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001DCBB File Offset: 0x0001BEBB
			// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001DCC3 File Offset: 0x0001BEC3
			public int Count { get; set; }

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001DCCC File Offset: 0x0001BECC
			// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001DCD4 File Offset: 0x0001BED4
			public int NumberSucceeded { get; set; }

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001DCDD File Offset: 0x0001BEDD
			// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001DCE5 File Offset: 0x0001BEE5
			public int NumberContested { get; set; }

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001DCEE File Offset: 0x0001BEEE
			// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001DCF6 File Offset: 0x0001BEF6
			public TimeSpan TimeSpentWaiting { get; set; }

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001DCFF File Offset: 0x0001BEFF
			// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001DD07 File Offset: 0x0001BF07
			public byte LockOwnerClient { get; set; }

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001DD10 File Offset: 0x0001BF10
			// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001DD18 File Offset: 0x0001BF18
			public byte LockOwnerOperation { get; set; }

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001DD21 File Offset: 0x0001BF21
			public TimeSpan TotalTime
			{
				get
				{
					return this.TimeSpentWaiting;
				}
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x0001DD2C File Offset: 0x0001BF2C
			public void Aggregate(LockAcquisitionTracker.Data dataToAggregate)
			{
				this.Count += dataToAggregate.Count;
				this.NumberSucceeded += dataToAggregate.NumberSucceeded;
				this.NumberContested += dataToAggregate.NumberContested;
				this.TimeSpentWaiting += dataToAggregate.TimeSpentWaiting;
				this.LockOwnerClient = ((dataToAggregate.LockOwnerClient > 0) ? dataToAggregate.LockOwnerClient : this.LockOwnerClient);
				this.LockOwnerOperation = ((dataToAggregate.LockOwnerOperation > 0) ? dataToAggregate.LockOwnerOperation : this.LockOwnerOperation);
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
			public void Reset()
			{
				this.Count = 0;
				this.NumberSucceeded = 0;
				this.NumberContested = 0;
				this.TimeSpentWaiting = TimeSpan.Zero;
				this.LockOwnerClient = 0;
				this.LockOwnerOperation = 0;
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
			public void AppendToTraceContentBuilder(TraceContentBuilder cb)
			{
				long num = (long)this.TimeSpentWaiting.TotalMicroseconds();
				cb.Append("Locked: ");
				cb.Append(this.NumberSucceeded);
				cb.Append(", Contest: ");
				cb.Append(this.NumberContested);
				cb.Append(", Waited: ");
				cb.Append(num.ToString("N0", CultureInfo.InvariantCulture));
				cb.Append(" us, Client: ");
				cb.Append((int)this.LockOwnerClient);
				cb.Append(", Oper: ");
				cb.Append((int)this.LockOwnerOperation);
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x0001DE8C File Offset: 0x0001C08C
			public void AppendDetailsToTraceContentBuilder(TraceContentBuilder cb, int indentLevel)
			{
			}
		}

		// Token: 0x02000093 RID: 147
		public enum LockCategory
		{
			// Token: 0x040003DC RID: 988
			Mailbox,
			// Token: 0x040003DD RID: 989
			Component,
			// Token: 0x040003DE RID: 990
			Other
		}
	}
}

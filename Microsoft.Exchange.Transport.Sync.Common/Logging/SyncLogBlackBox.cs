using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Sync.Common.Logging
{
	// Token: 0x02000086 RID: 134
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncLogBlackBox
	{
		// Token: 0x06000399 RID: 921 RVA: 0x00015340 File Offset: 0x00013540
		internal SyncLogBlackBox() : this(false)
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00015349 File Offset: 0x00013549
		internal SyncLogBlackBox(bool ignoreUser)
		{
			this.capacity = SyncLogBlackBox.InitialBlackBoxCapacity;
			this.syncLogs = new string[this.Capacity];
			this.ignoreUser = ignoreUser;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00015380 File Offset: 0x00013580
		// (set) Token: 0x0600039C RID: 924 RVA: 0x000153C4 File Offset: 0x000135C4
		public int Capacity
		{
			get
			{
				int result;
				lock (this.syncRoot)
				{
					result = this.capacity;
				}
				return result;
			}
			private set
			{
				this.capacity = value;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000153D0 File Offset: 0x000135D0
		public override string ToString()
		{
			string result;
			lock (this.syncRoot)
			{
				if (this.IsEmpty())
				{
					result = SyncLogBlackBox.EmptyOutput;
				}
				else if (!this.IsFull())
				{
					result = string.Join(Environment.NewLine, this.syncLogs, 0, this.end);
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(this.Capacity * 200);
					int num = this.end;
					for (int i = 0; i < this.Capacity; i++)
					{
						stringBuilder.AppendLine(this.syncLogs[num]);
						num = this.ComputeNextIndex(num);
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001548C File Offset: 0x0001368C
		public void ResizeAndClear(int newCapacity)
		{
			lock (this.syncRoot)
			{
				this.Capacity = newCapacity;
				this.InternalClear();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000154D4 File Offset: 0x000136D4
		public void Clear()
		{
			lock (this.syncRoot)
			{
				if (this.Capacity != SyncLogBlackBox.InitialBlackBoxCapacity)
				{
					this.Capacity = SyncLogBlackBox.InitialBlackBoxCapacity;
				}
				this.InternalClear();
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001552C File Offset: 0x0001372C
		public void Append(LogRowFormatter row)
		{
			lock (this.syncRoot)
			{
				this.syncLogs[this.end] = string.Concat(new object[]
				{
					ExDateTime.UtcNow.ToString(),
					", ",
					row[1].ToString(),
					", ",
					row[2].ToString(),
					", ",
					this.ignoreUser ? string.Empty : row[3],
					", ",
					row[4],
					", ",
					row[5],
					", ",
					row[6],
					", ",
					row[7]
				});
				this.end = this.ComputeNextIndex(this.end);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00015648 File Offset: 0x00013848
		private bool IsFull()
		{
			return this.syncLogs[this.end] != null;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001565D File Offset: 0x0001385D
		private bool IsEmpty()
		{
			return !this.IsFull() && this.end == 0;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00015672 File Offset: 0x00013872
		private int ComputeNextIndex(int index)
		{
			return (index + 1) % this.Capacity;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001567E File Offset: 0x0001387E
		private void InternalClear()
		{
			this.end = 0;
			this.syncLogs = new string[this.Capacity];
		}

		// Token: 0x040001CD RID: 461
		private const int EstimateSizePerLine = 200;

		// Token: 0x040001CE RID: 462
		public static readonly int InitialBlackBoxCapacity = 200;

		// Token: 0x040001CF RID: 463
		public static readonly string EmptyOutput = "<empty>";

		// Token: 0x040001D0 RID: 464
		private string[] syncLogs;

		// Token: 0x040001D1 RID: 465
		private int end;

		// Token: 0x040001D2 RID: 466
		private bool ignoreUser;

		// Token: 0x040001D3 RID: 467
		private int capacity;

		// Token: 0x040001D4 RID: 468
		private object syncRoot = new object();
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000623 RID: 1571
	[Serializable]
	public sealed class DumpsterStatisticsEntry
	{
		// Token: 0x06004A58 RID: 19032 RVA: 0x00112DD2 File Offset: 0x00110FD2
		internal DumpsterStatisticsEntry(string server, long ticksOldestItem, long queueSize, int numberOfItems)
		{
			this.m_server = server;
			this.m_queueSize = queueSize;
			this.m_numberOfItems = numberOfItems;
			this.m_oldestItem = DumpsterStatisticsEntry.ToNullableLocalDateTime(new DateTime(ticksOldestItem));
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x00112E01 File Offset: 0x00111001
		public string Server
		{
			get
			{
				return this.m_server;
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06004A5A RID: 19034 RVA: 0x00112E09 File Offset: 0x00111009
		public DateTime? OldestItem
		{
			get
			{
				return this.m_oldestItem;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06004A5B RID: 19035 RVA: 0x00112E11 File Offset: 0x00111011
		public long QueueSize
		{
			get
			{
				return this.m_queueSize;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06004A5C RID: 19036 RVA: 0x00112E19 File Offset: 0x00111019
		public int NumberOfItems
		{
			get
			{
				return this.m_numberOfItems;
			}
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x00112E24 File Offset: 0x00111024
		public override string ToString()
		{
			if (this.m_oldestItem != null)
			{
				return string.Format("{0}({1};{2};{3}KB)", new object[]
				{
					this.m_server,
					this.m_oldestItem,
					this.m_numberOfItems,
					this.m_queueSize
				});
			}
			return string.Format("{0}({1};{2}KB)", this.m_server, this.m_numberOfItems, this.m_queueSize);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00112EAA File Offset: 0x001110AA
		internal static bool IsValidDateTime(DateTime dateTime)
		{
			return dateTime > DumpsterStatisticsEntry.s_minDateTime && dateTime < DateTime.MaxValue;
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x00112EC8 File Offset: 0x001110C8
		internal static DateTime? ToNullableLocalDateTime(DateTime dateTime)
		{
			if (DumpsterStatisticsEntry.IsValidDateTime(dateTime))
			{
				return new DateTime?(dateTime.ToLocalTime());
			}
			return null;
		}

		// Token: 0x0400336C RID: 13164
		private string m_server;

		// Token: 0x0400336D RID: 13165
		private DateTime? m_oldestItem;

		// Token: 0x0400336E RID: 13166
		private long m_queueSize;

		// Token: 0x0400336F RID: 13167
		private int m_numberOfItems;

		// Token: 0x04003370 RID: 13168
		private static readonly DateTime s_minDateTime = DateTime.FromFileTimeUtc(0L);
	}
}

using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F7 RID: 503
	internal class MailboxStatistics : IComparable<MailboxStatistics>, IComparable
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x000376B2 File Offset: 0x000358B2
		public MailboxStatistics(MailboxInfo mailboxInfo, ulong count, ByteQuantifiedSize size)
		{
			this.mailboxInfo = mailboxInfo;
			this.count = count;
			this.size = size;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x000376CF File Offset: 0x000358CF
		public MailboxInfo MailboxInfo
		{
			get
			{
				return this.mailboxInfo;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000376D7 File Offset: 0x000358D7
		public ulong Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000376DF File Offset: 0x000358DF
		public ByteQuantifiedSize Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000376E8 File Offset: 0x000358E8
		public void Merge(MailboxStatistics other)
		{
			if (other == null || !this.mailboxInfo.LegacyExchangeDN.Equals(other.MailboxInfo.LegacyExchangeDN, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			this.count += other.Count;
			this.size += other.Size;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00037744 File Offset: 0x00035944
		public override string ToString()
		{
			return string.Format("[User:{0}, IsPrimary: {1}, Count: {2}, Size: {3} bytes]", new object[]
			{
				this.mailboxInfo.LegacyExchangeDN,
				this.mailboxInfo.IsPrimary,
				this.Count,
				this.Size.ToBytes()
			});
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000377A8 File Offset: 0x000359A8
		public override bool Equals(object obj)
		{
			MailboxStatistics mailboxStatistics = obj as MailboxStatistics;
			return mailboxStatistics != null && 0 == this.CompareTo(mailboxStatistics);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000377CB File Offset: 0x000359CB
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000377D8 File Offset: 0x000359D8
		public int CompareTo(object obj)
		{
			MailboxStatistics mailboxStatistics = obj as MailboxStatistics;
			if (mailboxStatistics == null)
			{
				throw new ArgumentException("Object is not a MailboxStatistics");
			}
			return this.CompareTo(mailboxStatistics);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00037804 File Offset: 0x00035A04
		public int CompareTo(MailboxStatistics other)
		{
			if (other == null)
			{
				return 1;
			}
			int num = this.Count.CompareTo(other.Count);
			if (num == 0)
			{
				num = this.Size.CompareTo(other.Size);
				if (num == 0)
				{
					num = string.Compare(this.MailboxInfo.LegacyExchangeDN, other.MailboxInfo.LegacyExchangeDN, CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase);
					if (num == 0)
					{
						num = this.MailboxInfo.IsPrimary.CompareTo(other.MailboxInfo.IsPrimary);
					}
				}
			}
			return num;
		}

		// Token: 0x04000937 RID: 2359
		private readonly MailboxInfo mailboxInfo;

		// Token: 0x04000938 RID: 2360
		private ulong count;

		// Token: 0x04000939 RID: 2361
		private ByteQuantifiedSize size;
	}
}

using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000075 RID: 117
	internal class MailboxState : IEquatable<MailboxState>
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00009D3F File Offset: 0x00007F3F
		public MailboxState()
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00009D47 File Offset: 0x00007F47
		public MailboxState(int mailboxNumber, int rawState)
		{
			this.MailboxNumber = mailboxNumber;
			this.RawState = rawState;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009D5D File Offset: 0x00007F5D
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00009D65 File Offset: 0x00007F65
		public int MailboxNumber { get; internal set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009D6E File Offset: 0x00007F6E
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00009D76 File Offset: 0x00007F76
		public int RawState { get; internal set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00009D7F File Offset: 0x00007F7F
		public bool IsCompleted
		{
			get
			{
				return this.RawState == int.MaxValue || this.RawState == -4;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009D9A File Offset: 0x00007F9A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MailboxState);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public override int GetHashCode()
		{
			return this.MailboxNumber.GetHashCode() ^ this.RawState.GetHashCode();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009DD2 File Offset: 0x00007FD2
		public bool Equals(MailboxState other)
		{
			return other != null && this.MailboxNumber == other.MailboxNumber && this.RawState == other.RawState;
		}

		// Token: 0x0400013E RID: 318
		public const int InitialState = -1;

		// Token: 0x0400013F RID: 319
		public const int RecrawlState = -2;

		// Token: 0x04000140 RID: 320
		public const int RecrawlFailureRetryState = -3;

		// Token: 0x04000141 RID: 321
		public const int RecrawlFailurePermanentState = -4;

		// Token: 0x04000142 RID: 322
		public const int CompleteState = 2147483647;
	}
}

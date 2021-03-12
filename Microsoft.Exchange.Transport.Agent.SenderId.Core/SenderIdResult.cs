using System;
using System.Globalization;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200000A RID: 10
	internal sealed class SenderIdResult
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002496 File Offset: 0x00000696
		public SenderIdResult(SenderIdStatus status)
		{
			if (status == SenderIdStatus.Fail)
			{
				throw new ArgumentOutOfRangeException("status", status, "Invalid constructor usage.");
			}
			this.status = status;
			this.failReason = SenderIdFailReason.None;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024C8 File Offset: 0x000006C8
		public SenderIdResult(SenderIdStatus status, SenderIdFailReason failReason)
		{
			if (status != SenderIdStatus.Fail || failReason == SenderIdFailReason.NotPermitted || failReason == SenderIdFailReason.None)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid constructor usage for {0}, {1}", new object[]
				{
					status,
					failReason
				}));
			}
			this.status = status;
			this.failReason = failReason;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002524 File Offset: 0x00000724
		public SenderIdResult(SenderIdStatus status, SenderIdFailReason failReason, string explanation)
		{
			if (status != SenderIdStatus.Fail || failReason != SenderIdFailReason.NotPermitted)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid constructor usage for {0}, {1}, {2}", new object[]
				{
					status,
					failReason,
					explanation
				}));
			}
			this.status = status;
			this.failReason = failReason;
			this.explanation = explanation;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002587 File Offset: 0x00000787
		public SenderIdStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000258F File Offset: 0x0000078F
		public SenderIdFailReason FailReason
		{
			get
			{
				return this.failReason;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002597 File Offset: 0x00000797
		public string Explanation
		{
			get
			{
				return this.explanation;
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly SenderIdStatus status;

		// Token: 0x0400001D RID: 29
		private readonly SenderIdFailReason failReason;

		// Token: 0x0400001E RID: 30
		private readonly string explanation;
	}
}

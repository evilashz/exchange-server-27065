using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200000C RID: 12
	internal sealed class SubmissionConnectionWrapper : DisposeTrackableBase
	{
		// Token: 0x06000040 RID: 64 RVA: 0x0000419A File Offset: 0x0000239A
		internal SubmissionConnectionWrapper(SubmissionConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.connection = connection;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000041B7 File Offset: 0x000023B7
		public ulong Id
		{
			get
			{
				return this.connection.Id;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000041C4 File Offset: 0x000023C4
		public void SubmissionSuccessful(long size, int recipients)
		{
			if (this.connection == null)
			{
				throw new InvalidOperationException();
			}
			this.connection.SubmissionSuccessful(size, recipients);
			this.connection = null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000041E8 File Offset: 0x000023E8
		public void SubmissionAborted(string reason)
		{
			if (this.connection == null)
			{
				throw new InvalidOperationException();
			}
			this.connection.SubmissionAborted(reason);
			this.connection = null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000420B File Offset: 0x0000240B
		public void SubmissionFailed(string description)
		{
			if (this.connection == null)
			{
				throw new InvalidOperationException();
			}
			this.connection.SubmissionFailed(description);
			this.connection = null;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000422E File Offset: 0x0000242E
		public override string ToString()
		{
			if (this.connection == null)
			{
				return "Wrapper on released connection.";
			}
			return "Wrapper on " + this.connection.ToString();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004253 File Offset: 0x00002453
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SubmissionConnectionWrapper>(this);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000425B File Offset: 0x0000245B
		protected override void InternalDispose(bool disposing)
		{
			if (this.connection != null)
			{
				throw new InvalidOperationException("The underlying submission connection was not released.");
			}
		}

		// Token: 0x0400001C RID: 28
		private SubmissionConnection connection;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000048 RID: 72
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToRefreshMailboxException : MapiOperationException
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000E349 File Offset: 0x0000C549
		public FailedToRefreshMailboxException(string exception, string mailbox) : base(Strings.FailedToRefreshMailboxExceptionError(exception, mailbox))
		{
			this.exception = exception;
			this.mailbox = mailbox;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E366 File Offset: 0x0000C566
		public FailedToRefreshMailboxException(string exception, string mailbox, Exception innerException) : base(Strings.FailedToRefreshMailboxExceptionError(exception, mailbox), innerException)
		{
			this.exception = exception;
			this.mailbox = mailbox;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E384 File Offset: 0x0000C584
		protected FailedToRefreshMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exception = (string)info.GetValue("exception", typeof(string));
			this.mailbox = (string)info.GetValue("mailbox", typeof(string));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E3D9 File Offset: 0x0000C5D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exception", this.exception);
			info.AddValue("mailbox", this.mailbox);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000E405 File Offset: 0x0000C605
		public string Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000E40D File Offset: 0x0000C60D
		public string Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x0400019E RID: 414
		private readonly string exception;

		// Token: 0x0400019F RID: 415
		private readonly string mailbox;
	}
}

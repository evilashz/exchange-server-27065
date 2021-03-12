using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000044 RID: 68
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapCommunicationException : OperationLevelTransientException
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00004024 File Offset: 0x00002224
		public ImapCommunicationException(string imapCommunicationErrorMsg, RetryPolicy retryPolicy) : base(CXStrings.ImapCommunicationErrorMsg(imapCommunicationErrorMsg, retryPolicy))
		{
			this.imapCommunicationErrorMsg = imapCommunicationErrorMsg;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004046 File Offset: 0x00002246
		public ImapCommunicationException(string imapCommunicationErrorMsg, RetryPolicy retryPolicy, Exception innerException) : base(CXStrings.ImapCommunicationErrorMsg(imapCommunicationErrorMsg, retryPolicy), innerException)
		{
			this.imapCommunicationErrorMsg = imapCommunicationErrorMsg;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000406C File Offset: 0x0000226C
		protected ImapCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.imapCommunicationErrorMsg = (string)info.GetValue("imapCommunicationErrorMsg", typeof(string));
			this.retryPolicy = (RetryPolicy)info.GetValue("retryPolicy", typeof(RetryPolicy));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000040C1 File Offset: 0x000022C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("imapCommunicationErrorMsg", this.imapCommunicationErrorMsg);
			info.AddValue("retryPolicy", this.retryPolicy);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000040F2 File Offset: 0x000022F2
		public string ImapCommunicationErrorMsg
		{
			get
			{
				return this.imapCommunicationErrorMsg;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000040FA File Offset: 0x000022FA
		public RetryPolicy RetryPolicy
		{
			get
			{
				return this.retryPolicy;
			}
		}

		// Token: 0x040000DF RID: 223
		private readonly string imapCommunicationErrorMsg;

		// Token: 0x040000E0 RID: 224
		private readonly RetryPolicy retryPolicy;
	}
}

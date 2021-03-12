using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000042 RID: 66
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapConnectionException : OperationLevelTransientException
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00003EC5 File Offset: 0x000020C5
		public ImapConnectionException(string imapConnectionErrorMsg, RetryPolicy retryPolicy) : base(CXStrings.ImapConnectionErrorMsg(imapConnectionErrorMsg, retryPolicy))
		{
			this.imapConnectionErrorMsg = imapConnectionErrorMsg;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003EE7 File Offset: 0x000020E7
		public ImapConnectionException(string imapConnectionErrorMsg, RetryPolicy retryPolicy, Exception innerException) : base(CXStrings.ImapConnectionErrorMsg(imapConnectionErrorMsg, retryPolicy), innerException)
		{
			this.imapConnectionErrorMsg = imapConnectionErrorMsg;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003F0C File Offset: 0x0000210C
		protected ImapConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.imapConnectionErrorMsg = (string)info.GetValue("imapConnectionErrorMsg", typeof(string));
			this.retryPolicy = (RetryPolicy)info.GetValue("retryPolicy", typeof(RetryPolicy));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003F61 File Offset: 0x00002161
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("imapConnectionErrorMsg", this.imapConnectionErrorMsg);
			info.AddValue("retryPolicy", this.retryPolicy);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003F92 File Offset: 0x00002192
		public string ImapConnectionErrorMsg
		{
			get
			{
				return this.imapConnectionErrorMsg;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003F9A File Offset: 0x0000219A
		public RetryPolicy RetryPolicy
		{
			get
			{
				return this.retryPolicy;
			}
		}

		// Token: 0x040000DC RID: 220
		private readonly string imapConnectionErrorMsg;

		// Token: 0x040000DD RID: 221
		private readonly RetryPolicy retryPolicy;
	}
}

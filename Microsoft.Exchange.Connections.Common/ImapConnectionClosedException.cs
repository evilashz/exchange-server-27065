using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000043 RID: 67
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapConnectionClosedException : OperationLevelTransientException
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00003FA2 File Offset: 0x000021A2
		public ImapConnectionClosedException(string imapConnectionClosedErrMsg) : base(CXStrings.ImapConnectionClosedErrorMsg(imapConnectionClosedErrMsg))
		{
			this.imapConnectionClosedErrMsg = imapConnectionClosedErrMsg;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003FBC File Offset: 0x000021BC
		public ImapConnectionClosedException(string imapConnectionClosedErrMsg, Exception innerException) : base(CXStrings.ImapConnectionClosedErrorMsg(imapConnectionClosedErrMsg), innerException)
		{
			this.imapConnectionClosedErrMsg = imapConnectionClosedErrMsg;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00003FD7 File Offset: 0x000021D7
		protected ImapConnectionClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.imapConnectionClosedErrMsg = (string)info.GetValue("imapConnectionClosedErrMsg", typeof(string));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004001 File Offset: 0x00002201
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("imapConnectionClosedErrMsg", this.imapConnectionClosedErrMsg);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000401C File Offset: 0x0000221C
		public string ImapConnectionClosedErrMsg
		{
			get
			{
				return this.imapConnectionClosedErrMsg;
			}
		}

		// Token: 0x040000DE RID: 222
		private readonly string imapConnectionClosedErrMsg;
	}
}

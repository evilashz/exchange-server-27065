using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000046 RID: 70
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapException : LocalizedException
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00004235 File Offset: 0x00002435
		public ImapException(string failureReason) : base(CXStrings.ImapErrorMsg(failureReason))
		{
			this.failureReason = failureReason;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000424A File Offset: 0x0000244A
		public ImapException(string failureReason, Exception innerException) : base(CXStrings.ImapErrorMsg(failureReason), innerException)
		{
			this.failureReason = failureReason;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004260 File Offset: 0x00002460
		protected ImapException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (string)info.GetValue("failureReason", typeof(string));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000428A File Offset: 0x0000248A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000042A5 File Offset: 0x000024A5
		public string FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x040000E4 RID: 228
		private readonly string failureReason;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000048 RID: 72
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapInvalidResponseException : LocalizedException
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00004325 File Offset: 0x00002525
		public ImapInvalidResponseException(string failureReason) : base(CXStrings.ImapInvalidResponseErrorMsg(failureReason))
		{
			this.failureReason = failureReason;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000433A File Offset: 0x0000253A
		public ImapInvalidResponseException(string failureReason, Exception innerException) : base(CXStrings.ImapInvalidResponseErrorMsg(failureReason), innerException)
		{
			this.failureReason = failureReason;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004350 File Offset: 0x00002550
		protected ImapInvalidResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (string)info.GetValue("failureReason", typeof(string));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000437A File Offset: 0x0000257A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004395 File Offset: 0x00002595
		public string FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x040000E6 RID: 230
		private readonly string failureReason;
	}
}

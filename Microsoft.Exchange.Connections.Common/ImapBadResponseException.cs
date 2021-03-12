using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000047 RID: 71
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapBadResponseException : LocalizedException
	{
		// Token: 0x0600015C RID: 348 RVA: 0x000042AD File Offset: 0x000024AD
		public ImapBadResponseException(string failureReason) : base(CXStrings.ImapBadResponseErrorMsg(failureReason))
		{
			this.failureReason = failureReason;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000042C2 File Offset: 0x000024C2
		public ImapBadResponseException(string failureReason, Exception innerException) : base(CXStrings.ImapBadResponseErrorMsg(failureReason), innerException)
		{
			this.failureReason = failureReason;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000042D8 File Offset: 0x000024D8
		protected ImapBadResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (string)info.GetValue("failureReason", typeof(string));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004302 File Offset: 0x00002502
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000431D File Offset: 0x0000251D
		public string FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x040000E5 RID: 229
		private readonly string failureReason;
	}
}

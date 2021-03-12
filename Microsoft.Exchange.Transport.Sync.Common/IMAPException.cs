using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000035 RID: 53
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IMAPException : LocalizedException
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00005A3A File Offset: 0x00003C3A
		public IMAPException(string failureReason) : base(Strings.IMAPException(failureReason))
		{
			this.failureReason = failureReason;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005A4F File Offset: 0x00003C4F
		public IMAPException(string failureReason, Exception innerException) : base(Strings.IMAPException(failureReason), innerException)
		{
			this.failureReason = failureReason;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005A65 File Offset: 0x00003C65
		protected IMAPException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (string)info.GetValue("failureReason", typeof(string));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005A8F File Offset: 0x00003C8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005AAA File Offset: 0x00003CAA
		public string FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x040000EC RID: 236
		private readonly string failureReason;
	}
}

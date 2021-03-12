using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000053 RID: 83
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TlsFailureException : LocalizedException
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000064DF File Offset: 0x000046DF
		public TlsFailureException(string failureReason) : base(Strings.TlsFailureException(failureReason))
		{
			this.failureReason = failureReason;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000064F4 File Offset: 0x000046F4
		public TlsFailureException(string failureReason, Exception innerException) : base(Strings.TlsFailureException(failureReason), innerException)
		{
			this.failureReason = failureReason;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000650A File Offset: 0x0000470A
		protected TlsFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failureReason = (string)info.GetValue("failureReason", typeof(string));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00006534 File Offset: 0x00004734
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureReason", this.failureReason);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000654F File Offset: 0x0000474F
		public string FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x040000FC RID: 252
		private readonly string failureReason;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D4 RID: 468
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OpenRestrictedContentException : LocalizedException
	{
		// Token: 0x06000F45 RID: 3909 RVA: 0x0003631F File Offset: 0x0003451F
		public OpenRestrictedContentException(string reason) : base(Strings.OpenRestrictedContentException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00036334 File Offset: 0x00034534
		public OpenRestrictedContentException(string reason, Exception innerException) : base(Strings.OpenRestrictedContentException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003634A File Offset: 0x0003454A
		protected OpenRestrictedContentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00036374 File Offset: 0x00034574
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0003638F File Offset: 0x0003458F
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040007A5 RID: 1957
		private readonly string reason;
	}
}

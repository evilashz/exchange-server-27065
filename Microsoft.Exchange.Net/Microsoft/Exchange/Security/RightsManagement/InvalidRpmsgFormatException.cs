using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020000CF RID: 207
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRpmsgFormatException : LocalizedException
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x00013DB2 File Offset: 0x00011FB2
		public InvalidRpmsgFormatException(string reason) : base(DrmStrings.InvalidRpmsgFormat(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00013DC7 File Offset: 0x00011FC7
		public InvalidRpmsgFormatException(string reason, Exception innerException) : base(DrmStrings.InvalidRpmsgFormat(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00013DDD File Offset: 0x00011FDD
		protected InvalidRpmsgFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00013E07 File Offset: 0x00012007
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00013E22 File Offset: 0x00012022
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000462 RID: 1122
		private readonly string reason;
	}
}

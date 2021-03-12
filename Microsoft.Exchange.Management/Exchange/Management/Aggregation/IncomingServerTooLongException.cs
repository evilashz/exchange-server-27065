using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A7 RID: 4263
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncomingServerTooLongException : LocalizedException
	{
		// Token: 0x0600B23D RID: 45629 RVA: 0x00299878 File Offset: 0x00297A78
		public IncomingServerTooLongException() : base(Strings.IncomingServerTooLong)
		{
		}

		// Token: 0x0600B23E RID: 45630 RVA: 0x00299885 File Offset: 0x00297A85
		public IncomingServerTooLongException(Exception innerException) : base(Strings.IncomingServerTooLong, innerException)
		{
		}

		// Token: 0x0600B23F RID: 45631 RVA: 0x00299893 File Offset: 0x00297A93
		protected IncomingServerTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B240 RID: 45632 RVA: 0x0029989D File Offset: 0x00297A9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

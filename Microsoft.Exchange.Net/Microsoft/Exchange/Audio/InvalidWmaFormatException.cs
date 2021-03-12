using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x020000E0 RID: 224
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidWmaFormatException : LocalizedException
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x00015BA0 File Offset: 0x00013DA0
		public InvalidWmaFormatException() : base(NetException.InvalidWmaFormat)
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00015BAD File Offset: 0x00013DAD
		public InvalidWmaFormatException(Exception innerException) : base(NetException.InvalidWmaFormat, innerException)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00015BBB File Offset: 0x00013DBB
		protected InvalidWmaFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00015BC5 File Offset: 0x00013DC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020000F9 RID: 249
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMserveRequestException : LocalizedException
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x000168E4 File Offset: 0x00014AE4
		public InvalidMserveRequestException() : base(NetServerException.InvalidMserveRequest)
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000168F1 File Offset: 0x00014AF1
		public InvalidMserveRequestException(Exception innerException) : base(NetServerException.InvalidMserveRequest, innerException)
		{
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000168FF File Offset: 0x00014AFF
		protected InvalidMserveRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00016909 File Offset: 0x00014B09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

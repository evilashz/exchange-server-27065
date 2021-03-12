using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200019F RID: 415
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidObjectIdException : LocalizedException
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x00034E8C File Offset: 0x0003308C
		public InvalidObjectIdException() : base(Strings.InvalidObjectIdException)
		{
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00034E99 File Offset: 0x00033099
		public InvalidObjectIdException(Exception innerException) : base(Strings.InvalidObjectIdException, innerException)
		{
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00034EA7 File Offset: 0x000330A7
		protected InvalidObjectIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00034EB1 File Offset: 0x000330B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

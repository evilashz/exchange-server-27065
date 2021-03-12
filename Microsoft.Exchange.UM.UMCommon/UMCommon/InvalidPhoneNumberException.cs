using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200019B RID: 411
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPhoneNumberException : LocalizedException
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x00034DD0 File Offset: 0x00032FD0
		public InvalidPhoneNumberException() : base(Strings.ExceptionInvalidPhoneNumber)
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00034DDD File Offset: 0x00032FDD
		public InvalidPhoneNumberException(Exception innerException) : base(Strings.ExceptionInvalidPhoneNumber, innerException)
		{
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00034DEB File Offset: 0x00032FEB
		protected InvalidPhoneNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00034DF5 File Offset: 0x00032FF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

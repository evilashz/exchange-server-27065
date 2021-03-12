using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200019D RID: 413
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCallIdException : LocalizedException
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x00034E2E File Offset: 0x0003302E
		public InvalidCallIdException() : base(Strings.InvalidCallIdException)
		{
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00034E3B File Offset: 0x0003303B
		public InvalidCallIdException(Exception innerException) : base(Strings.InvalidCallIdException, innerException)
		{
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00034E49 File Offset: 0x00033049
		protected InvalidCallIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00034E53 File Offset: 0x00033053
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200019C RID: 412
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSipUriException : LocalizedException
	{
		// Token: 0x06000E41 RID: 3649 RVA: 0x00034DFF File Offset: 0x00032FFF
		public InvalidSipUriException() : base(Strings.ExceptionInvalidSipUri)
		{
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00034E0C File Offset: 0x0003300C
		public InvalidSipUriException(Exception innerException) : base(Strings.ExceptionInvalidSipUri, innerException)
		{
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00034E1A File Offset: 0x0003301A
		protected InvalidSipUriException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00034E24 File Offset: 0x00033024
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

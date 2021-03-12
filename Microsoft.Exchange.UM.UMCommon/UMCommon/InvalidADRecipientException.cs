using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A9 RID: 425
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidADRecipientException : LocalizedException
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x00035135 File Offset: 0x00033335
		public InvalidADRecipientException() : base(Strings.InvalidADRecipientException)
		{
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00035142 File Offset: 0x00033342
		public InvalidADRecipientException(Exception innerException) : base(Strings.InvalidADRecipientException, innerException)
		{
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00035150 File Offset: 0x00033350
		protected InvalidADRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003515A File Offset: 0x0003335A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

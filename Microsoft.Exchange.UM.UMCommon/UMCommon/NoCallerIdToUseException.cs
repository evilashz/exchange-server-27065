using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200019E RID: 414
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoCallerIdToUseException : LocalizedException
	{
		// Token: 0x06000E49 RID: 3657 RVA: 0x00034E5D File Offset: 0x0003305D
		public NoCallerIdToUseException() : base(Strings.NoCallerIdToUseException)
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00034E6A File Offset: 0x0003306A
		public NoCallerIdToUseException(Exception innerException) : base(Strings.NoCallerIdToUseException, innerException)
		{
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00034E78 File Offset: 0x00033078
		protected NoCallerIdToUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00034E82 File Offset: 0x00033082
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

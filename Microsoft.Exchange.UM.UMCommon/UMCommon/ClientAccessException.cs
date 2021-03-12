using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A3 RID: 419
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClientAccessException : LocalizedException
	{
		// Token: 0x06000E5E RID: 3678 RVA: 0x00034F89 File Offset: 0x00033189
		public ClientAccessException() : base(Strings.ClientAccessException)
		{
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00034F96 File Offset: 0x00033196
		public ClientAccessException(Exception innerException) : base(Strings.ClientAccessException, innerException)
		{
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00034FA4 File Offset: 0x000331A4
		protected ClientAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00034FAE File Offset: 0x000331AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

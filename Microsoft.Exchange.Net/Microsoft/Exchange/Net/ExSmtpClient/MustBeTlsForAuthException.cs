using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E8 RID: 232
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MustBeTlsForAuthException : LocalizedException
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x00015ED8 File Offset: 0x000140D8
		public MustBeTlsForAuthException() : base(NetException.MustBeTlsForAuthException)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00015EE5 File Offset: 0x000140E5
		public MustBeTlsForAuthException(Exception innerException) : base(NetException.MustBeTlsForAuthException, innerException)
		{
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00015EF3 File Offset: 0x000140F3
		protected MustBeTlsForAuthException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015EFD File Offset: 0x000140FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E5 RID: 229
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AuthFailureException : LocalizedException
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00015DB9 File Offset: 0x00013FB9
		public AuthFailureException() : base(NetException.AuthFailureException)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00015DC6 File Offset: 0x00013FC6
		public AuthFailureException(Exception innerException) : base(NetException.AuthFailureException, innerException)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00015DD4 File Offset: 0x00013FD4
		protected AuthFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00015DDE File Offset: 0x00013FDE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

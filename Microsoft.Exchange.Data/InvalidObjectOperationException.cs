using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D8 RID: 216
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidObjectOperationException : DataSourceOperationException
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x0001A6B7 File Offset: 0x000188B7
		public InvalidObjectOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001A6C0 File Offset: 0x000188C0
		public InvalidObjectOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001A6CA File Offset: 0x000188CA
		protected InvalidObjectOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001A6D4 File Offset: 0x000188D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

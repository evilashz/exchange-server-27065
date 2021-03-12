using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000050 RID: 80
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiSessionLimitException : MapiOperationException
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public MapiSessionLimitException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E5D1 File Offset: 0x0000C7D1
		public MapiSessionLimitException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E5DB File Offset: 0x0000C7DB
		protected MapiSessionLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000E5E5 File Offset: 0x0000C7E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

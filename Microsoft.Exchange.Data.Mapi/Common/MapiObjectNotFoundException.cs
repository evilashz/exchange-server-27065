using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200004C RID: 76
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiObjectNotFoundException : MapiOperationException
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000E4DB File Offset: 0x0000C6DB
		public MapiObjectNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public MapiObjectNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000E4EE File Offset: 0x0000C6EE
		protected MapiObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

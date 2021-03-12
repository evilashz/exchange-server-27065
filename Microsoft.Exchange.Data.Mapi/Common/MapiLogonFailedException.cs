using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200004F RID: 79
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiLogonFailedException : MapiOperationException
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
		public MapiLogonFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000E5AA File Offset: 0x0000C7AA
		public MapiLogonFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		protected MapiLogonFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000E5BE File Offset: 0x0000C7BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

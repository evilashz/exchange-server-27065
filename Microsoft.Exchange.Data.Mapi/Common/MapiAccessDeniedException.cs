using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000051 RID: 81
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiAccessDeniedException : MapiOperationException
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000E5EF File Offset: 0x0000C7EF
		public MapiAccessDeniedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E5F8 File Offset: 0x0000C7F8
		public MapiAccessDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000E602 File Offset: 0x0000C802
		protected MapiAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E60C File Offset: 0x0000C80C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

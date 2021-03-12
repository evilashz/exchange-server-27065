using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200004A RID: 74
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiObjectAlreadyExistsException : MapiOperationException
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000E43C File Offset: 0x0000C63C
		public MapiObjectAlreadyExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E445 File Offset: 0x0000C645
		public MapiObjectAlreadyExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E44F File Offset: 0x0000C64F
		protected MapiObjectAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E459 File Offset: 0x0000C659
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

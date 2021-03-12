using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000055 RID: 85
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiExceptionNoReplicaHere : MapiOperationException
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public MapiExceptionNoReplicaHere(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
		public MapiExceptionNoReplicaHere(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E6EF File Offset: 0x0000C8EF
		protected MapiExceptionNoReplicaHere(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E6F9 File Offset: 0x0000C8F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

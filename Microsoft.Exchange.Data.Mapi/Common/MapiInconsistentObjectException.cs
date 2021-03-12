using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000040 RID: 64
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiInconsistentObjectException : MapiOperationException
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000DB97 File Offset: 0x0000BD97
		public MapiInconsistentObjectException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		public MapiInconsistentObjectException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DBAA File Offset: 0x0000BDAA
		protected MapiInconsistentObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

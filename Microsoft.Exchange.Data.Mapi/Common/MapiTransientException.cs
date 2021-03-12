using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200003D RID: 61
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiTransientException : DataSourceTransientException
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0000DB22 File Offset: 0x0000BD22
		public MapiTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DB2B File Offset: 0x0000BD2B
		public MapiTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DB35 File Offset: 0x0000BD35
		protected MapiTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DB3F File Offset: 0x0000BD3F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

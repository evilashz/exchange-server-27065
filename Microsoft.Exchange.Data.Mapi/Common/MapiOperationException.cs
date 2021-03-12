using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200003E RID: 62
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiOperationException : DataSourceOperationException
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000DB49 File Offset: 0x0000BD49
		public MapiOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DB52 File Offset: 0x0000BD52
		public MapiOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000DB5C File Offset: 0x0000BD5C
		protected MapiOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DB66 File Offset: 0x0000BD66
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

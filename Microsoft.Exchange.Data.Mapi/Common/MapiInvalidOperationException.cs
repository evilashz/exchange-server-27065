using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200003F RID: 63
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiInvalidOperationException : MapiOperationException
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public MapiInvalidOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DB79 File Offset: 0x0000BD79
		public MapiInvalidOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DB83 File Offset: 0x0000BD83
		protected MapiInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DB8D File Offset: 0x0000BD8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

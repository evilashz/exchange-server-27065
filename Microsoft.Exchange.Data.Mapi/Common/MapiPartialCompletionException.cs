using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000041 RID: 65
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiPartialCompletionException : MapiOperationException
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		public MapiPartialCompletionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
		public MapiPartialCompletionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DBD1 File Offset: 0x0000BDD1
		protected MapiPartialCompletionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000DBDB File Offset: 0x0000BDDB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

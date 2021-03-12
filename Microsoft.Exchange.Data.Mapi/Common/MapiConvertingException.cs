using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000042 RID: 66
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiConvertingException : MapiInvalidOperationException
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000DBE5 File Offset: 0x0000BDE5
		public MapiConvertingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DBEE File Offset: 0x0000BDEE
		public MapiConvertingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		protected MapiConvertingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000DC02 File Offset: 0x0000BE02
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

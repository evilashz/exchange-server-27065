using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AEF RID: 2799
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerComponentApiException : TransientException
	{
		// Token: 0x06008155 RID: 33109 RVA: 0x001A6585 File Offset: 0x001A4785
		public ServerComponentApiException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008156 RID: 33110 RVA: 0x001A658E File Offset: 0x001A478E
		public ServerComponentApiException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x001A6598 File Offset: 0x001A4798
		protected ServerComponentApiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x001A65A2 File Offset: 0x001A47A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

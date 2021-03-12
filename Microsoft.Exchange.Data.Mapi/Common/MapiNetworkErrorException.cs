using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000054 RID: 84
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MapiNetworkErrorException : MapiTransientException
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000E6B5 File Offset: 0x0000C8B5
		public MapiNetworkErrorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E6BE File Offset: 0x0000C8BE
		public MapiNetworkErrorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		protected MapiNetworkErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E6D2 File Offset: 0x0000C8D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

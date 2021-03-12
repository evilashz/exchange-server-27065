using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LiveClientException : LocalizedException
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000286E File Offset: 0x00000A6E
		public LiveClientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002877 File Offset: 0x00000A77
		public LiveClientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002881 File Offset: 0x00000A81
		protected LiveClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000288B File Offset: 0x00000A8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

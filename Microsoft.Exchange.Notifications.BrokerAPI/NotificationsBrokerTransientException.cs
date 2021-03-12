using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000030 RID: 48
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationsBrokerTransientException : NotificationsBrokerException
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00003BEA File Offset: 0x00001DEA
		public NotificationsBrokerTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003BF3 File Offset: 0x00001DF3
		public NotificationsBrokerTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003BFD File Offset: 0x00001DFD
		protected NotificationsBrokerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003C07 File Offset: 0x00001E07
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

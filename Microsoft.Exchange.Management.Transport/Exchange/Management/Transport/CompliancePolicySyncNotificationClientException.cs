using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000183 RID: 387
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CompliancePolicySyncNotificationClientException : LocalizedException
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0003654B File Offset: 0x0003474B
		public CompliancePolicySyncNotificationClientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00036554 File Offset: 0x00034754
		public CompliancePolicySyncNotificationClientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003655E File Offset: 0x0003475E
		protected CompliancePolicySyncNotificationClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00036568 File Offset: 0x00034768
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

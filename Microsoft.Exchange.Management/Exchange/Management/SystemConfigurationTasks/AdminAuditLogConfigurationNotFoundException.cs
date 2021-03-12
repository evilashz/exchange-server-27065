using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200113B RID: 4411
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminAuditLogConfigurationNotFoundException : AdminAuditLogException
	{
		// Token: 0x0600B50C RID: 46348 RVA: 0x0029D995 File Offset: 0x0029BB95
		public AdminAuditLogConfigurationNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B50D RID: 46349 RVA: 0x0029D99E File Offset: 0x0029BB9E
		public AdminAuditLogConfigurationNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B50E RID: 46350 RVA: 0x0029D9A8 File Offset: 0x0029BBA8
		protected AdminAuditLogConfigurationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B50F RID: 46351 RVA: 0x0029D9B2 File Offset: 0x0029BBB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

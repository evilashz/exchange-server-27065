using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200113E RID: 4414
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdminAuditLogImpersonationException : AdminAuditLogException
	{
		// Token: 0x0600B51C RID: 46364 RVA: 0x0029DB4A File Offset: 0x0029BD4A
		public AdminAuditLogImpersonationException() : base(Strings.FailedToCreateEwsConnection)
		{
		}

		// Token: 0x0600B51D RID: 46365 RVA: 0x0029DB57 File Offset: 0x0029BD57
		public AdminAuditLogImpersonationException(Exception innerException) : base(Strings.FailedToCreateEwsConnection, innerException)
		{
		}

		// Token: 0x0600B51E RID: 46366 RVA: 0x0029DB65 File Offset: 0x0029BD65
		protected AdminAuditLogImpersonationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B51F RID: 46367 RVA: 0x0029DB6F File Offset: 0x0029BD6F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

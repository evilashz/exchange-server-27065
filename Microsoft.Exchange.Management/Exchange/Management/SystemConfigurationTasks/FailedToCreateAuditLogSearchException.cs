using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001139 RID: 4409
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToCreateAuditLogSearchException : LocalizedException
	{
		// Token: 0x0600B504 RID: 46340 RVA: 0x0029D93F File Offset: 0x0029BB3F
		public FailedToCreateAuditLogSearchException() : base(Strings.FailedToCreateAuditLogSearch)
		{
		}

		// Token: 0x0600B505 RID: 46341 RVA: 0x0029D94C File Offset: 0x0029BB4C
		public FailedToCreateAuditLogSearchException(Exception innerException) : base(Strings.FailedToCreateAuditLogSearch, innerException)
		{
		}

		// Token: 0x0600B506 RID: 46342 RVA: 0x0029D95A File Offset: 0x0029BB5A
		protected FailedToCreateAuditLogSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B507 RID: 46343 RVA: 0x0029D964 File Offset: 0x0029BB64
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

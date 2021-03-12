using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001140 RID: 4416
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxAuditLogSearchCriteriaException : LocalizedException
	{
		// Token: 0x0600B524 RID: 46372 RVA: 0x0029DBA8 File Offset: 0x0029BDA8
		public MailboxAuditLogSearchCriteriaException() : base(Strings.ErrorInvalidMailboxAuditLogSearchCriteria)
		{
		}

		// Token: 0x0600B525 RID: 46373 RVA: 0x0029DBB5 File Offset: 0x0029BDB5
		public MailboxAuditLogSearchCriteriaException(Exception innerException) : base(Strings.ErrorInvalidMailboxAuditLogSearchCriteria, innerException)
		{
		}

		// Token: 0x0600B526 RID: 46374 RVA: 0x0029DBC3 File Offset: 0x0029BDC3
		protected MailboxAuditLogSearchCriteriaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B527 RID: 46375 RVA: 0x0029DBCD File Offset: 0x0029BDCD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

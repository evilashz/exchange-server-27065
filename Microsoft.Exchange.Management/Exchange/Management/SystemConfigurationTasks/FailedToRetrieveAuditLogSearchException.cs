using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001138 RID: 4408
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToRetrieveAuditLogSearchException : LocalizedException
	{
		// Token: 0x0600B500 RID: 46336 RVA: 0x0029D910 File Offset: 0x0029BB10
		public FailedToRetrieveAuditLogSearchException() : base(Strings.FailedToRetrieveAuditLogSearch)
		{
		}

		// Token: 0x0600B501 RID: 46337 RVA: 0x0029D91D File Offset: 0x0029BB1D
		public FailedToRetrieveAuditLogSearchException(Exception innerException) : base(Strings.FailedToRetrieveAuditLogSearch, innerException)
		{
		}

		// Token: 0x0600B502 RID: 46338 RVA: 0x0029D92B File Offset: 0x0029BB2B
		protected FailedToRetrieveAuditLogSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B503 RID: 46339 RVA: 0x0029D935 File Offset: 0x0029BB35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

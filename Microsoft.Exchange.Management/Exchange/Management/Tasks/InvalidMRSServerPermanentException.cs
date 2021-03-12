using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC0 RID: 3776
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMRSServerPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A898 RID: 43160 RVA: 0x0028A416 File Offset: 0x00288616
		public InvalidMRSServerPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A899 RID: 43161 RVA: 0x0028A41F File Offset: 0x0028861F
		public InvalidMRSServerPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A89A RID: 43162 RVA: 0x0028A429 File Offset: 0x00288629
		protected InvalidMRSServerPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A89B RID: 43163 RVA: 0x0028A433 File Offset: 0x00288633
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E6E RID: 3694
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GroupMailboxFailedToFindDatabaseException : RecipientTaskException
	{
		// Token: 0x0600A706 RID: 42758 RVA: 0x00287F37 File Offset: 0x00286137
		public GroupMailboxFailedToFindDatabaseException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A707 RID: 42759 RVA: 0x00287F40 File Offset: 0x00286140
		public GroupMailboxFailedToFindDatabaseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A708 RID: 42760 RVA: 0x00287F4A File Offset: 0x0028614A
		protected GroupMailboxFailedToFindDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A709 RID: 42761 RVA: 0x00287F54 File Offset: 0x00286154
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

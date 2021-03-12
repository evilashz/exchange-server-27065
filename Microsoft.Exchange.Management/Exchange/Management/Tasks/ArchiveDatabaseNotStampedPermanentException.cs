using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E97 RID: 3735
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArchiveDatabaseNotStampedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7CE RID: 42958 RVA: 0x0028912C File Offset: 0x0028732C
		public ArchiveDatabaseNotStampedPermanentException() : base(Strings.ArchiveDatabaseNotExplicitlyStampedError)
		{
		}

		// Token: 0x0600A7CF RID: 42959 RVA: 0x00289139 File Offset: 0x00287339
		public ArchiveDatabaseNotStampedPermanentException(Exception innerException) : base(Strings.ArchiveDatabaseNotExplicitlyStampedError, innerException)
		{
		}

		// Token: 0x0600A7D0 RID: 42960 RVA: 0x00289147 File Offset: 0x00287347
		protected ArchiveDatabaseNotStampedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7D1 RID: 42961 RVA: 0x00289151 File Offset: 0x00287351
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

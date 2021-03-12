using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E96 RID: 3734
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArchiveDisabledPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A7CA RID: 42954 RVA: 0x002890FD File Offset: 0x002872FD
		public ArchiveDisabledPermanentException() : base(Strings.ArchiveDisabledError)
		{
		}

		// Token: 0x0600A7CB RID: 42955 RVA: 0x0028910A File Offset: 0x0028730A
		public ArchiveDisabledPermanentException(Exception innerException) : base(Strings.ArchiveDisabledError, innerException)
		{
		}

		// Token: 0x0600A7CC RID: 42956 RVA: 0x00289118 File Offset: 0x00287318
		protected ArchiveDisabledPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A7CD RID: 42957 RVA: 0x00289122 File Offset: 0x00287322
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

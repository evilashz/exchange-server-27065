using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC4 RID: 3780
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultiplePublicFolderMigrationTypesNotAllowedException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8AD RID: 43181 RVA: 0x0028A64D File Offset: 0x0028884D
		public MultiplePublicFolderMigrationTypesNotAllowedException() : base(Strings.ErrorAnotherPublicFolderMigrationTypeAlreadyInProgress)
		{
		}

		// Token: 0x0600A8AE RID: 43182 RVA: 0x0028A65A File Offset: 0x0028885A
		public MultiplePublicFolderMigrationTypesNotAllowedException(Exception innerException) : base(Strings.ErrorAnotherPublicFolderMigrationTypeAlreadyInProgress, innerException)
		{
		}

		// Token: 0x0600A8AF RID: 43183 RVA: 0x0028A668 File Offset: 0x00288868
		protected MultiplePublicFolderMigrationTypesNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A8B0 RID: 43184 RVA: 0x0028A672 File Offset: 0x00288872
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200035E RID: 862
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublicFolderMailboxesNotProvisionedForMigrationException : MailboxReplicationPermanentException
	{
		// Token: 0x06002697 RID: 9879 RVA: 0x000536BE File Offset: 0x000518BE
		public PublicFolderMailboxesNotProvisionedForMigrationException() : base(MrsStrings.PublicFolderMailboxesNotProvisionedForMigration)
		{
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000536CB File Offset: 0x000518CB
		public PublicFolderMailboxesNotProvisionedForMigrationException(Exception innerException) : base(MrsStrings.PublicFolderMailboxesNotProvisionedForMigration, innerException)
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000536D9 File Offset: 0x000518D9
		protected PublicFolderMailboxesNotProvisionedForMigrationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000536E3 File Offset: 0x000518E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

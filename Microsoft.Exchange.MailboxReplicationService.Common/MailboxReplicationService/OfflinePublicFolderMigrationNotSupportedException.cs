using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000360 RID: 864
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OfflinePublicFolderMigrationNotSupportedException : MailboxReplicationPermanentException
	{
		// Token: 0x0600269F RID: 9887 RVA: 0x00053714 File Offset: 0x00051914
		public OfflinePublicFolderMigrationNotSupportedException() : base(MrsStrings.OfflinePublicFolderMigrationNotSupported)
		{
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00053721 File Offset: 0x00051921
		public OfflinePublicFolderMigrationNotSupportedException(Exception innerException) : base(MrsStrings.OfflinePublicFolderMigrationNotSupported, innerException)
		{
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0005372F File Offset: 0x0005192F
		protected OfflinePublicFolderMigrationNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00053739 File Offset: 0x00051939
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200035F RID: 863
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublicFolderMigrationNotSupportedFromVersionException : MailboxReplicationPermanentException
	{
		// Token: 0x0600269B RID: 9883 RVA: 0x000536ED File Offset: 0x000518ED
		public PublicFolderMigrationNotSupportedFromVersionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000536F6 File Offset: 0x000518F6
		public PublicFolderMigrationNotSupportedFromVersionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x00053700 File Offset: 0x00051900
		protected PublicFolderMigrationNotSupportedFromVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0005370A File Offset: 0x0005190A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

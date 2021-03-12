using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E9 RID: 745
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoPublicFolderMailboxFoundInSourceException : MailboxReplicationPermanentException
	{
		// Token: 0x06002450 RID: 9296 RVA: 0x0004FE49 File Offset: 0x0004E049
		public NoPublicFolderMailboxFoundInSourceException() : base(MrsStrings.NoPublicFolderMailboxFoundInSource)
		{
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0004FE56 File Offset: 0x0004E056
		public NoPublicFolderMailboxFoundInSourceException(Exception innerException) : base(MrsStrings.NoPublicFolderMailboxFoundInSource, innerException)
		{
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x0004FE64 File Offset: 0x0004E064
		protected NoPublicFolderMailboxFoundInSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0004FE6E File Offset: 0x0004E06E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

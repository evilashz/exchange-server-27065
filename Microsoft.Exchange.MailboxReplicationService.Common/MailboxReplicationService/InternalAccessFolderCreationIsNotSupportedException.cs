using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D0 RID: 720
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InternalAccessFolderCreationIsNotSupportedException : MailboxReplicationPermanentException
	{
		// Token: 0x060023D1 RID: 9169 RVA: 0x0004F172 File Offset: 0x0004D372
		public InternalAccessFolderCreationIsNotSupportedException() : base(MrsStrings.InternalAccessFolderCreationIsNotSupported)
		{
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x0004F17F File Offset: 0x0004D37F
		public InternalAccessFolderCreationIsNotSupportedException(Exception innerException) : base(MrsStrings.InternalAccessFolderCreationIsNotSupported, innerException)
		{
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0004F18D File Offset: 0x0004D38D
		protected InternalAccessFolderCreationIsNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x0004F197 File Offset: 0x0004D397
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000348 RID: 840
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PSTPathMustBeAFilePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002620 RID: 9760 RVA: 0x00052961 File Offset: 0x00050B61
		public PSTPathMustBeAFilePermanentException() : base(MrsStrings.PSTPathMustBeAFile)
		{
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0005296E File Offset: 0x00050B6E
		public PSTPathMustBeAFilePermanentException(Exception innerException) : base(MrsStrings.PSTPathMustBeAFile, innerException)
		{
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x0005297C File Offset: 0x00050B7C
		protected PSTPathMustBeAFilePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00052986 File Offset: 0x00050B86
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}

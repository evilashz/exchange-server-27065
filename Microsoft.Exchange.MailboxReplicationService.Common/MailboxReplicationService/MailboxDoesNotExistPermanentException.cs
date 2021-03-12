using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200031A RID: 794
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxDoesNotExistPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002537 RID: 9527 RVA: 0x000511E4 File Offset: 0x0004F3E4
		public MailboxDoesNotExistPermanentException(LocalizedString mbxId) : base(MrsStrings.MailboxDoesNotExist(mbxId))
		{
			this.mbxId = mbxId;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000511F9 File Offset: 0x0004F3F9
		public MailboxDoesNotExistPermanentException(LocalizedString mbxId, Exception innerException) : base(MrsStrings.MailboxDoesNotExist(mbxId), innerException)
		{
			this.mbxId = mbxId;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0005120F File Offset: 0x0004F40F
		protected MailboxDoesNotExistPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxId = (LocalizedString)info.GetValue("mbxId", typeof(LocalizedString));
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00051239 File Offset: 0x0004F439
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxId", this.mbxId);
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600253B RID: 9531 RVA: 0x00051259 File Offset: 0x0004F459
		public LocalizedString MbxId
		{
			get
			{
				return this.mbxId;
			}
		}

		// Token: 0x04001018 RID: 4120
		private readonly LocalizedString mbxId;
	}
}

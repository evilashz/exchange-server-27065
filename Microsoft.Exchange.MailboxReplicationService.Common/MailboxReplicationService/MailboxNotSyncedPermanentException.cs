using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002ED RID: 749
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxNotSyncedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002462 RID: 9314 RVA: 0x0004FFA8 File Offset: 0x0004E1A8
		public MailboxNotSyncedPermanentException(Guid mbxGuid) : base(MrsStrings.MailboxNotSynced(mbxGuid))
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0004FFBD File Offset: 0x0004E1BD
		public MailboxNotSyncedPermanentException(Guid mbxGuid, Exception innerException) : base(MrsStrings.MailboxNotSynced(mbxGuid), innerException)
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0004FFD3 File Offset: 0x0004E1D3
		protected MailboxNotSyncedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxGuid = (Guid)info.GetValue("mbxGuid", typeof(Guid));
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0004FFFD File Offset: 0x0004E1FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxGuid", this.mbxGuid);
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x0005001D File Offset: 0x0004E21D
		public Guid MbxGuid
		{
			get
			{
				return this.mbxGuid;
			}
		}

		// Token: 0x04000FF7 RID: 4087
		private readonly Guid mbxGuid;
	}
}

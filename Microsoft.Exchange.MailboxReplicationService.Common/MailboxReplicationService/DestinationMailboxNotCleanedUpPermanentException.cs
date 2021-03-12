using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002EE RID: 750
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DestinationMailboxNotCleanedUpPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002467 RID: 9319 RVA: 0x00050025 File Offset: 0x0004E225
		public DestinationMailboxNotCleanedUpPermanentException(Guid mbxGuid) : base(MrsStrings.DestinationMailboxNotCleanedUp(mbxGuid))
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0005003A File Offset: 0x0004E23A
		public DestinationMailboxNotCleanedUpPermanentException(Guid mbxGuid, Exception innerException) : base(MrsStrings.DestinationMailboxNotCleanedUp(mbxGuid), innerException)
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00050050 File Offset: 0x0004E250
		protected DestinationMailboxNotCleanedUpPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxGuid = (Guid)info.GetValue("mbxGuid", typeof(Guid));
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0005007A File Offset: 0x0004E27A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxGuid", this.mbxGuid);
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x0005009A File Offset: 0x0004E29A
		public Guid MbxGuid
		{
			get
			{
				return this.mbxGuid;
			}
		}

		// Token: 0x04000FF8 RID: 4088
		private readonly Guid mbxGuid;
	}
}

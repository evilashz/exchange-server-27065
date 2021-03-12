using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002EF RID: 751
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DestinationADNotUpToDatePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600246C RID: 9324 RVA: 0x000500A2 File Offset: 0x0004E2A2
		public DestinationADNotUpToDatePermanentException(Guid mbxGuid) : base(MrsStrings.DestinationADNotUpToDate(mbxGuid))
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000500B7 File Offset: 0x0004E2B7
		public DestinationADNotUpToDatePermanentException(Guid mbxGuid, Exception innerException) : base(MrsStrings.DestinationADNotUpToDate(mbxGuid), innerException)
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000500CD File Offset: 0x0004E2CD
		protected DestinationADNotUpToDatePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxGuid = (Guid)info.GetValue("mbxGuid", typeof(Guid));
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000500F7 File Offset: 0x0004E2F7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxGuid", this.mbxGuid);
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00050117 File Offset: 0x0004E317
		public Guid MbxGuid
		{
			get
			{
				return this.mbxGuid;
			}
		}

		// Token: 0x04000FF9 RID: 4089
		private readonly Guid mbxGuid;
	}
}

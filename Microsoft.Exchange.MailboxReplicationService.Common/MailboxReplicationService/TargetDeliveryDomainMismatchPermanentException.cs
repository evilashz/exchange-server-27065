using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000334 RID: 820
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetDeliveryDomainMismatchPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060025C0 RID: 9664 RVA: 0x000520D4 File Offset: 0x000502D4
		public TargetDeliveryDomainMismatchPermanentException(string targetDeliveryDomain) : base(MrsStrings.ErrorTargetDeliveryDomainMismatch(targetDeliveryDomain))
		{
			this.targetDeliveryDomain = targetDeliveryDomain;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000520E9 File Offset: 0x000502E9
		public TargetDeliveryDomainMismatchPermanentException(string targetDeliveryDomain, Exception innerException) : base(MrsStrings.ErrorTargetDeliveryDomainMismatch(targetDeliveryDomain), innerException)
		{
			this.targetDeliveryDomain = targetDeliveryDomain;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000520FF File Offset: 0x000502FF
		protected TargetDeliveryDomainMismatchPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.targetDeliveryDomain = (string)info.GetValue("targetDeliveryDomain", typeof(string));
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00052129 File Offset: 0x00050329
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("targetDeliveryDomain", this.targetDeliveryDomain);
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x00052144 File Offset: 0x00050344
		public string TargetDeliveryDomain
		{
			get
			{
				return this.targetDeliveryDomain;
			}
		}

		// Token: 0x04001039 RID: 4153
		private readonly string targetDeliveryDomain;
	}
}

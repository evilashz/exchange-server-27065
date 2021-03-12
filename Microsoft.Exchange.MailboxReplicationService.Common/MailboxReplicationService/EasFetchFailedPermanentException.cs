using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000380 RID: 896
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFetchFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600273C RID: 10044 RVA: 0x0005456A File Offset: 0x0005276A
		public EasFetchFailedPermanentException(string errorMessage) : base(MrsStrings.EasFetchFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0005457F File Offset: 0x0005277F
		public EasFetchFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasFetchFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x00054595 File Offset: 0x00052795
		protected EasFetchFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000545BF File Offset: 0x000527BF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x000545DA File Offset: 0x000527DA
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001085 RID: 4229
		private readonly string errorMessage;
	}
}

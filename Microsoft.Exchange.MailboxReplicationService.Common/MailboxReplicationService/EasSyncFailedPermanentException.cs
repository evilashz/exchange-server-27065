using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200037E RID: 894
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasSyncFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002732 RID: 10034 RVA: 0x0005447A File Offset: 0x0005267A
		public EasSyncFailedPermanentException(string errorMessage) : base(MrsStrings.EasSyncFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0005448F File Offset: 0x0005268F
		public EasSyncFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasSyncFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x000544A5 File Offset: 0x000526A5
		protected EasSyncFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x000544CF File Offset: 0x000526CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000544EA File Offset: 0x000526EA
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001083 RID: 4227
		private readonly string errorMessage;
	}
}

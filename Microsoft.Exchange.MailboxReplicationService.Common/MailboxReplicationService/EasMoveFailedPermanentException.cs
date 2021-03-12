using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000384 RID: 900
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasMoveFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002750 RID: 10064 RVA: 0x0005474A File Offset: 0x0005294A
		public EasMoveFailedPermanentException(string errorMessage) : base(MrsStrings.EasMoveFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0005475F File Offset: 0x0005295F
		public EasMoveFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasMoveFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00054775 File Offset: 0x00052975
		protected EasMoveFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0005479F File Offset: 0x0005299F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x000547BA File Offset: 0x000529BA
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001089 RID: 4233
		private readonly string errorMessage;
	}
}

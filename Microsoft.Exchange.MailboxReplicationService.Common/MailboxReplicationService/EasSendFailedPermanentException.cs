using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000386 RID: 902
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasSendFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600275A RID: 10074 RVA: 0x0005483A File Offset: 0x00052A3A
		public EasSendFailedPermanentException(string errorMessage) : base(MrsStrings.EasSendFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0005484F File Offset: 0x00052A4F
		public EasSendFailedPermanentException(string errorMessage, Exception innerException) : base(MrsStrings.EasSendFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00054865 File Offset: 0x00052A65
		protected EasSendFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0005488F File Offset: 0x00052A8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x000548AA File Offset: 0x00052AAA
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400108B RID: 4235
		private readonly string errorMessage;
	}
}

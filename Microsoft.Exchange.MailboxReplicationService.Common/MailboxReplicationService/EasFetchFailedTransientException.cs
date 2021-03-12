using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200037F RID: 895
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EasFetchFailedTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002737 RID: 10039 RVA: 0x000544F2 File Offset: 0x000526F2
		public EasFetchFailedTransientException(string errorMessage) : base(MrsStrings.EasFetchFailed(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x00054507 File Offset: 0x00052707
		public EasFetchFailedTransientException(string errorMessage, Exception innerException) : base(MrsStrings.EasFetchFailed(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0005451D File Offset: 0x0005271D
		protected EasFetchFailedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x00054547 File Offset: 0x00052747
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x00054562 File Offset: 0x00052762
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04001084 RID: 4228
		private readonly string errorMessage;
	}
}

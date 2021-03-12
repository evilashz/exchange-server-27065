using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F31 RID: 3889
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplicationCheckFailedException : ReplicationCheckException
	{
		// Token: 0x0600AAEA RID: 43754 RVA: 0x0028E35D File Offset: 0x0028C55D
		public ReplicationCheckFailedException(string checkTitle, string errorMessage) : base(Strings.ReplicationCheckGeneralFail(checkTitle, errorMessage))
		{
			this.checkTitle = checkTitle;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAEB RID: 43755 RVA: 0x0028E37A File Offset: 0x0028C57A
		public ReplicationCheckFailedException(string checkTitle, string errorMessage, Exception innerException) : base(Strings.ReplicationCheckGeneralFail(checkTitle, errorMessage), innerException)
		{
			this.checkTitle = checkTitle;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AAEC RID: 43756 RVA: 0x0028E398 File Offset: 0x0028C598
		protected ReplicationCheckFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.checkTitle = (string)info.GetValue("checkTitle", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AAED RID: 43757 RVA: 0x0028E3ED File Offset: 0x0028C5ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("checkTitle", this.checkTitle);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17003743 RID: 14147
		// (get) Token: 0x0600AAEE RID: 43758 RVA: 0x0028E419 File Offset: 0x0028C619
		public string CheckTitle
		{
			get
			{
				return this.checkTitle;
			}
		}

		// Token: 0x17003744 RID: 14148
		// (get) Token: 0x0600AAEF RID: 43759 RVA: 0x0028E421 File Offset: 0x0028C621
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040060A9 RID: 24745
		private readonly string checkTitle;

		// Token: 0x040060AA RID: 24746
		private readonly string errorMessage;
	}
}

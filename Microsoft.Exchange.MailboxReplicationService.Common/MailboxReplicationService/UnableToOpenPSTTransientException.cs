using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000347 RID: 839
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToOpenPSTTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600261A RID: 9754 RVA: 0x00052895 File Offset: 0x00050A95
		public UnableToOpenPSTTransientException(string filePath, string exceptionMessage) : base(MrsStrings.UnableToOpenPST2(filePath, exceptionMessage))
		{
			this.filePath = filePath;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000528B2 File Offset: 0x00050AB2
		public UnableToOpenPSTTransientException(string filePath, string exceptionMessage, Exception innerException) : base(MrsStrings.UnableToOpenPST2(filePath, exceptionMessage), innerException)
		{
			this.filePath = filePath;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000528D0 File Offset: 0x00050AD0
		protected UnableToOpenPSTTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
			this.exceptionMessage = (string)info.GetValue("exceptionMessage", typeof(string));
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00052925 File Offset: 0x00050B25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x00052951 File Offset: 0x00050B51
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x00052959 File Offset: 0x00050B59
		public string ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04001047 RID: 4167
		private readonly string filePath;

		// Token: 0x04001048 RID: 4168
		private readonly string exceptionMessage;
	}
}

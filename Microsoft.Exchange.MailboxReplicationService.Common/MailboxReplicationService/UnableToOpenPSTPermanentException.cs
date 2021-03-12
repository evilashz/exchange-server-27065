using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000346 RID: 838
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToOpenPSTPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002614 RID: 9748 RVA: 0x000527C7 File Offset: 0x000509C7
		public UnableToOpenPSTPermanentException(string filePath, string exceptionMessage) : base(MrsStrings.UnableToOpenPST2(filePath, exceptionMessage))
		{
			this.filePath = filePath;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000527E4 File Offset: 0x000509E4
		public UnableToOpenPSTPermanentException(string filePath, string exceptionMessage, Exception innerException) : base(MrsStrings.UnableToOpenPST2(filePath, exceptionMessage), innerException)
		{
			this.filePath = filePath;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x00052804 File Offset: 0x00050A04
		protected UnableToOpenPSTPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
			this.exceptionMessage = (string)info.GetValue("exceptionMessage", typeof(string));
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00052859 File Offset: 0x00050A59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x00052885 File Offset: 0x00050A85
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x0005288D File Offset: 0x00050A8D
		public string ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04001045 RID: 4165
		private readonly string filePath;

		// Token: 0x04001046 RID: 4166
		private readonly string exceptionMessage;
	}
}

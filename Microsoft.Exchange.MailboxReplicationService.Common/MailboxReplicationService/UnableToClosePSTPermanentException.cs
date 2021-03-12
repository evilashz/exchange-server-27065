using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200034A RID: 842
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToClosePSTPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002628 RID: 9768 RVA: 0x000529BF File Offset: 0x00050BBF
		public UnableToClosePSTPermanentException(string filePath) : base(MrsStrings.UnableToClosePST(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000529D4 File Offset: 0x00050BD4
		public UnableToClosePSTPermanentException(string filePath, Exception innerException) : base(MrsStrings.UnableToClosePST(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000529EA File Offset: 0x00050BEA
		protected UnableToClosePSTPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x00052A14 File Offset: 0x00050C14
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00052A2F File Offset: 0x00050C2F
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x04001049 RID: 4169
		private readonly string filePath;
	}
}

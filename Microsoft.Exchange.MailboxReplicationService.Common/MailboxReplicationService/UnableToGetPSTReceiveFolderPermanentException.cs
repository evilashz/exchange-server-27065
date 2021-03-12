using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000350 RID: 848
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTReceiveFolderPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002649 RID: 9801 RVA: 0x00052D89 File Offset: 0x00050F89
		public UnableToGetPSTReceiveFolderPermanentException(string filePath, string messageClass) : base(MrsStrings.UnableToGetPSTReceiveFolder(filePath, messageClass))
		{
			this.filePath = filePath;
			this.messageClass = messageClass;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00052DA6 File Offset: 0x00050FA6
		public UnableToGetPSTReceiveFolderPermanentException(string filePath, string messageClass, Exception innerException) : base(MrsStrings.UnableToGetPSTReceiveFolder(filePath, messageClass), innerException)
		{
			this.filePath = filePath;
			this.messageClass = messageClass;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00052DC4 File Offset: 0x00050FC4
		protected UnableToGetPSTReceiveFolderPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
			this.messageClass = (string)info.GetValue("messageClass", typeof(string));
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00052E19 File Offset: 0x00051019
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
			info.AddValue("messageClass", this.messageClass);
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x00052E45 File Offset: 0x00051045
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x00052E4D File Offset: 0x0005104D
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x04001052 RID: 4178
		private readonly string filePath;

		// Token: 0x04001053 RID: 4179
		private readonly string messageClass;
	}
}

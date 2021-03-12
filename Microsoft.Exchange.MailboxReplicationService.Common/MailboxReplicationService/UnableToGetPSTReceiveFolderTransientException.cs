using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000351 RID: 849
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToGetPSTReceiveFolderTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600264F RID: 9807 RVA: 0x00052E55 File Offset: 0x00051055
		public UnableToGetPSTReceiveFolderTransientException(string filePath, string messageClass) : base(MrsStrings.UnableToGetPSTReceiveFolder(filePath, messageClass))
		{
			this.filePath = filePath;
			this.messageClass = messageClass;
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x00052E72 File Offset: 0x00051072
		public UnableToGetPSTReceiveFolderTransientException(string filePath, string messageClass, Exception innerException) : base(MrsStrings.UnableToGetPSTReceiveFolder(filePath, messageClass), innerException)
		{
			this.filePath = filePath;
			this.messageClass = messageClass;
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x00052E90 File Offset: 0x00051090
		protected UnableToGetPSTReceiveFolderTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
			this.messageClass = (string)info.GetValue("messageClass", typeof(string));
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x00052EE5 File Offset: 0x000510E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
			info.AddValue("messageClass", this.messageClass);
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00052F11 File Offset: 0x00051111
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00052F19 File Offset: 0x00051119
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x04001054 RID: 4180
		private readonly string filePath;

		// Token: 0x04001055 RID: 4181
		private readonly string messageClass;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000374 RID: 884
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotCreateMessageIdException : MailboxReplicationPermanentException
	{
		// Token: 0x060026FE RID: 9982 RVA: 0x00053F15 File Offset: 0x00052115
		public CannotCreateMessageIdException(long uid, string folderName) : base(MrsStrings.CannotCreateMessageId(uid, folderName))
		{
			this.uid = uid;
			this.folderName = folderName;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x00053F32 File Offset: 0x00052132
		public CannotCreateMessageIdException(long uid, string folderName, Exception innerException) : base(MrsStrings.CannotCreateMessageId(uid, folderName), innerException)
		{
			this.uid = uid;
			this.folderName = folderName;
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x00053F50 File Offset: 0x00052150
		protected CannotCreateMessageIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.uid = (long)info.GetValue("uid", typeof(long));
			this.folderName = (string)info.GetValue("folderName", typeof(string));
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00053FA5 File Offset: 0x000521A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("uid", this.uid);
			info.AddValue("folderName", this.folderName);
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x00053FD1 File Offset: 0x000521D1
		public long Uid
		{
			get
			{
				return this.uid;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x00053FD9 File Offset: 0x000521D9
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x04001077 RID: 4215
		private readonly long uid;

		// Token: 0x04001078 RID: 4216
		private readonly string folderName;
	}
}

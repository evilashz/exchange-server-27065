using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200035A RID: 858
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToLoadPSTSyncStateTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x00053389 File Offset: 0x00051589
		public UnableToLoadPSTSyncStateTransientException(string filePath) : base(MrsStrings.UnableToLoadPSTSyncState(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x0005339E File Offset: 0x0005159E
		public UnableToLoadPSTSyncStateTransientException(string filePath, Exception innerException) : base(MrsStrings.UnableToLoadPSTSyncState(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000533B4 File Offset: 0x000515B4
		protected UnableToLoadPSTSyncStateTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000533DE File Offset: 0x000515DE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000533F9 File Offset: 0x000515F9
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x04001060 RID: 4192
		private readonly string filePath;
	}
}

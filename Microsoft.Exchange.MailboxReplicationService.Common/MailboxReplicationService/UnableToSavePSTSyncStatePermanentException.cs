using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200035B RID: 859
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToSavePSTSyncStatePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002684 RID: 9860 RVA: 0x00053401 File Offset: 0x00051601
		public UnableToSavePSTSyncStatePermanentException(string filePath) : base(MrsStrings.UnableToSavePSTSyncState(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x00053416 File Offset: 0x00051616
		public UnableToSavePSTSyncStatePermanentException(string filePath, Exception innerException) : base(MrsStrings.UnableToSavePSTSyncState(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0005342C File Offset: 0x0005162C
		protected UnableToSavePSTSyncStatePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x00053456 File Offset: 0x00051656
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x00053471 File Offset: 0x00051671
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x04001061 RID: 4193
		private readonly string filePath;
	}
}

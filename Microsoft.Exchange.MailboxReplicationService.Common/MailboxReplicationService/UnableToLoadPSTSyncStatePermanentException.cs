using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000359 RID: 857
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToLoadPSTSyncStatePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600267A RID: 9850 RVA: 0x00053311 File Offset: 0x00051511
		public UnableToLoadPSTSyncStatePermanentException(string filePath) : base(MrsStrings.UnableToLoadPSTSyncState(filePath))
		{
			this.filePath = filePath;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00053326 File Offset: 0x00051526
		public UnableToLoadPSTSyncStatePermanentException(string filePath, Exception innerException) : base(MrsStrings.UnableToLoadPSTSyncState(filePath), innerException)
		{
			this.filePath = filePath;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0005333C File Offset: 0x0005153C
		protected UnableToLoadPSTSyncStatePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filePath = (string)info.GetValue("filePath", typeof(string));
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00053366 File Offset: 0x00051566
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filePath", this.filePath);
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x00053381 File Offset: 0x00051581
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x0400105F RID: 4191
		private readonly string filePath;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F7 RID: 1271
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseVolumeInfoInitException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EC8 RID: 11976 RVA: 0x000C40ED File Offset: 0x000C22ED
		public DatabaseVolumeInfoInitException(string databaseCopy, string errMsg) : base(ReplayStrings.DatabaseVolumeInfoInitException(databaseCopy, errMsg))
		{
			this.databaseCopy = databaseCopy;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000C410F File Offset: 0x000C230F
		public DatabaseVolumeInfoInitException(string databaseCopy, string errMsg, Exception innerException) : base(ReplayStrings.DatabaseVolumeInfoInitException(databaseCopy, errMsg), innerException)
		{
			this.databaseCopy = databaseCopy;
			this.errMsg = errMsg;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000C4134 File Offset: 0x000C2334
		protected DatabaseVolumeInfoInitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseCopy = (string)info.GetValue("databaseCopy", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000C4189 File Offset: 0x000C2389
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseCopy", this.databaseCopy);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000C41B5 File Offset: 0x000C23B5
		public string DatabaseCopy
		{
			get
			{
				return this.databaseCopy;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000C41BD File Offset: 0x000C23BD
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001593 RID: 5523
		private readonly string databaseCopy;

		// Token: 0x04001594 RID: 5524
		private readonly string errMsg;
	}
}

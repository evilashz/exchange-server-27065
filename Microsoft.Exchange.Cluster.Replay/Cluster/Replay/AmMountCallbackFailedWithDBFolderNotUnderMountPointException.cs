using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000423 RID: 1059
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmMountCallbackFailedWithDBFolderNotUnderMountPointException : AmServerException
	{
		// Token: 0x06002A2C RID: 10796 RVA: 0x000BB0C2 File Offset: 0x000B92C2
		public AmMountCallbackFailedWithDBFolderNotUnderMountPointException(string dbName, string error) : base(ReplayStrings.AmMountCallbackFailedWithDBFolderNotUnderMountPointException(dbName, error))
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000BB0E4 File Offset: 0x000B92E4
		public AmMountCallbackFailedWithDBFolderNotUnderMountPointException(string dbName, string error, Exception innerException) : base(ReplayStrings.AmMountCallbackFailedWithDBFolderNotUnderMountPointException(dbName, error), innerException)
		{
			this.dbName = dbName;
			this.error = error;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000BB108 File Offset: 0x000B9308
		protected AmMountCallbackFailedWithDBFolderNotUnderMountPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000BB15D File Offset: 0x000B935D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000BB189 File Offset: 0x000B9389
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000BB191 File Offset: 0x000B9391
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001447 RID: 5191
		private readonly string dbName;

		// Token: 0x04001448 RID: 5192
		private readonly string error;
	}
}

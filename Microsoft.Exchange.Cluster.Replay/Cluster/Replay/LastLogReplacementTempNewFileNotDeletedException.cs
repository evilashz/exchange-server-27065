using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004AF RID: 1199
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementTempNewFileNotDeletedException : LastLogReplacementException
	{
		// Token: 0x06002D33 RID: 11571 RVA: 0x000C0F06 File Offset: 0x000BF106
		public LastLogReplacementTempNewFileNotDeletedException(string dbCopy, string tempNewFile, string error) : base(ReplayStrings.LastLogReplacementTempNewFileNotDeletedException(dbCopy, tempNewFile, error))
		{
			this.dbCopy = dbCopy;
			this.tempNewFile = tempNewFile;
			this.error = error;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000C0F30 File Offset: 0x000BF130
		public LastLogReplacementTempNewFileNotDeletedException(string dbCopy, string tempNewFile, string error, Exception innerException) : base(ReplayStrings.LastLogReplacementTempNewFileNotDeletedException(dbCopy, tempNewFile, error), innerException)
		{
			this.dbCopy = dbCopy;
			this.tempNewFile = tempNewFile;
			this.error = error;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000C0F5C File Offset: 0x000BF15C
		protected LastLogReplacementTempNewFileNotDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.tempNewFile = (string)info.GetValue("tempNewFile", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000C0FD1 File Offset: 0x000BF1D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("tempNewFile", this.tempNewFile);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x000C100E File Offset: 0x000BF20E
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000C1016 File Offset: 0x000BF216
		public string TempNewFile
		{
			get
			{
				return this.tempNewFile;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000C101E File Offset: 0x000BF21E
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400151E RID: 5406
		private readonly string dbCopy;

		// Token: 0x0400151F RID: 5407
		private readonly string tempNewFile;

		// Token: 0x04001520 RID: 5408
		private readonly string error;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004AE RID: 1198
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementTempOldFileNotDeletedException : LastLogReplacementException
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x000C0DE6 File Offset: 0x000BEFE6
		public LastLogReplacementTempOldFileNotDeletedException(string dbCopy, string tempOldFile, string error) : base(ReplayStrings.LastLogReplacementTempOldFileNotDeletedException(dbCopy, tempOldFile, error))
		{
			this.dbCopy = dbCopy;
			this.tempOldFile = tempOldFile;
			this.error = error;
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000C0E10 File Offset: 0x000BF010
		public LastLogReplacementTempOldFileNotDeletedException(string dbCopy, string tempOldFile, string error, Exception innerException) : base(ReplayStrings.LastLogReplacementTempOldFileNotDeletedException(dbCopy, tempOldFile, error), innerException)
		{
			this.dbCopy = dbCopy;
			this.tempOldFile = tempOldFile;
			this.error = error;
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000C0E3C File Offset: 0x000BF03C
		protected LastLogReplacementTempOldFileNotDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.tempOldFile = (string)info.GetValue("tempOldFile", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x000C0EB1 File Offset: 0x000BF0B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("tempOldFile", this.tempOldFile);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000C0EEE File Offset: 0x000BF0EE
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x000C0EF6 File Offset: 0x000BF0F6
		public string TempOldFile
		{
			get
			{
				return this.tempOldFile;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x000C0EFE File Offset: 0x000BF0FE
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400151B RID: 5403
		private readonly string dbCopy;

		// Token: 0x0400151C RID: 5404
		private readonly string tempOldFile;

		// Token: 0x0400151D RID: 5405
		private readonly string error;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200041E RID: 1054
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmMountBlockedOnStandaloneDbWithMissingEdbException : AmServerException
	{
		// Token: 0x06002A0B RID: 10763 RVA: 0x000BABB1 File Offset: 0x000B8DB1
		public AmMountBlockedOnStandaloneDbWithMissingEdbException(string dbName, long highestLogGen, string edbFilePath) : base(ReplayStrings.AmMountBlockedOnStandaloneDbWithMissingEdbException(dbName, highestLogGen, edbFilePath))
		{
			this.dbName = dbName;
			this.highestLogGen = highestLogGen;
			this.edbFilePath = edbFilePath;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000BABDB File Offset: 0x000B8DDB
		public AmMountBlockedOnStandaloneDbWithMissingEdbException(string dbName, long highestLogGen, string edbFilePath, Exception innerException) : base(ReplayStrings.AmMountBlockedOnStandaloneDbWithMissingEdbException(dbName, highestLogGen, edbFilePath), innerException)
		{
			this.dbName = dbName;
			this.highestLogGen = highestLogGen;
			this.edbFilePath = edbFilePath;
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000BAC08 File Offset: 0x000B8E08
		protected AmMountBlockedOnStandaloneDbWithMissingEdbException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.highestLogGen = (long)info.GetValue("highestLogGen", typeof(long));
			this.edbFilePath = (string)info.GetValue("edbFilePath", typeof(string));
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000BAC7D File Offset: 0x000B8E7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("highestLogGen", this.highestLogGen);
			info.AddValue("edbFilePath", this.edbFilePath);
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000BACBA File Offset: 0x000B8EBA
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000BACC2 File Offset: 0x000B8EC2
		public long HighestLogGen
		{
			get
			{
				return this.highestLogGen;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000BACCA File Offset: 0x000B8ECA
		public string EdbFilePath
		{
			get
			{
				return this.edbFilePath;
			}
		}

		// Token: 0x0400143A RID: 5178
		private readonly string dbName;

		// Token: 0x0400143B RID: 5179
		private readonly long highestLogGen;

		// Token: 0x0400143C RID: 5180
		private readonly string edbFilePath;
	}
}

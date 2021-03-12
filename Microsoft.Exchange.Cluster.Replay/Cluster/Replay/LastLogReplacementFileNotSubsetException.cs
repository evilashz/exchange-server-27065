using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004AD RID: 1197
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LastLogReplacementFileNotSubsetException : LastLogReplacementException
	{
		// Token: 0x06002D25 RID: 11557 RVA: 0x000C0CC5 File Offset: 0x000BEEC5
		public LastLogReplacementFileNotSubsetException(string dbCopy, string subsetFile, string superSetFile) : base(ReplayStrings.LastLogReplacementFileNotSubsetException(dbCopy, subsetFile, superSetFile))
		{
			this.dbCopy = dbCopy;
			this.subsetFile = subsetFile;
			this.superSetFile = superSetFile;
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000C0CEF File Offset: 0x000BEEEF
		public LastLogReplacementFileNotSubsetException(string dbCopy, string subsetFile, string superSetFile, Exception innerException) : base(ReplayStrings.LastLogReplacementFileNotSubsetException(dbCopy, subsetFile, superSetFile), innerException)
		{
			this.dbCopy = dbCopy;
			this.subsetFile = subsetFile;
			this.superSetFile = superSetFile;
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000C0D1C File Offset: 0x000BEF1C
		protected LastLogReplacementFileNotSubsetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
			this.subsetFile = (string)info.GetValue("subsetFile", typeof(string));
			this.superSetFile = (string)info.GetValue("superSetFile", typeof(string));
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000C0D91 File Offset: 0x000BEF91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
			info.AddValue("subsetFile", this.subsetFile);
			info.AddValue("superSetFile", this.superSetFile);
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002D29 RID: 11561 RVA: 0x000C0DCE File Offset: 0x000BEFCE
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000C0DD6 File Offset: 0x000BEFD6
		public string SubsetFile
		{
			get
			{
				return this.subsetFile;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x000C0DDE File Offset: 0x000BEFDE
		public string SuperSetFile
		{
			get
			{
				return this.superSetFile;
			}
		}

		// Token: 0x04001518 RID: 5400
		private readonly string dbCopy;

		// Token: 0x04001519 RID: 5401
		private readonly string subsetFile;

		// Token: 0x0400151A RID: 5402
		private readonly string superSetFile;
	}
}

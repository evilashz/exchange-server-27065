using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B6 RID: 1206
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogCopierFailsBecauseLogGapException : TransientException
	{
		// Token: 0x06002D5B RID: 11611 RVA: 0x000C1402 File Offset: 0x000BF602
		public LogCopierFailsBecauseLogGapException(string srcServer, string missingFileName) : base(ReplayStrings.LogCopierFailsBecauseLogGap(srcServer, missingFileName))
		{
			this.srcServer = srcServer;
			this.missingFileName = missingFileName;
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000C141F File Offset: 0x000BF61F
		public LogCopierFailsBecauseLogGapException(string srcServer, string missingFileName, Exception innerException) : base(ReplayStrings.LogCopierFailsBecauseLogGap(srcServer, missingFileName), innerException)
		{
			this.srcServer = srcServer;
			this.missingFileName = missingFileName;
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000C1440 File Offset: 0x000BF640
		protected LogCopierFailsBecauseLogGapException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcServer = (string)info.GetValue("srcServer", typeof(string));
			this.missingFileName = (string)info.GetValue("missingFileName", typeof(string));
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000C1495 File Offset: 0x000BF695
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcServer", this.srcServer);
			info.AddValue("missingFileName", this.missingFileName);
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000C14C1 File Offset: 0x000BF6C1
		public string SrcServer
		{
			get
			{
				return this.srcServer;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000C14C9 File Offset: 0x000BF6C9
		public string MissingFileName
		{
			get
			{
				return this.missingFileName;
			}
		}

		// Token: 0x0400152A RID: 5418
		private readonly string srcServer;

		// Token: 0x0400152B RID: 5419
		private readonly string missingFileName;
	}
}

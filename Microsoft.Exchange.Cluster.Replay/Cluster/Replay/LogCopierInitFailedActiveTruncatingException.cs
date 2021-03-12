using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004B5 RID: 1205
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogCopierInitFailedActiveTruncatingException : TransientException
	{
		// Token: 0x06002D54 RID: 11604 RVA: 0x000C12E9 File Offset: 0x000BF4E9
		public LogCopierInitFailedActiveTruncatingException(string srcServer, long startingLogGen, long srcLowestGen) : base(ReplayStrings.LogCopierInitFailedActiveTruncatingException(srcServer, startingLogGen, srcLowestGen))
		{
			this.srcServer = srcServer;
			this.startingLogGen = startingLogGen;
			this.srcLowestGen = srcLowestGen;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000C130E File Offset: 0x000BF50E
		public LogCopierInitFailedActiveTruncatingException(string srcServer, long startingLogGen, long srcLowestGen, Exception innerException) : base(ReplayStrings.LogCopierInitFailedActiveTruncatingException(srcServer, startingLogGen, srcLowestGen), innerException)
		{
			this.srcServer = srcServer;
			this.startingLogGen = startingLogGen;
			this.srcLowestGen = srcLowestGen;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000C1338 File Offset: 0x000BF538
		protected LogCopierInitFailedActiveTruncatingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcServer = (string)info.GetValue("srcServer", typeof(string));
			this.startingLogGen = (long)info.GetValue("startingLogGen", typeof(long));
			this.srcLowestGen = (long)info.GetValue("srcLowestGen", typeof(long));
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000C13AD File Offset: 0x000BF5AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcServer", this.srcServer);
			info.AddValue("startingLogGen", this.startingLogGen);
			info.AddValue("srcLowestGen", this.srcLowestGen);
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x000C13EA File Offset: 0x000BF5EA
		public string SrcServer
		{
			get
			{
				return this.srcServer;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000C13F2 File Offset: 0x000BF5F2
		public long StartingLogGen
		{
			get
			{
				return this.startingLogGen;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000C13FA File Offset: 0x000BF5FA
		public long SrcLowestGen
		{
			get
			{
				return this.srcLowestGen;
			}
		}

		// Token: 0x04001527 RID: 5415
		private readonly string srcServer;

		// Token: 0x04001528 RID: 5416
		private readonly long startingLogGen;

		// Token: 0x04001529 RID: 5417
		private readonly long srcLowestGen;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200044E RID: 1102
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CiSeederGenericException : SeedInProgressException
	{
		// Token: 0x06002B19 RID: 11033 RVA: 0x000BCD3F File Offset: 0x000BAF3F
		public CiSeederGenericException(string sourceServer, string destServer, string specificError) : base(ReplayStrings.CiSeederGenericException(sourceServer, destServer, specificError))
		{
			this.sourceServer = sourceServer;
			this.destServer = destServer;
			this.specificError = specificError;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000BCD69 File Offset: 0x000BAF69
		public CiSeederGenericException(string sourceServer, string destServer, string specificError, Exception innerException) : base(ReplayStrings.CiSeederGenericException(sourceServer, destServer, specificError), innerException)
		{
			this.sourceServer = sourceServer;
			this.destServer = destServer;
			this.specificError = specificError;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000BCD98 File Offset: 0x000BAF98
		protected CiSeederGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
			this.destServer = (string)info.GetValue("destServer", typeof(string));
			this.specificError = (string)info.GetValue("specificError", typeof(string));
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000BCE0D File Offset: 0x000BB00D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceServer", this.sourceServer);
			info.AddValue("destServer", this.destServer);
			info.AddValue("specificError", this.specificError);
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x000BCE4A File Offset: 0x000BB04A
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000BCE52 File Offset: 0x000BB052
		public string DestServer
		{
			get
			{
				return this.destServer;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000BCE5A File Offset: 0x000BB05A
		public string SpecificError
		{
			get
			{
				return this.specificError;
			}
		}

		// Token: 0x04001488 RID: 5256
		private readonly string sourceServer;

		// Token: 0x04001489 RID: 5257
		private readonly string destServer;

		// Token: 0x0400148A RID: 5258
		private readonly string specificError;
	}
}

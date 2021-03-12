using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003ED RID: 1005
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederRpcUnsupportedException : TaskServerException
	{
		// Token: 0x06002911 RID: 10513 RVA: 0x000B90BF File Offset: 0x000B72BF
		public SeederRpcUnsupportedException(string serverName, string serverVersion, string supportedVersion) : base(ReplayStrings.SeederRpcUnsupportedException(serverName, serverVersion, supportedVersion))
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000B90E9 File Offset: 0x000B72E9
		public SeederRpcUnsupportedException(string serverName, string serverVersion, string supportedVersion, Exception innerException) : base(ReplayStrings.SeederRpcUnsupportedException(serverName, serverVersion, supportedVersion), innerException)
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.supportedVersion = supportedVersion;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000B9118 File Offset: 0x000B7318
		protected SeederRpcUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.supportedVersion = (string)info.GetValue("supportedVersion", typeof(string));
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000B918D File Offset: 0x000B738D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("supportedVersion", this.supportedVersion);
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x000B91CA File Offset: 0x000B73CA
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x000B91D2 File Offset: 0x000B73D2
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x000B91DA File Offset: 0x000B73DA
		public string SupportedVersion
		{
			get
			{
				return this.supportedVersion;
			}
		}

		// Token: 0x04001404 RID: 5124
		private readonly string serverName;

		// Token: 0x04001405 RID: 5125
		private readonly string serverVersion;

		// Token: 0x04001406 RID: 5126
		private readonly string supportedVersion;
	}
}

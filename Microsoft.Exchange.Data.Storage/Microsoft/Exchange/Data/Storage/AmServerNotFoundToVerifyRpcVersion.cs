using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E7 RID: 231
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServerNotFoundToVerifyRpcVersion : AmServerTransientException
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x00068AAD File Offset: 0x00066CAD
		public AmServerNotFoundToVerifyRpcVersion(string serverName) : base(ServerStrings.AmServerNotFoundToVerifyRpcVersion(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00068AC7 File Offset: 0x00066CC7
		public AmServerNotFoundToVerifyRpcVersion(string serverName, Exception innerException) : base(ServerStrings.AmServerNotFoundToVerifyRpcVersion(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00068AE2 File Offset: 0x00066CE2
		protected AmServerNotFoundToVerifyRpcVersion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00068B0C File Offset: 0x00066D0C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00068B27 File Offset: 0x00066D27
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000979 RID: 2425
		private readonly string serverName;
	}
}

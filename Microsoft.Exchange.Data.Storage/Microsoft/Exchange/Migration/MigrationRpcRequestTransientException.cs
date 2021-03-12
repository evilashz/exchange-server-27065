using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020001A3 RID: 419
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationRpcRequestTransientException : MigrationTransientException
	{
		// Token: 0x06001783 RID: 6019 RVA: 0x00070D3D File Offset: 0x0006EF3D
		public MigrationRpcRequestTransientException(string requestType, string serverName) : base(Strings.RpcRequestFailed(requestType, serverName))
		{
			this.requestType = requestType;
			this.serverName = serverName;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00070D5A File Offset: 0x0006EF5A
		public MigrationRpcRequestTransientException(string requestType, string serverName, Exception innerException) : base(Strings.RpcRequestFailed(requestType, serverName), innerException)
		{
			this.requestType = requestType;
			this.serverName = serverName;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00070D78 File Offset: 0x0006EF78
		protected MigrationRpcRequestTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestType = (string)info.GetValue("requestType", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00070DCD File Offset: 0x0006EFCD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestType", this.requestType);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00070DF9 File Offset: 0x0006EFF9
		public string RequestType
		{
			get
			{
				return this.requestType;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x00070E01 File Offset: 0x0006F001
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000B1F RID: 2847
		private readonly string requestType;

		// Token: 0x04000B20 RID: 2848
		private readonly string serverName;
	}
}

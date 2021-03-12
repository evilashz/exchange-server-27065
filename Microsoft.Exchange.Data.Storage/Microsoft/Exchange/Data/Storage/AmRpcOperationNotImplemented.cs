using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E6 RID: 230
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmRpcOperationNotImplemented : AmServerTransientException
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x000689D7 File Offset: 0x00066BD7
		public AmRpcOperationNotImplemented(string operationHint, string serverName) : base(ServerStrings.AmRpcOperationNotImplemented(operationHint, serverName))
		{
			this.operationHint = operationHint;
			this.serverName = serverName;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000689F9 File Offset: 0x00066BF9
		public AmRpcOperationNotImplemented(string operationHint, string serverName, Exception innerException) : base(ServerStrings.AmRpcOperationNotImplemented(operationHint, serverName), innerException)
		{
			this.operationHint = operationHint;
			this.serverName = serverName;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00068A1C File Offset: 0x00066C1C
		protected AmRpcOperationNotImplemented(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationHint = (string)info.GetValue("operationHint", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00068A71 File Offset: 0x00066C71
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationHint", this.operationHint);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00068A9D File Offset: 0x00066C9D
		public string OperationHint
		{
			get
			{
				return this.operationHint;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00068AA5 File Offset: 0x00066CA5
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000977 RID: 2423
		private readonly string operationHint;

		// Token: 0x04000978 RID: 2424
		private readonly string serverName;
	}
}

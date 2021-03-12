using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002DC RID: 732
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedRemoteServerVersionWithOperationPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600240A RID: 9226 RVA: 0x0004F675 File Offset: 0x0004D875
		public UnsupportedRemoteServerVersionWithOperationPermanentException(string remoteServerAddress, string serverVersion, string operationName) : base(MrsStrings.UnsupportedRemoteServerVersionWithOperation(remoteServerAddress, serverVersion, operationName))
		{
			this.remoteServerAddress = remoteServerAddress;
			this.serverVersion = serverVersion;
			this.operationName = operationName;
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0004F69A File Offset: 0x0004D89A
		public UnsupportedRemoteServerVersionWithOperationPermanentException(string remoteServerAddress, string serverVersion, string operationName, Exception innerException) : base(MrsStrings.UnsupportedRemoteServerVersionWithOperation(remoteServerAddress, serverVersion, operationName), innerException)
		{
			this.remoteServerAddress = remoteServerAddress;
			this.serverVersion = serverVersion;
			this.operationName = operationName;
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0004F6C4 File Offset: 0x0004D8C4
		protected UnsupportedRemoteServerVersionWithOperationPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteServerAddress = (string)info.GetValue("remoteServerAddress", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.operationName = (string)info.GetValue("operationName", typeof(string));
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0004F739 File Offset: 0x0004D939
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteServerAddress", this.remoteServerAddress);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("operationName", this.operationName);
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x0004F776 File Offset: 0x0004D976
		public string RemoteServerAddress
		{
			get
			{
				return this.remoteServerAddress;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x0004F77E File Offset: 0x0004D97E
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x0004F786 File Offset: 0x0004D986
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x04000FE3 RID: 4067
		private readonly string remoteServerAddress;

		// Token: 0x04000FE4 RID: 4068
		private readonly string serverVersion;

		// Token: 0x04000FE5 RID: 4069
		private readonly string operationName;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E4 RID: 740
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedClientVersionPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002434 RID: 9268 RVA: 0x0004FAF0 File Offset: 0x0004DCF0
		public UnsupportedClientVersionPermanentException(string clientName, string clientVersion, string operationName) : base(MrsStrings.UnsupportedClientVersionWithOperation(clientName, clientVersion, operationName))
		{
			this.clientName = clientName;
			this.clientVersion = clientVersion;
			this.operationName = operationName;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0004FB15 File Offset: 0x0004DD15
		public UnsupportedClientVersionPermanentException(string clientName, string clientVersion, string operationName, Exception innerException) : base(MrsStrings.UnsupportedClientVersionWithOperation(clientName, clientVersion, operationName), innerException)
		{
			this.clientName = clientName;
			this.clientVersion = clientVersion;
			this.operationName = operationName;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0004FB3C File Offset: 0x0004DD3C
		protected UnsupportedClientVersionPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clientName = (string)info.GetValue("clientName", typeof(string));
			this.clientVersion = (string)info.GetValue("clientVersion", typeof(string));
			this.operationName = (string)info.GetValue("operationName", typeof(string));
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0004FBB1 File Offset: 0x0004DDB1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clientName", this.clientName);
			info.AddValue("clientVersion", this.clientVersion);
			info.AddValue("operationName", this.operationName);
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x0004FBEE File Offset: 0x0004DDEE
		public string ClientName
		{
			get
			{
				return this.clientName;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x0004FBF6 File Offset: 0x0004DDF6
		public string ClientVersion
		{
			get
			{
				return this.clientVersion;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600243A RID: 9274 RVA: 0x0004FBFE File Offset: 0x0004DDFE
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x04000FED RID: 4077
		private readonly string clientName;

		// Token: 0x04000FEE RID: 4078
		private readonly string clientVersion;

		// Token: 0x04000FEF RID: 4079
		private readonly string operationName;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200031E RID: 798
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestTypeNotUnderstoodPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600254D RID: 9549 RVA: 0x0005146A File Offset: 0x0004F66A
		public RequestTypeNotUnderstoodPermanentException(string serverName, string serverVersion, int requestType) : base(MrsStrings.RequestTypeNotUnderstoodOnThisServer(serverName, serverVersion, requestType))
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.requestType = requestType;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x0005148F File Offset: 0x0004F68F
		public RequestTypeNotUnderstoodPermanentException(string serverName, string serverVersion, int requestType, Exception innerException) : base(MrsStrings.RequestTypeNotUnderstoodOnThisServer(serverName, serverVersion, requestType), innerException)
		{
			this.serverName = serverName;
			this.serverVersion = serverVersion;
			this.requestType = requestType;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000514B8 File Offset: 0x0004F6B8
		protected RequestTypeNotUnderstoodPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.requestType = (int)info.GetValue("requestType", typeof(int));
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0005152D File Offset: 0x0004F72D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("requestType", this.requestType);
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0005156A File Offset: 0x0004F76A
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00051572 File Offset: 0x0004F772
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0005157A File Offset: 0x0004F77A
		public int RequestType
		{
			get
			{
				return this.requestType;
			}
		}

		// Token: 0x0400101E RID: 4126
		private readonly string serverName;

		// Token: 0x0400101F RID: 4127
		private readonly string serverVersion;

		// Token: 0x04001020 RID: 4128
		private readonly int requestType;
	}
}

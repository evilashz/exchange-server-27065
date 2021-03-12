using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E9 RID: 233
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveManagerUnknownGenericRpcCommandException : AmServerException
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x00068C52 File Offset: 0x00066E52
		public ActiveManagerUnknownGenericRpcCommandException(int requestedServerVersion, int replyingServerVersion, int commandId) : base(ServerStrings.ActiveManagerUnknownGenericRpcCommand(requestedServerVersion, replyingServerVersion, commandId))
		{
			this.requestedServerVersion = requestedServerVersion;
			this.replyingServerVersion = replyingServerVersion;
			this.commandId = commandId;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00068C7C File Offset: 0x00066E7C
		public ActiveManagerUnknownGenericRpcCommandException(int requestedServerVersion, int replyingServerVersion, int commandId, Exception innerException) : base(ServerStrings.ActiveManagerUnknownGenericRpcCommand(requestedServerVersion, replyingServerVersion, commandId), innerException)
		{
			this.requestedServerVersion = requestedServerVersion;
			this.replyingServerVersion = replyingServerVersion;
			this.commandId = commandId;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00068CA8 File Offset: 0x00066EA8
		protected ActiveManagerUnknownGenericRpcCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestedServerVersion = (int)info.GetValue("requestedServerVersion", typeof(int));
			this.replyingServerVersion = (int)info.GetValue("replyingServerVersion", typeof(int));
			this.commandId = (int)info.GetValue("commandId", typeof(int));
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00068D1D File Offset: 0x00066F1D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestedServerVersion", this.requestedServerVersion);
			info.AddValue("replyingServerVersion", this.replyingServerVersion);
			info.AddValue("commandId", this.commandId);
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00068D5A File Offset: 0x00066F5A
		public int RequestedServerVersion
		{
			get
			{
				return this.requestedServerVersion;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00068D62 File Offset: 0x00066F62
		public int ReplyingServerVersion
		{
			get
			{
				return this.replyingServerVersion;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00068D6A File Offset: 0x00066F6A
		public int CommandId
		{
			get
			{
				return this.commandId;
			}
		}

		// Token: 0x0400097D RID: 2429
		private readonly int requestedServerVersion;

		// Token: 0x0400097E RID: 2430
		private readonly int replyingServerVersion;

		// Token: 0x0400097F RID: 2431
		private readonly int commandId;
	}
}

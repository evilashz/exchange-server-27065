using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000127 RID: 295
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ActiveMonitoringUnknownGenericRpcCommandException : ActiveMonitoringServerException
	{
		// Token: 0x06001450 RID: 5200 RVA: 0x0006A8E1 File Offset: 0x00068AE1
		public ActiveMonitoringUnknownGenericRpcCommandException(int requestedServerVersion, int replyingServerVersion, int commandId) : base(ServerStrings.ActiveMonitoringUnknownGenericRpcCommand(requestedServerVersion, replyingServerVersion, commandId))
		{
			this.requestedServerVersion = requestedServerVersion;
			this.replyingServerVersion = replyingServerVersion;
			this.commandId = commandId;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0006A90B File Offset: 0x00068B0B
		public ActiveMonitoringUnknownGenericRpcCommandException(int requestedServerVersion, int replyingServerVersion, int commandId, Exception innerException) : base(ServerStrings.ActiveMonitoringUnknownGenericRpcCommand(requestedServerVersion, replyingServerVersion, commandId), innerException)
		{
			this.requestedServerVersion = requestedServerVersion;
			this.replyingServerVersion = replyingServerVersion;
			this.commandId = commandId;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0006A938 File Offset: 0x00068B38
		protected ActiveMonitoringUnknownGenericRpcCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestedServerVersion = (int)info.GetValue("requestedServerVersion", typeof(int));
			this.replyingServerVersion = (int)info.GetValue("replyingServerVersion", typeof(int));
			this.commandId = (int)info.GetValue("commandId", typeof(int));
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0006A9AD File Offset: 0x00068BAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestedServerVersion", this.requestedServerVersion);
			info.AddValue("replyingServerVersion", this.replyingServerVersion);
			info.AddValue("commandId", this.commandId);
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0006A9EA File Offset: 0x00068BEA
		public int RequestedServerVersion
		{
			get
			{
				return this.requestedServerVersion;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0006A9F2 File Offset: 0x00068BF2
		public int ReplyingServerVersion
		{
			get
			{
				return this.replyingServerVersion;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0006A9FA File Offset: 0x00068BFA
		public int CommandId
		{
			get
			{
				return this.commandId;
			}
		}

		// Token: 0x040009B3 RID: 2483
		private readonly int requestedServerVersion;

		// Token: 0x040009B4 RID: 2484
		private readonly int replyingServerVersion;

		// Token: 0x040009B5 RID: 2485
		private readonly int commandId;
	}
}

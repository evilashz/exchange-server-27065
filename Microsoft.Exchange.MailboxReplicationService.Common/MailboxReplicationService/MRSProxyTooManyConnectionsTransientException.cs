using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E5 RID: 741
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MRSProxyTooManyConnectionsTransientException : MRSProxyConnectionLimitReachedTransientException
	{
		// Token: 0x0600243B RID: 9275 RVA: 0x0004FC06 File Offset: 0x0004DE06
		public MRSProxyTooManyConnectionsTransientException(int activeConnections, int connectionLimit) : base(MrsStrings.MRSProxyConnectionLimitReachedError(activeConnections, connectionLimit))
		{
			this.activeConnections = activeConnections;
			this.connectionLimit = connectionLimit;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0004FC23 File Offset: 0x0004DE23
		public MRSProxyTooManyConnectionsTransientException(int activeConnections, int connectionLimit, Exception innerException) : base(MrsStrings.MRSProxyConnectionLimitReachedError(activeConnections, connectionLimit), innerException)
		{
			this.activeConnections = activeConnections;
			this.connectionLimit = connectionLimit;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0004FC44 File Offset: 0x0004DE44
		protected MRSProxyTooManyConnectionsTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.activeConnections = (int)info.GetValue("activeConnections", typeof(int));
			this.connectionLimit = (int)info.GetValue("connectionLimit", typeof(int));
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0004FC99 File Offset: 0x0004DE99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("activeConnections", this.activeConnections);
			info.AddValue("connectionLimit", this.connectionLimit);
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x0004FCC5 File Offset: 0x0004DEC5
		public int ActiveConnections
		{
			get
			{
				return this.activeConnections;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x0004FCCD File Offset: 0x0004DECD
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
		}

		// Token: 0x04000FF0 RID: 4080
		private readonly int activeConnections;

		// Token: 0x04000FF1 RID: 4081
		private readonly int connectionLimit;
	}
}

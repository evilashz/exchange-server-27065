using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RedundantAccountSubscriptionException : LocalizedException
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00004CF1 File Offset: 0x00002EF1
		public RedundantAccountSubscriptionException(string username, string server) : base(Strings.RedundantAccountSubscription(username, server))
		{
			this.username = username;
			this.server = server;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004D0E File Offset: 0x00002F0E
		public RedundantAccountSubscriptionException(string username, string server, Exception innerException) : base(Strings.RedundantAccountSubscription(username, server), innerException)
		{
			this.username = username;
			this.server = server;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004D2C File Offset: 0x00002F2C
		protected RedundantAccountSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.username = (string)info.GetValue("username", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004D81 File Offset: 0x00002F81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("username", this.username);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004DAD File Offset: 0x00002FAD
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004DB5 File Offset: 0x00002FB5
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040000D4 RID: 212
		private readonly string username;

		// Token: 0x040000D5 RID: 213
		private readonly string server;
	}
}

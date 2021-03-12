using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D1 RID: 209
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServerNotFoundException : AmServerException
	{
		// Token: 0x06001293 RID: 4755 RVA: 0x00067E55 File Offset: 0x00066055
		public AmServerNotFoundException(string server) : base(ServerStrings.AmServerNotFoundException(server))
		{
			this.server = server;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00067E6F File Offset: 0x0006606F
		public AmServerNotFoundException(string server, Exception innerException) : base(ServerStrings.AmServerNotFoundException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00067E8A File Offset: 0x0006608A
		protected AmServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00067EB4 File Offset: 0x000660B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00067ECF File Offset: 0x000660CF
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04000961 RID: 2401
		private readonly string server;
	}
}

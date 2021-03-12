using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001038 RID: 4152
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerIsNotCasServerException : LocalizedException
	{
		// Token: 0x0600AFD0 RID: 45008 RVA: 0x00294EBD File Offset: 0x002930BD
		public ServerIsNotCasServerException(string server) : base(Strings.ErrorServerIsNotCasServer(server))
		{
			this.server = server;
		}

		// Token: 0x0600AFD1 RID: 45009 RVA: 0x00294ED2 File Offset: 0x002930D2
		public ServerIsNotCasServerException(string server, Exception innerException) : base(Strings.ErrorServerIsNotCasServer(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600AFD2 RID: 45010 RVA: 0x00294EE8 File Offset: 0x002930E8
		protected ServerIsNotCasServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600AFD3 RID: 45011 RVA: 0x00294F12 File Offset: 0x00293112
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700380D RID: 14349
		// (get) Token: 0x0600AFD4 RID: 45012 RVA: 0x00294F2D File Offset: 0x0029312D
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04006173 RID: 24947
		private readonly string server;
	}
}

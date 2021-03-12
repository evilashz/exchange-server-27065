using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001063 RID: 4195
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerMustBeInDagException : LocalizedException
	{
		// Token: 0x0600B0C2 RID: 45250 RVA: 0x00296BB4 File Offset: 0x00294DB4
		public ServerMustBeInDagException(string serverName) : base(Strings.ServerMustBeInDagException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B0C3 RID: 45251 RVA: 0x00296BC9 File Offset: 0x00294DC9
		public ServerMustBeInDagException(string serverName, Exception innerException) : base(Strings.ServerMustBeInDagException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B0C4 RID: 45252 RVA: 0x00296BDF File Offset: 0x00294DDF
		protected ServerMustBeInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B0C5 RID: 45253 RVA: 0x00296C09 File Offset: 0x00294E09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003853 RID: 14419
		// (get) Token: 0x0600B0C6 RID: 45254 RVA: 0x00296C24 File Offset: 0x00294E24
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040061B9 RID: 25017
		private readonly string serverName;
	}
}

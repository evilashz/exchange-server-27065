using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001074 RID: 4212
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerNotInDagException : LocalizedException
	{
		// Token: 0x0600B127 RID: 45351 RVA: 0x002978CD File Offset: 0x00295ACD
		public ServerNotInDagException(string serverName) : base(Strings.ServerNotInDagError(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B128 RID: 45352 RVA: 0x002978E2 File Offset: 0x00295AE2
		public ServerNotInDagException(string serverName, Exception innerException) : base(Strings.ServerNotInDagError(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B129 RID: 45353 RVA: 0x002978F8 File Offset: 0x00295AF8
		protected ServerNotInDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B12A RID: 45354 RVA: 0x00297922 File Offset: 0x00295B22
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003874 RID: 14452
		// (get) Token: 0x0600B12B RID: 45355 RVA: 0x0029793D File Offset: 0x00295B3D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040061DA RID: 25050
		private readonly string serverName;
	}
}

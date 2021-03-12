using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E8 RID: 232
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindServerObject : TransientException
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x0001CBD9 File Offset: 0x0001ADD9
		public CouldNotFindServerObject(string serverName) : base(Strings.CouldNotFindServerObject(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001CBEE File Offset: 0x0001ADEE
		public CouldNotFindServerObject(string serverName, Exception innerException) : base(Strings.CouldNotFindServerObject(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001CC04 File Offset: 0x0001AE04
		protected CouldNotFindServerObject(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001CC2E File Offset: 0x0001AE2E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001CC49 File Offset: 0x0001AE49
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000744 RID: 1860
		private readonly string serverName;
	}
}

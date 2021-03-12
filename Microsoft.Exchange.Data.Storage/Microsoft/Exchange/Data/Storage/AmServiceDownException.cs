using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D2 RID: 210
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmServiceDownException : AmServerException
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x00067ED7 File Offset: 0x000660D7
		public AmServiceDownException(string serverName) : base(ServerStrings.AmServiceDownException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00067EF1 File Offset: 0x000660F1
		public AmServiceDownException(string serverName, Exception innerException) : base(ServerStrings.AmServiceDownException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00067F0C File Offset: 0x0006610C
		protected AmServiceDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00067F36 File Offset: 0x00066136
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00067F51 File Offset: 0x00066151
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000962 RID: 2402
		private readonly string serverName;
	}
}

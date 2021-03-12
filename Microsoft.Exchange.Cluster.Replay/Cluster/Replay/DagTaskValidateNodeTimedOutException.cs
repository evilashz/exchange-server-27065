using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003DD RID: 989
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskValidateNodeTimedOutException : DagTaskServerException
	{
		// Token: 0x060028BF RID: 10431 RVA: 0x000B87E8 File Offset: 0x000B69E8
		public DagTaskValidateNodeTimedOutException(string serverName) : base(ReplayStrings.DagTaskValidateNodeTimedOutException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000B8802 File Offset: 0x000B6A02
		public DagTaskValidateNodeTimedOutException(string serverName, Exception innerException) : base(ReplayStrings.DagTaskValidateNodeTimedOutException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x000B881D File Offset: 0x000B6A1D
		protected DagTaskValidateNodeTimedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000B8847 File Offset: 0x000B6A47
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x000B8862 File Offset: 0x000B6A62
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040013F2 RID: 5106
		private readonly string serverName;
	}
}

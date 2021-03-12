using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000526 RID: 1318
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmClusterFileNotFoundException : AmClusterException
	{
		// Token: 0x06002FDA RID: 12250 RVA: 0x000C656F File Offset: 0x000C476F
		public AmClusterFileNotFoundException(string nodeName) : base(ReplayStrings.AmClusterFileNotFoundException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000C6589 File Offset: 0x000C4789
		public AmClusterFileNotFoundException(string nodeName, Exception innerException) : base(ReplayStrings.AmClusterFileNotFoundException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000C65A4 File Offset: 0x000C47A4
		protected AmClusterFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000C65CE File Offset: 0x000C47CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000C65E9 File Offset: 0x000C47E9
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x040015E9 RID: 5609
		private readonly string nodeName;
	}
}

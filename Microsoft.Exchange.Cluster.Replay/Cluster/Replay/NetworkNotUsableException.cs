using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003BA RID: 954
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkNotUsableException : TransientException
	{
		// Token: 0x060027F7 RID: 10231 RVA: 0x000B6E5D File Offset: 0x000B505D
		public NetworkNotUsableException(string netName, string nodeName, string reason) : base(ReplayStrings.NetworkNotUsable(netName, nodeName, reason))
		{
			this.netName = netName;
			this.nodeName = nodeName;
			this.reason = reason;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000B6E82 File Offset: 0x000B5082
		public NetworkNotUsableException(string netName, string nodeName, string reason, Exception innerException) : base(ReplayStrings.NetworkNotUsable(netName, nodeName, reason), innerException)
		{
			this.netName = netName;
			this.nodeName = nodeName;
			this.reason = reason;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000B6EAC File Offset: 0x000B50AC
		protected NetworkNotUsableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.netName = (string)info.GetValue("netName", typeof(string));
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000B6F21 File Offset: 0x000B5121
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("netName", this.netName);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x000B6F5E File Offset: 0x000B515E
		public string NetName
		{
			get
			{
				return this.netName;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000B6F66 File Offset: 0x000B5166
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x000B6F6E File Offset: 0x000B516E
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040013B6 RID: 5046
		private readonly string netName;

		// Token: 0x040013B7 RID: 5047
		private readonly string nodeName;

		// Token: 0x040013B8 RID: 5048
		private readonly string reason;
	}
}

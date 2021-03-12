using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B9 RID: 953
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkNameException : TransientException
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x000B6DE5 File Offset: 0x000B4FE5
		public NetworkNameException(string netName) : base(ReplayStrings.NetworkNameNotFound(netName))
		{
			this.netName = netName;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000B6DFA File Offset: 0x000B4FFA
		public NetworkNameException(string netName, Exception innerException) : base(ReplayStrings.NetworkNameNotFound(netName), innerException)
		{
			this.netName = netName;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000B6E10 File Offset: 0x000B5010
		protected NetworkNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.netName = (string)info.GetValue("netName", typeof(string));
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000B6E3A File Offset: 0x000B503A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("netName", this.netName);
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000B6E55 File Offset: 0x000B5055
		public string NetName
		{
			get
			{
				return this.netName;
			}
		}

		// Token: 0x040013B5 RID: 5045
		private readonly string netName;
	}
}

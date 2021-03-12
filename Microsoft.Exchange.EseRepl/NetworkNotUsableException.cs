using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000050 RID: 80
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkNotUsableException : TransientException
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00009415 File Offset: 0x00007615
		public NetworkNotUsableException(string netName, string nodeName, string reason) : base(Strings.NetworkNotUsable(netName, nodeName, reason))
		{
			this.netName = netName;
			this.nodeName = nodeName;
			this.reason = reason;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000943A File Offset: 0x0000763A
		public NetworkNotUsableException(string netName, string nodeName, string reason, Exception innerException) : base(Strings.NetworkNotUsable(netName, nodeName, reason), innerException)
		{
			this.netName = netName;
			this.nodeName = nodeName;
			this.reason = reason;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009464 File Offset: 0x00007664
		protected NetworkNotUsableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.netName = (string)info.GetValue("netName", typeof(string));
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000094D9 File Offset: 0x000076D9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("netName", this.netName);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009516 File Offset: 0x00007716
		public string NetName
		{
			get
			{
				return this.netName;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000951E File Offset: 0x0000771E
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009526 File Offset: 0x00007726
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000167 RID: 359
		private readonly string netName;

		// Token: 0x04000168 RID: 360
		private readonly string nodeName;

		// Token: 0x04000169 RID: 361
		private readonly string reason;
	}
}

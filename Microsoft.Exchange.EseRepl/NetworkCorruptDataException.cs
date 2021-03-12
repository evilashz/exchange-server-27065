using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200004D RID: 77
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCorruptDataException : NetworkTransportException
	{
		// Token: 0x06000273 RID: 627 RVA: 0x000092E2 File Offset: 0x000074E2
		public NetworkCorruptDataException(string srcNode) : base(Strings.NetworkCorruptData(srcNode))
		{
			this.srcNode = srcNode;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000092FC File Offset: 0x000074FC
		public NetworkCorruptDataException(string srcNode, Exception innerException) : base(Strings.NetworkCorruptData(srcNode), innerException)
		{
			this.srcNode = srcNode;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009317 File Offset: 0x00007517
		protected NetworkCorruptDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcNode = (string)info.GetValue("srcNode", typeof(string));
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009341 File Offset: 0x00007541
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcNode", this.srcNode);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000935C File Offset: 0x0000755C
		public string SrcNode
		{
			get
			{
				return this.srcNode;
			}
		}

		// Token: 0x04000165 RID: 357
		private readonly string srcNode;
	}
}

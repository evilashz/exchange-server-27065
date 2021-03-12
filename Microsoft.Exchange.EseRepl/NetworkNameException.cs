using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200004F RID: 79
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkNameException : TransientException
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000939D File Offset: 0x0000759D
		public NetworkNameException(string netName) : base(Strings.NetworkNameNotFound(netName))
		{
			this.netName = netName;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000093B2 File Offset: 0x000075B2
		public NetworkNameException(string netName, Exception innerException) : base(Strings.NetworkNameNotFound(netName), innerException)
		{
			this.netName = netName;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000093C8 File Offset: 0x000075C8
		protected NetworkNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.netName = (string)info.GetValue("netName", typeof(string));
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000093F2 File Offset: 0x000075F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("netName", this.netName);
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000940D File Offset: 0x0000760D
		public string NetName
		{
			get
			{
				return this.netName;
			}
		}

		// Token: 0x04000166 RID: 358
		private readonly string netName;
	}
}

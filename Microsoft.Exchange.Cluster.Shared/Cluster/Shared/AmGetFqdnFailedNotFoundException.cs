using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E6 RID: 230
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmGetFqdnFailedNotFoundException : AmServerNameResolveFqdnException
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x0001CA7F File Offset: 0x0001AC7F
		public AmGetFqdnFailedNotFoundException(string nodeName) : base(Strings.AmGetFqdnFailedNotFound(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001CA99 File Offset: 0x0001AC99
		public AmGetFqdnFailedNotFoundException(string nodeName, Exception innerException) : base(Strings.AmGetFqdnFailedNotFound(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001CAB4 File Offset: 0x0001ACB4
		protected AmGetFqdnFailedNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001CADE File Offset: 0x0001ACDE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001CAF9 File Offset: 0x0001ACF9
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04000741 RID: 1857
		private readonly string nodeName;
	}
}

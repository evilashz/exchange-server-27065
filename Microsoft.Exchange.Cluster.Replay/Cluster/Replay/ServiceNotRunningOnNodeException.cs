using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000446 RID: 1094
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceNotRunningOnNodeException : SeedPrepareException
	{
		// Token: 0x06002AEE RID: 10990 RVA: 0x000BC816 File Offset: 0x000BAA16
		public ServiceNotRunningOnNodeException(string serviceName, string nodeName) : base(ReplayStrings.ServiceNotRunningOnNodeException(serviceName, nodeName))
		{
			this.serviceName = serviceName;
			this.nodeName = nodeName;
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000BC838 File Offset: 0x000BAA38
		public ServiceNotRunningOnNodeException(string serviceName, string nodeName, Exception innerException) : base(ReplayStrings.ServiceNotRunningOnNodeException(serviceName, nodeName), innerException)
		{
			this.serviceName = serviceName;
			this.nodeName = nodeName;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000BC85C File Offset: 0x000BAA5C
		protected ServiceNotRunningOnNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000BC8B1 File Offset: 0x000BAAB1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000BC8DD File Offset: 0x000BAADD
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000BC8E5 File Offset: 0x000BAAE5
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x0400147D RID: 5245
		private readonly string serviceName;

		// Token: 0x0400147E RID: 5246
		private readonly string nodeName;
	}
}

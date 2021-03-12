using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003AF RID: 943
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCommunicationException : NetworkTransportException
	{
		// Token: 0x060027BD RID: 10173 RVA: 0x000B67B1 File Offset: 0x000B49B1
		public NetworkCommunicationException(string remoteNodeName, string errorText) : base(ReplayStrings.NetworkCommunicationError(remoteNodeName, errorText))
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000B67D3 File Offset: 0x000B49D3
		public NetworkCommunicationException(string remoteNodeName, string errorText, Exception innerException) : base(ReplayStrings.NetworkCommunicationError(remoteNodeName, errorText), innerException)
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000B67F8 File Offset: 0x000B49F8
		protected NetworkCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteNodeName = (string)info.GetValue("remoteNodeName", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000B684D File Offset: 0x000B4A4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteNodeName", this.remoteNodeName);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000B6879 File Offset: 0x000B4A79
		public string RemoteNodeName
		{
			get
			{
				return this.remoteNodeName;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000B6881 File Offset: 0x000B4A81
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x040013A8 RID: 5032
		private readonly string remoteNodeName;

		// Token: 0x040013A9 RID: 5033
		private readonly string errorText;
	}
}

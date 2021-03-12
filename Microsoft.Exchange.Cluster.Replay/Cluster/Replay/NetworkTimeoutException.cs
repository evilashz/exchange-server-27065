using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B0 RID: 944
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkTimeoutException : NetworkTransportException
	{
		// Token: 0x060027C3 RID: 10179 RVA: 0x000B6889 File Offset: 0x000B4A89
		public NetworkTimeoutException(string remoteNodeName, string errorText) : base(ReplayStrings.NetworkTimeoutError(remoteNodeName, errorText))
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000B68AB File Offset: 0x000B4AAB
		public NetworkTimeoutException(string remoteNodeName, string errorText, Exception innerException) : base(ReplayStrings.NetworkTimeoutError(remoteNodeName, errorText), innerException)
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000B68D0 File Offset: 0x000B4AD0
		protected NetworkTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteNodeName = (string)info.GetValue("remoteNodeName", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000B6925 File Offset: 0x000B4B25
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteNodeName", this.remoteNodeName);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000B6951 File Offset: 0x000B4B51
		public string RemoteNodeName
		{
			get
			{
				return this.remoteNodeName;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000B6959 File Offset: 0x000B4B59
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x040013AA RID: 5034
		private readonly string remoteNodeName;

		// Token: 0x040013AB RID: 5035
		private readonly string errorText;
	}
}

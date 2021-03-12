using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000046 RID: 70
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkTimeoutException : NetworkTransportException
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008E41 File Offset: 0x00007041
		public NetworkTimeoutException(string remoteNodeName, string errorText) : base(Strings.NetworkTimeoutError(remoteNodeName, errorText))
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008E63 File Offset: 0x00007063
		public NetworkTimeoutException(string remoteNodeName, string errorText, Exception innerException) : base(Strings.NetworkTimeoutError(remoteNodeName, errorText), innerException)
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008E88 File Offset: 0x00007088
		protected NetworkTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteNodeName = (string)info.GetValue("remoteNodeName", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008EDD File Offset: 0x000070DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteNodeName", this.remoteNodeName);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008F09 File Offset: 0x00007109
		public string RemoteNodeName
		{
			get
			{
				return this.remoteNodeName;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00008F11 File Offset: 0x00007111
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x0400015B RID: 347
		private readonly string remoteNodeName;

		// Token: 0x0400015C RID: 348
		private readonly string errorText;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000045 RID: 69
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCommunicationException : NetworkTransportException
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00008D6B File Offset: 0x00006F6B
		public NetworkCommunicationException(string remoteNodeName, string errorText) : base(Strings.NetworkCommunicationError(remoteNodeName, errorText))
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008D8D File Offset: 0x00006F8D
		public NetworkCommunicationException(string remoteNodeName, string errorText, Exception innerException) : base(Strings.NetworkCommunicationError(remoteNodeName, errorText), innerException)
		{
			this.remoteNodeName = remoteNodeName;
			this.errorText = errorText;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008DB0 File Offset: 0x00006FB0
		protected NetworkCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteNodeName = (string)info.GetValue("remoteNodeName", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008E05 File Offset: 0x00007005
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteNodeName", this.remoteNodeName);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00008E31 File Offset: 0x00007031
		public string RemoteNodeName
		{
			get
			{
				return this.remoteNodeName;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00008E39 File Offset: 0x00007039
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x04000159 RID: 345
		private readonly string remoteNodeName;

		// Token: 0x0400015A RID: 346
		private readonly string errorText;
	}
}

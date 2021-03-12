using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200004B RID: 75
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkRemoteExceptionUnknown : NetworkTransportException
	{
		// Token: 0x06000269 RID: 617 RVA: 0x000091D1 File Offset: 0x000073D1
		public NetworkRemoteExceptionUnknown(string nodeName, string messageText) : base(Strings.NetworkRemoteErrorUnknown(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000091F3 File Offset: 0x000073F3
		public NetworkRemoteExceptionUnknown(string nodeName, string messageText, Exception innerException) : base(Strings.NetworkRemoteErrorUnknown(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009218 File Offset: 0x00007418
		protected NetworkRemoteExceptionUnknown(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000926D File Offset: 0x0000746D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009299 File Offset: 0x00007499
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000092A1 File Offset: 0x000074A1
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x04000163 RID: 355
		private readonly string nodeName;

		// Token: 0x04000164 RID: 356
		private readonly string messageText;
	}
}

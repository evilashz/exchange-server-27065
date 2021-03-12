using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000047 RID: 71
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkUnexpectedMessageException : NetworkTransportException
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00008F19 File Offset: 0x00007119
		public NetworkUnexpectedMessageException(string nodeName, string messageText) : base(Strings.NetworkUnexpectedMessage(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008F3B File Offset: 0x0000713B
		public NetworkUnexpectedMessageException(string nodeName, string messageText, Exception innerException) : base(Strings.NetworkUnexpectedMessage(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008F60 File Offset: 0x00007160
		protected NetworkUnexpectedMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008FB5 File Offset: 0x000071B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008FE1 File Offset: 0x000071E1
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00008FE9 File Offset: 0x000071E9
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x0400015D RID: 349
		private readonly string nodeName;

		// Token: 0x0400015E RID: 350
		private readonly string messageText;
	}
}

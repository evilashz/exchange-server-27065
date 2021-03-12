using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B1 RID: 945
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkUnexpectedMessageException : NetworkTransportException
	{
		// Token: 0x060027C9 RID: 10185 RVA: 0x000B6961 File Offset: 0x000B4B61
		public NetworkUnexpectedMessageException(string nodeName, string messageText) : base(ReplayStrings.NetworkUnexpectedMessage(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000B6983 File Offset: 0x000B4B83
		public NetworkUnexpectedMessageException(string nodeName, string messageText, Exception innerException) : base(ReplayStrings.NetworkUnexpectedMessage(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000B69A8 File Offset: 0x000B4BA8
		protected NetworkUnexpectedMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000B69FD File Offset: 0x000B4BFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x000B6A29 File Offset: 0x000B4C29
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x000B6A31 File Offset: 0x000B4C31
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x040013AC RID: 5036
		private readonly string nodeName;

		// Token: 0x040013AD RID: 5037
		private readonly string messageText;
	}
}

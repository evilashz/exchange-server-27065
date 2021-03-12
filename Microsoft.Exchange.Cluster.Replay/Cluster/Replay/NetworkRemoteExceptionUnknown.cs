using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B5 RID: 949
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkRemoteExceptionUnknown : NetworkTransportException
	{
		// Token: 0x060027DF RID: 10207 RVA: 0x000B6C19 File Offset: 0x000B4E19
		public NetworkRemoteExceptionUnknown(string nodeName, string messageText) : base(ReplayStrings.NetworkRemoteErrorUnknown(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000B6C3B File Offset: 0x000B4E3B
		public NetworkRemoteExceptionUnknown(string nodeName, string messageText, Exception innerException) : base(ReplayStrings.NetworkRemoteErrorUnknown(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000B6C60 File Offset: 0x000B4E60
		protected NetworkRemoteExceptionUnknown(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000B6CB5 File Offset: 0x000B4EB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000B6CE1 File Offset: 0x000B4EE1
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x000B6CE9 File Offset: 0x000B4EE9
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x040013B2 RID: 5042
		private readonly string nodeName;

		// Token: 0x040013B3 RID: 5043
		private readonly string messageText;
	}
}

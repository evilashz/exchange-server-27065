using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200004A RID: 74
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkRemoteException : LocalizedException
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00009102 File Offset: 0x00007302
		public NetworkRemoteException(string nodeName, string messageText) : base(Strings.NetworkRemoteError(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000911F File Offset: 0x0000731F
		public NetworkRemoteException(string nodeName, string messageText, Exception innerException) : base(Strings.NetworkRemoteError(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009140 File Offset: 0x00007340
		protected NetworkRemoteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009195 File Offset: 0x00007395
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000091C1 File Offset: 0x000073C1
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000091C9 File Offset: 0x000073C9
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x04000161 RID: 353
		private readonly string nodeName;

		// Token: 0x04000162 RID: 354
		private readonly string messageText;
	}
}

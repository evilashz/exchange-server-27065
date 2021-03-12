using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000048 RID: 72
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkEndOfDataException : NetworkTransportException
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00008FF1 File Offset: 0x000071F1
		public NetworkEndOfDataException(string nodeName, string messageText) : base(Strings.NetworkEndOfData(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009013 File Offset: 0x00007213
		public NetworkEndOfDataException(string nodeName, string messageText, Exception innerException) : base(Strings.NetworkEndOfData(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009038 File Offset: 0x00007238
		protected NetworkEndOfDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000908D File Offset: 0x0000728D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000090B9 File Offset: 0x000072B9
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025E RID: 606 RVA: 0x000090C1 File Offset: 0x000072C1
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x0400015F RID: 351
		private readonly string nodeName;

		// Token: 0x04000160 RID: 352
		private readonly string messageText;
	}
}

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B2 RID: 946
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkEndOfDataException : NetworkTransportException
	{
		// Token: 0x060027CF RID: 10191 RVA: 0x000B6A39 File Offset: 0x000B4C39
		public NetworkEndOfDataException(string nodeName, string messageText) : base(ReplayStrings.NetworkEndOfData(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000B6A5B File Offset: 0x000B4C5B
		public NetworkEndOfDataException(string nodeName, string messageText, Exception innerException) : base(ReplayStrings.NetworkEndOfData(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000B6A80 File Offset: 0x000B4C80
		protected NetworkEndOfDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000B6AD5 File Offset: 0x000B4CD5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x000B6B01 File Offset: 0x000B4D01
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x000B6B09 File Offset: 0x000B4D09
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x040013AE RID: 5038
		private readonly string nodeName;

		// Token: 0x040013AF RID: 5039
		private readonly string messageText;
	}
}

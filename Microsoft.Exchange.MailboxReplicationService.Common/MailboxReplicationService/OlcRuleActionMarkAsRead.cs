using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B5 RID: 181
	[DataContract]
	internal sealed class OlcRuleActionMarkAsRead : OlcRuleActionBase
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000B89F File Offset: 0x00009A9F
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		[DataMember]
		public int ReadStateInt { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public OlcMessageReadState ReadState
		{
			get
			{
				return (OlcMessageReadState)this.ReadStateInt;
			}
			set
			{
				this.ReadStateInt = (int)value;
			}
		}
	}
}

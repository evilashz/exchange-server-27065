using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	[DataContract]
	internal sealed class InboxRuleSettings : ItemPropertiesBase
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000028E9 File Offset: 0x00000AE9
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000028F1 File Offset: 0x00000AF1
		[DataMember]
		public OlcInboxRule[] Rules { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000E RID: 14
	[DataContract]
	internal sealed class UnschematizedSettings : ItemPropertiesBase
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002E17 File Offset: 0x00001017
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002E1F File Offset: 0x0000101F
		[DataMember]
		public NameValuePair[] KvpSettings { get; set; }
	}
}

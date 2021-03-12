using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000207 RID: 519
	[DataContract]
	public class MigrationCsvFile : EncodedFile
	{
		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000786C0 File Offset: 0x000768C0
		// (set) Token: 0x060026B8 RID: 9912 RVA: 0x000786C8 File Offset: 0x000768C8
		[DataMember]
		public string FirstSmtpAddress { get; set; }

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000786D1 File Offset: 0x000768D1
		// (set) Token: 0x060026BA RID: 9914 RVA: 0x000786D9 File Offset: 0x000768D9
		[DataMember]
		public int MailboxCount { get; set; }
	}
}

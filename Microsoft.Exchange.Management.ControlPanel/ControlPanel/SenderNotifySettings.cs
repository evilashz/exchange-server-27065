using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200044B RID: 1099
	[DataContract]
	public class SenderNotifySettings
	{
		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000A7AB5 File Offset: 0x000A5CB5
		// (set) Token: 0x06003638 RID: 13880 RVA: 0x000A7ABD File Offset: 0x000A5CBD
		[DataMember]
		public string NotifySender { get; set; }

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000A7AC6 File Offset: 0x000A5CC6
		// (set) Token: 0x0600363A RID: 13882 RVA: 0x000A7ACE File Offset: 0x000A5CCE
		[DataMember]
		public string RejectMessage { get; set; }
	}
}

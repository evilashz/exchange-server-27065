using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000269 RID: 617
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateMeetNowRequestWrapper
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x0005388D File Offset: 0x00051A8D
		// (set) Token: 0x06001709 RID: 5897 RVA: 0x00053895 File Offset: 0x00051A95
		[DataMember(Name = "sipUri")]
		public string SipUri { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0005389E File Offset: 0x00051A9E
		// (set) Token: 0x0600170B RID: 5899 RVA: 0x000538A6 File Offset: 0x00051AA6
		[DataMember(Name = "subject")]
		public string Subject { get; set; }
	}
}

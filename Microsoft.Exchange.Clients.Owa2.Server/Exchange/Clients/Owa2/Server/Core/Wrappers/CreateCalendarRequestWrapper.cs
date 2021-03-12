using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000266 RID: 614
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateCalendarRequestWrapper
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x000537FE File Offset: 0x000519FE
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00053806 File Offset: 0x00051A06
		[DataMember(Name = "newCalendarName")]
		public string NewCalendarName { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0005380F File Offset: 0x00051A0F
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00053817 File Offset: 0x00051A17
		[DataMember(Name = "parentGroupGuid")]
		public string ParentGroupGuid { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00053820 File Offset: 0x00051A20
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x00053828 File Offset: 0x00051A28
		[DataMember(Name = "emailAddress")]
		public string EmailAddress { get; set; }
	}
}

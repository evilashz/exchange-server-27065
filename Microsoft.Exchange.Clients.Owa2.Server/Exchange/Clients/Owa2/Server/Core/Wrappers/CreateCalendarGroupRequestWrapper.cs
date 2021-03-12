using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000267 RID: 615
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateCalendarGroupRequestWrapper
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00053839 File Offset: 0x00051A39
		// (set) Token: 0x060016FF RID: 5887 RVA: 0x00053841 File Offset: 0x00051A41
		[DataMember(Name = "newGroupName")]
		public string NewGroupName { get; set; }
	}
}

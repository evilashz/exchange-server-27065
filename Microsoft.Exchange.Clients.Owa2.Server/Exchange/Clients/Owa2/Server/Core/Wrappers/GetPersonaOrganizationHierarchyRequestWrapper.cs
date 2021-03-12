using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200028E RID: 654
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaOrganizationHierarchyRequestWrapper
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00053CF6 File Offset: 0x00051EF6
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x00053CFE File Offset: 0x00051EFE
		[DataMember(Name = "galObjectGuid")]
		public string GalObjectGuid { get; set; }
	}
}

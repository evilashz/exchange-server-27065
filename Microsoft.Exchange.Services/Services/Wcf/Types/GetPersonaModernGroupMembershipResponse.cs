using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B4D RID: 2893
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaModernGroupMembershipResponse
	{
		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060051F7 RID: 20983 RVA: 0x0010B1A4 File Offset: 0x001093A4
		// (set) Token: 0x060051F8 RID: 20984 RVA: 0x0010B1AC File Offset: 0x001093AC
		[DataMember(Name = "Groups", IsRequired = true)]
		public Persona[] Groups { get; set; }
	}
}

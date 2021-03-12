using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B4B RID: 2891
	[DataContract(Name = "ModernGroupMembershipRequestMessageDetailsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ModernGroupMembershipRequestMessageDetailsResponse : BaseJsonResponse
	{
		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x0010B0B5 File Offset: 0x001092B5
		// (set) Token: 0x060051EB RID: 20971 RVA: 0x0010B0BD File Offset: 0x001092BD
		[DataMember(Name = "IsOwner", IsRequired = true)]
		public bool IsOwner { get; set; }

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x0010B0C6 File Offset: 0x001092C6
		// (set) Token: 0x060051ED RID: 20973 RVA: 0x0010B0CE File Offset: 0x001092CE
		[DataMember(Name = "GroupPersona", IsRequired = true)]
		public Persona GroupPersona { get; set; }
	}
}

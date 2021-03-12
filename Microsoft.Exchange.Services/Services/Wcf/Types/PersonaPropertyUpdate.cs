using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F9 RID: 2553
	[KnownType(typeof(PersonaPropertyUpdate))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PersonaPropertyUpdate : PropertyUpdate
	{
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x001012C1 File Offset: 0x000FF4C1
		// (set) Token: 0x06004826 RID: 18470 RVA: 0x001012C9 File Offset: 0x000FF4C9
		[DataMember(Name = "OldValue", EmitDefaultValue = false, Order = 0)]
		public string OldValue { get; internal set; }

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x001012D2 File Offset: 0x000FF4D2
		// (set) Token: 0x06004828 RID: 18472 RVA: 0x001012DA File Offset: 0x000FF4DA
		[DataMember(Name = "NewValue", EmitDefaultValue = false, Order = 1)]
		public string NewValue { get; internal set; }

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x001012E3 File Offset: 0x000FF4E3
		// (set) Token: 0x0600482A RID: 18474 RVA: 0x001012EB File Offset: 0x000FF4EB
		[DataMember(Name = "AddMemberToPDL", EmitDefaultValue = false, Order = 2)]
		public EmailAddressWrapper AddMemberToPDL { get; internal set; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x001012F4 File Offset: 0x000FF4F4
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x001012FC File Offset: 0x000FF4FC
		[DataMember(Name = "DeleteMemberFromPDL", EmitDefaultValue = false, Order = 3)]
		public EmailAddressWrapper DeleteMemberFromPDL { get; internal set; }
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009EC RID: 2540
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ModernGroupMember")]
	[Serializable]
	public class ModernGroupMemberType : ItemType
	{
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060047C5 RID: 18373 RVA: 0x001007D1 File Offset: 0x000FE9D1
		// (set) Token: 0x060047C6 RID: 18374 RVA: 0x001007D9 File Offset: 0x000FE9D9
		[DataMember]
		public Persona Persona { get; set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x001007E2 File Offset: 0x000FE9E2
		// (set) Token: 0x060047C8 RID: 18376 RVA: 0x001007EA File Offset: 0x000FE9EA
		[DataMember]
		public bool IsOwner { get; set; }
	}
}

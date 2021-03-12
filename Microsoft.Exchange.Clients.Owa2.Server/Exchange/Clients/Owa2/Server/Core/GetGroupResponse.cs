using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200020A RID: 522
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetGroupResponse
	{
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00048DB5 File Offset: 0x00046FB5
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00048DBD File Offset: 0x00046FBD
		[DataMember]
		public Persona[] Owners { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00048DC6 File Offset: 0x00046FC6
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00048DCE File Offset: 0x00046FCE
		[DataMember]
		public Persona[] Members { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00048DD7 File Offset: 0x00046FD7
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00048DDF File Offset: 0x00046FDF
		[DataMember]
		public int MembersCount { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00048DE8 File Offset: 0x00046FE8
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00048DF0 File Offset: 0x00046FF0
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00048DF9 File Offset: 0x00046FF9
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x00048E01 File Offset: 0x00047001
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00048E0A File Offset: 0x0004700A
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x00048E12 File Offset: 0x00047012
		[DataMember]
		public ItemId PersonaId { get; set; }
	}
}

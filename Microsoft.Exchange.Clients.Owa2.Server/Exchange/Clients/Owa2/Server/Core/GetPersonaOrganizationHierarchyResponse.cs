using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200020D RID: 525
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetPersonaOrganizationHierarchyResponse
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00048E99 File Offset: 0x00047099
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x00048EA1 File Offset: 0x000470A1
		[DataMember]
		public Persona[] ManagementChain { get; set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00048EAA File Offset: 0x000470AA
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00048EB2 File Offset: 0x000470B2
		[DataMember]
		public Persona Manager { get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00048EBB File Offset: 0x000470BB
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x00048EC3 File Offset: 0x000470C3
		[DataMember]
		public Persona[] Peers { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00048ECC File Offset: 0x000470CC
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x00048ED4 File Offset: 0x000470D4
		[DataMember]
		public Persona[] DirectReports { get; set; }
	}
}

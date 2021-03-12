using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200020C RID: 524
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetNotesForPersonaRequest
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00048E4D File Offset: 0x0004704D
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x00048E55 File Offset: 0x00047055
		[DataMember]
		public string PersonaId { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00048E5E File Offset: 0x0004705E
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x00048E66 File Offset: 0x00047066
		[DataMember]
		public int MaxBytesToFetch { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x00048E6F File Offset: 0x0004706F
		// (set) Token: 0x06001457 RID: 5207 RVA: 0x00048E77 File Offset: 0x00047077
		[DataMember]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00048E80 File Offset: 0x00047080
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00048E88 File Offset: 0x00047088
		[DataMember(Name = "ParentFolderId", IsRequired = false, EmitDefaultValue = false)]
		public TargetFolderId ParentFolderId { get; set; }
	}
}

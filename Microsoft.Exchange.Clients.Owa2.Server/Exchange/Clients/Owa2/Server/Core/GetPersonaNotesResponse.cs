using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200020E RID: 526
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetPersonaNotesResponse
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00048EE5 File Offset: 0x000470E5
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x00048EED File Offset: 0x000470ED
		[DataMember]
		public Persona PersonaWithNotes { get; set; }
	}
}

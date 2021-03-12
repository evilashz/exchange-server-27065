using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200020B RID: 523
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetOrganizationHierarchyForPersonaRequest
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00048E23 File Offset: 0x00047023
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00048E2B File Offset: 0x0004702B
		[DataMember]
		public string GalObjectGuid { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00048E34 File Offset: 0x00047034
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x00048E3C File Offset: 0x0004703C
		[DataMember]
		public EmailAddressWrapper EmailAddress { get; set; }
	}
}

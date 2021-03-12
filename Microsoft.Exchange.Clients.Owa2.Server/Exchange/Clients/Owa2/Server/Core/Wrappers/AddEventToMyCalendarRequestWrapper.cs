using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200025D RID: 605
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddEventToMyCalendarRequestWrapper
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x000536C8 File Offset: 0x000518C8
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x000536D0 File Offset: 0x000518D0
		[DataMember(Name = "request")]
		public AddEventToMyCalendarRequest Request { get; set; }
	}
}

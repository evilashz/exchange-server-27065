using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x02000272 RID: 626
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteCalendarRequestWrapper
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x000539B2 File Offset: 0x00051BB2
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x000539BA File Offset: 0x00051BBA
		[DataMember(Name = "itemId")]
		public ItemId ItemId { get; set; }
	}
}

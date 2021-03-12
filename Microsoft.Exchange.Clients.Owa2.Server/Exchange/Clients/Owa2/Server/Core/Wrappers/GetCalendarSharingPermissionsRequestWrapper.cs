using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200027C RID: 636
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarSharingPermissionsRequestWrapper
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00053ABD File Offset: 0x00051CBD
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x00053AC5 File Offset: 0x00051CC5
		[DataMember(Name = "request")]
		public GetCalendarSharingPermissionsRequest Request { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.Wrappers
{
	// Token: 0x0200027B RID: 635
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarFolderConfigurationRequestWrapper
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00053AA4 File Offset: 0x00051CA4
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00053AAC File Offset: 0x00051CAC
		[DataMember(Name = "request")]
		public GetCalendarFolderConfigurationRequest Request { get; set; }
	}
}

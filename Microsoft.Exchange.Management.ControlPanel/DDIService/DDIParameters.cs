using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000137 RID: 311
	[DataContract]
	public class DDIParameters
	{
		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00063F7B File Offset: 0x0006217B
		// (set) Token: 0x060020EF RID: 8431 RVA: 0x00063F83 File Offset: 0x00062183
		[DataMember]
		public JsonDictionary<object> Parameters { get; set; }
	}
}

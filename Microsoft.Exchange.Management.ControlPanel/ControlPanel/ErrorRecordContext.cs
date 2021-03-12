using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B8 RID: 1720
	[DataContract]
	public class ErrorRecordContext
	{
		// Token: 0x170027D3 RID: 10195
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x000DFD43 File Offset: 0x000DDF43
		// (set) Token: 0x0600493D RID: 18749 RVA: 0x000DFD4B File Offset: 0x000DDF4B
		[DataMember(EmitDefaultValue = false)]
		public JsonDictionary<object> LastOuput { get; set; }
	}
}

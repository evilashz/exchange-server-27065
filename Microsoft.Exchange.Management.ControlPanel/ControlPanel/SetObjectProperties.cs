using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000061 RID: 97
	[DataContract]
	public abstract class SetObjectProperties : WebServiceParameters
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x00054477 File Offset: 0x00052677
		public SetObjectProperties()
		{
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0005447F File Offset: 0x0005267F
		// (set) Token: 0x06001A69 RID: 6761 RVA: 0x00054487 File Offset: 0x00052687
		[DataMember]
		public ReturnObjectTypes ReturnObjectType { get; set; }
	}
}

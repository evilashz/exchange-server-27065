using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F5 RID: 245
	[DataContract]
	public class TrusteeRow : BaseRow
	{
		// Token: 0x06001EA4 RID: 7844 RVA: 0x0005C2C1 File Offset: 0x0005A4C1
		public TrusteeRow(string name) : base(new Identity(name, name), null)
		{
			this.DisplayName = name;
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x0005C2D8 File Offset: 0x0005A4D8
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
		[DataMember]
		public string DisplayName { get; private set; }
	}
}

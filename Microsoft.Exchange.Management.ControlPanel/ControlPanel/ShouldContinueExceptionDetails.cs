using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C7 RID: 1735
	[DataContract]
	public sealed class ShouldContinueExceptionDetails
	{
		// Token: 0x060049D2 RID: 18898 RVA: 0x000E1442 File Offset: 0x000DF642
		public ShouldContinueExceptionDetails(string cmdlet, string suppressConfirmParameterName)
		{
			this.CurrentCmdlet = cmdlet;
			this.SuppressConfirmParameterName = suppressConfirmParameterName;
		}

		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x000E1458 File Offset: 0x000DF658
		// (set) Token: 0x060049D4 RID: 18900 RVA: 0x000E1460 File Offset: 0x000DF660
		public string CurrentCmdlet { get; set; }

		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x000E1469 File Offset: 0x000DF669
		// (set) Token: 0x060049D6 RID: 18902 RVA: 0x000E1471 File Offset: 0x000DF671
		[DataMember]
		public string SuppressConfirmParameterName { get; set; }
	}
}

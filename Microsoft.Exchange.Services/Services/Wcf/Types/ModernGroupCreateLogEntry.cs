using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B47 RID: 2887
	public class ModernGroupCreateLogEntry
	{
		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x0010B00B File Offset: 0x0010920B
		// (set) Token: 0x060051DA RID: 20954 RVA: 0x0010B013 File Offset: 0x00109213
		[DataMember(Name = "Delta", IsRequired = false)]
		public float Delta { get; set; }

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x0010B01C File Offset: 0x0010921C
		// (set) Token: 0x060051DC RID: 20956 RVA: 0x0010B024 File Offset: 0x00109224
		[DataMember(Name = "Data", IsRequired = false)]
		public string Data { get; set; }
	}
}

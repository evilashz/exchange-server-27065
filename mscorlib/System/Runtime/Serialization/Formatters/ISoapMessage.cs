using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000733 RID: 1843
	[ComVisible(true)]
	public interface ISoapMessage
	{
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060051D9 RID: 20953
		// (set) Token: 0x060051DA RID: 20954
		string[] ParamNames { get; set; }

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060051DB RID: 20955
		// (set) Token: 0x060051DC RID: 20956
		object[] ParamValues { get; set; }

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060051DD RID: 20957
		// (set) Token: 0x060051DE RID: 20958
		Type[] ParamTypes { get; set; }

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060051DF RID: 20959
		// (set) Token: 0x060051E0 RID: 20960
		string MethodName { get; set; }

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x060051E1 RID: 20961
		// (set) Token: 0x060051E2 RID: 20962
		string XmlNameSpace { get; set; }

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x060051E3 RID: 20963
		// (set) Token: 0x060051E4 RID: 20964
		Header[] Headers { get; set; }
	}
}

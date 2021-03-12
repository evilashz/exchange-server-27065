using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007EA RID: 2026
	[ComVisible(true)]
	public interface IDynamicProperty
	{
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x060057C1 RID: 22465
		string Name { [SecurityCritical] get; }
	}
}

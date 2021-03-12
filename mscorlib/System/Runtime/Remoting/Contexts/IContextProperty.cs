using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E0 RID: 2016
	[ComVisible(true)]
	public interface IContextProperty
	{
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060057A2 RID: 22434
		string Name { [SecurityCritical] get; }

		// Token: 0x060057A3 RID: 22435
		[SecurityCritical]
		bool IsNewContextOK(Context newCtx);

		// Token: 0x060057A4 RID: 22436
		[SecurityCritical]
		void Freeze(Context newContext);
	}
}

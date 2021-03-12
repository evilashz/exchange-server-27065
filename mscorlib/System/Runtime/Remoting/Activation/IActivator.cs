using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200086C RID: 2156
	[ComVisible(true)]
	public interface IActivator
	{
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06005C0B RID: 23563
		// (set) Token: 0x06005C0C RID: 23564
		IActivator NextActivator { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x06005C0D RID: 23565
		[SecurityCritical]
		IConstructionReturnMessage Activate(IConstructionCallMessage msg);

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06005C0E RID: 23566
		ActivatorLevel Level { [SecurityCritical] get; }
	}
}

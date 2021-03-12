using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007DF RID: 2015
	[ComVisible(true)]
	public interface IContextAttribute
	{
		// Token: 0x060057A0 RID: 22432
		[SecurityCritical]
		bool IsContextOK(Context ctx, IConstructionCallMessage msg);

		// Token: 0x060057A1 RID: 22433
		[SecurityCritical]
		void GetPropertiesForNewContext(IConstructionCallMessage msg);
	}
}

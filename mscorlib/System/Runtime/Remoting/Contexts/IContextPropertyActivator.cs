using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E1 RID: 2017
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		// Token: 0x060057A5 RID: 22437
		[SecurityCritical]
		bool IsOKToActivate(IConstructionCallMessage msg);

		// Token: 0x060057A6 RID: 22438
		[SecurityCritical]
		void CollectFromClientContext(IConstructionCallMessage msg);

		// Token: 0x060057A7 RID: 22439
		[SecurityCritical]
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		// Token: 0x060057A8 RID: 22440
		[SecurityCritical]
		void CollectFromServerContext(IConstructionReturnMessage msg);

		// Token: 0x060057A9 RID: 22441
		[SecurityCritical]
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);
	}
}

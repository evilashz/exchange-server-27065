using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200086E RID: 2158
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06005C0F RID: 23567
		// (set) Token: 0x06005C10 RID: 23568
		IActivator Activator { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06005C11 RID: 23569
		object[] CallSiteActivationAttributes { [SecurityCritical] get; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06005C12 RID: 23570
		string ActivationTypeName { [SecurityCritical] get; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06005C13 RID: 23571
		Type ActivationType { [SecurityCritical] get; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06005C14 RID: 23572
		IList ContextProperties { [SecurityCritical] get; }
	}
}

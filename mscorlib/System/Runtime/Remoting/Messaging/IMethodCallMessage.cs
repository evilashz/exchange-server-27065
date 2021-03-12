using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200082E RID: 2094
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600596F RID: 22895
		int InArgCount { [SecurityCritical] get; }

		// Token: 0x06005970 RID: 22896
		[SecurityCritical]
		string GetInArgName(int index);

		// Token: 0x06005971 RID: 22897
		[SecurityCritical]
		object GetInArg(int argNum);

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005972 RID: 22898
		object[] InArgs { [SecurityCritical] get; }
	}
}

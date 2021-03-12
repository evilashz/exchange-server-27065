using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200082D RID: 2093
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005964 RID: 22884
		string Uri { [SecurityCritical] get; }

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005965 RID: 22885
		string MethodName { [SecurityCritical] get; }

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005966 RID: 22886
		string TypeName { [SecurityCritical] get; }

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06005967 RID: 22887
		object MethodSignature { [SecurityCritical] get; }

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005968 RID: 22888
		int ArgCount { [SecurityCritical] get; }

		// Token: 0x06005969 RID: 22889
		[SecurityCritical]
		string GetArgName(int index);

		// Token: 0x0600596A RID: 22890
		[SecurityCritical]
		object GetArg(int argNum);

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600596B RID: 22891
		object[] Args { [SecurityCritical] get; }

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x0600596C RID: 22892
		bool HasVarArgs { [SecurityCritical] get; }

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x0600596D RID: 22893
		LogicalCallContext LogicalCallContext { [SecurityCritical] get; }

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x0600596E RID: 22894
		MethodBase MethodBase { [SecurityCritical] get; }
	}
}

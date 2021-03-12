using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200082F RID: 2095
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMethodMessage, IMessage
	{
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005973 RID: 22899
		int OutArgCount { [SecurityCritical] get; }

		// Token: 0x06005974 RID: 22900
		[SecurityCritical]
		string GetOutArgName(int index);

		// Token: 0x06005975 RID: 22901
		[SecurityCritical]
		object GetOutArg(int argNum);

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005976 RID: 22902
		object[] OutArgs { [SecurityCritical] get; }

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06005977 RID: 22903
		Exception Exception { [SecurityCritical] get; }

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06005978 RID: 22904
		object ReturnValue { [SecurityCritical] get; }
	}
}

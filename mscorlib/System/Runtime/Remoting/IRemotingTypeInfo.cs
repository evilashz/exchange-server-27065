using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x02000788 RID: 1928
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06005465 RID: 21605
		// (set) Token: 0x06005466 RID: 21606
		string TypeName { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x06005467 RID: 21607
		[SecurityCritical]
		bool CanCastTo(Type fromType, object o);
	}
}

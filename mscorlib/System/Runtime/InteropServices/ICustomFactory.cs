using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000938 RID: 2360
	[ComVisible(true)]
	public interface ICustomFactory
	{
		// Token: 0x0600610A RID: 24842
		MarshalByRefObject CreateInstance(Type serverType);
	}
}

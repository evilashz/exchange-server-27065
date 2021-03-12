using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200081F RID: 2079
	[ComVisible(true)]
	public interface ITransportHeaders
	{
		// Token: 0x17000EE8 RID: 3816
		object this[object key]
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x06005912 RID: 22802
		[SecurityCritical]
		IEnumerator GetEnumerator();
	}
}

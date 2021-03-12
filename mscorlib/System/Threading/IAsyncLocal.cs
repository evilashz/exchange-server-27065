using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004B9 RID: 1209
	internal interface IAsyncLocal
	{
		// Token: 0x06003A5D RID: 14941
		[SecurityCritical]
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}

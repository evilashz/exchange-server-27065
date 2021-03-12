using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005BD RID: 1469
	[ComVisible(true)]
	public interface ICustomAttributeProvider
	{
		// Token: 0x0600452F RID: 17711
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004530 RID: 17712
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004531 RID: 17713
		bool IsDefined(Type attributeType, bool inherit);
	}
}

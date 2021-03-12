using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091E RID: 2334
	[ComVisible(true)]
	public interface ICustomMarshaler
	{
		// Token: 0x06005F8E RID: 24462
		object MarshalNativeToManaged(IntPtr pNativeData);

		// Token: 0x06005F8F RID: 24463
		IntPtr MarshalManagedToNative(object ManagedObj);

		// Token: 0x06005F90 RID: 24464
		void CleanUpNativeData(IntPtr pNativeData);

		// Token: 0x06005F91 RID: 24465
		void CleanUpManagedData(object ManagedObj);

		// Token: 0x06005F92 RID: 24466
		int GetNativeDataSize();
	}
}

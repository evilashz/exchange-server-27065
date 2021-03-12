using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009EF RID: 2543
	[Guid("393de7de-6fd0-4c0d-bb71-47244a113e93")]
	[ComImport]
	internal interface IBindableVector : IBindableIterable
	{
		// Token: 0x060064CA RID: 25802
		object GetAt(uint index);

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060064CB RID: 25803
		uint Size { get; }

		// Token: 0x060064CC RID: 25804
		IBindableVectorView GetView();

		// Token: 0x060064CD RID: 25805
		bool IndexOf(object value, out uint index);

		// Token: 0x060064CE RID: 25806
		void SetAt(uint index, object value);

		// Token: 0x060064CF RID: 25807
		void InsertAt(uint index, object value);

		// Token: 0x060064D0 RID: 25808
		void RemoveAt(uint index);

		// Token: 0x060064D1 RID: 25809
		void Append(object value);

		// Token: 0x060064D2 RID: 25810
		void RemoveAtEnd();

		// Token: 0x060064D3 RID: 25811
		void Clear();
	}
}

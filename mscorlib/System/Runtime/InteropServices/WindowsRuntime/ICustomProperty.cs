using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E3 RID: 2531
	[Guid("30DA92C0-23E8-42A0-AE7C-734A0E5D2782")]
	[ComImport]
	internal interface ICustomProperty
	{
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x0600648A RID: 25738
		Type Type { get; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x0600648B RID: 25739
		string Name { get; }

		// Token: 0x0600648C RID: 25740
		object GetValue(object target);

		// Token: 0x0600648D RID: 25741
		void SetValue(object target, object value);

		// Token: 0x0600648E RID: 25742
		object GetValue(object target, object indexValue);

		// Token: 0x0600648F RID: 25743
		void SetValue(object target, object value, object indexValue);

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06006490 RID: 25744
		bool CanWrite { get; }

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06006491 RID: 25745
		bool CanRead { get; }
	}
}

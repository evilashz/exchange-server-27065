using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000906 RID: 2310
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class DefaultDllImportSearchPathsAttribute : Attribute
	{
		// Token: 0x06005F1B RID: 24347 RVA: 0x00147018 File Offset: 0x00145218
		[__DynamicallyInvokable]
		public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
		{
			this._paths = paths;
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x00147027 File Offset: 0x00145227
		[__DynamicallyInvokable]
		public DllImportSearchPath Paths
		{
			[__DynamicallyInvokable]
			get
			{
				return this._paths;
			}
		}

		// Token: 0x04002A56 RID: 10838
		internal DllImportSearchPath _paths;
	}
}

using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000914 RID: 2324
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[ComVisible(false)]
	public sealed class ManagedToNativeComInteropStubAttribute : Attribute
	{
		// Token: 0x06005F45 RID: 24389 RVA: 0x0014749F File Offset: 0x0014569F
		public ManagedToNativeComInteropStubAttribute(Type classType, string methodName)
		{
			this._classType = classType;
			this._methodName = methodName;
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06005F46 RID: 24390 RVA: 0x001474B5 File Offset: 0x001456B5
		public Type ClassType
		{
			get
			{
				return this._classType;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06005F47 RID: 24391 RVA: 0x001474BD File Offset: 0x001456BD
		public string MethodName
		{
			get
			{
				return this._methodName;
			}
		}

		// Token: 0x04002A76 RID: 10870
		internal Type _classType;

		// Token: 0x04002A77 RID: 10871
		internal string _methodName;
	}
}

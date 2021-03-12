using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090D RID: 2317
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class CoClassAttribute : Attribute
	{
		// Token: 0x06005F33 RID: 24371 RVA: 0x001473C1 File Offset: 0x001455C1
		[__DynamicallyInvokable]
		public CoClassAttribute(Type coClass)
		{
			this._CoClass = coClass;
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06005F34 RID: 24372 RVA: 0x001473D0 File Offset: 0x001455D0
		[__DynamicallyInvokable]
		public Type CoClass
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CoClass;
			}
		}

		// Token: 0x04002A6A RID: 10858
		internal Type _CoClass;
	}
}

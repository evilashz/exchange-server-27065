using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090E RID: 2318
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		// Token: 0x06005F35 RID: 24373 RVA: 0x001473D8 File Offset: 0x001455D8
		[__DynamicallyInvokable]
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this._SourceInterface = SourceInterface;
			this._EventProvider = EventProvider;
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06005F36 RID: 24374 RVA: 0x001473EE File Offset: 0x001455EE
		[__DynamicallyInvokable]
		public Type SourceInterface
		{
			[__DynamicallyInvokable]
			get
			{
				return this._SourceInterface;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x001473F6 File Offset: 0x001455F6
		[__DynamicallyInvokable]
		public Type EventProvider
		{
			[__DynamicallyInvokable]
			get
			{
				return this._EventProvider;
			}
		}

		// Token: 0x04002A6B RID: 10859
		internal Type _SourceInterface;

		// Token: 0x04002A6C RID: 10860
		internal Type _EventProvider;
	}
}

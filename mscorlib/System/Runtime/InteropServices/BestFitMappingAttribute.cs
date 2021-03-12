using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000911 RID: 2321
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class BestFitMappingAttribute : Attribute
	{
		// Token: 0x06005F40 RID: 24384 RVA: 0x00147469 File Offset: 0x00145669
		[__DynamicallyInvokable]
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x00147478 File Offset: 0x00145678
		[__DynamicallyInvokable]
		public bool BestFitMapping
		{
			[__DynamicallyInvokable]
			get
			{
				return this._bestFitMapping;
			}
		}

		// Token: 0x04002A73 RID: 10867
		internal bool _bestFitMapping;

		// Token: 0x04002A74 RID: 10868
		[__DynamicallyInvokable]
		public bool ThrowOnUnmappableChar;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000895 RID: 2197
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		// Token: 0x06005C9E RID: 23710 RVA: 0x00144D58 File Offset: 0x00142F58
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06005C9F RID: 23711 RVA: 0x00144D67 File Offset: 0x00142F67
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04002970 RID: 10608
		private Type requiredContract;
	}
}

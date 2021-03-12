using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000899 RID: 2201
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x06005CA5 RID: 23717 RVA: 0x00144DAC File Offset: 0x00142FAC
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04002978 RID: 10616
		private string typeName;
	}
}

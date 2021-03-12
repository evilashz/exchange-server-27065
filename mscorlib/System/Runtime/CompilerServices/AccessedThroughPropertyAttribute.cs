using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000875 RID: 2165
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		// Token: 0x06005C5B RID: 23643 RVA: 0x001447FE File Offset: 0x001429FE
		[__DynamicallyInvokable]
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x0014480D File Offset: 0x00142A0D
		[__DynamicallyInvokable]
		public string PropertyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04002954 RID: 10580
		private readonly string propertyName;
	}
}

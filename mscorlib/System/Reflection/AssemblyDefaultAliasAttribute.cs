using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200058E RID: 1422
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		// Token: 0x06004340 RID: 17216 RVA: 0x000F7A0A File Offset: 0x000F5C0A
		[__DynamicallyInvokable]
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.m_defaultAlias = defaultAlias;
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x000F7A19 File Offset: 0x000F5C19
		[__DynamicallyInvokable]
		public string DefaultAlias
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultAlias;
			}
		}

		// Token: 0x04001B49 RID: 6985
		private string m_defaultAlias;
	}
}

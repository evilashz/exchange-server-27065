using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000591 RID: 1425
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		// Token: 0x06004346 RID: 17222 RVA: 0x000F7A5D File Offset: 0x000F5C5D
		[__DynamicallyInvokable]
		public AssemblyCultureAttribute(string culture)
		{
			this.m_culture = culture;
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x000F7A6C File Offset: 0x000F5C6C
		[__DynamicallyInvokable]
		public string Culture
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_culture;
			}
		}

		// Token: 0x04001B4C RID: 6988
		private string m_culture;
	}
}

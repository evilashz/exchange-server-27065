using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000588 RID: 1416
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		// Token: 0x06004334 RID: 17204 RVA: 0x000F7980 File Offset: 0x000F5B80
		[__DynamicallyInvokable]
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.m_trademark = trademark;
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x000F798F File Offset: 0x000F5B8F
		[__DynamicallyInvokable]
		public string Trademark
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_trademark;
			}
		}

		// Token: 0x04001B43 RID: 6979
		private string m_trademark;
	}
}

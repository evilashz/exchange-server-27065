using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000592 RID: 1426
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		// Token: 0x06004348 RID: 17224 RVA: 0x000F7A74 File Offset: 0x000F5C74
		[__DynamicallyInvokable]
		public AssemblyVersionAttribute(string version)
		{
			this.m_version = version;
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x000F7A83 File Offset: 0x000F5C83
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x04001B4D RID: 6989
		private string m_version;
	}
}

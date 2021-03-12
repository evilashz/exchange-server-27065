using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200058C RID: 1420
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		// Token: 0x0600433C RID: 17212 RVA: 0x000F79DC File Offset: 0x000F5BDC
		[__DynamicallyInvokable]
		public AssemblyTitleAttribute(string title)
		{
			this.m_title = title;
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000F79EB File Offset: 0x000F5BEB
		[__DynamicallyInvokable]
		public string Title
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_title;
			}
		}

		// Token: 0x04001B47 RID: 6983
		private string m_title;
	}
}

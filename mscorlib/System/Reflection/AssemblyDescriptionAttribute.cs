using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200058B RID: 1419
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		// Token: 0x0600433A RID: 17210 RVA: 0x000F79C5 File Offset: 0x000F5BC5
		[__DynamicallyInvokable]
		public AssemblyDescriptionAttribute(string description)
		{
			this.m_description = description;
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000F79D4 File Offset: 0x000F5BD4
		[__DynamicallyInvokable]
		public string Description
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x04001B46 RID: 6982
		private string m_description;
	}
}

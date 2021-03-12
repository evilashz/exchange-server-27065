using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000587 RID: 1415
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		// Token: 0x06004332 RID: 17202 RVA: 0x000F7969 File Offset: 0x000F5B69
		[__DynamicallyInvokable]
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.m_copyright = copyright;
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x000F7978 File Offset: 0x000F5B78
		[__DynamicallyInvokable]
		public string Copyright
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_copyright;
			}
		}

		// Token: 0x04001B42 RID: 6978
		private string m_copyright;
	}
}

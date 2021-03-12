using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000593 RID: 1427
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		// Token: 0x0600434A RID: 17226 RVA: 0x000F7A8B File Offset: 0x000F5C8B
		[__DynamicallyInvokable]
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.m_keyFile = keyFile;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x000F7A9A File Offset: 0x000F5C9A
		[__DynamicallyInvokable]
		public string KeyFile
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_keyFile;
			}
		}

		// Token: 0x04001B4E RID: 6990
		private string m_keyFile;
	}
}

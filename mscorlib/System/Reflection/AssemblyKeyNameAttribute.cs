using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000599 RID: 1433
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		// Token: 0x0600435C RID: 17244 RVA: 0x000F7B68 File Offset: 0x000F5D68
		[__DynamicallyInvokable]
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.m_keyName = keyName;
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000F7B77 File Offset: 0x000F5D77
		[__DynamicallyInvokable]
		public string KeyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_keyName;
			}
		}

		// Token: 0x04001B56 RID: 6998
		private string m_keyName;
	}
}

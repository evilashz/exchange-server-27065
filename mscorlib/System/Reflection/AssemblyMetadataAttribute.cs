using System;

namespace System.Reflection
{
	// Token: 0x02000597 RID: 1431
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06004356 RID: 17238 RVA: 0x000F7B1C File Offset: 0x000F5D1C
		[__DynamicallyInvokable]
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.m_key = key;
			this.m_value = value;
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x000F7B32 File Offset: 0x000F5D32
		[__DynamicallyInvokable]
		public string Key
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_key;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x000F7B3A File Offset: 0x000F5D3A
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001B52 RID: 6994
		private string m_key;

		// Token: 0x04001B53 RID: 6995
		private string m_value;
	}
}

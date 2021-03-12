using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000590 RID: 1424
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		// Token: 0x06004344 RID: 17220 RVA: 0x000F7A38 File Offset: 0x000F5C38
		[__DynamicallyInvokable]
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x000F7A55 File Offset: 0x000F5C55
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._version;
			}
		}

		// Token: 0x04001B4B RID: 6987
		private string _version;
	}
}

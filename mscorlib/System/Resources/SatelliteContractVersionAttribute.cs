using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x02000371 RID: 881
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		// Token: 0x06002C7A RID: 11386 RVA: 0x000A9FCC File Offset: 0x000A81CC
		[__DynamicallyInvokable]
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._version = version;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000A9FE9 File Offset: 0x000A81E9
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._version;
			}
		}

		// Token: 0x040011D7 RID: 4567
		private string _version;
	}
}

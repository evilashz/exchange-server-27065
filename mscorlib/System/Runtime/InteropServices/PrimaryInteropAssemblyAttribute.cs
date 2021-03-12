using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090C RID: 2316
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class PrimaryInteropAssemblyAttribute : Attribute
	{
		// Token: 0x06005F30 RID: 24368 RVA: 0x0014739B File Offset: 0x0014559B
		public PrimaryInteropAssemblyAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x001473B1 File Offset: 0x001455B1
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x001473B9 File Offset: 0x001455B9
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002A68 RID: 10856
		internal int _major;

		// Token: 0x04002A69 RID: 10857
		internal int _minor;
	}
}

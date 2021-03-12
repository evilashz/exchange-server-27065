using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090F RID: 2319
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibVersionAttribute : Attribute
	{
		// Token: 0x06005F38 RID: 24376 RVA: 0x001473FE File Offset: 0x001455FE
		public TypeLibVersionAttribute(int major, int minor)
		{
			this._major = major;
			this._minor = minor;
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x00147414 File Offset: 0x00145614
		public int MajorVersion
		{
			get
			{
				return this._major;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06005F3A RID: 24378 RVA: 0x0014741C File Offset: 0x0014561C
		public int MinorVersion
		{
			get
			{
				return this._minor;
			}
		}

		// Token: 0x04002A6D RID: 10861
		internal int _major;

		// Token: 0x04002A6E RID: 10862
		internal int _minor;
	}
}

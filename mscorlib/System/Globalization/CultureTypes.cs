using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x0200037E RID: 894
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CultureTypes
	{
		// Token: 0x0400129E RID: 4766
		NeutralCultures = 1,
		// Token: 0x0400129F RID: 4767
		SpecificCultures = 2,
		// Token: 0x040012A0 RID: 4768
		InstalledWin32Cultures = 4,
		// Token: 0x040012A1 RID: 4769
		AllCultures = 7,
		// Token: 0x040012A2 RID: 4770
		UserCustomCulture = 8,
		// Token: 0x040012A3 RID: 4771
		ReplacementCultures = 16,
		// Token: 0x040012A4 RID: 4772
		[Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")]
		WindowsOnlyCultures = 32,
		// Token: 0x040012A5 RID: 4773
		[Obsolete("This value has been deprecated.  Please use other values in CultureTypes.")]
		FrameworkCultures = 64
	}
}

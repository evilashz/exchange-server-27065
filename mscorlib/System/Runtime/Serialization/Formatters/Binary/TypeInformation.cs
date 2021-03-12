using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000778 RID: 1912
	internal sealed class TypeInformation
	{
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060053CD RID: 21453 RVA: 0x00129542 File Offset: 0x00127742
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x0012954A File Offset: 0x0012774A
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x00129552 File Offset: 0x00127752
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x0012955A File Offset: 0x0012775A
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x04002653 RID: 9811
		private string fullTypeName;

		// Token: 0x04002654 RID: 9812
		private string assemblyString;

		// Token: 0x04002655 RID: 9813
		private bool hasTypeForwardedFrom;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200070C RID: 1804
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060050AB RID: 20651 RVA: 0x0011B35A File Offset: 0x0011955A
		// (set) Token: 0x060050AC RID: 20652 RVA: 0x0011B362 File Offset: 0x00119562
		public int VersionAdded
		{
			get
			{
				return this.versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_OptionalFieldVersionValue"));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x04002388 RID: 9096
		private int versionAdded = 1;
	}
}

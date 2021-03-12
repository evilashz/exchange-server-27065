using System;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000008 RID: 8
	internal sealed class ExtenderVisiblePropertyAttribute : Attribute
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000024AC File Offset: 0x000006AC
		public ExtenderVisiblePropertyAttribute(bool value)
		{
			this.value = value;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024BB File Offset: 0x000006BB
		public bool Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024C3 File Offset: 0x000006C3
		public override bool IsDefaultAttribute()
		{
			return !this.value;
		}

		// Token: 0x0400000B RID: 11
		private bool value;

		// Token: 0x0400000C RID: 12
		public static ExtenderVisiblePropertyAttribute Yes = new ExtenderVisiblePropertyAttribute(true);

		// Token: 0x0400000D RID: 13
		public static ExtenderVisiblePropertyAttribute No = new ExtenderVisiblePropertyAttribute(false);

		// Token: 0x0400000E RID: 14
		public static ExtenderVisiblePropertyAttribute Default = ExtenderVisiblePropertyAttribute.No;
	}
}

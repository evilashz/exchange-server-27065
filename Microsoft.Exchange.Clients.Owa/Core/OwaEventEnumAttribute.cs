using System;
using System.Collections;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000185 RID: 389
	[AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
	public sealed class OwaEventEnumAttribute : Attribute
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x0005B956 File Offset: 0x00059B56
		public OwaEventEnumAttribute()
		{
			this.enumValueTable = new Hashtable();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0005B969 File Offset: 0x00059B69
		internal void AddValueInfo(int intValue, object enumValue)
		{
			this.enumValueTable[intValue] = enumValue;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0005B97D File Offset: 0x00059B7D
		internal object FindValueInfo(int intValue)
		{
			return this.enumValueTable[intValue];
		}

		// Token: 0x040009AD RID: 2477
		private Hashtable enumValueTable;
	}
}

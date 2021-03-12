using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000005 RID: 5
	public class EnumListSource : ObjectListSource
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022A2 File Offset: 0x000004A2
		public EnumListSource(Type enumType) : this(Enum.GetValues(enumType), enumType)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022B1 File Offset: 0x000004B1
		public EnumListSource(Array values, Type enumType) : base(values)
		{
			this.enumType = enumType;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022C1 File Offset: 0x000004C1
		public Type EnumType
		{
			get
			{
				return this.enumType;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022CC File Offset: 0x000004CC
		protected override string GetValueText(object objectValue)
		{
			string result = string.Empty;
			if (this.enumType.IsInstanceOfType(objectValue))
			{
				result = LocalizedDescriptionAttribute.FromEnum(this.enumType, objectValue);
			}
			return result;
		}

		// Token: 0x0400000B RID: 11
		private Type enumType;
	}
}

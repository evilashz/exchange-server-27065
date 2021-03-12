using System;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000DA RID: 218
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	internal sealed class DataColumnViewAttribute : Attribute
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x0001F8C7 File Offset: 0x0001DAC7
		public DataColumnViewAttribute(RowType rowType)
		{
			this.rowTypeBits = rowType;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001F8D6 File Offset: 0x0001DAD6
		public bool IsViewOf(RowType rowType)
		{
			return (this.rowTypeBits & rowType) == rowType;
		}

		// Token: 0x040003F5 RID: 1013
		private readonly RowType rowTypeBits;
	}
}

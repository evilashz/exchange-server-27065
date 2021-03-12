using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C2 RID: 194
	public class UnlimitedItems : Unlimited<long>
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0001A754 File Offset: 0x00018954
		public static bool TryParse(string input, out UnlimitedItems result)
		{
			long value;
			if (!long.TryParse(input, out value))
			{
				result = UnlimitedItems.UnlimitedValue;
				return false;
			}
			result = new UnlimitedItems(value);
			return true;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001A77D File Offset: 0x0001897D
		public UnlimitedItems()
		{
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001A785 File Offset: 0x00018985
		public UnlimitedItems(long value) : base(value)
		{
		}

		// Token: 0x0400073E RID: 1854
		public static UnlimitedItems Zero = new UnlimitedItems(0L);

		// Token: 0x0400073F RID: 1855
		public new static UnlimitedItems UnlimitedValue = new UnlimitedItems();
	}
}

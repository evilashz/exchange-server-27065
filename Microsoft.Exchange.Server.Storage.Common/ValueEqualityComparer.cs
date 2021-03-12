using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C3 RID: 195
	public sealed class ValueEqualityComparer : IEqualityComparer<object>
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x0001A7A6 File Offset: 0x000189A6
		public ValueEqualityComparer(CompareInfo compareInfo)
		{
			this.compareInfo = compareInfo;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001A7B5 File Offset: 0x000189B5
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			return ValueHelper.ValuesEqual(x, y, this.compareInfo, CompareOptions.None);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001A7C5 File Offset: 0x000189C5
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			if (obj is string)
			{
				return obj.GetHashCode();
			}
			return ValueHelper.GetHashCode(obj);
		}

		// Token: 0x04000740 RID: 1856
		private readonly CompareInfo compareInfo;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000FA RID: 250
	internal static class CmdletStaticDataWithUniqueId<T>
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		private static Dictionary<Guid, T> Data
		{
			get
			{
				Dictionary<Guid, T> result;
				if ((result = CmdletStaticDataWithUniqueId<T>.data) == null)
				{
					result = (CmdletStaticDataWithUniqueId<T>.data = new Dictionary<Guid, T>());
				}
				return result;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001C712 File Offset: 0x0001A912
		internal static bool ContainsKey(Guid cmdletUniqueId)
		{
			return CmdletStaticDataWithUniqueId<T>.Data.ContainsKey(cmdletUniqueId);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001C720 File Offset: 0x0001A920
		internal static T Get(Guid cmdletUniqueId)
		{
			T result;
			CmdletStaticDataWithUniqueId<T>.TryGet(cmdletUniqueId, out result);
			return result;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001C737 File Offset: 0x0001A937
		internal static bool TryGet(Guid cmdletUniqueId, out T output)
		{
			return CmdletStaticDataWithUniqueId<T>.Data.TryGetValue(cmdletUniqueId, out output);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001C745 File Offset: 0x0001A945
		internal static void Set(Guid cmdletUniqueId, T value)
		{
			CmdletStaticDataWithUniqueId<T>.Data[cmdletUniqueId] = value;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001C753 File Offset: 0x0001A953
		internal static bool Remove(Guid cmdletUniqueId)
		{
			return CmdletStaticDataWithUniqueId<T>.Data.Remove(cmdletUniqueId);
		}

		// Token: 0x0400048B RID: 1163
		[ThreadStatic]
		private static Dictionary<Guid, T> data;
	}
}

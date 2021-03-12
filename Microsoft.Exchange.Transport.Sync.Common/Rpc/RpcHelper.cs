using System;
using System.Globalization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc
{
	// Token: 0x02000095 RID: 149
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RpcHelper
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x00015EC8 File Offset: 0x000140C8
		public static bool TryGetProperty<TOutputType>(MdbefPropertyCollection args, uint property, out TOutputType output)
		{
			string text;
			return RpcHelper.TryGetProperty<TOutputType>(args, property, out output, out text);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00015EE0 File Offset: 0x000140E0
		public static bool TryGetProperty<TOutputType>(MdbefPropertyCollection args, uint property, out TOutputType output, out string errorString)
		{
			object obj;
			if (args.TryGetValue(property, out obj) && obj is TOutputType)
			{
				output = (TOutputType)((object)obj);
				errorString = null;
				return true;
			}
			output = default(TOutputType);
			errorString = string.Format(CultureInfo.InvariantCulture, "Could not read property {0}. Found {1}.", new object[]
			{
				property,
				obj
			});
			return false;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00015F40 File Offset: 0x00014140
		public static byte[] CreateResponsePropertyCollection(uint returnValuePropTag, object value)
		{
			return new MdbefPropertyCollection
			{
				{
					returnValuePropTag,
					value
				}
			}.GetBytes();
		}
	}
}

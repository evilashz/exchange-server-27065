using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F2 RID: 754
	internal static class Parse
	{
		// Token: 0x06001635 RID: 5685 RVA: 0x0006774C File Offset: 0x0006594C
		internal static TimeSpan? ParseFromMilliseconds(string value)
		{
			TimeSpan? result = null;
			int num;
			if (!string.IsNullOrEmpty(value) && int.TryParse(value, out num))
			{
				Exception ex = null;
				try
				{
					result = new TimeSpan?(TimeSpan.FromMilliseconds((double)num));
				}
				catch (ArgumentException ex2)
				{
					ex = ex2;
				}
				catch (OverflowException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<Exception>(0, "Error decoding timeout property: {0}", ex);
				}
			}
			return result;
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000677C4 File Offset: 0x000659C4
		internal static bool IsSMSRecipient(string recipient)
		{
			ProxyAddress proxyAddress;
			return SmtpProxyAddress.TryDeencapsulate(recipient, out proxyAddress) && string.Equals(proxyAddress.PrefixString, "MOBILE", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000677F4 File Offset: 0x000659F4
		internal static string RemoveControlChars(string s)
		{
			StringBuilder stringBuilder = null;
			if (string.IsNullOrEmpty(s))
			{
				return null;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsControl(s, i))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(s.Length);
						stringBuilder.Append(s, 0, i);
					}
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(s[i]);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return s;
		}
	}
}

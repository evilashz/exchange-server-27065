using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x0200013C RID: 316
	internal class ResubmitFilter
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000334D9 File Offset: 0x000316D9
		internal bool FromAddressChecking
		{
			get
			{
				return !string.IsNullOrEmpty(this.fromAddress);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x000334E9 File Offset: 0x000316E9
		internal bool ToAddressChecking
		{
			get
			{
				return !string.IsNullOrEmpty(this.toAddress);
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000334F9 File Offset: 0x000316F9
		private ResubmitFilter(string fromAddress, string toAddress)
		{
			this.fromAddress = fromAddress;
			this.toAddress = toAddress;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00033510 File Offset: 0x00031710
		internal bool ValidateStringParam(ResubmitFilter.FilterParameterType paramType, string value)
		{
			string value2;
			switch (paramType)
			{
			case ResubmitFilter.FilterParameterType.ToAddress:
				value2 = this.toAddress;
				break;
			case ResubmitFilter.FilterParameterType.FromAddress:
				value2 = this.fromAddress;
				break;
			default:
				throw new ArgumentException("Inproper parameter type pssed to ResubmitFilter.ValidateStringParam", paramType.ToString());
			}
			return string.IsNullOrEmpty(value2) || value.Equals(value2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000335A8 File Offset: 0x000317A8
		internal static bool TryBuild(string conditionstring, out ResubmitFilter generatedFilter)
		{
			generatedFilter = null;
			Dictionary<string, string> dictionary;
			try
			{
				dictionary = (from part in conditionstring.Trim().Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries)
				select part.Split(new char[]
				{
					'='
				})).ToDictionary((string[] split) => split[0].ToLower().Trim(), (string[] split) => split[1].Trim());
			}
			catch
			{
				return false;
			}
			string text;
			dictionary.TryGetValue("fromaddress", out text);
			string text2;
			dictionary.TryGetValue("toaddress", out text2);
			generatedFilter = new ResubmitFilter(text, text2);
			return true;
		}

		// Token: 0x040005FB RID: 1531
		private readonly string fromAddress;

		// Token: 0x040005FC RID: 1532
		private readonly string toAddress;

		// Token: 0x0200013D RID: 317
		internal enum FilterParameterType : byte
		{
			// Token: 0x04000601 RID: 1537
			ToAddress,
			// Token: 0x04000602 RID: 1538
			FromAddress
		}
	}
}

using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncWatermarkElementEncoder
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000105A0 File Offset: 0x0000E7A0
		public string Encode(string elementToEncode)
		{
			SyncUtilities.ThrowIfArgumentNull("elementToEncode", elementToEncode);
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				elementToEncode.Length,
				SyncWatermarkElementEncoder.Delimiter,
				elementToEncode
			});
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000105F0 File Offset: 0x0000E7F0
		public bool TryDecodeElementFrom(string toDecode, int offset, out string decodedElement, out int charactersConsumed)
		{
			SyncUtilities.ThrowIfArgumentNull("toDecode", toDecode);
			SyncUtilities.ThrowIfArgumentLessThanZero("offset", offset);
			decodedElement = null;
			charactersConsumed = 0;
			int num = toDecode.IndexOf(SyncWatermarkElementEncoder.Delimiter, offset);
			if (num == -1)
			{
				return false;
			}
			string s = toDecode.Substring(offset, num - offset);
			int num2;
			if (!int.TryParse(s, out num2))
			{
				return false;
			}
			int num3 = num + 1;
			int num4 = num3 + num2;
			if (num4 > toDecode.Length)
			{
				return false;
			}
			charactersConsumed = num4 - offset;
			decodedElement = toDecode.Substring(num3, num2);
			return true;
		}

		// Token: 0x040001EB RID: 491
		internal static readonly char Delimiter = '|';
	}
}

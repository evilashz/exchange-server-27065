using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AddressBookEntryId : AddressEntryId
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000023B0 File Offset: 0x000005B0
		internal AddressBookEntryId(byte[] entryId) : base(entryId)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023BC File Offset: 0x000005BC
		public static bool IsAddressBookEntryId(Reader reader, uint sizeEntry)
		{
			if (sizeEntry < 29U || reader.Length - reader.Position < (long)((ulong)sizeEntry))
			{
				return false;
			}
			long position = reader.Position;
			bool result;
			try
			{
				reader.ReadArraySegment(4U);
				result = reader.ReadGuid().Equals(AddressBookEntryId.ExchangeProviderGuid);
			}
			finally
			{
				reader.Position = position;
			}
			return result;
		}

		// Token: 0x0400000C RID: 12
		public const int MinimalSize = 29;

		// Token: 0x0400000D RID: 13
		private static readonly Guid ExchangeProviderGuid = new Guid("{c840a7dc-42c0-1a10-b4b9-08002b2fe182}");
	}
}

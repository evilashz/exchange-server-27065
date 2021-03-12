using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200005B RID: 91
	public static class EntryIdHelpers
	{
		// Token: 0x06000327 RID: 807 RVA: 0x000180B0 File Offset: 0x000162B0
		public static byte[] FormatServerEntryId(bool export, ExchangeId fid, ExchangeId mid, int instanceNum)
		{
			if (!export)
			{
				return ExchangeIdHelpers.BuildOursServerEntryId(fid.ToLong(), mid.ToLong(), instanceNum);
			}
			byte[] array = new byte[23 + (mid.IsValid ? 22 : 0)];
			int num = 0;
			array[num++] = 2;
			num += ExchangeIdHelpers.To22ByteArray(fid.Guid, fid.Counter, array, num);
			if (mid.IsValid)
			{
				ExchangeIdHelpers.To22ByteArray(mid.Guid, mid.Counter, array, num);
			}
			return array;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00018130 File Offset: 0x00016330
		public static bool ParseServerEntryId(Context context, IReplidGuidMap replidGuidMap, byte[] entryId, bool export, out ExchangeId fid, out ExchangeId mid, out int instanceNum)
		{
			fid = ExchangeId.Null;
			mid = ExchangeId.Null;
			instanceNum = 0;
			if (entryId == null || entryId.Length < 1)
			{
				return false;
			}
			byte b = entryId[0];
			if (b == 1 && !export)
			{
				long legacyId;
				long legacyId2;
				if (!ExchangeIdHelpers.ParseOursServerEntryId(entryId, out legacyId, out legacyId2, out instanceNum))
				{
					return false;
				}
				fid = ExchangeId.CreateFromInt64(context, replidGuidMap, legacyId);
				mid = ExchangeId.CreateFromInt64(context, replidGuidMap, legacyId2);
			}
			else
			{
				if (b != 2 || !export)
				{
					return false;
				}
				if (entryId.Length != 23 && entryId.Length != 45)
				{
					return false;
				}
				fid = ExchangeId.CreateFrom22ByteArray(context, replidGuidMap, entryId, 1);
				if (entryId.Length == 45)
				{
					mid = ExchangeId.CreateFrom22ByteArray(context, replidGuidMap, entryId, 23);
				}
			}
			return true;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000181E4 File Offset: 0x000163E4
		public static byte[] ExchangeIdTo46ByteEntryId(ExchangeId exchangeId, Guid entryIdGuid, EntryIdHelpers.EIDType eidType)
		{
			byte[] array = new byte[46];
			int num = 0;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 0;
			array[num++] = 0;
			num += ParseSerialize.SerializeGuid(entryIdGuid, array, num);
			array[num++] = (byte)eidType;
			array[num++] = 0;
			num += ExchangeIdHelpers.To22ByteArray(exchangeId.Guid, exchangeId.Counter, array, num);
			array[num++] = 0;
			array[num++] = 0;
			return array;
		}

		// Token: 0x0200005C RID: 92
		public enum EIDType
		{
			// Token: 0x040002FA RID: 762
			eitSTPrivateFolder,
			// Token: 0x040002FB RID: 763
			eitLTPrivateFolder,
			// Token: 0x040002FC RID: 764
			eitSTPublicFolder,
			// Token: 0x040002FD RID: 765
			eitLTPublicFolder,
			// Token: 0x040002FE RID: 766
			eitSTWackyFolder,
			// Token: 0x040002FF RID: 767
			eitLTWackyFolder,
			// Token: 0x04000300 RID: 768
			eitSTPrivateMessage,
			// Token: 0x04000301 RID: 769
			eitLTPrivateMessage,
			// Token: 0x04000302 RID: 770
			eitSTPublicMessage,
			// Token: 0x04000303 RID: 771
			eitLTPublicMessage,
			// Token: 0x04000304 RID: 772
			eitSTWackyMessage,
			// Token: 0x04000305 RID: 773
			eitLTWackyMessage,
			// Token: 0x04000306 RID: 774
			eitLTPublicFolderByName
		}

		// Token: 0x0200005D RID: 93
		public enum SVREIDType : byte
		{
			// Token: 0x04000308 RID: 776
			NotOurs,
			// Token: 0x04000309 RID: 777
			Ours,
			// Token: 0x0400030A RID: 778
			Export,
			// Token: 0x0400030B RID: 779
			Gids,
			// Token: 0x0400030C RID: 780
			Max
		}
	}
}

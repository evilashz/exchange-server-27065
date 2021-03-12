using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreIntegrityCheck;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000047 RID: 71
	internal static class AdminRpcParseFormat
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000C108 File Offset: 0x0000A308
		public static ErrorCode ParseReadEventsRequest(Context context, byte[] request, out EventHistory.ReadEventsFlags flags, out long startCounter, out uint eventsWant, out uint eventsToCheck, out Restriction restriction)
		{
			uint num = (uint)request.Length;
			uint num2 = 0U;
			restriction = null;
			ErrorCode first = AdminRpcParseFormat.ParseReadEventsRequestHeaderBlock(request, ref num2, ref num, out flags, out startCounter, out eventsWant, out eventsToCheck);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)45909U);
			}
			if (num != 0U)
			{
				first = AdminRpcParseFormat.ParseReadEventsRestrictionBlock(context, request, ref num2, ref num, out restriction);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)62293U);
				}
			}
			if (num != 0U)
			{
				return ErrorCode.CreateRpcFormat((LID)37717U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C198 File Offset: 0x0000A398
		public static ErrorCode FormatReadEventsResponse(int flags, long endCounter, List<EventEntry> events, out byte[] response)
		{
			uint num = (uint)((events == null) ? 0 : events.Count);
			uint num2 = AdminRpcParseFormat.PaddedBlockLength(AdminRpcParseFormat.ReadEventsResponseHeaderLengthV2) + num * AdminRpcParseFormat.PaddedBlockLength(AdminRpcParseFormat.EventEntryLengthV7);
			response = new byte[num2];
			ParseSerialize.SerializeInt32((int)AdminRpcParseFormat.ReadEventsResponseHeaderLengthV2, response, (int)AdminRpcParseFormat.DataBlockLengthOffset);
			ParseSerialize.SerializeInt32(flags, response, (int)AdminRpcParseFormat.ReadEventsResponseHeaderFlagsOffset);
			ParseSerialize.SerializeInt32((int)num, response, (int)AdminRpcParseFormat.ReadEventsResponseHeaderEventsCountOffset);
			ParseSerialize.SerializeInt32(0, response, (int)AdminRpcParseFormat.ReadEventsResponseHeaderPaddingOffset);
			ParseSerialize.SerializeInt64(endCounter, response, (int)AdminRpcParseFormat.ReadEventsResponseHeaderEndCounterOffset);
			if (events != null)
			{
				AdminRpcParseFormat.FormatEventList(events, response, AdminRpcParseFormat.PaddedBlockLength(AdminRpcParseFormat.ReadEventsResponseHeaderLengthV2));
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000C238 File Offset: 0x0000A438
		public static ErrorCode ParseWriteEventsRequest(byte[] request, out int flags, out List<EventEntry> events)
		{
			uint num = (uint)request.Length;
			uint num2 = 0U;
			events = null;
			uint eventsCount;
			ErrorCode first = AdminRpcParseFormat.ParseWriteEventsRequestHeaderBlock(request, ref num2, ref num, out flags, out eventsCount);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)54101U);
			}
			first = AdminRpcParseFormat.ParseEventList(request, false, ref num2, ref num, eventsCount, out events);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)41813U);
			}
			if (num != 0U)
			{
				return ErrorCode.CreateRpcFormat((LID)58197U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public static ErrorCode FormatWriteEventsResponse(List<long> eventCounters, out byte[] response)
		{
			uint num = AdminRpcParseFormat.WriteEventsResponseHeaderLength + (uint)(eventCounters.Count * (int)AdminRpcParseFormat.WriteEventsResponseSingleAdjustedEventCounterLength);
			response = new byte[num];
			ParseSerialize.SerializeInt32((int)num, response, (int)AdminRpcParseFormat.DataBlockLengthOffset);
			ParseSerialize.SerializeInt32(eventCounters.Count, response, (int)AdminRpcParseFormat.WriteEventsResponseEventsCountOffset);
			uint num2 = AdminRpcParseFormat.WriteEventsResponseHeaderLength;
			for (int i = 0; i < eventCounters.Count; i++)
			{
				ParseSerialize.SerializeInt64(eventCounters[i], response, (int)num2);
				num2 += AdminRpcParseFormat.WriteEventsResponseSingleAdjustedEventCounterLength;
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C340 File Offset: 0x0000A540
		public static ErrorCode ParseMDBEVENTWMs(MDBEVENTWM[] wms, out List<EventWatermark> watermarks)
		{
			watermarks = new List<EventWatermark>(wms.Length);
			for (int i = 0; i < wms.Length; i++)
			{
				watermarks.Add(new EventWatermark(wms[i].MailboxGuid, wms[i].ConsumerGuid, wms[i].EventCounter));
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C39C File Offset: 0x0000A59C
		public static ErrorCode FormatMDBEVENTWMs(List<EventWatermark> watermarks, out MDBEVENTWM[] wms)
		{
			wms = new MDBEVENTWM[watermarks.Count];
			for (int i = 0; i < watermarks.Count; i++)
			{
				wms[i].MailboxGuid = watermarks[i].MailboxGuid;
				wms[i].ConsumerGuid = watermarks[i].ConsumerGuid;
				wms[i].EventCounter = watermarks[i].EventCounter;
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C420 File Offset: 0x0000A620
		private static ErrorCode CheckDataBlock(byte[] request, uint ib, uint cb, out uint blockDataLength)
		{
			if (cb < 4U)
			{
				blockDataLength = 0U;
				return ErrorCode.CreateRpcFormat((LID)33621U);
			}
			blockDataLength = (uint)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.DataBlockLengthOffset));
			if (blockDataLength < AdminRpcParseFormat.DataBlockLengthLength || blockDataLength > cb || (blockDataLength < cb && AdminRpcParseFormat.PaddedBlockLength(blockDataLength) > cb))
			{
				return ErrorCode.CreateRpcFormat((LID)50005U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C485 File Offset: 0x0000A685
		public static uint ActualBlockLength(uint blockDataLength, uint blockMaxLength)
		{
			if (blockDataLength > blockMaxLength)
			{
				throw new InvalidSerializedFormatException("at this point, we should have checked blockDataLength");
			}
			if (blockDataLength >= blockMaxLength)
			{
				return blockMaxLength;
			}
			return AdminRpcParseFormat.PaddedBlockLength(blockDataLength);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		public static uint PaddedBlockLength(uint dataLength)
		{
			return dataLength + (AdminRpcParseFormat.DataBlockLengthPaddingMultiple - 1U) & ~(AdminRpcParseFormat.DataBlockLengthPaddingMultiple - 1U);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
		private static ErrorCode ParseReadEventsRequestHeaderBlock(byte[] request, ref uint ib, ref uint cb, out EventHistory.ReadEventsFlags flags, out long startCounter, out uint eventsWant, out uint eventsToCheck)
		{
			flags = EventHistory.ReadEventsFlags.None;
			startCounter = 0L;
			eventsWant = 0U;
			eventsToCheck = 0U;
			uint num;
			ErrorCode first = AdminRpcParseFormat.CheckDataBlock(request, ib, cb, out num);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)48213U);
			}
			if (num != AdminRpcParseFormat.ReadEventsRequestHeaderLengthV1 && num != AdminRpcParseFormat.ReadEventsRequestHeaderLengthV2)
			{
				return ErrorCode.CreateRpcFormat((LID)64597U);
			}
			flags = (EventHistory.ReadEventsFlags)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.ReadEventsRequestHeaderFlagsOffset));
			startCounter = ParseSerialize.ParseInt64(request, (int)(ib + AdminRpcParseFormat.ReadEventsRequestHeaderStartCounterOffset));
			eventsWant = (uint)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.ReadEventsRequestHeaderEventsWantOffset));
			if (num == AdminRpcParseFormat.ReadEventsRequestHeaderLengthV2)
			{
				eventsToCheck = (uint)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.ReadEventsRequestHeaderEventsToCheckOffset));
			}
			uint num2 = AdminRpcParseFormat.ActualBlockLength(num, cb);
			ib += num2;
			cb -= num2;
			return ErrorCode.NoError;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C588 File Offset: 0x0000A788
		private static ErrorCode ParseReadEventsRestrictionBlock(Context context, byte[] request, ref uint ib, ref uint cb, out Restriction restriction)
		{
			restriction = null;
			uint num;
			ErrorCode first = AdminRpcParseFormat.CheckDataBlock(request, ib, cb, out num);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)40021U);
			}
			if (num > AdminRpcParseFormat.DataBlockLengthLength)
			{
				int num2 = (int)(ib + AdminRpcParseFormat.DataBlockLengthLength);
				restriction = Restriction.Deserialize(context, request, ref num2, (int)(ib + num), null, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Event);
			}
			uint num3 = AdminRpcParseFormat.ActualBlockLength(num, cb);
			ib += num3;
			cb -= num3;
			return ErrorCode.NoError;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C604 File Offset: 0x0000A804
		private static ErrorCode ParseWriteEventsRequestHeaderBlock(byte[] request, ref uint ib, ref uint cb, out int flags, out uint eventsCount)
		{
			flags = 0;
			eventsCount = 0U;
			uint num;
			ErrorCode first = AdminRpcParseFormat.CheckDataBlock(request, ib, cb, out num);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)56405U);
			}
			if (num != AdminRpcParseFormat.WriteEventsRequestHeaderLength)
			{
				return ErrorCode.CreateRpcFormat((LID)44117U);
			}
			flags = ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.WriteEventsRequestHeaderFlagsOffset));
			eventsCount = (uint)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.WriteEventsRequestHeaderEventsCountOffset));
			uint num2 = AdminRpcParseFormat.ActualBlockLength(num, cb);
			ib += num2;
			cb -= num2;
			return ErrorCode.NoError;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C698 File Offset: 0x0000A898
		public static ErrorCode ParseEventList(byte[] request, bool preserveUntrustworthyData, ref uint ib, ref uint cb, uint eventsCount, out List<EventEntry> events)
		{
			events = null;
			if (cb / AdminRpcParseFormat.PaddedBlockLength(AdminRpcParseFormat.EventEntryLengthV1) < eventsCount)
			{
				return ErrorCode.CreateRpcFormat((LID)60501U);
			}
			events = new List<EventEntry>((int)eventsCount);
			for (uint num = 0U; num < eventsCount; num += 1U)
			{
				uint num2;
				ErrorCode first = AdminRpcParseFormat.CheckDataBlock(request, ib, cb, out num2);
				if (first != ErrorCode.NoError)
				{
					return first.Propagate((LID)35925U);
				}
				if (num2 <= AdminRpcParseFormat.EventEntryLengthV7 && num2 != AdminRpcParseFormat.EventEntryLengthV1 && num2 != AdminRpcParseFormat.EventEntryLengthV2 && num2 != AdminRpcParseFormat.EventEntryLengthV3 && num2 != AdminRpcParseFormat.EventEntryLengthV4 && num2 != AdminRpcParseFormat.EventEntryLengthV5 && num2 != AdminRpcParseFormat.EventEntryLengthV6 && num2 != AdminRpcParseFormat.EventEntryLengthV7)
				{
					DiagnosticContext.TraceDword((LID)63824U, num2);
					return ErrorCode.CreateRpcFormat((LID)52309U);
				}
				EventType eventType = (EventType)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.EventEntryMaskOffset));
				long eventCounter = ParseSerialize.ParseInt64(request, (int)(ib + AdminRpcParseFormat.EventEntryEventCounterOffset));
				DateTime createTime = ParseSerialize.ParseFileTime(request, (int)(ib + AdminRpcParseFormat.EventEntryCreateTimeOffset));
				int transactionId = ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.EventEntryTransacIdOffset));
				int? itemCount = AdminRpcParseFormat.ReadInt32OrNull(request, ib + AdminRpcParseFormat.EventEntryItemCountOffset, -1);
				int? unreadCount = AdminRpcParseFormat.ReadInt32OrNull(request, ib + AdminRpcParseFormat.EventEntryUnreadCountOffset, -1);
				EventFlags flags = (EventFlags)ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.EventEntryFlagsOffset));
				Guid value = ParseSerialize.ParseGuid(request, (int)(ib + AdminRpcParseFormat.EventEntryMailboxGuidOffset));
				Guid value2 = ParseSerialize.ParseGuid(request, (int)(ib + AdminRpcParseFormat.EventEntryMapiEntryIdGuidOffset));
				byte[] fid = AdminRpcParseFormat.ReadByteArrayOrNull(request, ib + AdminRpcParseFormat.EventEntryFidOffset, AdminRpcParseFormat.EventEntryFidLength);
				byte[] mid = AdminRpcParseFormat.ReadByteArrayOrNull(request, ib + AdminRpcParseFormat.EventEntryMidOffset, AdminRpcParseFormat.EventEntryMidLength);
				byte[] parentFid = AdminRpcParseFormat.ReadByteArrayOrNull(request, ib + AdminRpcParseFormat.EventEntryParentFidOffset, AdminRpcParseFormat.EventEntryParentFidLength);
				byte[] oldFid = AdminRpcParseFormat.ReadByteArrayOrNull(request, ib + AdminRpcParseFormat.EventEntryOldFidOffset, AdminRpcParseFormat.EventEntryOldFidLength);
				byte[] oldMid = AdminRpcParseFormat.ReadByteArrayOrNull(request, ib + AdminRpcParseFormat.EventEntryOldMidOffset, AdminRpcParseFormat.EventEntryOldMidLength);
				byte[] oldParentFid = AdminRpcParseFormat.ReadByteArrayOrNull(request, ib + AdminRpcParseFormat.EventEntryOldParentFidOffset, AdminRpcParseFormat.EventEntryOldParentFidLength);
				string objectClass = AdminRpcParseFormat.ReadAsciiStringOrNull(request, ib + AdminRpcParseFormat.EventEntryObjectClassOffset, AdminRpcParseFormat.EventEntryObjectClassLength);
				ExtendedEventFlags? extendedFlags = null;
				ClientType clientType = ClientType.User;
				byte[] sid = null;
				int? documentId = null;
				int? mailboxNumber = null;
				TenantHint empty = TenantHint.Empty;
				Guid? unifiedMailboxGuid = null;
				if (num2 > AdminRpcParseFormat.EventEntryLengthV1)
				{
					long? num3 = AdminRpcParseFormat.ReadInt64OrNull(request, ib + AdminRpcParseFormat.EventEntryExtendedFlagsOffset, 0L);
					extendedFlags = ((num3 != null) ? new ExtendedEventFlags?((ExtendedEventFlags)num3.GetValueOrDefault()) : null);
				}
				if (num2 > AdminRpcParseFormat.EventEntryLengthV2)
				{
					int num4 = ParseSerialize.ParseInt32(request, (int)(ib + AdminRpcParseFormat.EventEntryClientTypeOffset));
					if (preserveUntrustworthyData)
					{
						clientType = (ClientType)num4;
					}
					else
					{
						clientType = ((num4 == 0) ? ClientType.User : ((ClientType)num4));
					}
					sid = AdminRpcParseFormat.ReadSidOrNull(request, ib + AdminRpcParseFormat.EventEntrySidOffset, AdminRpcParseFormat.EventEntrySidLength);
				}
				if (num2 > AdminRpcParseFormat.EventEntryLengthV3)
				{
					documentId = AdminRpcParseFormat.ReadInt32OrNull(request, ib + AdminRpcParseFormat.EventEntryDocIdOffset, 0);
				}
				if (num2 > AdminRpcParseFormat.EventEntryLengthV4 && preserveUntrustworthyData)
				{
					mailboxNumber = AdminRpcParseFormat.ReadInt32OrNull(request, ib + AdminRpcParseFormat.EventEntryMailboxNumberOffset, 0);
				}
				if (num2 > AdminRpcParseFormat.EventEntryLengthV5)
				{
					int? num5 = AdminRpcParseFormat.ReadInt32OrNull(request, ib + AdminRpcParseFormat.EventEntryTenantHintBlobSizeOffset, -1);
					uint? num6 = (num5 != null) ? new uint?((uint)num5.GetValueOrDefault()) : null;
					if (num6 == null || num6.Value > AdminRpcParseFormat.EventEntryTenantHintBlobLength)
					{
						return ErrorCode.CreateCorruptData((LID)49232U);
					}
					if (preserveUntrustworthyData)
					{
						byte[] tenantHintBlob = AdminRpcParseFormat.ReadByteArrayOrNull(false, request, ib + AdminRpcParseFormat.EventEntryTenantHintBlobOffset, num6.Value);
						empty = new TenantHint(tenantHintBlob);
					}
				}
				if (num2 > AdminRpcParseFormat.EventEntryLengthV6 && preserveUntrustworthyData)
				{
					unifiedMailboxGuid = new Guid?(ParseSerialize.ParseGuid(request, (int)(ib + AdminRpcParseFormat.EventEntryUnifiedMailboxGuidOffset)));
				}
				EventEntry item = new EventEntry(eventCounter, createTime, transactionId, eventType, mailboxNumber, new Guid?(value), new Guid?(value2), objectClass, fid, mid, parentFid, oldFid, oldMid, oldParentFid, itemCount, unreadCount, flags, extendedFlags, clientType, sid, documentId, empty, unifiedMailboxGuid);
				events.Add(item);
				uint num7 = AdminRpcParseFormat.ActualBlockLength(num2, cb);
				ib += num7;
				cb -= num7;
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public static int SerializeIntegrityCheckRequest(byte[] buffer, ref int pos, int posMax, Guid requestGuid, uint flags, TaskId[] taskIds, StorePropTag[] propTags)
		{
			int num = 0;
			num += 4;
			int num2 = pos;
			if (buffer != null)
			{
				pos += 4;
			}
			num += 4;
			if (buffer != null)
			{
				AdminRpcParseFormat.CheckBounds(pos, posMax, 4);
				ParseSerialize.SerializeInt32((int)flags, buffer, pos);
				pos += 4;
			}
			num += 16;
			if (buffer != null)
			{
				AdminRpcParseFormat.CheckBounds(pos, posMax, 16);
				ParseSerialize.SerializeGuid(requestGuid, buffer, pos);
				pos += 16;
			}
			num += 4;
			if (buffer != null)
			{
				AdminRpcParseFormat.CheckBounds(pos, posMax, 4);
				ParseSerialize.SerializeInt32((taskIds == null) ? 0 : taskIds.Length, buffer, pos);
				pos += 4;
			}
			if (taskIds != null)
			{
				num += 4 * taskIds.Length;
				foreach (TaskId value in taskIds)
				{
					if (buffer != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, 4);
						ParseSerialize.SerializeInt32((int)value, buffer, pos);
						pos += 4;
					}
				}
			}
			num += 4;
			if (buffer != null)
			{
				AdminRpcParseFormat.CheckBounds(pos, posMax, 4);
				ParseSerialize.SerializeInt32((propTags == null) ? 0 : propTags.Length, buffer, pos);
				pos += 4;
			}
			if (propTags != null)
			{
				num += 4 * propTags.Length;
				foreach (StorePropTag storePropTag in propTags)
				{
					if (buffer != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, 4);
						ParseSerialize.SerializeInt32((int)storePropTag.PropTag, buffer, pos);
						pos += 4;
					}
				}
			}
			if (buffer != null)
			{
				AdminRpcParseFormat.CheckBounds(num2, posMax, 4);
				ParseSerialize.SerializeInt32(num, buffer, num2);
			}
			return num;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public static ErrorCode ParseIntegrityCheckRequest(byte[] request, out int flags, out Guid requestGuid, out TaskId[] taskIds, out StorePropTag[] propTags)
		{
			flags = 0;
			requestGuid = Guid.Empty;
			taskIds = null;
			propTags = null;
			uint num = 0U;
			uint cb = (uint)((request == null) ? 0 : request.Length);
			uint num2;
			ErrorCode errorCode = AdminRpcParseFormat.CheckDataBlock(request, num, cb, out num2);
			if (errorCode != ErrorCode.NoError)
			{
				return errorCode.Propagate((LID)33440U);
			}
			int num3 = 32;
			if (num2 != (uint)request.Length || num2 < (uint)num3)
			{
				return ErrorCode.CreateRpcFormat((LID)49824U);
			}
			num += 4U;
			flags = ParseSerialize.ParseInt32(request, (int)num);
			num += 4U;
			requestGuid = ParseSerialize.ParseGuid(request, (int)num);
			num += 16U;
			int num4 = ParseSerialize.ParseInt32(request, (int)num);
			num += 4U;
			taskIds = new TaskId[num4];
			for (int i = 0; i < num4; i++)
			{
				if (num + 4U > num2)
				{
					return ErrorCode.CreateRpcFormat((LID)54620U);
				}
				uint num5 = (uint)ParseSerialize.ParseInt32(request, (int)num);
				taskIds[i] = (TaskId)num5;
				num += 4U;
			}
			int num6 = ParseSerialize.ParseInt32(request, (int)num);
			propTags = new StorePropTag[num6];
			num += 4U;
			for (int j = 0; j < num6; j++)
			{
				if (num + 4U > num2)
				{
					return ErrorCode.CreateRpcFormat((LID)42332U);
				}
				uint legacyPropTag = (uint)ParseSerialize.ParseInt32(request, (int)num);
				propTags[j] = LegacyHelper.ConvertFromLegacyPropTag(legacyPropTag, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.IsIntegJob, null, true);
				num += 4U;
			}
			return errorCode;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000CD44 File Offset: 0x0000AF44
		public static int SerializeIntegrityCheckResponse(byte[] buffer, ref int pos, int posMax, IEnumerable<Properties> rows)
		{
			int num = 0;
			int num2 = 0;
			num += 4;
			int num3 = pos;
			pos += 4;
			num += 4;
			int num4 = pos;
			pos += 4;
			if (rows != null)
			{
				foreach (Properties properties in rows)
				{
					num2++;
					num += AdminRpcParseFormat.SetValues(buffer, ref pos, posMax, properties);
				}
			}
			if (buffer != null)
			{
				AdminRpcParseFormat.CheckBounds(num3, posMax, 4);
				ParseSerialize.SerializeInt32(num, buffer, num3);
				AdminRpcParseFormat.CheckBounds(num4, posMax, 4);
				ParseSerialize.SerializeInt32(num2, buffer, num4);
			}
			return num;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public static void FormatEventList(List<EventEntry> events, byte[] buffer, uint ib)
		{
			for (int i = 0; i < events.Count; i++)
			{
				EventEntry eventEntry = events[i];
				ParseSerialize.SerializeInt32((int)AdminRpcParseFormat.EventEntryLengthV7, buffer, (int)(ib + AdminRpcParseFormat.DataBlockLengthOffset));
				ParseSerialize.SerializeInt32((int)eventEntry.EventType, buffer, (int)(ib + AdminRpcParseFormat.EventEntryMaskOffset));
				ParseSerialize.SerializeInt64(eventEntry.EventCounter, buffer, (int)(ib + AdminRpcParseFormat.EventEntryEventCounterOffset));
				ParseSerialize.SerializeFileTime(eventEntry.CreateTime, buffer, (int)(ib + AdminRpcParseFormat.EventEntryCreateTimeOffset));
				ParseSerialize.SerializeInt32(eventEntry.TransactionId, buffer, (int)(ib + AdminRpcParseFormat.EventEntryTransacIdOffset));
				ParseSerialize.SerializeInt32((eventEntry.ItemCount == null) ? -1 : eventEntry.ItemCount.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryItemCountOffset));
				ParseSerialize.SerializeInt32((eventEntry.UnreadCount == null) ? -1 : eventEntry.UnreadCount.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryUnreadCountOffset));
				ParseSerialize.SerializeInt32((int)eventEntry.Flags, buffer, (int)(ib + AdminRpcParseFormat.EventEntryFlagsOffset));
				ParseSerialize.SerializeGuid((eventEntry.MailboxGuid == null) ? Guid.Empty : eventEntry.MailboxGuid.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryMailboxGuidOffset));
				ParseSerialize.SerializeGuid((eventEntry.MapiEntryIdGuid == null) ? Guid.Empty : eventEntry.MapiEntryIdGuid.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryMapiEntryIdGuidOffset));
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.Fid24, buffer, ib + AdminRpcParseFormat.EventEntryFidOffset, AdminRpcParseFormat.EventEntryFidLength);
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.Mid24, buffer, ib + AdminRpcParseFormat.EventEntryMidOffset, AdminRpcParseFormat.EventEntryMidLength);
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.ParentFid24, buffer, ib + AdminRpcParseFormat.EventEntryParentFidOffset, AdminRpcParseFormat.EventEntryParentFidLength);
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.OldFid24, buffer, ib + AdminRpcParseFormat.EventEntryOldFidOffset, AdminRpcParseFormat.EventEntryOldFidLength);
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.OldMid24, buffer, ib + AdminRpcParseFormat.EventEntryOldMidOffset, AdminRpcParseFormat.EventEntryOldMidLength);
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.OldParentFid24, buffer, ib + AdminRpcParseFormat.EventEntryOldParentFidOffset, AdminRpcParseFormat.EventEntryOldParentFidLength);
				AdminRpcParseFormat.WriteAsciiStringIfNotNull(eventEntry.ObjectClass, buffer, ib + AdminRpcParseFormat.EventEntryObjectClassOffset, AdminRpcParseFormat.EventEntryObjectClassLength);
				ParseSerialize.SerializeInt64((long)((eventEntry.ExtendedFlags == null) ? ExtendedEventFlags.None : eventEntry.ExtendedFlags.Value), buffer, (int)(ib + AdminRpcParseFormat.EventEntryExtendedFlagsOffset));
				ParseSerialize.SerializeInt32((int)eventEntry.ClientType, buffer, (int)(ib + AdminRpcParseFormat.EventEntryClientTypeOffset));
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.Sid, buffer, ib + AdminRpcParseFormat.EventEntrySidOffset, AdminRpcParseFormat.EventEntrySidLength);
				ParseSerialize.SerializeInt32((eventEntry.DocumentId == null) ? 0 : eventEntry.DocumentId.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryDocIdOffset));
				ParseSerialize.SerializeInt32((eventEntry.MailboxNumber == null) ? 0 : eventEntry.MailboxNumber.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryMailboxNumberOffset));
				ParseSerialize.SerializeInt32(eventEntry.TenantHint.TenantHintBlobSize, buffer, (int)(ib + AdminRpcParseFormat.EventEntryTenantHintBlobSizeOffset));
				AdminRpcParseFormat.WriteBytesIfNotNull(eventEntry.TenantHint.TenantHintBlob, buffer, ib + AdminRpcParseFormat.EventEntryTenantHintBlobOffset, AdminRpcParseFormat.EventEntryTenantHintBlobLength);
				ParseSerialize.SerializeGuid((eventEntry.UnifiedMailboxGuid == null) ? Guid.Empty : eventEntry.UnifiedMailboxGuid.Value, buffer, (int)(ib + AdminRpcParseFormat.EventEntryUnifiedMailboxGuidOffset));
				ib += AdminRpcParseFormat.PaddedBlockLength(AdminRpcParseFormat.EventEntryLengthV7);
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000D13C File Offset: 0x0000B33C
		public static void SerializeMdbStatus(List<AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus> mdbStatus, bool basicInformation, out byte[] mdbStatusRaw)
		{
			uint num = (uint)((mdbStatus == null) ? 0 : mdbStatus.Count);
			int num2 = 0;
			foreach (AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus mdbStatus2 in mdbStatus)
			{
				num2 += (int)AdminRpcParseFormat.SizeofMdbStatusRaw;
				if (!basicInformation)
				{
					num2 += mdbStatus2.MdbName.Length + 1 + mdbStatus2.VServerName.Length + 1 + mdbStatus2.LegacyDN.Length + 1;
				}
			}
			if (num2 == 0)
			{
				mdbStatusRaw = null;
				return;
			}
			mdbStatusRaw = new byte[num2];
			uint num3 = 0U;
			if (!basicInformation)
			{
				num3 = num * AdminRpcParseFormat.SizeofMdbStatusRaw;
			}
			for (int i = 0; i < mdbStatus.Count; i++)
			{
				AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus mdbStatus3 = mdbStatus[i];
				uint num4 = (uint)(i * (int)AdminRpcParseFormat.SizeofMdbStatusRaw);
				ParseSerialize.SerializeGuid(mdbStatus3.GuidMdb, mdbStatusRaw, (int)num4);
				ParseSerialize.SerializeInt32((int)mdbStatus3.UlStatus, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusUlStatusOffset));
				if (!basicInformation)
				{
					ParseSerialize.SerializeInt32(mdbStatus3.MdbName.Length + 1, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbMdbNameOffset));
					ParseSerialize.SerializeInt32(mdbStatus3.VServerName.Length + 1, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbVServerNameOffset));
					ParseSerialize.SerializeInt32(mdbStatus3.LegacyDN.Length + 1, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbMdbLegacyDNOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbStorageGroupNameOffset));
					uint num5 = num3;
					ParseSerialize.SerializeInt32((int)num5, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbMdbNameOffset));
					num5 += (uint)(mdbStatus3.MdbName.Length + 1);
					ParseSerialize.SerializeInt32((int)num5, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbVServerNameOffset));
					num5 += (uint)(mdbStatus3.VServerName.Length + 1);
					ParseSerialize.SerializeInt32((int)num5, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbMdbLegacyDNOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbStorageGroupNameOffset));
					if (mdbStatus3.MdbName != Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(mdbStatus3.MdbName)))
					{
						throw new CorruptDataException((LID)55368U, "Database name string is not ASCII.");
					}
					Encoding.ASCII.GetBytes(mdbStatus3.MdbName, 0, mdbStatus3.MdbName.Length, mdbStatusRaw, (int)num3);
					num3 += (uint)(mdbStatus3.MdbName.Length + 1);
					if (mdbStatus3.VServerName != Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(mdbStatus3.VServerName)))
					{
						throw new CorruptDataException((LID)43080U, "Virtual ServerName string is not ASCII.");
					}
					Encoding.ASCII.GetBytes(mdbStatus3.VServerName, 0, mdbStatus3.VServerName.Length, mdbStatusRaw, (int)num3);
					num3 += (uint)(mdbStatus3.VServerName.Length + 1);
					if (mdbStatus3.LegacyDN != Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(mdbStatus3.LegacyDN)))
					{
						throw new CorruptDataException((LID)59464U, "LegacyDN string is not ASCII.");
					}
					Encoding.ASCII.GetBytes(mdbStatus3.LegacyDN, 0, mdbStatus3.LegacyDN.Length, mdbStatusRaw, (int)num3);
					num3 += (uint)(mdbStatus3.LegacyDN.Length + 1);
				}
				else
				{
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbMdbNameOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbVServerNameOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbMdbLegacyDNOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusCbStorageGroupNameOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbMdbNameOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbVServerNameOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbMdbLegacyDNOffset));
					ParseSerialize.SerializeInt32(0, mdbStatusRaw, (int)(num4 + AdminRpcParseFormat.MdbStatusIbStorageGroupNameOffset));
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000D500 File Offset: 0x0000B700
		public static byte[] ReadSidOrNull(byte[] buffer, uint ib, uint cb)
		{
			if (cb < 8U || buffer[(int)((UIntPtr)ib)] != 1 || buffer[(int)((UIntPtr)(ib + 1U))] > 15 || cb < (uint)(8 + buffer[(int)((UIntPtr)(ib + 1U))] * 4))
			{
				return null;
			}
			return ParseSerialize.ParseBinary(buffer, (int)ib, (int)(8 + buffer[(int)((UIntPtr)(ib + 1U))] * 4));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000D537 File Offset: 0x0000B737
		private static bool IsZeroByteArray(byte[] buffer, uint ib, uint cb)
		{
			while (cb-- != 0U)
			{
				if (buffer[(int)((UIntPtr)(ib++))] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D551 File Offset: 0x0000B751
		public static byte[] ReadByteArrayOrNull(byte[] buffer, uint ib, uint cb)
		{
			return AdminRpcParseFormat.ReadByteArrayOrNull(true, buffer, ib, cb);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D55C File Offset: 0x0000B75C
		public static byte[] ReadByteArrayOrNull(bool checkIsZeroByteArray, byte[] buffer, uint ib, uint cb)
		{
			if (checkIsZeroByteArray && AdminRpcParseFormat.IsZeroByteArray(buffer, ib, cb))
			{
				return null;
			}
			return ParseSerialize.ParseBinary(buffer, (int)ib, (int)cb);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000D578 File Offset: 0x0000B778
		public static string ReadAsciiStringOrNull(byte[] buffer, uint ib, uint cb)
		{
			uint num = 0U;
			while (num != cb && buffer[(int)((UIntPtr)(ib + num))] != 0)
			{
				num += 1U;
			}
			if (num != 0U)
			{
				return Encoding.ASCII.GetString(buffer, (int)ib, (int)num);
			}
			return null;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public static int? ReadInt32OrNull(byte[] buffer, uint ib, int defaultValue)
		{
			int num = ParseSerialize.ParseInt32(buffer, (int)ib);
			if (num != defaultValue)
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		public static long? ReadInt64OrNull(byte[] buffer, uint ib, long defaultValue)
		{
			long num = ParseSerialize.ParseInt64(buffer, (int)ib);
			if (num != defaultValue)
			{
				return new long?(num);
			}
			return null;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000D601 File Offset: 0x0000B801
		public static void WriteBytesIfNotNull(byte[] bytes, byte[] buffer, uint ib, uint cb)
		{
			if (bytes != null)
			{
				Buffer.BlockCopy(bytes, 0, buffer, (int)ib, Math.Min(bytes.Length, (int)cb));
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000D618 File Offset: 0x0000B818
		public static void WriteAsciiStringIfNotNull(string str, byte[] buffer, uint ib, uint cb)
		{
			if (str != null)
			{
				if ((long)str.Length >= (long)((ulong)cb))
				{
					str = str.Substring(0, (int)(cb - 1U));
				}
				ParseSerialize.SerializeAsciiString(str, buffer, (int)ib);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000D63D File Offset: 0x0000B83D
		public static void CheckBounds(int pos, int posMax, int sizeNeeded)
		{
			if (posMax < pos + sizeNeeded)
			{
				throw new BufferTooSmall((LID)55736U, "Rpc buffer is too small for results!");
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000D65C File Offset: 0x0000B85C
		public static int SetValue(byte[] buff, ref int pos, int posMax, StorePropTag propTag, object propValue)
		{
			int num = 0;
			int num2 = -1;
			PropertyType propertyType = propTag.ExternalType;
			if (propertyType == PropertyType.Unspecified)
			{
				propertyType = propTag.PropType;
			}
			else if (propTag.PropType == PropertyType.Error)
			{
				propertyType = PropertyType.Error;
			}
			PropertyType propertyType2 = propertyType;
			if (propertyType2 <= PropertyType.SRestriction)
			{
				if (propertyType2 <= PropertyType.Unicode)
				{
					switch (propertyType2)
					{
					case PropertyType.Null:
						return num;
					case PropertyType.Int16:
						num = 2;
						if (buff == null)
						{
							return num;
						}
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						if (propValue is short)
						{
							ParseSerialize.SetWord(buff, ref pos, (short)propValue);
							return num;
						}
						string.Format("Property {0} is type {1} and must be Int16", propTag, propValue.GetType());
						return num;
					case PropertyType.Int32:
						num = 4;
						if (buff == null)
						{
							return num;
						}
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						if (propValue is int)
						{
							ParseSerialize.SetDword(buff, ref pos, (int)propValue);
							return num;
						}
						string.Format("Property {0} is type {1} and must be Int32", propTag, propValue.GetType());
						return num;
					case PropertyType.Real32:
						num = 4;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetFloat(buff, ref pos, (float)propValue);
							return num;
						}
						return num;
					case PropertyType.Real64:
					case PropertyType.AppTime:
						num = 8;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetDouble(buff, ref pos, (double)propValue);
							return num;
						}
						return num;
					case PropertyType.Currency:
						break;
					case (PropertyType)8:
					case (PropertyType)9:
						goto IL_876;
					case PropertyType.Error:
						num = 4;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetDword(buff, ref pos, (int)((ErrorCodeValue)propValue));
							return num;
						}
						return num;
					case PropertyType.Boolean:
						num = 1;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetByte(buff, ref pos, ((bool)propValue) ? 1 : 0);
							return num;
						}
						return num;
					default:
						if (propertyType2 != PropertyType.Int64)
						{
							switch (propertyType2)
							{
							case PropertyType.String8:
								num2 = ((string)propValue).IndexOf('\0');
								if (num2 > 0)
								{
									propValue = ((string)propValue).Substring(0, num2);
								}
								num = ((string)propValue).Length + 1;
								if (buff != null)
								{
									AdminRpcParseFormat.CheckBounds(pos, posMax, num);
									ParseSerialize.SetASCIIString(buff, ref pos, (string)propValue);
									return num;
								}
								return num;
							case PropertyType.Unicode:
								num2 = ((string)propValue).IndexOf('\0');
								if (num2 > 0)
								{
									propValue = ((string)propValue).Substring(0, num2);
								}
								num = (((string)propValue).Length + 1) * 2;
								if (buff != null)
								{
									AdminRpcParseFormat.CheckBounds(pos, posMax, num);
									ParseSerialize.SetUnicodeString(buff, ref pos, (string)propValue);
									return num;
								}
								return num;
							default:
								goto IL_876;
							}
						}
						break;
					}
					num = 8;
					if (buff == null)
					{
						return num;
					}
					AdminRpcParseFormat.CheckBounds(pos, posMax, num);
					if (propValue is long)
					{
						ParseSerialize.SetQword(buff, ref pos, (long)propValue);
						return num;
					}
					string.Format("Property {0} is type {1} and must be Int64", propTag, propValue.GetType());
					return num;
				}
				else if (propertyType2 != PropertyType.SysTime)
				{
					if (propertyType2 != PropertyType.Guid)
					{
						switch (propertyType2)
						{
						case PropertyType.SvrEid:
							break;
						case (PropertyType)252:
							goto IL_876;
						case PropertyType.SRestriction:
							if (!(propValue is byte[]))
							{
								string.Format("Property {0} is type {1} and must be byte[]", propTag, propValue.GetType());
								return num;
							}
							num = ((byte[])propValue).Length;
							if (buff != null)
							{
								AdminRpcParseFormat.CheckBounds(pos, posMax, num);
								ParseSerialize.SetRestrictionByteArray(buff, ref pos, (byte[])propValue);
								return num;
							}
							return num;
						default:
							goto IL_876;
						}
					}
					else
					{
						num = 16;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetGuid(buff, ref pos, (Guid)propValue);
							return num;
						}
						return num;
					}
				}
				else
				{
					num = 8;
					if (buff != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						ParseSerialize.SetSysTime(buff, ref pos, (DateTime)propValue);
						return num;
					}
					return num;
				}
			}
			else if (propertyType2 <= PropertyType.MVInt64)
			{
				if (propertyType2 != PropertyType.Binary)
				{
					switch (propertyType2)
					{
					case PropertyType.MVInt16:
					{
						short[] array = (short[])propValue;
						num = 4;
						num += array.Length * 2;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetDword(buff, ref pos, array.Length);
							for (int i = 0; i < array.Length; i++)
							{
								ParseSerialize.SetWord(buff, ref pos, array[i]);
							}
							return num;
						}
						return num;
					}
					case PropertyType.MVInt32:
					{
						int[] array2 = (int[])propValue;
						num = 4;
						num += array2.Length * 4;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetDword(buff, ref pos, array2.Length);
							for (int j = 0; j < array2.Length; j++)
							{
								ParseSerialize.SetDword(buff, ref pos, array2[j]);
							}
							return num;
						}
						return num;
					}
					case PropertyType.MVReal32:
					{
						float[] array3 = (float[])propValue;
						num = 4;
						num += array3.Length * 4;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetDword(buff, ref pos, array3.Length);
							for (int k = 0; k < array3.Length; k++)
							{
								ParseSerialize.SetFloat(buff, ref pos, array3[k]);
							}
							return num;
						}
						return num;
					}
					case PropertyType.MVReal64:
					case PropertyType.MVAppTime:
					{
						double[] array4 = (double[])propValue;
						num = 4;
						num += array4.Length * 8;
						if (buff != null)
						{
							AdminRpcParseFormat.CheckBounds(pos, posMax, num);
							ParseSerialize.SetDword(buff, ref pos, array4.Length);
							for (int l = 0; l < array4.Length; l++)
							{
								ParseSerialize.SetDouble(buff, ref pos, array4[l]);
							}
							return num;
						}
						return num;
					}
					case PropertyType.MVCurrency:
						break;
					default:
						if (propertyType2 != PropertyType.MVInt64)
						{
							goto IL_876;
						}
						break;
					}
					long[] array5 = (long[])propValue;
					num = 4;
					num += array5.Length * 8;
					if (buff != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						ParseSerialize.SetDword(buff, ref pos, array5.Length);
						for (int m = 0; m < array5.Length; m++)
						{
							ParseSerialize.SetQword(buff, ref pos, array5[m]);
						}
						return num;
					}
					return num;
				}
			}
			else if (propertyType2 <= PropertyType.MVSysTime)
			{
				switch (propertyType2)
				{
				case PropertyType.MVString8:
				{
					string[] array6 = (string[])propValue;
					num = 4;
					for (int n = 0; n < array6.Length; n++)
					{
						num2 = array6[n].IndexOf('\0');
						if (num2 > 0)
						{
							num += num2 + 1;
						}
						else
						{
							num += array6[n].Length + 1;
						}
					}
					if (buff != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						ParseSerialize.SetDword(buff, ref pos, (uint)array6.Length);
						for (int num3 = 0; num3 < array6.Length; num3++)
						{
							num2 = array6[num3].IndexOf('\0');
							if (num2 > 0)
							{
								string str = array6[num3].Substring(0, num2);
								ParseSerialize.SetASCIIString(buff, ref pos, str);
							}
							else
							{
								ParseSerialize.SetASCIIString(buff, ref pos, array6[num3]);
							}
						}
						return num;
					}
					return num;
				}
				case PropertyType.MVUnicode:
				{
					string[] array7 = (string[])propValue;
					num = 4;
					bool flag = false;
					for (int num4 = 0; num4 < array7.Length; num4++)
					{
						num2 = array7[num4].IndexOf('\0');
						if (num2 > 0)
						{
							flag = true;
							num += (num2 + 1) * 2;
						}
						else
						{
							num += (array7[num4].Length + 1) * 2;
						}
					}
					if (buff != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						ParseSerialize.SetDword(buff, ref pos, (uint)array7.Length);
						for (int num5 = 0; num5 < array7.Length; num5++)
						{
							if (flag)
							{
								num2 = array7[num5].IndexOf('\0');
							}
							if (num2 > 0)
							{
								string str2 = array7[num5].Substring(0, num2);
								ParseSerialize.SetUnicodeString(buff, ref pos, str2);
							}
							else
							{
								ParseSerialize.SetUnicodeString(buff, ref pos, array7[num5]);
							}
						}
						return num;
					}
					return num;
				}
				default:
				{
					if (propertyType2 != PropertyType.MVSysTime)
					{
						goto IL_876;
					}
					DateTime[] array8 = (DateTime[])propValue;
					num = 4;
					num += array8.Length * 8;
					if (buff != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, num);
						ParseSerialize.SetDword(buff, ref pos, array8.Length);
						for (int num6 = 0; num6 < array8.Length; num6++)
						{
							ParseSerialize.SetSysTime(buff, ref pos, array8[num6]);
						}
						return num;
					}
					return num;
				}
				}
			}
			else if (propertyType2 != PropertyType.MVGuid)
			{
				if (propertyType2 != PropertyType.MVBinary)
				{
					goto IL_876;
				}
				byte[][] array9 = (byte[][])propValue;
				num = 4;
				for (int num7 = 0; num7 < array9.Length; num7++)
				{
					num += array9[num7].Length + 2;
				}
				if (buff != null)
				{
					AdminRpcParseFormat.CheckBounds(pos, posMax, num);
					ParseSerialize.SetDword(buff, ref pos, array9.Length);
					for (int num8 = 0; num8 < array9.Length; num8++)
					{
						ParseSerialize.SetByteArray(buff, ref pos, array9[num8]);
					}
					return num;
				}
				return num;
			}
			else
			{
				Guid[] array10 = (Guid[])propValue;
				num = 4;
				num += array10.Length * 16;
				if (buff != null)
				{
					AdminRpcParseFormat.CheckBounds(pos, posMax, num);
					ParseSerialize.SetDword(buff, ref pos, array10.Length);
					for (int num9 = 0; num9 < array10.Length; num9++)
					{
						ParseSerialize.SetGuid(buff, ref pos, array10[num9]);
					}
					return num;
				}
				return num;
			}
			if (!(propValue is byte[]))
			{
				string.Format("Property {0} is type {1} and must be byte[] or ExchangeId[] or Guid", propTag, propValue.GetType());
				return num;
			}
			num = ((byte[])propValue).Length + 2;
			if (buff != null)
			{
				AdminRpcParseFormat.CheckBounds(pos, posMax, num);
				ParseSerialize.SetByteArray(buff, ref pos, (byte[])propValue);
				return num;
			}
			return num;
			IL_876:
			throw new ExExceptionNoSupport((LID)43448U, "We do not support this property type (" + propTag.PropType + ") yet!");
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000DF0C File Offset: 0x0000C10C
		public static int SetValues(byte[] buff, ref int pos, int posMax, Properties properties)
		{
			bool flag = false;
			int num = 0;
			for (int i = 0; i < properties.Count; i++)
			{
				if (properties[i].IsError)
				{
					flag = true;
					break;
				}
			}
			num++;
			if (buff != null)
			{
				AdminRpcParseFormat.CheckBounds(pos, posMax, 1);
				ParseSerialize.SetByte(buff, ref pos, flag ? 1 : 0);
			}
			for (int j = 0; j < properties.Count; j++)
			{
				if (properties[j].Tag.ExternalType == PropertyType.Unspecified)
				{
					num += 2;
					if (buff != null)
					{
						PropertyType propType = properties[j].Tag.PropType;
						AdminRpcParseFormat.CheckBounds(pos, posMax, 2);
						ParseSerialize.SetWord(buff, ref pos, (ushort)propType);
					}
				}
				if (flag)
				{
					num++;
					if (buff != null)
					{
						AdminRpcParseFormat.CheckBounds(pos, posMax, 1);
						if (properties[j].IsError)
						{
							ParseSerialize.SetByte(buff, ref pos, (byte)properties[j].Tag.PropType);
						}
						else
						{
							ParseSerialize.SetByte(buff, ref pos, 0);
						}
					}
				}
				if (PropertyType.Null != properties[j].Tag.PropType)
				{
					num += AdminRpcParseFormat.SetValue(buff, ref pos, posMax, properties[j].Tag, properties[j].Value);
				}
			}
			return num;
		}

		// Token: 0x04000122 RID: 290
		public static readonly uint DataBlockLengthOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.DataBlock), "BlockLength"));

		// Token: 0x04000123 RID: 291
		public static readonly uint DataBlockLengthLength = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x04000124 RID: 292
		public static readonly uint DataBlockLengthPaddingMultiple = 8U;

		// Token: 0x04000125 RID: 293
		public static readonly uint EventEntryMaskOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "EventMask"));

		// Token: 0x04000126 RID: 294
		public static readonly uint EventEntryMaskLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000127 RID: 295
		public static readonly uint EventEntryEventCounterOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "EventCounter"));

		// Token: 0x04000128 RID: 296
		public static readonly uint EventEntryEventCounterLength = (uint)Marshal.SizeOf(typeof(long));

		// Token: 0x04000129 RID: 297
		public static readonly uint EventEntryCreateTimeOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "CreateTime"));

		// Token: 0x0400012A RID: 298
		public static readonly uint EventEntryCreateTimeLength = (uint)Marshal.SizeOf(typeof(long));

		// Token: 0x0400012B RID: 299
		public static readonly uint EventEntryTransacIdOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "TransacId"));

		// Token: 0x0400012C RID: 300
		public static readonly uint EventEntryTransacIdLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x0400012D RID: 301
		public static readonly uint EventEntryItemCountOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "ItemCount"));

		// Token: 0x0400012E RID: 302
		public static readonly uint EventEntryItemCountLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x0400012F RID: 303
		public static readonly uint EventEntryUnreadCountOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "UnreadCount"));

		// Token: 0x04000130 RID: 304
		public static readonly uint EventEntryUnreadCountLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000131 RID: 305
		public static readonly uint EventEntryFlagsOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "Flags"));

		// Token: 0x04000132 RID: 306
		public static readonly uint EventEntryFlagsLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000133 RID: 307
		public static readonly uint EventEntryMailboxGuidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "MailboxGuid"));

		// Token: 0x04000134 RID: 308
		public static readonly uint EventEntryMailboxGuidLength = (uint)Marshal.SizeOf(typeof(Guid));

		// Token: 0x04000135 RID: 309
		public static readonly uint EventEntryMapiEntryIdGuidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "MapiEntryIdGuid"));

		// Token: 0x04000136 RID: 310
		public static readonly uint EventEntryMapiEntryIdGuidLength = (uint)Marshal.SizeOf(typeof(Guid));

		// Token: 0x04000137 RID: 311
		public static readonly uint EventEntryFidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "Fid"));

		// Token: 0x04000138 RID: 312
		public static readonly uint EventEntryFidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Ltid));

		// Token: 0x04000139 RID: 313
		public static readonly uint EventEntryMidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "Mid"));

		// Token: 0x0400013A RID: 314
		public static readonly uint EventEntryMidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Ltid));

		// Token: 0x0400013B RID: 315
		public static readonly uint EventEntryParentFidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "ParentFid"));

		// Token: 0x0400013C RID: 316
		public static readonly uint EventEntryParentFidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Ltid));

		// Token: 0x0400013D RID: 317
		public static readonly uint EventEntryOldFidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "OldFid"));

		// Token: 0x0400013E RID: 318
		public static readonly uint EventEntryOldFidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Ltid));

		// Token: 0x0400013F RID: 319
		public static readonly uint EventEntryOldMidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "OldMid"));

		// Token: 0x04000140 RID: 320
		public static readonly uint EventEntryOldMidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Ltid));

		// Token: 0x04000141 RID: 321
		public static readonly uint EventEntryOldParentFidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "OldParentFid"));

		// Token: 0x04000142 RID: 322
		public static readonly uint EventEntryOldParentFidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Ltid));

		// Token: 0x04000143 RID: 323
		public static readonly uint EventEntryObjectClassOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV1), "ItemClass"));

		// Token: 0x04000144 RID: 324
		public static readonly uint EventEntryObjectClassLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.ObjectClass));

		// Token: 0x04000145 RID: 325
		public static readonly uint EventEntryLengthV1 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV1));

		// Token: 0x04000146 RID: 326
		public static readonly uint EventEntryExtendedFlagsOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV2), "ExtendedFlags"));

		// Token: 0x04000147 RID: 327
		public static readonly uint EventEntryExtendedFlagsLength = (uint)Marshal.SizeOf(typeof(long));

		// Token: 0x04000148 RID: 328
		public static readonly uint EventEntryLengthV2 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV2));

		// Token: 0x04000149 RID: 329
		public static readonly uint EventEntryClientTypeOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV3), "ClientType"));

		// Token: 0x0400014A RID: 330
		public static readonly uint EventEntryClientTypeLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x0400014B RID: 331
		public static readonly uint EventEntrySidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV3), "ClientSid"));

		// Token: 0x0400014C RID: 332
		public static readonly uint EventEntrySidLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.Sid));

		// Token: 0x0400014D RID: 333
		public static readonly uint EventEntryLengthV3 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV3));

		// Token: 0x0400014E RID: 334
		public static readonly uint EventEntryDocIdOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV4), "DocId"));

		// Token: 0x0400014F RID: 335
		public static readonly uint EventEntryDocIdLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000150 RID: 336
		public static readonly uint EventEntryLengthV4 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV4));

		// Token: 0x04000151 RID: 337
		public static readonly uint EventEntryMailboxNumberOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV5), "MailboxNumber"));

		// Token: 0x04000152 RID: 338
		public static readonly uint EventEntryMailboxNumberLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000153 RID: 339
		public static readonly uint EventEntryLengthV5 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV5));

		// Token: 0x04000154 RID: 340
		public static readonly uint EventEntryTenantHintBlobSizeOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV6), "TenantHintBlobSize"));

		// Token: 0x04000155 RID: 341
		public static readonly uint EventEntryTenantHintBlobSizeLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000156 RID: 342
		public static readonly uint EventEntryTenantHintBlobOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV6), "TenantHintBlob"));

		// Token: 0x04000157 RID: 343
		public static readonly uint EventEntryTenantHintBlobLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.TenantHintBlob));

		// Token: 0x04000158 RID: 344
		public static readonly uint EventEntryLengthV6 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV6));

		// Token: 0x04000159 RID: 345
		public static readonly uint EventEntryUnifiedMailboxGuidOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.EventEntryBlockV7), "UnifiedMailboxGuid"));

		// Token: 0x0400015A RID: 346
		public static readonly uint EventEntryUnifiedMailboxGuidLength = (uint)Marshal.SizeOf(typeof(Guid));

		// Token: 0x0400015B RID: 347
		public static readonly uint EventEntryLengthV7 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.EventEntryBlockV7));

		// Token: 0x0400015C RID: 348
		public static readonly uint ReadEventsRequestHeaderFlagsOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsRequestHeaderBlockV1), "Flags"));

		// Token: 0x0400015D RID: 349
		public static readonly uint ReadEventsRequestHeaderFlagsLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x0400015E RID: 350
		public static readonly uint ReadEventsRequestHeaderStartCounterOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsRequestHeaderBlockV1), "StartCounter"));

		// Token: 0x0400015F RID: 351
		public static readonly uint ReadEventsRequestHeaderStartCounterLength = (uint)Marshal.SizeOf(typeof(long));

		// Token: 0x04000160 RID: 352
		public static readonly uint ReadEventsRequestHeaderEventsWantOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsRequestHeaderBlockV1), "EventsWant"));

		// Token: 0x04000161 RID: 353
		public static readonly uint ReadEventsRequestHeaderEventsWantLength = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x04000162 RID: 354
		public static readonly uint ReadEventsRequestHeaderLengthV1 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.ReadEventsRequestHeaderBlockV1));

		// Token: 0x04000163 RID: 355
		public static readonly uint ReadEventsRequestHeaderEventsToCheckOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsRequestHeaderBlockV2), "EventsToCheck"));

		// Token: 0x04000164 RID: 356
		public static readonly uint ReadEventsRequestHeaderEventsToCheckLength = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x04000165 RID: 357
		public static readonly uint ReadEventsRequestHeaderLengthV2 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.ReadEventsRequestHeaderBlockV2));

		// Token: 0x04000166 RID: 358
		public static readonly uint ReadEventsResponseHeaderFlagsOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsResponseHeaderBlockV1), "Flags"));

		// Token: 0x04000167 RID: 359
		public static readonly uint ReadEventsResponseHeaderFlagsLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000168 RID: 360
		public static readonly uint ReadEventsResponseHeaderEventsCountOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsResponseHeaderBlockV1), "EventsCount"));

		// Token: 0x04000169 RID: 361
		public static readonly uint ReadEventsResponseHeaderEventsCountLength = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x0400016A RID: 362
		public static readonly uint ReadEventsResponseHeaderLengthV1 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.ReadEventsResponseHeaderBlockV1));

		// Token: 0x0400016B RID: 363
		public static readonly uint ReadEventsResponseHeaderPaddingOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsResponseHeaderBlockV2), "Padding"));

		// Token: 0x0400016C RID: 364
		public static readonly uint ReadEventsResponseHeaderPaddingLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x0400016D RID: 365
		public static readonly uint ReadEventsResponseHeaderEndCounterOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.ReadEventsResponseHeaderBlockV2), "EndCounter"));

		// Token: 0x0400016E RID: 366
		public static readonly uint ReadEventsResponseHeaderEndCounterLength = (uint)Marshal.SizeOf(typeof(long));

		// Token: 0x0400016F RID: 367
		public static readonly uint ReadEventsResponseHeaderLengthV2 = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.ReadEventsResponseHeaderBlockV2));

		// Token: 0x04000170 RID: 368
		public static readonly uint WriteEventsRequestHeaderFlagsOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.WriteEventsRequestHeaderBlock), "Flags"));

		// Token: 0x04000171 RID: 369
		public static readonly uint WriteEventsRequestHeaderFlagsLength = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04000172 RID: 370
		public static readonly uint WriteEventsRequestHeaderEventsCountOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.WriteEventsRequestHeaderBlock), "EventsCount"));

		// Token: 0x04000173 RID: 371
		public static readonly uint WriteEventsRequestHeaderEventsCountLength = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x04000174 RID: 372
		public static readonly uint WriteEventsRequestHeaderLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.WriteEventsRequestHeaderBlock));

		// Token: 0x04000175 RID: 373
		public static readonly uint WriteEventsResponseEventsCountOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.WriteEventsResponseBlockHeader), "EventsCount"));

		// Token: 0x04000176 RID: 374
		public static readonly uint WriteEventsResponseEventsCountLength = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x04000177 RID: 375
		public static readonly uint WriteEventsResponseHeaderLength = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.WriteEventsResponseBlockHeader));

		// Token: 0x04000178 RID: 376
		public static readonly uint WriteEventsResponseSingleAdjustedEventCounterLength = (uint)Marshal.SizeOf(typeof(long));

		// Token: 0x04000179 RID: 377
		public static readonly uint SizeofMdbStatusRaw = (uint)Marshal.SizeOf(typeof(AdminRpcParseFormat.MdbStatusRaw));

		// Token: 0x0400017A RID: 378
		public static readonly uint MdbStatusUlStatusOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "ulStatus"));

		// Token: 0x0400017B RID: 379
		public static readonly uint MdbStatusCbMdbNameOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "cbMdbName"));

		// Token: 0x0400017C RID: 380
		public static readonly uint MdbStatusCbVServerNameOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "cbVServerName"));

		// Token: 0x0400017D RID: 381
		public static readonly uint MdbStatusCbMdbLegacyDNOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "cbMdbLegacyDN"));

		// Token: 0x0400017E RID: 382
		public static readonly uint MdbStatusCbStorageGroupNameOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "cbStorageGroupName"));

		// Token: 0x0400017F RID: 383
		public static readonly uint MdbStatusIbMdbNameOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "ibMdbName"));

		// Token: 0x04000180 RID: 384
		public static readonly uint MdbStatusIbVServerNameOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "ibVServerName"));

		// Token: 0x04000181 RID: 385
		public static readonly uint MdbStatusIbMdbLegacyDNOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "ibMdbLegacyDN"));

		// Token: 0x04000182 RID: 386
		public static readonly uint MdbStatusIbStorageGroupNameOffset = (uint)((int)Marshal.OffsetOf(typeof(AdminRpcParseFormat.MdbStatusRaw), "ibStorageGroupName"));

		// Token: 0x02000048 RID: 72
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct DataBlock
		{
			// Token: 0x04000183 RID: 387
			public uint BlockLength;
		}

		// Token: 0x02000049 RID: 73
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Ltid
		{
			// Token: 0x04000184 RID: 388
			public Guid ReplicationGuid;

			// Token: 0x04000185 RID: 389
			public ulong GlobcntAndPadding;
		}

		// Token: 0x0200004A RID: 74
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ObjectClass
		{
			// Token: 0x04000186 RID: 390
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 60)]
			public string Value;
		}

		// Token: 0x0200004B RID: 75
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct Sid
		{
			// Token: 0x04000187 RID: 391
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 68)]
			public byte[] Value;
		}

		// Token: 0x0200004C RID: 76
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct TenantHintBlob
		{
			// Token: 0x04000188 RID: 392
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.U1)]
			public byte[] Value;
		}

		// Token: 0x0200004D RID: 77
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV1
		{
			// Token: 0x04000189 RID: 393
			public AdminRpcParseFormat.DataBlock BlockHeader;

			// Token: 0x0400018A RID: 394
			public int EventMask;

			// Token: 0x0400018B RID: 395
			public long EventCounter;

			// Token: 0x0400018C RID: 396
			public long CreateTime;

			// Token: 0x0400018D RID: 397
			public int TransacId;

			// Token: 0x0400018E RID: 398
			public int ItemCount;

			// Token: 0x0400018F RID: 399
			public int UnreadCount;

			// Token: 0x04000190 RID: 400
			public int Flags;

			// Token: 0x04000191 RID: 401
			public Guid MailboxGuid;

			// Token: 0x04000192 RID: 402
			public Guid MapiEntryIdGuid;

			// Token: 0x04000193 RID: 403
			public AdminRpcParseFormat.Ltid Fid;

			// Token: 0x04000194 RID: 404
			public AdminRpcParseFormat.Ltid Mid;

			// Token: 0x04000195 RID: 405
			public AdminRpcParseFormat.Ltid ParentFid;

			// Token: 0x04000196 RID: 406
			public AdminRpcParseFormat.Ltid OldFid;

			// Token: 0x04000197 RID: 407
			public AdminRpcParseFormat.Ltid OldMid;

			// Token: 0x04000198 RID: 408
			public AdminRpcParseFormat.Ltid OldParentFid;

			// Token: 0x04000199 RID: 409
			public AdminRpcParseFormat.ObjectClass ItemClass;
		}

		// Token: 0x0200004E RID: 78
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV2
		{
			// Token: 0x0400019A RID: 410
			public AdminRpcParseFormat.EventEntryBlockV1 V1;

			// Token: 0x0400019B RID: 411
			public long ExtendedFlags;
		}

		// Token: 0x0200004F RID: 79
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV3
		{
			// Token: 0x0400019C RID: 412
			public AdminRpcParseFormat.EventEntryBlockV2 V2;

			// Token: 0x0400019D RID: 413
			public int ClientType;

			// Token: 0x0400019E RID: 414
			public AdminRpcParseFormat.Sid ClientSid;
		}

		// Token: 0x02000050 RID: 80
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV4
		{
			// Token: 0x0400019F RID: 415
			public AdminRpcParseFormat.EventEntryBlockV3 V3;

			// Token: 0x040001A0 RID: 416
			public int DocId;
		}

		// Token: 0x02000051 RID: 81
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV5
		{
			// Token: 0x040001A1 RID: 417
			public AdminRpcParseFormat.EventEntryBlockV4 V4;

			// Token: 0x040001A2 RID: 418
			public int MailboxNumber;
		}

		// Token: 0x02000052 RID: 82
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV6
		{
			// Token: 0x040001A3 RID: 419
			public AdminRpcParseFormat.EventEntryBlockV5 V5;

			// Token: 0x040001A4 RID: 420
			public int TenantHintBlobSize;

			// Token: 0x040001A5 RID: 421
			public AdminRpcParseFormat.TenantHintBlob TenantHintBlob;
		}

		// Token: 0x02000053 RID: 83
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct EventEntryBlockV7
		{
			// Token: 0x040001A6 RID: 422
			public AdminRpcParseFormat.EventEntryBlockV6 V6;

			// Token: 0x040001A7 RID: 423
			public Guid UnifiedMailboxGuid;
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ReadEventsRequestHeaderBlockV1
		{
			// Token: 0x040001A8 RID: 424
			public AdminRpcParseFormat.DataBlock BlockHeader;

			// Token: 0x040001A9 RID: 425
			public int Flags;

			// Token: 0x040001AA RID: 426
			public long StartCounter;

			// Token: 0x040001AB RID: 427
			public uint EventsWant;
		}

		// Token: 0x02000055 RID: 85
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ReadEventsRequestHeaderBlockV2
		{
			// Token: 0x040001AC RID: 428
			public AdminRpcParseFormat.ReadEventsRequestHeaderBlockV1 V1;

			// Token: 0x040001AD RID: 429
			public uint EventsToCheck;
		}

		// Token: 0x02000056 RID: 86
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ReadEventsResponseHeaderBlockV1
		{
			// Token: 0x040001AE RID: 430
			public AdminRpcParseFormat.DataBlock BlockHeader;

			// Token: 0x040001AF RID: 431
			public int Flags;

			// Token: 0x040001B0 RID: 432
			public uint EventsCount;
		}

		// Token: 0x02000057 RID: 87
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ReadEventsResponseHeaderBlockV2
		{
			// Token: 0x040001B1 RID: 433
			public AdminRpcParseFormat.ReadEventsResponseHeaderBlockV1 V1;

			// Token: 0x040001B2 RID: 434
			public int Padding;

			// Token: 0x040001B3 RID: 435
			public long EndCounter;
		}

		// Token: 0x02000058 RID: 88
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct WriteEventsRequestHeaderBlock
		{
			// Token: 0x040001B4 RID: 436
			public AdminRpcParseFormat.DataBlock BlockHeader;

			// Token: 0x040001B5 RID: 437
			public int Flags;

			// Token: 0x040001B6 RID: 438
			public uint EventsCount;
		}

		// Token: 0x02000059 RID: 89
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct WriteEventsResponseBlockHeader
		{
			// Token: 0x040001B7 RID: 439
			public AdminRpcParseFormat.DataBlock BlockHeader;

			// Token: 0x040001B8 RID: 440
			public uint EventsCount;
		}

		// Token: 0x0200005A RID: 90
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct MdbStatusRaw
		{
			// Token: 0x040001B9 RID: 441
			private Guid guidMdb;

			// Token: 0x040001BA RID: 442
			private int ulStatus;

			// Token: 0x040001BB RID: 443
			private int cbMdbName;

			// Token: 0x040001BC RID: 444
			private int cbVServerName;

			// Token: 0x040001BD RID: 445
			private int cbMdbLegacyDN;

			// Token: 0x040001BE RID: 446
			private int cbStorageGroupName;

			// Token: 0x040001BF RID: 447
			private int ibMdbName;

			// Token: 0x040001C0 RID: 448
			private int ibVServerName;

			// Token: 0x040001C1 RID: 449
			private int ibMdbLegacyDN;

			// Token: 0x040001C2 RID: 450
			private int ibStorageGroupName;
		}
	}
}

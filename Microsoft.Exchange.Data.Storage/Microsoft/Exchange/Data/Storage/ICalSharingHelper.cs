using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D94 RID: 3476
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ICalSharingHelper
	{
		// Token: 0x060077AB RID: 30635 RVA: 0x0021041B File Offset: 0x0020E61B
		internal static ImportCalendarResults ImportCalendar(Stream inputStream, string charset, InboundConversionOptions options, StoreSession session, StoreObjectId folderId)
		{
			return ICalSharingHelper.ImportCalendar(inputStream, charset, options, session, folderId, Deadline.NoDeadline);
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x00210430 File Offset: 0x0020E630
		internal static ImportCalendarResults ImportCalendar(Stream inputStream, string charset, InboundConversionOptions options, StoreSession session, StoreObjectId folderId, Deadline deadline)
		{
			ImportCalendarResults result;
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, folderId))
			{
				result = ICalSharingHelper.ImportCalendar(inputStream, charset, options, session, calendarFolder, deadline, ExDateTime.MinValue, ExDateTime.MaxValue);
			}
			return result;
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x002104C0 File Offset: 0x0020E6C0
		public static ImportCalendarResults ImportCalendar(Stream inputStream, string charset, InboundConversionOptions options, StoreSession session, CalendarFolder folder, Deadline deadline, ExDateTime importWindowStart, ExDateTime importWindowEnd)
		{
			ImportCalendarResults results = new ImportCalendarResults();
			try
			{
				ConvertUtils.CallCts(ExTraceGlobals.ICalTracer, "ICalSharingHelper::ImportCalendar", ServerStrings.ConversionCorruptContent, delegate
				{
					new CalendarImporter(inputStream, charset, options, folder, results, deadline, importWindowStart, importWindowEnd).Run();
				});
			}
			catch (ConversionFailedException ex)
			{
				results.RawErrors.Add(new LocalizedString(ex.ToString()));
			}
			catch (ExchangeDataException ex2)
			{
				results.RawErrors.Add(new LocalizedString(ex2.ToString()));
			}
			return results;
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x0021061C File Offset: 0x0020E81C
		public static void ExportCalendar(Stream outputStream, string charset, OutboundConversionOptions options, MailboxSession session, StoreObjectId folderId, ExDateTime windowStart, ExDateTime windowEnd, DetailLevelEnumType detailType)
		{
			EnumValidator.ThrowIfInvalid<DetailLevelEnumType>(detailType, "detailType");
			PropertyDefinition[] array = InternetCalendarSchema.FromDetailLevel(detailType);
			Array.IndexOf<PropertyDefinition>(array, CalendarItemBaseSchema.FreeBusyStatus);
			int num = Array.IndexOf<PropertyDefinition>(array, InternalSchema.ItemId);
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, folderId))
			{
				object[][] array2 = calendarFolder.InternalGetCalendarView(windowStart, windowEnd, detailType == DetailLevelEnumType.AvailabilityOnly, true, true, RecurrenceExpansionOption.IncludeMaster | RecurrenceExpansionOption.TruncateMaster, array);
				Item[] items = new Item[array2.Length];
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						items[i] = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
						for (int j = 0; j < array.Length; j++)
						{
							StorePropertyDefinition storePropertyDefinition = array[j] as StorePropertyDefinition;
							if (storePropertyDefinition != null && (storePropertyDefinition.PropertyFlags & PropertyFlags.ReadOnly) != PropertyFlags.ReadOnly)
							{
								object obj = array2[i][j];
								if (!PropertyError.IsPropertyError(obj))
								{
									items[i][storePropertyDefinition] = obj;
								}
							}
						}
						if (detailType == DetailLevelEnumType.FullDetails && array2[i][num] is VersionedId)
						{
							using (CoreItem coreItem = CoreItem.Bind(session, (VersionedId)array2[i][num], new PropertyDefinition[]
							{
								ItemSchema.TextBody,
								ItemSchema.HtmlBody,
								ItemSchema.RtfBody
							}))
							{
								using (TextReader textReader = coreItem.Body.OpenTextReader(BodyFormat.TextPlain))
								{
									items[i][ItemSchema.TextBody] = textReader.ReadToEnd();
								}
							}
						}
					}
					OutboundAddressCache addressCache = new OutboundAddressCache(options, new ConversionLimitsTracker(options.Limits));
					List<LocalizedString> errorStream = new List<LocalizedString>();
					ConvertUtils.CallCts(ExTraceGlobals.ICalTracer, "ICalSharingHelper::ExportCalendar", ServerStrings.ConversionCorruptContent, delegate
					{
						CalendarDocument.InternalItemsToICal(calendarFolder.DisplayName, items, null, addressCache, true, outputStream, errorStream, charset, options);
					});
					if (errorStream.Count > 0)
					{
						ExTraceGlobals.ICalTracer.TraceError<int>(0L, "{0} errors found during outbound iCal content conversion.", errorStream.Count);
						AnonymousSharingLog.LogEntries(session, errorStream);
					}
				}
				finally
				{
					foreach (Item item in items)
					{
						if (item != null)
						{
							item.Dispose();
						}
					}
				}
			}
		}
	}
}

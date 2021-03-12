using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000150 RID: 336
	internal sealed class ItemUtility
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00050ABF File Offset: 0x0004ECBF
		private ItemUtility()
		{
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00050AC8 File Offset: 0x0004ECC8
		public static T GetProperty<T>(IStorePropertyBag propertyBag, PropertyDefinition propertyDefinition, T defaultValue)
		{
			object obj = propertyBag.TryGetProperty(propertyDefinition);
			if (obj is PropertyError || obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00050AF0 File Offset: 0x0004ECF0
		public static bool HasCategories(IStorePropertyBag storePropertyBag)
		{
			string[] array = null;
			if (OwaContext.Current.UserContext.CanActAsOwner)
			{
				array = ItemUtility.GetProperty<string[]>(storePropertyBag, ItemSchema.Categories, null);
			}
			return (array != null && 0 < array.Length) || ItemUtility.GetLegacyColoredFlag(storePropertyBag) != int.MinValue;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00050B38 File Offset: 0x0004ED38
		public static int GetLegacyColoredFlag(IStorePropertyBag storePropertyBag)
		{
			FlagStatus property = ItemUtility.GetProperty<FlagStatus>(storePropertyBag, ItemSchema.FlagStatus, FlagStatus.NotFlagged);
			int property2 = ItemUtility.GetProperty<int>(storePropertyBag, ItemSchema.ItemColor, int.MinValue);
			bool property3 = ItemUtility.GetProperty<bool>(storePropertyBag, ItemSchema.IsToDoItem, false);
			if (property == FlagStatus.Flagged && property2 != -2147483648 && !property3)
			{
				return property2;
			}
			return int.MinValue;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00050B88 File Offset: 0x0004ED88
		public static bool ShouldRenderSendAgain(Item item, bool isEmbeddedItem)
		{
			ReportMessage reportMessage = item as ReportMessage;
			return !isEmbeddedItem && reportMessage != null && reportMessage.IsSendAgainAllowed;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00050BB0 File Offset: 0x0004EDB0
		public static string GetCategoriesAsString(Item item)
		{
			string result = null;
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			string[] property = ItemUtility.GetProperty<string[]>(item, ItemSchema.Categories, null);
			if (property != null)
			{
				result = string.Join("; ", property);
			}
			return result;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00050BEA File Offset: 0x0004EDEA
		public static bool UserCanEditItem(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return ItemUtility.HasRight(item, EffectiveRights.Modify);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00050C01 File Offset: 0x0004EE01
		public static bool UserCanDeleteItem(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return ItemUtility.HasRight(item, EffectiveRights.Delete);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00050C18 File Offset: 0x0004EE18
		private static bool HasRight(Item item, EffectiveRights effectiveRightToCheck)
		{
			EffectiveRights property = ItemUtility.GetProperty<EffectiveRights>(item, StoreObjectSchema.EffectiveRights, EffectiveRights.None);
			return (property & effectiveRightToCheck) == effectiveRightToCheck;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00050C38 File Offset: 0x0004EE38
		internal static bool IsReplySupported(Item item)
		{
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			bool flag = calendarItemBase != null && calendarItemBase.IsMeeting;
			return item is MessageItem || flag || item is PostItem;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00050C70 File Offset: 0x0004EE70
		internal static bool IsForwardSupported(Item item)
		{
			bool flag = ObjectClass.IsOfClass(item.ClassName, "IPM.Note.Microsoft.Approval.Request");
			bool flag2 = ObjectClass.IsOfClass(item.ClassName, "IPM.Sharing");
			bool flag3 = false;
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			if (calendarItemBase != null && !calendarItemBase.IsMeeting)
			{
				if (calendarItemBase.IsCalendarItemTypeOccurrenceOrException)
				{
					flag3 = true;
				}
				else
				{
					CalendarItem calendarItem = calendarItemBase as CalendarItem;
					if (calendarItem != null)
					{
						flag3 = (calendarItem.Recurrence != null);
					}
				}
			}
			return !flag && !flag2 && !flag3;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00050CE8 File Offset: 0x0004EEE8
		internal static void SetItemBody(Item item, BodyFormat bodyFormat, string content)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			Body body = item.Body;
			if (!OwaContext.Current.UserContext.IsBasicExperience && OwaContext.Current.UserContext.IsIrmEnabled && Utilities.IsIrmDecrypted(item))
			{
				body = ((RightsManagedMessageItem)item).ProtectedBody;
			}
			using (TextWriter textWriter = body.OpenTextWriter(bodyFormat))
			{
				textWriter.Write(content);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00050D7C File Offset: 0x0004EF7C
		internal static string GetItemBody(Item item, BodyFormat desiredFormat)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			string result;
			using (TextReader textReader = item.Body.OpenTextReader(desiredFormat))
			{
				result = textReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00050DC8 File Offset: 0x0004EFC8
		public static bool HasDeletePolicy(IStorePropertyBag storePropertyBag)
		{
			byte[] property = ItemUtility.GetProperty<byte[]>(storePropertyBag, StoreObjectSchema.PolicyTag, null);
			return property != null;
		}
	}
}

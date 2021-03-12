using System;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	internal class XsoFlagProperty : XsoNestedProperty
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x000778EC File Offset: 0x00075AEC
		public XsoFlagProperty() : base(new FlagData(), new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.FlagRequest,
			ItemSchema.FlagCompleteTime,
			ItemSchema.ReminderIsSet,
			ItemSchema.ReminderDueBy,
			ItemSchema.LocalStartDate,
			ItemSchema.LocalDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.UtcDueDate,
			ItemSchema.FlagSubject,
			ItemSchema.CompleteDate,
			MessageItemSchema.ToDoOrdinalDate,
			MessageItemSchema.ToDoSubOrdinal
		})
		{
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0007797C File Offset: 0x00075B7C
		public override INestedData NestedData
		{
			get
			{
				FlagData flagData = (FlagData)base.NestedData;
				if (flagData.ContainsValidData())
				{
					return flagData;
				}
				Item mailboxItem = (Item)base.XsoItem;
				if (!this.LoadFlagData(mailboxItem, flagData))
				{
					base.State = PropertyState.SetToDefault;
				}
				return flagData;
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000779C0 File Offset: 0x00075BC0
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			Item mailboxItem = (Item)base.XsoItem;
			XsoFlagProperty.ClearFlag(mailboxItem);
			base.InternalSetToDefault(srcProperty);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000779E8 File Offset: 0x00075BE8
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			Item mailboxItem = (Item)base.XsoItem;
			INestedProperty nestedProperty = (INestedProperty)srcProperty;
			FlagData flagData = (FlagData)nestedProperty.NestedData;
			if (flagData.Status == null || flagData.Status.Value == 0)
			{
				XsoFlagProperty.ClearFlag(mailboxItem);
				return;
			}
			if (flagData.Status == 2)
			{
				XsoFlagProperty.SetFlag(mailboxItem, flagData);
				return;
			}
			if (flagData.Status == 1)
			{
				XsoFlagProperty.CompleteFlag(mailboxItem, flagData);
				return;
			}
			throw new ConversionException("Flag Status cannot be a value other than 0, 1, 2");
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00077A94 File Offset: 0x00075C94
		private static void ClearFlag(Item mailboxItem)
		{
			mailboxItem.DeleteProperties(new PropertyDefinition[]
			{
				MessageItemSchema.ReplyTime
			});
			mailboxItem.ClearFlag();
			if (mailboxItem.Reminder != null)
			{
				mailboxItem.Reminder.Dismiss(ExDateTime.GetNow(mailboxItem.Session.ExTimeZone));
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00077AE0 File Offset: 0x00075CE0
		private static void SetFlag(Item mailboxItem, FlagData flagData)
		{
			if (flagData.Type == null && !(mailboxItem is MeetingMessage))
			{
				throw new ConversionException("Missing required data from the client for non-MeetingMessage item: Type!");
			}
			if ((flagData.StartDate != null || flagData.UtcStartDate != null || flagData.DueDate != null || flagData.UtcDueDate != null) && (flagData.StartDate == null || flagData.UtcStartDate == null || flagData.DueDate == null || flagData.UtcDueDate == null))
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Incorrect all or nothing datetime properties: StartDate={0}, UtcStartDate={1}, DueDate={2}, UtcDueDate={3}", new object[]
				{
					flagData.StartDate,
					flagData.UtcStartDate,
					flagData.DueDate,
					flagData.UtcDueDate
				}));
			}
			if (flagData.StartDate != null && (flagData.StartDate.Value > flagData.DueDate.Value || flagData.UtcStartDate.Value > flagData.UtcDueDate.Value))
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Start date cannot be later than due date: StartDate={0}, UtcStartDate={1}, DueDate={2}, UtcDueDate={3}", new object[]
				{
					flagData.StartDate,
					flagData.UtcStartDate,
					flagData.DueDate,
					flagData.UtcDueDate
				}));
			}
			if (flagData.ReminderSet != null && flagData.ReminderSet.Value && flagData.ReminderTime == null)
			{
				throw new ConversionException("Missing ReminderTime while ReminderSet is true.");
			}
			if (flagData.ReminderSet == null)
			{
				flagData.ReminderSet = new bool?(false);
			}
			if (!flagData.ReminderSet.Value)
			{
				XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, MessageItemSchema.ReplyTime, null);
			}
			else
			{
				XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, MessageItemSchema.ReplyTime, flagData.ReminderTime);
			}
			if (mailboxItem.Reminder != null)
			{
				mailboxItem.Reminder.IsSet = flagData.ReminderSet.Value;
			}
			if (flagData.ReminderTime != null && mailboxItem.Reminder != null && !flagData.ReminderTime.Equals(mailboxItem.Reminder.DueBy))
			{
				mailboxItem.Reminder.DueBy = flagData.ReminderTime;
			}
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, MessageItemSchema.ToDoOrdinalDate, flagData.OrdinalDate);
			XsoFlagProperty.SetOrDeleteProperty<string>(mailboxItem, MessageItemSchema.ToDoSubOrdinal, flagData.SubOrdinalDate);
			mailboxItem.SetFlagForUtcSession(flagData.Type, flagData.StartDate, flagData.UtcStartDate, flagData.DueDate, flagData.UtcDueDate);
			XsoFlagProperty.SetOrDeleteProperty<string>(mailboxItem, ItemSchema.FlagSubject, flagData.Subject);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00077E04 File Offset: 0x00076004
		private static void CompleteFlag(Item mailboxItem, FlagData flagData)
		{
			if (flagData.CompleteTime == null)
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Missing required data from the client: CompleteTime={0}", new object[]
				{
					flagData.CompleteTime
				}));
			}
			if ((flagData.StartDate != null && flagData.DueDate != null && flagData.StartDate.Value > flagData.DueDate.Value) || (flagData.UtcStartDate != null && flagData.UtcDueDate != null && flagData.UtcStartDate.Value > flagData.UtcDueDate.Value))
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Start date cannot be later than due date: StartDate={0}, UtcStartDate={1}, DueDate={2}, UtcDueDate={3}", new object[]
				{
					flagData.StartDate,
					flagData.UtcStartDate,
					flagData.DueDate,
					flagData.UtcDueDate
				}));
			}
			if (mailboxItem.Reminder != null)
			{
				mailboxItem.Reminder.IsSet = false;
			}
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, MessageItemSchema.ReplyTime, flagData.ReminderTime);
			XsoFlagProperty.SetOrDeleteProperty<string>(mailboxItem, ItemSchema.FlagSubject, flagData.Subject);
			if (!(mailboxItem is MeetingMessage))
			{
				XsoFlagProperty.SetOrDeleteProperty<string>(mailboxItem, ItemSchema.FlagRequest, flagData.Type);
			}
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, ItemSchema.LocalStartDate, flagData.StartDate);
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, ItemSchema.LocalDueDate, flagData.DueDate);
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, ItemSchema.UtcStartDate, flagData.UtcStartDate);
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, ItemSchema.UtcDueDate, flagData.UtcDueDate);
			XsoFlagProperty.SetOrDeleteValueProperty<ExDateTime>(mailboxItem, MessageItemSchema.ToDoOrdinalDate, flagData.OrdinalDate);
			XsoFlagProperty.SetOrDeleteProperty<string>(mailboxItem, MessageItemSchema.ToDoSubOrdinal, flagData.SubOrdinalDate);
			mailboxItem.CompleteFlagForUtcSession(flagData.DateCompleted, flagData.CompleteTime.Value);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00078028 File Offset: 0x00076228
		private static void SetOrDeleteValueProperty<T>(Item mailboxItem, PropertyDefinition property, object value) where T : struct
		{
			if (value == null)
			{
				mailboxItem.DeleteProperties(new PropertyDefinition[]
				{
					property
				});
				return;
			}
			if (value is T?)
			{
				mailboxItem[property] = ((T?)value).Value;
				return;
			}
			mailboxItem[property] = value;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00078078 File Offset: 0x00076278
		private static void SetOrDeleteProperty<T>(Item mailboxItem, PropertyDefinition property, object value) where T : class
		{
			if (value == null)
			{
				mailboxItem.DeleteProperties(new PropertyDefinition[]
				{
					property
				});
				return;
			}
			mailboxItem[property] = value;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000780A4 File Offset: 0x000762A4
		private static ExDateTime? GetStoreDateTime(Item mailboxItem, PropertyDefinition property)
		{
			object obj = mailboxItem.TryGetProperty(property);
			if (obj is ExDateTime)
			{
				return new ExDateTime?(ExTimeZone.UtcTimeZone.ConvertDateTime((ExDateTime)obj));
			}
			return null;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000780E0 File Offset: 0x000762E0
		private bool LoadFlagData(Item mailboxItem, FlagData flagData)
		{
			object obj = mailboxItem.TryGetProperty(ItemSchema.FlagStatus);
			if (Enum.IsDefined(typeof(FlagStatus), obj) && (int)obj != 0)
			{
				flagData.Status = new int?((int)obj);
				flagData.Type = (mailboxItem.TryGetProperty(ItemSchema.FlagRequest) as string);
				flagData.DateCompleted = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.CompleteDate);
				flagData.CompleteTime = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.FlagCompleteTime);
				object obj2 = mailboxItem.TryGetProperty(ItemSchema.ReminderIsSet);
				if (obj2 is bool)
				{
					flagData.ReminderSet = new bool?((bool)obj2);
				}
				flagData.ReminderTime = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.ReminderDueBy);
				flagData.StartDate = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.LocalStartDate);
				flagData.UtcStartDate = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.UtcStartDate);
				flagData.DueDate = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.LocalDueDate);
				flagData.UtcDueDate = XsoFlagProperty.GetStoreDateTime(mailboxItem, ItemSchema.UtcDueDate);
				flagData.Subject = (base.XsoItem.TryGetProperty(ItemSchema.FlagSubject) as string);
				flagData.OrdinalDate = XsoFlagProperty.GetStoreDateTime(mailboxItem, MessageItemSchema.ToDoOrdinalDate);
				flagData.SubOrdinalDate = (base.XsoItem.TryGetProperty(MessageItemSchema.ToDoSubOrdinal) as string);
				return true;
			}
			return false;
		}

		// Token: 0x04000C6C RID: 3180
		private const string DefaultFlagType = "Follow up";
	}
}

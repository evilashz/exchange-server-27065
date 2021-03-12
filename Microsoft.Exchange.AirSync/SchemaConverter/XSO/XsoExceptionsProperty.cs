using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200021A RID: 538
	internal class XsoExceptionsProperty : XsoProperty, IExceptionsProperty, IMultivaluedProperty<ExceptionInstance>, IProperty, IEnumerable<ExceptionInstance>, IEnumerable, IDataObjectGeneratorContainer
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x00076C25 File Offset: 0x00074E25
		public XsoExceptionsProperty() : base(null)
		{
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00076C30 File Offset: 0x00074E30
		public int Count
		{
			get
			{
				CalendarItem calendarItem = base.XsoItem as CalendarItem;
				if (calendarItem.Recurrence == null)
				{
					return 0;
				}
				int num = 0;
				ExDateTime[] deletedOccurrences = calendarItem.Recurrence.GetDeletedOccurrences();
				if (deletedOccurrences != null)
				{
					num = deletedOccurrences.Length;
				}
				object[] array = (object[])calendarItem.Recurrence.GetModifiedOccurrences();
				if (array != null)
				{
					num += array.Length;
				}
				return num;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x00076C83 File Offset: 0x00074E83
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x00076C8B File Offset: 0x00074E8B
		public IDataObjectGenerator SchemaState
		{
			get
			{
				return this.schemaState;
			}
			set
			{
				this.schemaState = (value as IXsoDataObjectGenerator);
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00076C9C File Offset: 0x00074E9C
		public static VersionedId GetOccurrenceItemId(MailboxSession mailboxSession, ExDateTime startTime, ExDateTime endTime)
		{
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSession, DefaultFolderType.Calendar, null))
			{
				object[][] calendarView = calendarFolder.GetCalendarView(startTime, endTime, new PropertyDefinition[]
				{
					ItemSchema.Id
				});
				for (int i = 0; i < calendarView.Length; i++)
				{
					if (!(calendarView[i][0] is PropertyError))
					{
						return (VersionedId)calendarView[i][0];
					}
				}
			}
			return null;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00076FD8 File Offset: 0x000751D8
		public IEnumerator<ExceptionInstance> GetEnumerator()
		{
			CalendarItem calItem = base.XsoItem as CalendarItem;
			if (calItem.Recurrence == null)
			{
				base.State = PropertyState.SetToDefault;
			}
			else
			{
				ExDateTime[] deletedExceptions = calItem.Recurrence.GetDeletedOccurrences();
				for (int idx = 0; idx < deletedExceptions.Length; idx++)
				{
					yield return new ExceptionInstance(ExTimeZone.UtcTimeZone.ConvertDateTime(deletedExceptions[idx]), 1);
				}
				XsoDataObject mailboxDataObject = null;
				OccurrenceInfo[] modifiedExceptions = (OccurrenceInfo[])calItem.Recurrence.GetModifiedOccurrences();
				for (int idx2 = 0; idx2 < modifiedExceptions.Length; idx2++)
				{
					if (mailboxDataObject == null)
					{
						mailboxDataObject = this.schemaState.GetInnerXsoDataObject();
					}
					ExceptionInstance instance = new ExceptionInstance(ExTimeZone.UtcTimeZone.ConvertDateTime(modifiedExceptions[idx2].OriginalStartTime), 0);
					using (CalendarItemOccurrence mailboxOccurrence = calItem.OpenOccurrenceByOriginalStartTime(modifiedExceptions[idx2].OriginalStartTime, mailboxDataObject.GetPrefetchProperties()))
					{
						if (mailboxOccurrence == null)
						{
							throw new ConversionException("Could not look up the modified item by itemId");
						}
						mailboxDataObject.Bind(mailboxOccurrence);
						instance.ModifiedException = mailboxDataObject;
						yield return instance;
						mailboxDataObject.Unbind();
					}
				}
			}
			yield break;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00076FF4 File Offset: 0x000751F4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00076FFC File Offset: 0x000751FC
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			CalendarItem calendarItem = base.XsoItem as CalendarItem;
			IExceptionsProperty exceptionsProperty = srcProperty as IExceptionsProperty;
			XsoDataObject xsoDataObject = null;
			foreach (ExceptionInstance exceptionInstance in exceptionsProperty)
			{
				ExDateTime exDateTime = ExTimeZone.UtcTimeZone.ConvertDateTime(exceptionInstance.ExceptionStartTime);
				ExDateTime exDateTime2 = calendarItem.ExTimeZone.ConvertDateTime(exDateTime);
				if (exceptionInstance.Deleted == 1)
				{
					if (calendarItem.Id == null)
					{
						calendarItem.Save(SaveMode.ResolveConflicts);
						calendarItem.Load();
					}
					try
					{
						this.ValidateCalendarItem(calendarItem);
						calendarItem.DeleteOccurrenceByOriginalStartTime(exDateTime2);
						continue;
					}
					catch (OccurrenceNotFoundException)
					{
						AirSyncDiagnostics.TraceDebug<ExDateTime>(ExTraceGlobals.XsoTracer, this, "Exception with OriginalStartTime: {0} already deleted.", exDateTime2);
						continue;
					}
				}
				if (xsoDataObject == null)
				{
					xsoDataObject = this.schemaState.GetInnerXsoDataObject();
				}
				CalendarItemOccurrence calendarItemOccurrence = null;
				try
				{
					if (calendarItem.Id != null)
					{
						this.ValidateCalendarItem(calendarItem);
						calendarItemOccurrence = calendarItem.OpenOccurrenceByOriginalStartTime(exDateTime2, xsoDataObject.GetPrefetchProperties());
					}
					else
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.XsoTracer, this, "New calendar recurrence item added with exceptions.  Extra save (RPC to store) needed for this");
						calendarItem.Save(SaveMode.ResolveConflicts);
						calendarItem.Load();
						this.ValidateCalendarItem(calendarItem);
						calendarItemOccurrence = calendarItem.OpenOccurrenceByOriginalStartTime(exDateTime2, xsoDataObject.GetPrefetchProperties());
					}
					if (calendarItemOccurrence == null)
					{
						throw new ConversionException("Could not open the Calendar Occurrence using the Original StartTime");
					}
					calendarItemOccurrence.OpenAsReadWrite();
					try
					{
						xsoDataObject.Bind(calendarItemOccurrence);
						xsoDataObject.CopyFrom(exceptionInstance.ModifiedException);
					}
					finally
					{
						xsoDataObject.Unbind();
					}
					calendarItemOccurrence.Save(SaveMode.NoConflictResolution);
				}
				finally
				{
					if (calendarItemOccurrence != null)
					{
						calendarItemOccurrence.Dispose();
					}
				}
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x000771D0 File Offset: 0x000753D0
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000771D4 File Offset: 0x000753D4
		private void ValidateCalendarItem(CalendarItem calendarItem)
		{
			if (calendarItem.Recurrence == null)
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "CalendarItem {0} does not have Recurrence!", new object[]
				{
					calendarItem.Id
				}));
			}
		}

		// Token: 0x04000C6A RID: 3178
		private IXsoDataObjectGenerator schemaState;
	}
}

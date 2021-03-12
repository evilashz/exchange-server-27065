using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000F1 RID: 241
	internal sealed class StartEndProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x00022294 File Offset: 0x00020494
		private StartEndProperty(CommandContext commandContext, PropertyDefinition propertyDefinition) : base(commandContext)
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000222A4 File Offset: 0x000204A4
		private bool IsWallClockProperty
		{
			get
			{
				return this.propertyDefinition == CalendarItemInstanceSchema.StartWallClock || this.propertyDefinition == CalendarItemInstanceSchema.EndWallClock;
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000222C2 File Offset: 0x000204C2
		public static StartEndProperty CreateCommandForStart(CommandContext commandContext)
		{
			return new StartEndProperty(commandContext, CalendarItemInstanceSchema.StartTime);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000222CF File Offset: 0x000204CF
		public static StartEndProperty CreateCommandForEnd(CommandContext commandContext)
		{
			return new StartEndProperty(commandContext, CalendarItemInstanceSchema.EndTime);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000222DC File Offset: 0x000204DC
		public static StartEndProperty CreateCommandForProposedStart(CommandContext commandContext)
		{
			return new StartEndProperty(commandContext, MeetingResponseSchema.AppointmentCounterStartWhole);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000222E9 File Offset: 0x000204E9
		public static StartEndProperty CreateCommandForProposedEnd(CommandContext commandContext)
		{
			return new StartEndProperty(commandContext, MeetingResponseSchema.AppointmentCounterEndWhole);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000222F6 File Offset: 0x000204F6
		public static StartEndProperty CreateCommandForStartWallClock(CommandContext commandContext)
		{
			return new StartEndProperty(commandContext, CalendarItemInstanceSchema.StartWallClock);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00022303 File Offset: 0x00020503
		public static StartEndProperty CreateCommandForEndWallClock(CommandContext commandContext)
		{
			return new StartEndProperty(commandContext, CalendarItemInstanceSchema.EndWallClock);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00022310 File Offset: 0x00020510
		public static string ConvertDateTimeToString(ExDateTime? dateTime, bool preserveTimeZone)
		{
			if (dateTime == null)
			{
				return null;
			}
			return StartEndProperty.ConvertDateTimeToString(dateTime.Value, preserveTimeZone);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0002232A File Offset: 0x0002052A
		public static string ConvertDateTimeToString(ExDateTime dateTime, bool preserveTimeZone)
		{
			if (dateTime == ExDateTime.MinValue)
			{
				return null;
			}
			if (preserveTimeZone)
			{
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			return ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(dateTime);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00022352 File Offset: 0x00020552
		public void Set()
		{
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00022354 File Offset: 0x00020554
		void ISetCommand.SetPhase3()
		{
			this.SetPhase3();
			this.ValidateStartEnd(base.GetCommandSettings<SetCommandSettings>().StoreObject);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00022370 File Offset: 0x00020570
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			CalendarItemBase calendarItemBase = (CalendarItemBase)updateCommandSettings.StoreObject;
			string valueOrDefault = setPropertyUpdate.ServiceObject.GetValueOrDefault<string>(this.commandContext.PropertyInformation);
			this.SetProperty(calendarItemBase, valueOrDefault);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000223A8 File Offset: 0x000205A8
		void IUpdateCommand.PostUpdate()
		{
			this.PostUpdate();
			this.ValidateStartEnd(base.GetCommandSettings<UpdateCommandSettings>().StoreObject);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000223C1 File Offset: 0x000205C1
		public void ToXml()
		{
			throw new InvalidOperationException("StartEndProperty.ToXml should not be called.");
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000223CD File Offset: 0x000205CD
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("StartEndProperty.ToXmlForPropertyBag should not be called.");
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000223DC File Offset: 0x000205DC
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			StoreObject storeObject = commandSettings.StoreObject;
			if (PropertyCommand.StorePropertyExists(storeObject, this.propertyDefinition))
			{
				ExDateTime dateTime = (ExDateTime)storeObject[this.propertyDefinition];
				serviceObject[propertyInformation] = StartEndProperty.ConvertDateTimeToString(dateTime, this.IsWallClockProperty);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00022440 File Offset: 0x00020640
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ExDateTime dateTime;
			if (PropertyCommand.TryGetValueFromPropertyBag<ExDateTime>(propertyBag, this.propertyDefinition, out dateTime))
			{
				serviceObject[propertyInformation] = StartEndProperty.ConvertDateTimeToString(dateTime, this.IsWallClockProperty);
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00022494 File Offset: 0x00020694
		private void ValidateStartEnd(StoreObject storeObject)
		{
			bool flag = PropertyCommand.StorePropertyExists(storeObject, CalendarItemInstanceSchema.StartTime);
			bool flag2 = PropertyCommand.StorePropertyExists(storeObject, CalendarItemInstanceSchema.EndTime);
			if (flag && flag2)
			{
				ExDateTime exDateTime = (ExDateTime)storeObject[CalendarItemInstanceSchema.StartTime];
				ExDateTime exDateTime2 = (ExDateTime)storeObject[CalendarItemInstanceSchema.EndTime];
				if (exDateTime.CompareTo(exDateTime2) > 0)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(CallContext.Current.ProtocolLog, "InvalidStartTime", string.Format("Start: {0} End: {1}", exDateTime, exDateTime2));
					throw new CalendarExceptionEndDateIsEarlierThanStartDate();
				}
				ExDateTime other = ExDateTime.MaxValue.AddYears(-5);
				if (exDateTime.CompareTo(other) < 0)
				{
					exDateTime = exDateTime.AddYears(5);
				}
				else
				{
					exDateTime2 = exDateTime2.AddYears(-5);
				}
				if (exDateTime.CompareTo(exDateTime2) < 0)
				{
					throw new CalendarExceptionDurationIsTooLong();
				}
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00022568 File Offset: 0x00020768
		private void SetProperty(CalendarItemBase calendarItemBase, string value)
		{
			ExTimeZone timeZone;
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				timeZone = ((this.propertyDefinition == CalendarItemInstanceSchema.StartTime) ? calendarItemBase.StartTimeZone : calendarItemBase.EndTimeZone);
			}
			else
			{
				timeZone = calendarItemBase.Session.ExTimeZone;
			}
			ExDateTime exDateTime = ExDateTimeConverter.ParseTimeZoneRelated(value, timeZone);
			calendarItemBase[this.propertyDefinition] = exDateTime;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000225CC File Offset: 0x000207CC
		void ISetCommand.SetPhase2()
		{
			this.SetPhase2();
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			CalendarItemBase calendarItemBase = (CalendarItemBase)commandSettings.StoreObject;
			string value;
			if (commandSettings.ServiceObject != null)
			{
				value = (commandSettings.ServiceObject[this.commandContext.PropertyInformation] as string);
			}
			else
			{
				value = ServiceXml.GetXmlTextNodeValue(commandSettings.ServiceProperty);
			}
			this.SetProperty(calendarItemBase, value);
		}

		// Token: 0x040006C4 RID: 1732
		private const int MaxCalendarDurationInYears = 5;

		// Token: 0x040006C5 RID: 1733
		private PropertyDefinition propertyDefinition;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000811 RID: 2065
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarComponentBase
	{
		// Token: 0x06004D03 RID: 19715 RVA: 0x0013FC98 File Offset: 0x0013DE98
		protected CalendarComponentBase(ICalContext context) : this(null, context)
		{
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0013FCA2 File Offset: 0x0013DEA2
		protected CalendarComponentBase(CalendarComponentBase root) : this(root, root.context)
		{
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0013FCB1 File Offset: 0x0013DEB1
		private CalendarComponentBase(CalendarComponentBase root, ICalContext context)
		{
			this.root = (root ?? this);
			this.context = context;
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0013FCCC File Offset: 0x0013DECC
		protected virtual void ProcessProperty(CalendarPropertyBase calendarProperty)
		{
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0013FCCE File Offset: 0x0013DECE
		protected virtual bool ValidateProperty(CalendarPropertyBase calendarProperty)
		{
			return true;
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0013FCD1 File Offset: 0x0013DED1
		protected virtual bool ProcessSubComponent(CalendarComponentBase calendarComponent)
		{
			return true;
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0013FCD4 File Offset: 0x0013DED4
		protected virtual bool ValidateProperties()
		{
			return true;
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0013FCD7 File Offset: 0x0013DED7
		protected virtual bool ValidateStructure()
		{
			return true;
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x0013FCDA File Offset: 0x0013DEDA
		protected ICalOutboundContext OutboundContext
		{
			get
			{
				return (ICalOutboundContext)this.context;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x06004D0C RID: 19724 RVA: 0x0013FCE7 File Offset: 0x0013DEE7
		protected ICalInboundContext InboundContext
		{
			get
			{
				return (ICalInboundContext)this.context;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x0013FCF4 File Offset: 0x0013DEF4
		protected ICalContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x0013FCFC File Offset: 0x0013DEFC
		internal bool Parse(CalendarReader calReader)
		{
			this.componentId = calReader.ComponentId;
			this.componentName = calReader.ComponentName;
			this.icalProperties = new List<CalendarPropertyBase>();
			this.subComponents = new List<CalendarComponentBase>();
			bool result;
			try
			{
				result = (this.ParseProperties(calReader) && this.ParseSubComponents(calReader));
			}
			catch (ArgumentException)
			{
				this.Context.AddError(ServerStrings.InvalidICalElement(this.componentName));
				result = false;
			}
			return result;
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x0013FD7C File Offset: 0x0013DF7C
		internal bool Validate()
		{
			foreach (CalendarPropertyBase calendarProperty in this.icalProperties)
			{
				if (!this.ValidateProperty(calendarProperty))
				{
					return false;
				}
			}
			return this.ValidateProperties();
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06004D10 RID: 19728 RVA: 0x0013FDE0 File Offset: 0x0013DFE0
		internal ComponentId ComponentId
		{
			get
			{
				return this.componentId;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06004D11 RID: 19729 RVA: 0x0013FDE8 File Offset: 0x0013DFE8
		internal string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x0013FDF0 File Offset: 0x0013DFF0
		internal List<CalendarPropertyBase> ICalProperties
		{
			get
			{
				return this.icalProperties;
			}
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0013FDF8 File Offset: 0x0013DFF8
		internal bool ValidateTimeZoneInfo(bool isRecursive)
		{
			bool flag = this.InboundContext.DeclaredTimeZones.Count == 0;
			foreach (CalendarDateTime calendarDateTime in this.icalProperties.OfType<CalendarDateTime>())
			{
				string timeZoneId = calendarDateTime.TimeZoneId;
				if (!string.IsNullOrEmpty(timeZoneId))
				{
					if (flag)
					{
						this.Context.AddError(ServerStrings.TimeZoneReferenceWithNullTimeZone(timeZoneId));
						return false;
					}
					if (!this.InboundContext.DeclaredTimeZones.ContainsKey(timeZoneId))
					{
						this.Context.AddError(ServerStrings.WrongTimeZoneReference(timeZoneId));
						return false;
					}
				}
				if (!this.NormalizeCalendarDateTime(calendarDateTime))
				{
					return false;
				}
			}
			if (isRecursive)
			{
				foreach (CalendarComponentBase calendarComponentBase in this.subComponents)
				{
					if (!calendarComponentBase.ValidateTimeZoneInfo(isRecursive))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x0013FF10 File Offset: 0x0013E110
		protected bool NormalizeCalendarDateTime(CalendarDateTime property)
		{
			if (property == null)
			{
				return true;
			}
			bool flag = true;
			ExTimeZone timeZone = ExTimeZone.UnspecifiedTimeZone;
			if (!string.IsNullOrEmpty(property.TimeZoneId))
			{
				timeZone = this.InboundContext.DeclaredTimeZones[property.TimeZoneId];
			}
			List<object> list = property.Value as List<object>;
			object value2;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					object value;
					if (!CalendarComponentBase.NormalizeSingleDateTime(timeZone, list[i], out value))
					{
						flag = false;
						break;
					}
					list[i] = value;
				}
			}
			else if (!CalendarComponentBase.NormalizeSingleDateTime(timeZone, property.Value, out value2))
			{
				flag = false;
			}
			else
			{
				property.Value = value2;
			}
			if (!flag)
			{
				this.Context.AddError(ServerStrings.InvalidICalElement(property.CalendarPropertyId.ToString()));
			}
			return flag;
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x0013FFCC File Offset: 0x0013E1CC
		private static bool NormalizeSingleDateTime(ExTimeZone timeZone, object rawValue, out object normalizedValue)
		{
			bool result = true;
			normalizedValue = rawValue;
			if (rawValue is DateTime)
			{
				try
				{
					normalizedValue = new ExDateTime(timeZone, (DateTime)rawValue);
					return result;
				}
				catch (ArgumentOutOfRangeException)
				{
					ExTraceGlobals.ICalTracer.TraceError(0L, "CalendarComponentBase::NormalizeSingleDateTime. Invalid DateTime value found. Value:'{0}'", new object[]
					{
						rawValue
					});
					return false;
				}
			}
			ExTraceGlobals.ICalTracer.TraceDebug(0L, "CalendarComponentBase::NormalizeSingleDateTime. Non DateTime value found for CalendarDateTime. Value:'{0}'", new object[]
			{
				rawValue
			});
			result = false;
			return result;
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x00140050 File Offset: 0x0013E250
		private bool ParseProperties(CalendarReader calReader)
		{
			CalendarPropertyReader propertyReader = calReader.PropertyReader;
			while (propertyReader.ReadNextProperty())
			{
				if (!this.ParseProperty(propertyReader) && !string.IsNullOrEmpty(propertyReader.Name))
				{
					this.Context.AddError(ServerStrings.InvalidICalElement(propertyReader.Name));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x001400A0 File Offset: 0x0013E2A0
		private bool ParseProperty(CalendarPropertyReader propertyReader)
		{
			bool result = false;
			CalendarPropertyBase calendarPropertyBase = this.NewProperty(propertyReader);
			if (calendarPropertyBase.Parse(propertyReader))
			{
				this.ProcessProperty(calendarPropertyBase);
				this.icalProperties.Add(calendarPropertyBase);
				result = true;
			}
			return result;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x001400D6 File Offset: 0x0013E2D6
		private bool ParseSubComponents(CalendarReader calReader)
		{
			if (calReader.ReadFirstChildComponent())
			{
				while (this.ParseSubComponent(calReader))
				{
					if (!calReader.ReadNextSiblingComponent())
					{
						goto IL_37;
					}
				}
				ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "CalendarComponentBase::ParseSubComponents. Failed to parse subcomponent: {0}", calReader.ComponentName);
				return false;
			}
			IL_37:
			return this.ValidateStructure();
		}

		// Token: 0x06004D19 RID: 19737 RVA: 0x00140118 File Offset: 0x0013E318
		private bool ParseSubComponent(CalendarReader calReader)
		{
			CalendarComponentBase calendarComponentBase = this.NewComponent(calReader);
			bool result;
			if (calendarComponentBase.Parse(calReader))
			{
				if (this.ProcessSubComponent(calendarComponentBase))
				{
					this.subComponents.Add(calendarComponentBase);
					result = true;
				}
				else
				{
					ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "CalendarComponentBase::ParseSubComponent. Failed to process component: {0}", calendarComponentBase.componentName);
					result = false;
				}
			}
			else
			{
				ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "CalendarComponentBase::ParseSubComponent. Failed to parse component: {0}", calendarComponentBase.componentName);
				result = false;
			}
			return result;
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x00140190 File Offset: 0x0013E390
		private CalendarPropertyBase NewProperty(CalendarPropertyReader pr)
		{
			CalendarValueType valueType = pr.ValueType;
			if (valueType <= CalendarValueType.Date)
			{
				if (valueType == CalendarValueType.CalAddress)
				{
					return new CalendarAttendee();
				}
				if (valueType != CalendarValueType.Date)
				{
					goto IL_83;
				}
			}
			else if (valueType != CalendarValueType.DateTime)
			{
				if (valueType != CalendarValueType.Text)
				{
					goto IL_83;
				}
				if (string.Compare(pr.Name, "X-MS-OLK-ORIGINALSTART", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(pr.Name, "X-MS-OLK-ORIGINALEND", StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(pr.Name, "X-MICROSOFT-EXDATE", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					return new CalendarDateTime();
				}
				return new CalendarPropertyBase();
			}
			return new CalendarDateTime();
			IL_83:
			return new CalendarPropertyBase();
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x00140228 File Offset: 0x0013E428
		private CalendarComponentBase NewComponent(CalendarReader calReader)
		{
			ComponentId componentId = calReader.ComponentId;
			if (componentId <= ComponentId.VTimeZone)
			{
				if (componentId == ComponentId.VEvent)
				{
					return new VEvent(this.root);
				}
				if (componentId == ComponentId.VTodo)
				{
					return new VTodo(this.root);
				}
				if (componentId == ComponentId.VTimeZone)
				{
					return new VTimeZone(this.root);
				}
			}
			else
			{
				if (componentId == ComponentId.VAlarm)
				{
					return new VAlarm(this.root);
				}
				if (componentId == ComponentId.Standard)
				{
					return new TimeZoneRule(this.root);
				}
				if (componentId == ComponentId.Daylight)
				{
					return new TimeZoneRule(this.root);
				}
			}
			return new CalendarComponentBase(this.root);
		}

		// Token: 0x040029FF RID: 10751
		private ComponentId componentId;

		// Token: 0x04002A00 RID: 10752
		private string componentName;

		// Token: 0x04002A01 RID: 10753
		private CalendarComponentBase root;

		// Token: 0x04002A02 RID: 10754
		private List<CalendarPropertyBase> icalProperties;

		// Token: 0x04002A03 RID: 10755
		private List<CalendarComponentBase> subComponents;

		// Token: 0x04002A04 RID: 10756
		private ICalContext context;
	}
}

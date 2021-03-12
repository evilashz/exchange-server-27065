using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200080F RID: 2063
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarPropertyBase
	{
		// Token: 0x06004CED RID: 19693 RVA: 0x0013F8B0 File Offset: 0x0013DAB0
		internal bool Parse(CalendarPropertyReader propertyReader)
		{
			this.calendarPropertyId = new CalendarPropertyId(propertyReader.PropertyId, propertyReader.Name);
			this.value = null;
			this.valueType = propertyReader.ValueType;
			this.parameters = new List<CalendarParameter>();
			CalendarParameterReader parameterReader = propertyReader.ParameterReader;
			while (parameterReader.ReadNextParameter())
			{
				CalendarParameter calendarParameter = new CalendarParameter();
				if (!calendarParameter.Parse(parameterReader) || !this.ProcessParameter(calendarParameter))
				{
					return false;
				}
				this.parameters.Add(calendarParameter);
			}
			this.valueType = propertyReader.ValueType;
			SchemaInfo schemaInfo;
			if (VEvent.GetConversionSchema().TryGetValue(this.calendarPropertyId.Key, out schemaInfo) && schemaInfo.IsMultiValue)
			{
				List<object> list = new List<object>();
				while (propertyReader.ReadNextValue())
				{
					object item = this.ReadValue(propertyReader);
					list.Add(item);
				}
				this.value = list;
			}
			else
			{
				this.value = this.ReadValue(propertyReader);
			}
			return this.Validate();
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x0013F99A File Offset: 0x0013DB9A
		internal CalendarValueType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x0013F9A2 File Offset: 0x0013DBA2
		// (set) Token: 0x06004CF0 RID: 19696 RVA: 0x0013F9AA File Offset: 0x0013DBAA
		internal object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x0013F9B3 File Offset: 0x0013DBB3
		internal List<CalendarParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0013F9BC File Offset: 0x0013DBBC
		internal CalendarParameter GetParameter(ParameterId parameterId)
		{
			foreach (CalendarParameter calendarParameter in this.parameters)
			{
				if (calendarParameter.ParameterId == parameterId)
				{
					return calendarParameter;
				}
			}
			return null;
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0013FA18 File Offset: 0x0013DC18
		internal CalendarParameter GetParameter(string parameterName)
		{
			foreach (CalendarParameter calendarParameter in this.parameters)
			{
				if (string.Compare(calendarParameter.Name, parameterName, StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					return calendarParameter;
				}
			}
			return null;
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x0013FA7C File Offset: 0x0013DC7C
		internal CalendarPropertyId CalendarPropertyId
		{
			get
			{
				return this.calendarPropertyId;
			}
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x0013FA84 File Offset: 0x0013DC84
		protected virtual bool ProcessParameter(CalendarParameter parameter)
		{
			return true;
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0013FA87 File Offset: 0x0013DC87
		protected virtual bool Validate()
		{
			return this.calendarPropertyId.PropertyId == PropertyId.Unknown || this.value != null;
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0013FAA4 File Offset: 0x0013DCA4
		protected virtual object ReadValue(CalendarPropertyReader propertyReader)
		{
			return propertyReader.ReadValue();
		}

		// Token: 0x040029F4 RID: 10740
		private CalendarValueType valueType;

		// Token: 0x040029F5 RID: 10741
		private object value;

		// Token: 0x040029F6 RID: 10742
		private List<CalendarParameter> parameters;

		// Token: 0x040029F7 RID: 10743
		private CalendarPropertyId calendarPropertyId;
	}
}

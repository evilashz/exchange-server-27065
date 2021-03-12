using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000817 RID: 2071
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarDateTime : CalendarPropertyBase
	{
		// Token: 0x06004DB6 RID: 19894 RVA: 0x00144ECC File Offset: 0x001430CC
		protected override bool ProcessParameter(CalendarParameter parameter)
		{
			ParameterId parameterId = parameter.ParameterId;
			if (parameterId == ParameterId.TimeZoneId)
			{
				this.timeZoneId = (string)parameter.Value;
			}
			return true;
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x00144EFC File Offset: 0x001430FC
		protected override bool Validate()
		{
			if (base.Validate())
			{
				if (base.Value is DateTime)
				{
					return true;
				}
				List<object> list = base.Value as List<object>;
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (!(list[i] is DateTime))
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x00144F53 File Offset: 0x00143153
		protected override object ReadValue(CalendarPropertyReader propertyReader)
		{
			return propertyReader.ReadValueAsDateTime();
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x00144F61 File Offset: 0x00143161
		// (set) Token: 0x06004DBA RID: 19898 RVA: 0x00144F69 File Offset: 0x00143169
		public string TimeZoneId
		{
			get
			{
				return this.timeZoneId;
			}
			set
			{
				this.timeZoneId = value;
			}
		}

		// Token: 0x04002A32 RID: 10802
		private string timeZoneId;
	}
}

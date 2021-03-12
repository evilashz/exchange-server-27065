using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000819 RID: 2073
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarParameter
	{
		// Token: 0x06004DC3 RID: 19907 RVA: 0x00145427 File Offset: 0x00143627
		internal CalendarParameter()
		{
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x00145430 File Offset: 0x00143630
		internal bool Parse(CalendarParameterReader parameterReader)
		{
			this.parameterId = parameterReader.ParameterId;
			this.parameterName = parameterReader.Name;
			this.value = null;
			List<object> list = new List<object>();
			while (parameterReader.ReadNextValue())
			{
				if (this.value == null)
				{
					this.value = parameterReader.ReadValue();
				}
				else if (list == null)
				{
					list = new List<object>();
					list.Add(this.value);
					this.value = list;
				}
				else
				{
					list.Add(this.value);
				}
			}
			return this.value != null;
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x001454BC File Offset: 0x001436BC
		internal ParameterId ParameterId
		{
			get
			{
				return this.parameterId;
			}
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x001454C4 File Offset: 0x001436C4
		internal string Name
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x001454CC File Offset: 0x001436CC
		internal object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04002A34 RID: 10804
		private ParameterId parameterId;

		// Token: 0x04002A35 RID: 10805
		private string parameterName;

		// Token: 0x04002A36 RID: 10806
		private object value;
	}
}

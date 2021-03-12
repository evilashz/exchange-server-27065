using System;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000A7 RID: 167
	internal class CalendarValueTypeContainer : ValueTypeContainer
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x00026214 File Offset: 0x00024414
		private void CalculateValueType()
		{
			if (this.isValueTypeInitialized)
			{
				return;
			}
			this.valueType = CalendarValueType.Unknown;
			if (this.valueTypeParameter != null)
			{
				this.valueType = CalendarCommon.GetValueTypeEnum(this.valueTypeParameter);
			}
			else
			{
				PropertyId propertyEnum = CalendarCommon.GetPropertyEnum(this.propertyName);
				if (propertyEnum != PropertyId.Unknown)
				{
					this.valueType = CalendarCommon.GetDefaultValueType(propertyEnum);
				}
			}
			if (this.valueType == CalendarValueType.Unknown)
			{
				this.valueType = CalendarValueType.Text;
			}
			this.isValueTypeInitialized = true;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00026282 File Offset: 0x00024482
		public override bool IsTextType
		{
			get
			{
				this.CalculateValueType();
				return this.valueType == CalendarValueType.Text;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00026297 File Offset: 0x00024497
		public override bool CanBeMultivalued
		{
			get
			{
				this.CalculateValueType();
				return this.valueType != CalendarValueType.Recurrence && this.valueType != CalendarValueType.Binary;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000262BA File Offset: 0x000244BA
		public override bool CanBeCompound
		{
			get
			{
				this.CalculateValueType();
				return this.valueType != CalendarValueType.Recurrence && this.valueType != CalendarValueType.Binary;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000262DD File Offset: 0x000244DD
		public CalendarValueType ValueType
		{
			get
			{
				this.CalculateValueType();
				return this.valueType;
			}
		}

		// Token: 0x040005A4 RID: 1444
		private CalendarValueType valueType;
	}
}

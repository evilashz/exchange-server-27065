using System;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200081A RID: 2074
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CalendarPropertyId
	{
		// Token: 0x06004DC8 RID: 19912 RVA: 0x001454D4 File Offset: 0x001436D4
		internal CalendarPropertyId(PropertyId propertyId, string propertyName)
		{
			this.propertyId = propertyId;
			this.propertyName = propertyName;
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x001454EA File Offset: 0x001436EA
		internal CalendarPropertyId(PropertyId propertyId) : this(propertyId, string.Empty)
		{
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x001454F8 File Offset: 0x001436F8
		internal CalendarPropertyId(string propertyName) : this(PropertyId.Unknown, propertyName)
		{
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x00145502 File Offset: 0x00143702
		internal PropertyId PropertyId
		{
			get
			{
				return this.propertyId;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x0014550A File Offset: 0x0014370A
		internal string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x00145512 File Offset: 0x00143712
		internal object Key
		{
			get
			{
				if (this.propertyId == PropertyId.Unknown)
				{
					return this.propertyName;
				}
				return this.propertyId;
			}
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x0014552E File Offset: 0x0014372E
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.propertyId, this.propertyName);
		}

		// Token: 0x04002A37 RID: 10807
		private readonly PropertyId propertyId;

		// Token: 0x04002A38 RID: 10808
		private readonly string propertyName;
	}
}

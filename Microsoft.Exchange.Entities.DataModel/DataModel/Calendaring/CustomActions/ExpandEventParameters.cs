using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x0200003B RID: 59
	public sealed class ExpandEventParameters : SchematizedObject<ExpandEventParametersSchema>
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003AB2 File Offset: 0x00001CB2
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003AC5 File Offset: 0x00001CC5
		public bool ReturnMaster
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.ReturnMasterProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.ReturnMasterProperty, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003AD9 File Offset: 0x00001CD9
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003AEC File Offset: 0x00001CEC
		public bool ReturnRegularOccurrences
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.ReturnRegularOccurrencesProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.ReturnRegularOccurrencesProperty, value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003B00 File Offset: 0x00001D00
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003B13 File Offset: 0x00001D13
		public bool ReturnExceptions
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.ReturnExceptionsProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.ReturnExceptionsProperty, value);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003B27 File Offset: 0x00001D27
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00003B3A File Offset: 0x00001D3A
		public bool ReturnCancellations
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.ReturnCancellationsProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.ReturnCancellationsProperty, value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003B4E File Offset: 0x00001D4E
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00003B61 File Offset: 0x00001D61
		public ExDateTime WindowStart
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime>(base.Schema.WindowStartProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime>(base.Schema.WindowStartProperty, value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003B75 File Offset: 0x00001D75
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00003B88 File Offset: 0x00001D88
		public ExDateTime WindowEnd
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime>(base.Schema.WindowEndProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime>(base.Schema.WindowEndProperty, value);
			}
		}
	}
}

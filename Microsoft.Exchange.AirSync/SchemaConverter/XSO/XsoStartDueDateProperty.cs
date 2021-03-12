using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000239 RID: 569
	[Serializable]
	internal class XsoStartDueDateProperty : XsoProperty, IStartDueDateProperty, IProperty
	{
		// Token: 0x0600150F RID: 5391 RVA: 0x0007B791 File Offset: 0x00079991
		public XsoStartDueDateProperty() : base(null)
		{
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0007B79A File Offset: 0x0007999A
		public ExDateTime? UtcStartDate
		{
			get
			{
				return base.XsoItem.GetValueAsNullable<ExDateTime>(ItemSchema.UtcStartDate);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x0007B7AC File Offset: 0x000799AC
		public ExDateTime? StartDate
		{
			get
			{
				return base.XsoItem.GetValueAsNullable<ExDateTime>(ItemSchema.LocalStartDate);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0007B7BE File Offset: 0x000799BE
		public ExDateTime? UtcDueDate
		{
			get
			{
				return base.XsoItem.GetValueAsNullable<ExDateTime>(ItemSchema.UtcDueDate);
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0007B7D0 File Offset: 0x000799D0
		public ExDateTime? DueDate
		{
			get
			{
				return base.XsoItem.GetValueAsNullable<ExDateTime>(ItemSchema.LocalDueDate);
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0007B7E4 File Offset: 0x000799E4
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			Task task = base.XsoItem as Task;
			if (task == null)
			{
				throw new UnexpectedTypeException("Task", base.XsoItem);
			}
			task.StartDate = null;
			task.DueDate = null;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0007B830 File Offset: 0x00079A30
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			IStartDueDateProperty startDueDateProperty = (IStartDueDateProperty)srcProperty;
			Task task = base.XsoItem as Task;
			if ((startDueDateProperty.StartDate == null && startDueDateProperty.UtcStartDate != null) || (startDueDateProperty.StartDate != null && startDueDateProperty.UtcStartDate == null) || (startDueDateProperty.DueDate == null && startDueDateProperty.UtcDueDate != null) || (startDueDateProperty.DueDate != null && startDueDateProperty.UtcDueDate == null))
			{
				throw new ConversionException("Both Utc and local dates should be present for StartDate and DueDate");
			}
			if ((startDueDateProperty.UtcStartDate != null && startDueDateProperty.UtcDueDate != null && startDueDateProperty.UtcStartDate.Value > startDueDateProperty.UtcDueDate.Value) || (startDueDateProperty.StartDate != null && startDueDateProperty.DueDate != null && startDueDateProperty.StartDate.Value > startDueDateProperty.DueDate.Value))
			{
				throw new ConversionException("StartDate should be before DueDate");
			}
			if (startDueDateProperty.StartDate == null)
			{
				task.StartDate = null;
			}
			else
			{
				base.XsoItem.SetOrDeleteProperty(ItemSchema.UtcStartDate, startDueDateProperty.UtcStartDate);
				base.XsoItem.SetOrDeleteProperty(ItemSchema.LocalStartDate, startDueDateProperty.StartDate);
			}
			if (startDueDateProperty.DueDate == null)
			{
				task.DueDate = null;
				return;
			}
			base.XsoItem.SetOrDeleteProperty(ItemSchema.UtcDueDate, startDueDateProperty.UtcDueDate);
			base.XsoItem.SetOrDeleteProperty(ItemSchema.LocalDueDate, startDueDateProperty.DueDate);
		}
	}
}

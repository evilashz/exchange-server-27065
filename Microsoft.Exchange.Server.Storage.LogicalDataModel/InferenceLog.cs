using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000066 RID: 102
	public class InferenceLog : PrivateObjectPropertyBag
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00045B44 File Offset: 0x00043D44
		protected InferenceLog(Context context, Mailbox mailbox, bool newItem, params ColumnValue[] initialValues) : base(context, DatabaseSchema.InferenceLogTable(context.Database).Table, newItem, false, newItem, true, initialValues)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				mailbox.IsValid();
				this.mailbox = mailbox;
				base.LoadData(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00045BB0 File Offset: 0x00043DB0
		protected InferenceLog(Context context, Mailbox mailbox, Reader reader) : base(context, DatabaseSchema.InferenceLogTable(context.Database).Table, false, true, reader)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				mailbox.IsValid();
				this.mailbox = mailbox;
				base.LoadData(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00045C1C File Offset: 0x00043E1C
		protected override ObjectType GetObjectType()
		{
			return ObjectType.InferenceLog;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00045C20 File Offset: 0x00043E20
		public override ObjectPropertySchema Schema
		{
			get
			{
				if (this.propertySchema == null)
				{
					this.propertySchema = PropertySchema.GetObjectSchema(this.Mailbox.Database, ObjectType.InferenceLog);
				}
				return this.propertySchema;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00045C48 File Offset: 0x00043E48
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00045C50 File Offset: 0x00043E50
		public override Context CurrentOperationContext
		{
			get
			{
				return this.Mailbox.CurrentOperationContext;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00045C5D File Offset: 0x00043E5D
		public override ReplidGuidMap ReplidGuidMap
		{
			get
			{
				return this.Mailbox.ReplidGuidMap;
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00045C6A File Offset: 0x00043E6A
		protected override StorePropTag MapPropTag(Context context, uint propertyTag)
		{
			return this.Mailbox.GetStorePropTag(context, propertyTag, this.GetObjectType());
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00045C7F File Offset: 0x00043E7F
		public static InferenceLog Open(Context context, Mailbox mailbox, Reader reader)
		{
			return new InferenceLog(context, mailbox, reader);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00045C8C File Offset: 0x00043E8C
		public static InferenceLog Create(Context context, Mailbox mailbox)
		{
			InferenceLogTable inferenceLogTable = DatabaseSchema.InferenceLogTable(mailbox.Database);
			return new InferenceLog(context, mailbox, true, new ColumnValue[]
			{
				new ColumnValue(inferenceLogTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber),
				new ColumnValue(inferenceLogTable.CreateTime, DateTime.UtcNow)
			});
		}

		// Token: 0x040003F0 RID: 1008
		private readonly Mailbox mailbox;

		// Token: 0x040003F1 RID: 1009
		private ObjectPropertySchema propertySchema;
	}
}

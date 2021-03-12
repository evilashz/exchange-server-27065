using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CBF RID: 3263
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReportMessageSchema : MessageItemSchema
	{
		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x06007171 RID: 29041 RVA: 0x001F73C4 File Offset: 0x001F55C4
		public new static ReportMessageSchema Instance
		{
			get
			{
				if (ReportMessageSchema.instance == null)
				{
					ReportMessageSchema.instance = new ReportMessageSchema();
				}
				return ReportMessageSchema.instance;
			}
		}

		// Token: 0x06007172 RID: 29042 RVA: 0x001F73DC File Offset: 0x001F55DC
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			base.CoreObjectUpdate(coreItem, operation);
			ReportMessage.CoreObjectUpdateSubjectPrefix(coreItem);
		}

		// Token: 0x04004EAD RID: 20141
		private static ReportMessageSchema instance = null;

		// Token: 0x04004EAE RID: 20142
		[Autoload]
		public static readonly StorePropertyDefinition OriginalMessageId = InternalSchema.OriginalMessageId;

		// Token: 0x04004EAF RID: 20143
		[Autoload]
		public new static readonly StorePropertyDefinition ReceivedRepresenting = InternalSchema.ReceivedRepresenting;

		// Token: 0x04004EB0 RID: 20144
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalDisplayBcc = InternalSchema.OriginalDisplayBcc;

		// Token: 0x04004EB1 RID: 20145
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalDisplayCc = InternalSchema.OriginalDisplayCc;

		// Token: 0x04004EB2 RID: 20146
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalDisplayTo = InternalSchema.OriginalDisplayTo;

		// Token: 0x04004EB3 RID: 20147
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalSentTime = InternalSchema.OriginalSentTime;

		// Token: 0x04004EB4 RID: 20148
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalSubject = InternalSchema.OriginalSubject;

		// Token: 0x04004EB5 RID: 20149
		[Autoload]
		internal static readonly StorePropertyDefinition ReportTime = InternalSchema.ReportTime;

		// Token: 0x04004EB6 RID: 20150
		[Autoload]
		internal static readonly StorePropertyDefinition ReportingMta = InternalSchema.ReportingMta;

		// Token: 0x04004EB7 RID: 20151
		[Autoload]
		internal new static readonly StorePropertyDefinition MessageLocaleId = InternalSchema.MessageLocaleId;
	}
}

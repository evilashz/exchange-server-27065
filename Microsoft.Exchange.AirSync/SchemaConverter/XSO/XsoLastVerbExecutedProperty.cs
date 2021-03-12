using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000221 RID: 545
	internal class XsoLastVerbExecutedProperty : XsoIntegerProperty
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x00078523 File Offset: 0x00076723
		public XsoLastVerbExecutedProperty() : base(MessageItemSchema.LastVerbExecuted, false)
		{
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00078534 File Offset: 0x00076734
		public override int IntegerData
		{
			get
			{
				switch (base.XsoItem.GetValueOrDefault<int>(base.PropertyDef, -1))
				{
				case 102:
					return 1;
				case 103:
					return 2;
				case 104:
					return 3;
				default:
					return 0;
				}
			}
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00078574 File Offset: 0x00076774
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			switch (((IIntegerProperty)srcProperty).IntegerData)
			{
			case 1:
				base.XsoItem[base.PropertyDef] = 102;
				return;
			case 2:
				base.XsoItem[base.PropertyDef] = 103;
				return;
			case 3:
				base.XsoItem[base.PropertyDef] = 104;
				return;
			default:
				return;
			}
		}

		// Token: 0x02000222 RID: 546
		private struct ProtocolValue
		{
			// Token: 0x04000C6E RID: 3182
			public const int Unknown = 0;

			// Token: 0x04000C6F RID: 3183
			public const int ReplyToSender = 1;

			// Token: 0x04000C70 RID: 3184
			public const int ReplyToAll = 2;

			// Token: 0x04000C71 RID: 3185
			public const int Forward = 3;
		}

		// Token: 0x02000223 RID: 547
		private struct StoreValue
		{
			// Token: 0x04000C72 RID: 3186
			public const int ReplyToSender = 102;

			// Token: 0x04000C73 RID: 3187
			public const int ReplyToAll = 103;

			// Token: 0x04000C74 RID: 3188
			public const int Forward = 104;
		}
	}
}

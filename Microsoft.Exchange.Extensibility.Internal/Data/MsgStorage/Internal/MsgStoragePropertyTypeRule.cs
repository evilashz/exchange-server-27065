using System;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000AE RID: 174
	internal class MsgStoragePropertyTypeRule
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x00019702 File Offset: 0x00017902
		internal MsgStoragePropertyTypeRule(MsgStoragePropertyTypeRule.ReadFixedValueDelegate valueReader, MsgStoragePropertyTypeRule.WriteValueDelegate valueWriter)
		{
			this.fixedValueReader = valueReader;
			this.valueWriter = valueWriter;
			this.canOpenStream = false;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001971F File Offset: 0x0001791F
		internal MsgStoragePropertyTypeRule(bool canOpenStream, MsgStoragePropertyTypeRule.ReadStreamedValueDelegate valueReader, MsgStoragePropertyTypeRule.WriteValueDelegate valueWriter)
		{
			this.streamedValueReader = valueReader;
			this.valueWriter = valueWriter;
			this.canOpenStream = canOpenStream;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001973C File Offset: 0x0001793C
		internal MsgStoragePropertyTypeRule.ReadFixedValueDelegate ReadFixedValue
		{
			get
			{
				return this.fixedValueReader;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00019744 File Offset: 0x00017944
		internal MsgStoragePropertyTypeRule.ReadStreamedValueDelegate ReadStreamedValue
		{
			get
			{
				return this.streamedValueReader;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001974C File Offset: 0x0001794C
		internal MsgStoragePropertyTypeRule.WriteValueDelegate WriteValue
		{
			get
			{
				return this.valueWriter;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00019754 File Offset: 0x00017954
		internal bool IsFixedValue
		{
			get
			{
				return this.fixedValueReader != null;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00019762 File Offset: 0x00017962
		internal bool CanOpenStream
		{
			get
			{
				return this.canOpenStream;
			}
		}

		// Token: 0x04000589 RID: 1417
		private MsgStoragePropertyTypeRule.WriteValueDelegate valueWriter;

		// Token: 0x0400058A RID: 1418
		private MsgStoragePropertyTypeRule.ReadFixedValueDelegate fixedValueReader;

		// Token: 0x0400058B RID: 1419
		private MsgStoragePropertyTypeRule.ReadStreamedValueDelegate streamedValueReader;

		// Token: 0x0400058C RID: 1420
		private bool canOpenStream;

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x060005A1 RID: 1441
		internal delegate object ReadFixedValueDelegate(byte[] propertyStreamData, int propertyOffset);

		// Token: 0x020000B0 RID: 176
		// (Invoke) Token: 0x060005A5 RID: 1445
		internal delegate object ReadStreamedValueDelegate(MsgSubStorageReader parser, MsgSubStorageReader.PropertyInfo propertyInfo);

		// Token: 0x020000B1 RID: 177
		// (Invoke) Token: 0x060005A9 RID: 1449
		internal delegate void WriteValueDelegate(MsgSubStorageWriter writer, TnefPropertyTag propertyTag, object propertyValue);
	}
}

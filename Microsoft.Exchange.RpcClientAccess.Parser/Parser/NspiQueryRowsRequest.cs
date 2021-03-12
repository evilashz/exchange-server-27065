using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001DE RID: 478
	internal sealed class NspiQueryRowsRequest : MapiHttpRequest
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		public NspiQueryRowsRequest(NspiQueryRowsFlags flags, NspiState state, int[] explicitTable, uint rowCount, PropertyTag[] columns, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.state = state;
			this.explicitTable = explicitTable;
			this.rowCount = rowCount;
			this.columns = columns;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		public NspiQueryRowsRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiQueryRowsFlags)reader.ReadUInt32();
			this.state = reader.ReadNspiState();
			this.explicitTable = reader.ReadSizeAndIntegerArray(FieldLength.DWordSize);
			this.rowCount = reader.ReadUInt32();
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001F631 File Offset: 0x0001D831
		public NspiQueryRowsFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0001F639 File Offset: 0x0001D839
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001F641 File Offset: 0x0001D841
		public int[] ExplicitTable
		{
			get
			{
				return this.explicitTable;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0001F649 File Offset: 0x0001D849
		public uint RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001F651 File Offset: 0x0001D851
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001F65C File Offset: 0x0001D85C
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteNspiState(this.state);
			writer.WriteSizeAndIntegerArray(this.explicitTable, FieldLength.DWordSize);
			writer.WriteUInt32(this.rowCount);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000466 RID: 1126
		private readonly NspiQueryRowsFlags flags;

		// Token: 0x04000467 RID: 1127
		private readonly NspiState state;

		// Token: 0x04000468 RID: 1128
		private readonly int[] explicitTable;

		// Token: 0x04000469 RID: 1129
		private readonly uint rowCount;

		// Token: 0x0400046A RID: 1130
		private readonly PropertyTag[] columns;
	}
}

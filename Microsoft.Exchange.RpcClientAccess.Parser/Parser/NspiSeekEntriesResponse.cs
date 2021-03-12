using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E5 RID: 485
	internal sealed class NspiSeekEntriesResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x0001FD34 File Offset: 0x0001DF34
		public NspiSeekEntriesResponse(uint returnCode, NspiState state, PropertyTag[] columns, PropertyValue[][] propertyValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			if (columns != null && propertyValues == null)
			{
				throw new ArgumentException("The propertyValues argument cannot be null if columns is not null.");
			}
			if (columns == null && propertyValues != null)
			{
				throw new ArgumentException("The columns argument cannot be null if propertyValues is not null.");
			}
			this.state = state;
			this.columns = columns;
			if (propertyValues != null)
			{
				this.rows = new List<PropertyRow>(propertyValues.GetLength(0));
				foreach (PropertyValue[] propertyValues2 in propertyValues)
				{
					this.rows.Add(new PropertyRow(columns, propertyValues2));
				}
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public NspiSeekEntriesResponse(Reader reader) : base(reader)
		{
			this.state = reader.ReadNspiState();
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			if (this.columns != null)
			{
				this.rows = reader.ReadSizeAndPropertyRowList(this.columns, stateEncodingOrDefault, WireFormatStyle.Nspi);
			}
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001FE18 File Offset: 0x0001E018
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x0001FE20 File Offset: 0x0001E020
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001FE31 File Offset: 0x0001E031
		public PropertyValue[][] PropertyValues
		{
			get
			{
				if (this.rows != null)
				{
					return (from row in this.rows
					select row.PropertyValues).ToArray<PropertyValue[]>();
				}
				return null;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001FE6C File Offset: 0x0001E06C
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			writer.WriteNspiState(this.state);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			if (this.columns != null)
			{
				writer.WriteSizeAndPropertyRowList(this.rows, stateEncodingOrDefault, WireFormatStyle.Nspi);
			}
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000482 RID: 1154
		private readonly NspiState state;

		// Token: 0x04000483 RID: 1155
		private readonly PropertyTag[] columns;

		// Token: 0x04000484 RID: 1156
		private readonly IList<PropertyRow> rows;
	}
}

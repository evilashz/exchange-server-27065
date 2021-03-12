using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001DF RID: 479
	internal sealed class NspiQueryRowsResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		public NspiQueryRowsResponse(uint returnCode, NspiState state, PropertyTag[] columns, PropertyValue[][] propertyValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
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

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001F738 File Offset: 0x0001D938
		public NspiQueryRowsResponse(Reader reader) : base(reader)
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

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001F794 File Offset: 0x0001D994
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001F79C File Offset: 0x0001D99C
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001F7AD File Offset: 0x0001D9AD
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

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001F7E8 File Offset: 0x0001D9E8
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

		// Token: 0x0400046B RID: 1131
		private readonly NspiState state;

		// Token: 0x0400046C RID: 1132
		private readonly PropertyTag[] columns;

		// Token: 0x0400046D RID: 1133
		private readonly IList<PropertyRow> rows;
	}
}

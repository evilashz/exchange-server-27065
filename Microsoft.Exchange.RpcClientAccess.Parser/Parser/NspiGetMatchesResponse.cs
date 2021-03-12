using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001CF RID: 463
	internal sealed class NspiGetMatchesResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x0001EB58 File Offset: 0x0001CD58
		public NspiGetMatchesResponse(uint returnCode, NspiState state, int[] matches, PropertyTag[] columns, PropertyValue[][] propertyValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
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
			this.matches = matches;
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

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001EBEC File Offset: 0x0001CDEC
		public NspiGetMatchesResponse(Reader reader) : base(reader)
		{
			this.state = reader.ReadNspiState();
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			this.matches = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			if (this.columns != null)
			{
				this.rows = reader.ReadSizeAndPropertyRowList(this.columns, stateEncodingOrDefault, WireFormatStyle.Nspi);
			}
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001EC55 File Offset: 0x0001CE55
		public NspiState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0001EC5D File Offset: 0x0001CE5D
		public int[] Matches
		{
			get
			{
				return this.matches;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0001EC65 File Offset: 0x0001CE65
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001EC76 File Offset: 0x0001CE76
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

		// Token: 0x060009CD RID: 2509 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			Encoding stateEncodingOrDefault = MapiHttpOperationUtilities.GetStateEncodingOrDefault(this.state);
			writer.WriteNspiState(this.state);
			writer.WriteNullableSizeAndIntegerArray(this.matches, FieldLength.DWordSize);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			if (this.columns != null)
			{
				writer.WriteSizeAndPropertyRowList(this.rows, stateEncodingOrDefault, WireFormatStyle.Nspi);
			}
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000440 RID: 1088
		private readonly NspiState state;

		// Token: 0x04000441 RID: 1089
		private readonly int[] matches;

		// Token: 0x04000442 RID: 1090
		private readonly PropertyTag[] columns;

		// Token: 0x04000443 RID: 1091
		private readonly IList<PropertyRow> rows;
	}
}

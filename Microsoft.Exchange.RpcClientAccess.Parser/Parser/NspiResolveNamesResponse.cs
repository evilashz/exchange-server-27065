using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E1 RID: 481
	internal sealed class NspiResolveNamesResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x0001F914 File Offset: 0x0001DB14
		public NspiResolveNamesResponse(uint returnCode, uint codePage, int[] ephemeralIds, PropertyTag[] columns, PropertyValue[][] resolvedValues, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			if (columns != null && resolvedValues == null)
			{
				throw new ArgumentException("The resolvedValues argument cannot be null if columns is not null.");
			}
			if (columns == null && resolvedValues != null)
			{
				throw new ArgumentException("The columns argument cannot be null if resolvedValues is not null.");
			}
			this.codePage = codePage;
			this.ephemeralIds = ephemeralIds;
			this.columns = columns;
			if (resolvedValues != null)
			{
				this.rows = new List<PropertyRow>(resolvedValues.GetLength(0));
				foreach (PropertyValue[] propertyValues in resolvedValues)
				{
					this.rows.Add(new PropertyRow(columns, propertyValues));
				}
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		public NspiResolveNamesResponse(Reader reader) : base(reader)
		{
			this.codePage = reader.ReadUInt32();
			Encoding asciiEncoding;
			if (!String8Encodings.TryGetEncoding((int)this.codePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			this.ephemeralIds = reader.ReadNullableSizeAndIntegerArray(FieldLength.DWordSize);
			this.columns = reader.ReadNullableCountAndPropertyTagArray(FieldLength.DWordSize);
			if (this.columns != null)
			{
				this.rows = reader.ReadSizeAndPropertyRowList(this.columns, asciiEncoding, WireFormatStyle.Nspi);
			}
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001FA1A File Offset: 0x0001DC1A
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0001FA22 File Offset: 0x0001DC22
		public int[] EphemeralIds
		{
			get
			{
				return this.ephemeralIds;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001FA2A File Offset: 0x0001DC2A
		public PropertyTag[] Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001FA3B File Offset: 0x0001DC3B
		public PropertyValue[][] ResolvedValues
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

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001FA74 File Offset: 0x0001DC74
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			Encoding asciiEncoding;
			if (!String8Encodings.TryGetEncoding((int)this.codePage, out asciiEncoding))
			{
				asciiEncoding = CTSGlobals.AsciiEncoding;
			}
			writer.WriteUInt32(this.codePage);
			writer.WriteNullableSizeAndIntegerArray(this.ephemeralIds, FieldLength.DWordSize);
			writer.WriteNullableCountAndPropertyTagArray(this.columns, FieldLength.DWordSize);
			if (this.columns != null)
			{
				writer.WriteSizeAndPropertyRowList(this.rows, asciiEncoding, WireFormatStyle.Nspi);
			}
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000473 RID: 1139
		private readonly uint codePage;

		// Token: 0x04000474 RID: 1140
		private readonly int[] ephemeralIds;

		// Token: 0x04000475 RID: 1141
		private readonly PropertyTag[] columns;

		// Token: 0x04000476 RID: 1142
		private readonly IList<PropertyRow> rows;
	}
}

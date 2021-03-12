using System;
using System.IO;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000F3 RID: 243
	public struct TnefPropertyReader
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00039A54 File Offset: 0x00037C54
		internal TnefPropertyReader(TnefReader reader)
		{
			this.Reader = reader;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00039A5D File Offset: 0x00037C5D
		public int RowCount
		{
			get
			{
				return this.Reader.RowCount;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00039A6A File Offset: 0x00037C6A
		public int PropertyCount
		{
			get
			{
				return this.Reader.PropertyCount;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00039A77 File Offset: 0x00037C77
		public TnefPropertyTag PropertyTag
		{
			get
			{
				return this.Reader.PropertyTag;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00039A84 File Offset: 0x00037C84
		public bool IsMultiValuedProperty
		{
			get
			{
				return this.Reader.PropertyTag.IsMultiValued;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00039AA4 File Offset: 0x00037CA4
		public int ValueCount
		{
			get
			{
				return this.Reader.PropertyValueCount;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00039AB4 File Offset: 0x00037CB4
		public bool IsNamedProperty
		{
			get
			{
				return this.Reader.PropertyTag.IsNamed;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00039AD4 File Offset: 0x00037CD4
		public TnefNameId PropertyNameId
		{
			get
			{
				return this.Reader.PropertyNameId;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00039AE1 File Offset: 0x00037CE1
		public Type ValueType
		{
			get
			{
				return this.Reader.PropertyValueClrType;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00039AF0 File Offset: 0x00037CF0
		public bool IsObjectProperty
		{
			get
			{
				return this.Reader.PropertyTag.ValueTnefType == TnefPropertyType.Object;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00039B14 File Offset: 0x00037D14
		public Guid ObjectIid
		{
			get
			{
				return this.Reader.PropertyValueOleIID;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00039B21 File Offset: 0x00037D21
		public bool IsEmbeddedMessage
		{
			get
			{
				return this.Reader.IsPropertyEmbeddedMessage;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00039B2E File Offset: 0x00037D2E
		public bool IsLargeValue
		{
			get
			{
				return this.Reader.IsLargePropertyValue;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00039B3B File Offset: 0x00037D3B
		public bool IsComputedProperty
		{
			get
			{
				return this.Reader.IsComputedProperty;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00039B48 File Offset: 0x00037D48
		public int RawValueStreamOffset
		{
			get
			{
				return this.Reader.PropertyRawValueStreamOffset;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00039B55 File Offset: 0x00037D55
		public int RawValueLength
		{
			get
			{
				return this.Reader.PropertyRawValueLength;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00039B62 File Offset: 0x00037D62
		public bool ReadValueAsBoolean()
		{
			return this.Reader.ReadPropertyValueAsBool();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00039B6F File Offset: 0x00037D6F
		public short ReadValueAsInt16()
		{
			return this.Reader.ReadPropertyValueAsShort();
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00039B7C File Offset: 0x00037D7C
		public int ReadValueAsInt32()
		{
			return this.Reader.ReadPropertyValueAsInt();
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00039B89 File Offset: 0x00037D89
		public long ReadValueAsInt64()
		{
			return this.Reader.ReadPropertyValueAsLong();
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00039B96 File Offset: 0x00037D96
		public Guid ReadValueAsGuid()
		{
			return this.Reader.ReadPropertyValueAsGuid();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00039BA3 File Offset: 0x00037DA3
		public float ReadValueAsFloat()
		{
			return this.Reader.ReadPropertyValueAsFloat();
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00039BB0 File Offset: 0x00037DB0
		public double ReadValueAsDouble()
		{
			return this.Reader.ReadPropertyValueAsDouble();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00039BBD File Offset: 0x00037DBD
		public DateTime ReadValueAsDateTime()
		{
			return this.Reader.ReadPropertyValueAsDateTime();
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00039BCA File Offset: 0x00037DCA
		public string ReadValueAsString()
		{
			return this.Reader.ReadPropertyValueAsString();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00039BD7 File Offset: 0x00037DD7
		public byte[] ReadValueAsBytes()
		{
			return this.Reader.ReadPropertyValueAsByteArray();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00039BE4 File Offset: 0x00037DE4
		public object ReadValue()
		{
			return this.Reader.ReadPropertyValue();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00039BF1 File Offset: 0x00037DF1
		public int ReadTextValue(char[] buffer, int offset, int count)
		{
			return this.Reader.ReadPropertyTextValue(buffer, offset, count);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00039C01 File Offset: 0x00037E01
		public int ReadRawValue(byte[] buffer, int offset, int count)
		{
			return this.Reader.ReadPropertyRawValue(buffer, offset, count, false);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00039C12 File Offset: 0x00037E12
		public TnefReader GetEmbeddedMessageReader()
		{
			return this.Reader.GetEmbeddedMessageReader();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00039C1F File Offset: 0x00037E1F
		public Stream GetRawValueReadStream()
		{
			return this.Reader.GetRawPropertyValueReadStream();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00039C2C File Offset: 0x00037E2C
		public bool ReadNextValue()
		{
			return this.Reader.ReadNextPropertyValue();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00039C39 File Offset: 0x00037E39
		public bool ReadNextProperty()
		{
			return this.Reader.ReadNextProperty();
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00039C46 File Offset: 0x00037E46
		public bool ReadNextRow()
		{
			return this.Reader.ReadNextRow();
		}

		// Token: 0x04000D38 RID: 3384
		internal TnefReader Reader;
	}
}

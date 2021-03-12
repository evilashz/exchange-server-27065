using System;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000B3 RID: 179
	public struct MsgStoragePropertyReader
	{
		// Token: 0x060005BC RID: 1468 RVA: 0x000199F6 File Offset: 0x00017BF6
		internal MsgStoragePropertyReader(MsgStorageReader reader)
		{
			this.Reader = reader;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000199FF File Offset: 0x00017BFF
		public bool ReadNextProperty()
		{
			return this.Reader.SubStorageReader.ReadNextProperty();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00019A11 File Offset: 0x00017C11
		public TnefPropertyTag PropertyTag
		{
			get
			{
				return this.Reader.SubStorageReader.PropertyTag;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00019A24 File Offset: 0x00017C24
		public TnefPropertyType PropertyType
		{
			get
			{
				return this.Reader.SubStorageReader.PropertyTag.TnefType;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00019A4C File Offset: 0x00017C4C
		public bool IsNamedProperty
		{
			get
			{
				return this.Reader.SubStorageReader.PropertyTag.IsNamed;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00019A74 File Offset: 0x00017C74
		public TnefNameId PropertyNameId
		{
			get
			{
				TnefPropertyTag propertyTag = this.Reader.SubStorageReader.PropertyTag;
				if (!propertyTag.IsNamed)
				{
					throw new InvalidOperationException(MsgStorageStrings.NotANamedProperty);
				}
				TnefNameId result;
				if (!this.Reader.NamedPropertyList.TryGetValue(propertyTag.Id, out result))
				{
					throw new MsgStorageException(MsgStorageErrorCode.NamedPropertyNotFound, MsgStorageStrings.CorruptData);
				}
				return result;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00019AD0 File Offset: 0x00017CD0
		public bool IsMultiValuedProperty
		{
			get
			{
				return this.Reader.SubStorageReader.PropertyTag.IsMultiValued;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00019AF5 File Offset: 0x00017CF5
		public bool IsLargeValue
		{
			get
			{
				return this.Reader.SubStorageReader.IsLargeValue();
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00019B07 File Offset: 0x00017D07
		public object ReadValue()
		{
			return this.Reader.SubStorageReader.ReadPropertyValue();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019B19 File Offset: 0x00017D19
		public Stream GetValueReadStream()
		{
			return this.Reader.SubStorageReader.OpenPropertyStream();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019B2B File Offset: 0x00017D2B
		public Stream GetValueReadStream(TnefPropertyTag propertyTag)
		{
			return this.Reader.SubStorageReader.OpenPropertyStream(propertyTag);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019B3E File Offset: 0x00017D3E
		public MsgStorageReader GetEmbeddedMessageReader()
		{
			return this.Reader.SubStorageReader.OpenAttachedMessage();
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00019B50 File Offset: 0x00017D50
		public int AttachMethod
		{
			get
			{
				return this.Reader.SubStorageReader.AttachMethod;
			}
		}

		// Token: 0x04000598 RID: 1432
		internal MsgStorageReader Reader;
	}
}

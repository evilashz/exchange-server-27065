using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006AD RID: 1709
	[XmlInclude(typeof(AlternatePublicFolderId))]
	[KnownType(typeof(AlternateId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(AlternatePublicFolderItemId))]
	[XmlInclude(typeof(AlternateId))]
	[XmlType(TypeName = "AlternateIdBaseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[KnownType(typeof(AlternatePublicFolderId))]
	[KnownType(typeof(AlternatePublicFolderItemId))]
	[Serializable]
	public class AlternateIdBase
	{
		// Token: 0x060034B8 RID: 13496 RVA: 0x000BE0B8 File Offset: 0x000BC2B8
		public AlternateIdBase()
		{
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000BE0C0 File Offset: 0x000BC2C0
		internal AlternateIdBase(IdFormat format)
		{
			this.format = format;
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000BE0CF File Offset: 0x000BC2CF
		// (set) Token: 0x060034BB RID: 13499 RVA: 0x000BE0D7 File Offset: 0x000BC2D7
		[XmlAttribute]
		[IgnoreDataMember]
		public IdFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x000BE0E0 File Offset: 0x000BC2E0
		// (set) Token: 0x060034BD RID: 13501 RVA: 0x000BE0F2 File Offset: 0x000BC2F2
		[XmlIgnore]
		[DataMember(Name = "Format", IsRequired = true, Order = 1)]
		public string FormatString
		{
			get
			{
				return this.format.ToString();
			}
			set
			{
				this.format = (IdFormat)Enum.Parse(typeof(IdFormat), value);
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000BE10F File Offset: 0x000BC30F
		internal static BaseAlternateIdConverter GetIdConverter(IdFormat idFormat)
		{
			return AlternateIdBase.alternateIdMap.Member[idFormat];
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000BE121 File Offset: 0x000BC321
		internal virtual CanonicalConvertedId Parse()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000BE128 File Offset: 0x000BC328
		internal AlternateIdBase ConvertId(IdFormat destinationFormat)
		{
			CanonicalConvertedId canonicalId = this.Parse();
			BaseAlternateIdConverter idConverter = AlternateIdBase.GetIdConverter(destinationFormat);
			return idConverter.Format(canonicalId);
		}

		// Token: 0x04001DB3 RID: 7603
		private static LazyMember<Dictionary<IdFormat, BaseAlternateIdConverter>> alternateIdMap = new LazyMember<Dictionary<IdFormat, BaseAlternateIdConverter>>(() => new Dictionary<IdFormat, BaseAlternateIdConverter>
		{
			{
				IdFormat.EntryId,
				new EntryIdConverter()
			},
			{
				IdFormat.EwsLegacyId,
				new EwsLegacyIdConverter()
			},
			{
				IdFormat.EwsId,
				new EwsIdConverter()
			},
			{
				IdFormat.HexEntryId,
				new HexEntryIdConverter()
			},
			{
				IdFormat.StoreId,
				new StoreIdConverter()
			},
			{
				IdFormat.OwaId,
				new OwaIdConverter()
			}
		});

		// Token: 0x04001DB4 RID: 7604
		private IdFormat format;
	}
}

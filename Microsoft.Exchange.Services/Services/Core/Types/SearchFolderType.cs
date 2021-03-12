using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000636 RID: 1590
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "SearchFolder")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchFolderType : FolderType
	{
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000B739A File Offset: 0x000B559A
		// (set) Token: 0x060031A5 RID: 12709 RVA: 0x000B73AC File Offset: 0x000B55AC
		[DataMember(EmitDefaultValue = false)]
		public SearchParametersType SearchParameters
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SearchParametersType>(SearchFolderSchema.SearchParameters);
			}
			set
			{
				base.PropertyBag[SearchFolderSchema.SearchParameters] = value;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000B73BF File Offset: 0x000B55BF
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.SearchFolder;
			}
		}
	}
}

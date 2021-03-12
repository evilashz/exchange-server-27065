using System;
using Microsoft.Ceres.ContentEngine.Parsing.Component;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x0200015E RID: 350
	[Serializable]
	internal class SearchDocumentFormatInfoObject : ConfigurableObject
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003A650 File Offset: 0x00038850
		internal SearchDocumentFormatInfoObject(FileFormatInfo ffInfo) : base(new SimpleProviderPropertyBag())
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Current);
			this[SimpleProviderObjectSchema.Identity] = new SearchDocumentFormatId(ffInfo.Id);
			this[SearchDocumentFormatInfoSchema.DocumentClass] = ffInfo.DocumentClass;
			this[SearchDocumentFormatInfoSchema.Enabled] = ffInfo.Enabled;
			this[SearchDocumentFormatInfoSchema.Extension] = ffInfo.Extension;
			this[SearchDocumentFormatInfoSchema.FormatHandler] = ffInfo.FormatHandler;
			this[SearchDocumentFormatInfoSchema.IsBindUserDefined] = ffInfo.IsBindUserDefined;
			this[SearchDocumentFormatInfoSchema.IsFormatUserDefined] = ffInfo.IsFormatUserDefined;
			this[SearchDocumentFormatInfoSchema.MimeType] = ffInfo.Mime;
			this[SearchDocumentFormatInfoSchema.Name] = ffInfo.Name;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0003A720 File Offset: 0x00038920
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SearchDocumentFormatInfoObject.schema;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0003A727 File Offset: 0x00038927
		public string DocumentClass
		{
			get
			{
				return (string)this[SearchDocumentFormatInfoSchema.DocumentClass];
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0003A739 File Offset: 0x00038939
		public bool Enabled
		{
			get
			{
				return (bool)this[SearchDocumentFormatInfoSchema.Enabled];
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0003A74B File Offset: 0x0003894B
		public string Extension
		{
			get
			{
				return (string)this[SearchDocumentFormatInfoSchema.Extension];
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0003A75D File Offset: 0x0003895D
		public string FormatHandler
		{
			get
			{
				return (string)this[SearchDocumentFormatInfoSchema.FormatHandler];
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0003A76F File Offset: 0x0003896F
		public bool IsBindUserDefined
		{
			get
			{
				return (bool)this[SearchDocumentFormatInfoSchema.IsBindUserDefined];
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0003A781 File Offset: 0x00038981
		public bool IsFormatUserDefined
		{
			get
			{
				return (bool)this[SearchDocumentFormatInfoSchema.IsFormatUserDefined];
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0003A793 File Offset: 0x00038993
		public string MimeType
		{
			get
			{
				return (string)this[SearchDocumentFormatInfoSchema.MimeType];
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0003A7A5 File Offset: 0x000389A5
		public string Name
		{
			get
			{
				return (string)this[SearchDocumentFormatInfoSchema.Name];
			}
		}

		// Token: 0x04000632 RID: 1586
		private static ObjectSchema schema = ObjectSchema.GetInstance<SearchDocumentFormatInfoSchema>();
	}
}

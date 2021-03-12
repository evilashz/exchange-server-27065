using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000213 RID: 531
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SearchFolderType : FolderType
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0002617C File Offset: 0x0002437C
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00026184 File Offset: 0x00024384
		public SearchParametersType SearchParameters
		{
			get
			{
				return this.searchParametersField;
			}
			set
			{
				this.searchParametersField = value;
			}
		}

		// Token: 0x04000E83 RID: 3715
		private SearchParametersType searchParametersField;
	}
}

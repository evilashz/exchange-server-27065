using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000278 RID: 632
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class SuggestionsResponseType
	{
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00027591 File Offset: 0x00025791
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x00027599 File Offset: 0x00025799
		public ResponseMessageType ResponseMessage
		{
			get
			{
				return this.responseMessageField;
			}
			set
			{
				this.responseMessageField = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x000275A2 File Offset: 0x000257A2
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x000275AA File Offset: 0x000257AA
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SuggestionDayResult[] SuggestionDayResultArray
		{
			get
			{
				return this.suggestionDayResultArrayField;
			}
			set
			{
				this.suggestionDayResultArrayField = value;
			}
		}

		// Token: 0x04000FD6 RID: 4054
		private ResponseMessageType responseMessageField;

		// Token: 0x04000FD7 RID: 4055
		private SuggestionDayResult[] suggestionDayResultArrayField;
	}
}

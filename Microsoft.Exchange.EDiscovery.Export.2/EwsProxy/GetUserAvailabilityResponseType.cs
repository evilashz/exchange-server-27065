using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000282 RID: 642
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserAvailabilityResponseType
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0002781B File Offset: 0x00025A1B
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x00027823 File Offset: 0x00025A23
		[XmlArrayItem("FreeBusyResponse", IsNullable = false)]
		public FreeBusyResponseType[] FreeBusyResponseArray
		{
			get
			{
				return this.freeBusyResponseArrayField;
			}
			set
			{
				this.freeBusyResponseArrayField = value;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0002782C File Offset: 0x00025A2C
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x00027834 File Offset: 0x00025A34
		public SuggestionsResponseType SuggestionsResponse
		{
			get
			{
				return this.suggestionsResponseField;
			}
			set
			{
				this.suggestionsResponseField = value;
			}
		}

		// Token: 0x04000FFF RID: 4095
		private FreeBusyResponseType[] freeBusyResponseArrayField;

		// Token: 0x04001000 RID: 4096
		private SuggestionsResponseType suggestionsResponseField;
	}
}

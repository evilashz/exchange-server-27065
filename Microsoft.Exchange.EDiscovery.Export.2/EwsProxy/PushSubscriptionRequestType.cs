using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D5 RID: 725
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PushSubscriptionRequestType : BaseSubscriptionRequestType
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00027EE5 File Offset: 0x000260E5
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x00027EED File Offset: 0x000260ED
		public int StatusFrequency
		{
			get
			{
				return this.statusFrequencyField;
			}
			set
			{
				this.statusFrequencyField = value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00027EF6 File Offset: 0x000260F6
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x00027EFE File Offset: 0x000260FE
		public string URL
		{
			get
			{
				return this.uRLField;
			}
			set
			{
				this.uRLField = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00027F07 File Offset: 0x00026107
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x00027F0F File Offset: 0x0002610F
		public string CallerData
		{
			get
			{
				return this.callerDataField;
			}
			set
			{
				this.callerDataField = value;
			}
		}

		// Token: 0x040010A3 RID: 4259
		private int statusFrequencyField;

		// Token: 0x040010A4 RID: 4260
		private string uRLField;

		// Token: 0x040010A5 RID: 4261
		private string callerDataField;
	}
}

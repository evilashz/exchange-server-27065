using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200031B RID: 795
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class StartFindInGALSpeechRecognitionType : BaseRequestType
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x00028B56 File Offset: 0x00026D56
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x00028B5E File Offset: 0x00026D5E
		public string Culture
		{
			get
			{
				return this.cultureField;
			}
			set
			{
				this.cultureField = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00028B67 File Offset: 0x00026D67
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x00028B6F File Offset: 0x00026D6F
		public string TimeZone
		{
			get
			{
				return this.timeZoneField;
			}
			set
			{
				this.timeZoneField = value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x00028B78 File Offset: 0x00026D78
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x00028B80 File Offset: 0x00026D80
		public string UserObjectGuid
		{
			get
			{
				return this.userObjectGuidField;
			}
			set
			{
				this.userObjectGuidField = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00028B89 File Offset: 0x00026D89
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x00028B91 File Offset: 0x00026D91
		public string TenantGuid
		{
			get
			{
				return this.tenantGuidField;
			}
			set
			{
				this.tenantGuidField = value;
			}
		}

		// Token: 0x0400117B RID: 4475
		private string cultureField;

		// Token: 0x0400117C RID: 4476
		private string timeZoneField;

		// Token: 0x0400117D RID: 4477
		private string userObjectGuidField;

		// Token: 0x0400117E RID: 4478
		private string tenantGuidField;
	}
}

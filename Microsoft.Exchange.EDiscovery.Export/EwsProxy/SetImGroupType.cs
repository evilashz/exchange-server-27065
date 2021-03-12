using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000320 RID: 800
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class SetImGroupType : BaseRequestType
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00028BED File Offset: 0x00026DED
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x00028BF5 File Offset: 0x00026DF5
		public ItemIdType GroupId
		{
			get
			{
				return this.groupIdField;
			}
			set
			{
				this.groupIdField = value;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00028BFE File Offset: 0x00026DFE
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x00028C06 File Offset: 0x00026E06
		public string NewDisplayName
		{
			get
			{
				return this.newDisplayNameField;
			}
			set
			{
				this.newDisplayNameField = value;
			}
		}

		// Token: 0x0400118C RID: 4492
		private ItemIdType groupIdField;

		// Token: 0x0400118D RID: 4493
		private string newDisplayNameField;
	}
}

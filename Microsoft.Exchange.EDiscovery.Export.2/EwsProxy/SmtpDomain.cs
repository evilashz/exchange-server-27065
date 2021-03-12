using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C4 RID: 452
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class SmtpDomain
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x000253C3 File Offset: 0x000235C3
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x000253CB File Offset: 0x000235CB
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000253D4 File Offset: 0x000235D4
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x000253DC File Offset: 0x000235DC
		[XmlAttribute]
		public bool IncludeSubdomains
		{
			get
			{
				return this.includeSubdomainsField;
			}
			set
			{
				this.includeSubdomainsField = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x000253E5 File Offset: 0x000235E5
		// (set) Token: 0x06001371 RID: 4977 RVA: 0x000253ED File Offset: 0x000235ED
		[XmlIgnore]
		public bool IncludeSubdomainsSpecified
		{
			get
			{
				return this.includeSubdomainsFieldSpecified;
			}
			set
			{
				this.includeSubdomainsFieldSpecified = value;
			}
		}

		// Token: 0x04000D72 RID: 3442
		private string nameField;

		// Token: 0x04000D73 RID: 3443
		private bool includeSubdomainsField;

		// Token: 0x04000D74 RID: 3444
		private bool includeSubdomainsFieldSpecified;
	}
}

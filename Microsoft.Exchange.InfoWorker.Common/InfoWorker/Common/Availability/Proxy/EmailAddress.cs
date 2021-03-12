using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000137 RID: 311
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailAddress
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00025137 File Offset: 0x00023337
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0002513F File Offset: 0x0002333F
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

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x00025148 File Offset: 0x00023348
		// (set) Token: 0x06000879 RID: 2169 RVA: 0x00025150 File Offset: 0x00023350
		public string Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00025159 File Offset: 0x00023359
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x00025161 File Offset: 0x00023361
		public string RoutingType
		{
			get
			{
				return this.routingTypeField;
			}
			set
			{
				this.routingTypeField = value;
			}
		}

		// Token: 0x040006A7 RID: 1703
		private string nameField;

		// Token: 0x040006A8 RID: 1704
		private string addressField;

		// Token: 0x040006A9 RID: 1705
		private string routingTypeField;
	}
}

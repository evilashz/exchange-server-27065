using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020000DF RID: 223
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailAddressType : BaseEmailAddressType
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000192F1 File Offset: 0x000174F1
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x000192F9 File Offset: 0x000174F9
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00019302 File Offset: 0x00017502
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001930A File Offset: 0x0001750A
		public string EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00019313 File Offset: 0x00017513
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0001931B File Offset: 0x0001751B
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

		// Token: 0x04000361 RID: 865
		private string nameField;

		// Token: 0x04000362 RID: 866
		private string emailAddressField;

		// Token: 0x04000363 RID: 867
		private string routingTypeField;
	}
}

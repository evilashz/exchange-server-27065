using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000079 RID: 121
	[XmlRoot(Namespace = "", IsNullable = false)]
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TextMessagingHostingData
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00015ABD File Offset: 0x00013CBD
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00015AC5 File Offset: 0x00013CC5
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingData_locDefinition _locDefinition
		{
			get
			{
				return this._locDefinitionField;
			}
			set
			{
				this._locDefinitionField = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00015ACE File Offset: 0x00013CCE
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00015AD6 File Offset: 0x00013CD6
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataRegions Regions
		{
			get
			{
				return this.regionsField;
			}
			set
			{
				this.regionsField = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00015ADF File Offset: 0x00013CDF
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00015AE7 File Offset: 0x00013CE7
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataCarriers Carriers
		{
			get
			{
				return this.carriersField;
			}
			set
			{
				this.carriersField = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00015AF0 File Offset: 0x00013CF0
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x00015AF8 File Offset: 0x00013CF8
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataServices Services
		{
			get
			{
				return this.servicesField;
			}
			set
			{
				this.servicesField = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00015B01 File Offset: 0x00013D01
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x00015B09 File Offset: 0x00013D09
		[XmlAttribute]
		public string Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x0400025E RID: 606
		private TextMessagingHostingData_locDefinition _locDefinitionField;

		// Token: 0x0400025F RID: 607
		private TextMessagingHostingDataRegions regionsField;

		// Token: 0x04000260 RID: 608
		private TextMessagingHostingDataCarriers carriersField;

		// Token: 0x04000261 RID: 609
		private TextMessagingHostingDataServices servicesField;

		// Token: 0x04000262 RID: 610
		private string versionField;
	}
}

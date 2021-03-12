using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200018A RID: 394
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01")]
	[Serializable]
	public class AttributeSet
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00021660 File Offset: 0x0001F860
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00021668 File Offset: 0x0001F868
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

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00021671 File Offset: 0x0001F871
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00021679 File Offset: 0x0001F879
		[XmlAttribute]
		public bool ExchangeMastered
		{
			get
			{
				return this.exchangeMasteredField;
			}
			set
			{
				this.exchangeMasteredField = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00021682 File Offset: 0x0001F882
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0002168A File Offset: 0x0001F88A
		[XmlAttribute(DataType = "positiveInteger")]
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00021693 File Offset: 0x0001F893
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0002169B File Offset: 0x0001F89B
		[XmlAttribute(DataType = "positiveInteger")]
		public string LastVersionSeized
		{
			get
			{
				return this.lastVersionSeizedField;
			}
			set
			{
				this.lastVersionSeizedField = value;
			}
		}

		// Token: 0x04000538 RID: 1336
		private string nameField;

		// Token: 0x04000539 RID: 1337
		private bool exchangeMasteredField;

		// Token: 0x0400053A RID: 1338
		private string versionField;

		// Token: 0x0400053B RID: 1339
		private string lastVersionSeizedField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000952 RID: 2386
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AttributeSet
	{
		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x06007061 RID: 28769 RVA: 0x001773D7 File Offset: 0x001755D7
		// (set) Token: 0x06007062 RID: 28770 RVA: 0x001773DF File Offset: 0x001755DF
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

		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x06007063 RID: 28771 RVA: 0x001773E8 File Offset: 0x001755E8
		// (set) Token: 0x06007064 RID: 28772 RVA: 0x001773F0 File Offset: 0x001755F0
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

		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x06007065 RID: 28773 RVA: 0x001773F9 File Offset: 0x001755F9
		// (set) Token: 0x06007066 RID: 28774 RVA: 0x00177401 File Offset: 0x00175601
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

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x06007067 RID: 28775 RVA: 0x0017740A File Offset: 0x0017560A
		// (set) Token: 0x06007068 RID: 28776 RVA: 0x00177412 File Offset: 0x00175612
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

		// Token: 0x040048E8 RID: 18664
		private string nameField;

		// Token: 0x040048E9 RID: 18665
		private bool exchangeMasteredField;

		// Token: 0x040048EA RID: 18666
		private string versionField;

		// Token: 0x040048EB RID: 18667
		private string lastVersionSeizedField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200007C RID: 124
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ServerVersionInfo : SoapHeader
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001F3B3 File Offset: 0x0001D5B3
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0001F3BB File Offset: 0x0001D5BB
		public int MajorVersion
		{
			get
			{
				return this.majorVersionField;
			}
			set
			{
				this.majorVersionField = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0001F3CC File Offset: 0x0001D5CC
		[XmlIgnore]
		public bool MajorVersionSpecified
		{
			get
			{
				return this.majorVersionFieldSpecified;
			}
			set
			{
				this.majorVersionFieldSpecified = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001F3D5 File Offset: 0x0001D5D5
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001F3DD File Offset: 0x0001D5DD
		public int MinorVersion
		{
			get
			{
				return this.minorVersionField;
			}
			set
			{
				this.minorVersionField = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001F3E6 File Offset: 0x0001D5E6
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001F3EE File Offset: 0x0001D5EE
		[XmlIgnore]
		public bool MinorVersionSpecified
		{
			get
			{
				return this.minorVersionFieldSpecified;
			}
			set
			{
				this.minorVersionFieldSpecified = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001F3F7 File Offset: 0x0001D5F7
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0001F3FF File Offset: 0x0001D5FF
		public int MajorBuildNumber
		{
			get
			{
				return this.majorBuildNumberField;
			}
			set
			{
				this.majorBuildNumberField = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001F408 File Offset: 0x0001D608
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x0001F410 File Offset: 0x0001D610
		[XmlIgnore]
		public bool MajorBuildNumberSpecified
		{
			get
			{
				return this.majorBuildNumberFieldSpecified;
			}
			set
			{
				this.majorBuildNumberFieldSpecified = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001F419 File Offset: 0x0001D619
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x0001F421 File Offset: 0x0001D621
		public int MinorBuildNumber
		{
			get
			{
				return this.minorBuildNumberField;
			}
			set
			{
				this.minorBuildNumberField = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001F42A File Offset: 0x0001D62A
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0001F432 File Offset: 0x0001D632
		[XmlIgnore]
		public bool MinorBuildNumberSpecified
		{
			get
			{
				return this.minorBuildNumberFieldSpecified;
			}
			set
			{
				this.minorBuildNumberFieldSpecified = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001F43B File Offset: 0x0001D63B
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001F443 File Offset: 0x0001D643
		[XmlElement(IsNullable = true)]
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

		// Token: 0x040002F5 RID: 757
		private int majorVersionField;

		// Token: 0x040002F6 RID: 758
		private bool majorVersionFieldSpecified;

		// Token: 0x040002F7 RID: 759
		private int minorVersionField;

		// Token: 0x040002F8 RID: 760
		private bool minorVersionFieldSpecified;

		// Token: 0x040002F9 RID: 761
		private int majorBuildNumberField;

		// Token: 0x040002FA RID: 762
		private bool majorBuildNumberFieldSpecified;

		// Token: 0x040002FB RID: 763
		private int minorBuildNumberField;

		// Token: 0x040002FC RID: 764
		private bool minorBuildNumberFieldSpecified;

		// Token: 0x040002FD RID: 765
		private string versionField;
	}
}

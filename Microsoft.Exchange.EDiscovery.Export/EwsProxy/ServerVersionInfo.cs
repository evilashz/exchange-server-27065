using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003AF RID: 943
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ServerVersionInfo : SoapHeader
	{
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0002A72E File Offset: 0x0002892E
		// (set) Token: 0x06001D55 RID: 7509 RVA: 0x0002A736 File Offset: 0x00028936
		[XmlAttribute]
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

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0002A73F File Offset: 0x0002893F
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0002A747 File Offset: 0x00028947
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

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0002A750 File Offset: 0x00028950
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0002A758 File Offset: 0x00028958
		[XmlAttribute]
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

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0002A761 File Offset: 0x00028961
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0002A769 File Offset: 0x00028969
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0002A772 File Offset: 0x00028972
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0002A77A File Offset: 0x0002897A
		[XmlAttribute]
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

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0002A783 File Offset: 0x00028983
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0002A78B File Offset: 0x0002898B
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

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0002A794 File Offset: 0x00028994
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0002A79C File Offset: 0x0002899C
		[XmlAttribute]
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

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0002A7A5 File Offset: 0x000289A5
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0002A7AD File Offset: 0x000289AD
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

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0002A7B6 File Offset: 0x000289B6
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0002A7BE File Offset: 0x000289BE
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

		// Token: 0x04001365 RID: 4965
		private int majorVersionField;

		// Token: 0x04001366 RID: 4966
		private bool majorVersionFieldSpecified;

		// Token: 0x04001367 RID: 4967
		private int minorVersionField;

		// Token: 0x04001368 RID: 4968
		private bool minorVersionFieldSpecified;

		// Token: 0x04001369 RID: 4969
		private int majorBuildNumberField;

		// Token: 0x0400136A RID: 4970
		private bool majorBuildNumberFieldSpecified;

		// Token: 0x0400136B RID: 4971
		private int minorBuildNumberField;

		// Token: 0x0400136C RID: 4972
		private bool minorBuildNumberFieldSpecified;

		// Token: 0x0400136D RID: 4973
		private string versionField;
	}
}

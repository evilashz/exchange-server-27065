using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000188 RID: 392
	[XmlRoot(Namespace = "DeltaSyncV2:", IsNullable = false)]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class Stateless
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001DB72 File Offset: 0x0001BD72
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0001DB7A File Offset: 0x0001BD7A
		[XmlElement(Namespace = "HMSYNC:")]
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0001DB83 File Offset: 0x0001BD83
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0001DB8B File Offset: 0x0001BD8B
		[XmlIgnore]
		public bool StatusSpecified
		{
			get
			{
				return this.statusFieldSpecified;
			}
			set
			{
				this.statusFieldSpecified = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001DB94 File Offset: 0x0001BD94
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		[XmlElement(Namespace = "HMSYNC:")]
		public Fault Fault
		{
			get
			{
				return this.faultField;
			}
			set
			{
				this.faultField = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0001DBA5 File Offset: 0x0001BDA5
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0001DBAD File Offset: 0x0001BDAD
		[XmlElement(Namespace = "HMSYNC:")]
		public AuthPolicy AuthPolicy
		{
			get
			{
				return this.authPolicyField;
			}
			set
			{
				this.authPolicyField = value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0001DBB6 File Offset: 0x0001BDB6
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0001DBBE File Offset: 0x0001BDBE
		[XmlArrayItem("Collection", IsNullable = false)]
		public StatelessCollection[] Collections
		{
			get
			{
				return this.collectionsField;
			}
			set
			{
				this.collectionsField = value;
			}
		}

		// Token: 0x04000648 RID: 1608
		private int statusField;

		// Token: 0x04000649 RID: 1609
		private bool statusFieldSpecified;

		// Token: 0x0400064A RID: 1610
		private Fault faultField;

		// Token: 0x0400064B RID: 1611
		private AuthPolicy authPolicyField;

		// Token: 0x0400064C RID: 1612
		private StatelessCollection[] collectionsField;
	}
}

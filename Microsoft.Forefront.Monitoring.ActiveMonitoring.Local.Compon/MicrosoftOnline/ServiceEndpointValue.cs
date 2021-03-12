using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000DC RID: 220
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class ServiceEndpointValue
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		[XmlAnyElement]
		public XmlElement[] Any
		{
			get
			{
				return this.anyField;
			}
			set
			{
				this.anyField = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001F6D5 File Offset: 0x0001D8D5
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001F6DD File Offset: 0x0001D8DD
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001F6E6 File Offset: 0x0001D8E6
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001F6EE File Offset: 0x0001D8EE
		[XmlAttribute]
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001F6F7 File Offset: 0x0001D8F7
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0001F6FF File Offset: 0x0001D8FF
		[XmlAttribute]
		public int[] PartitionSubset
		{
			get
			{
				return this.partitionSubsetField;
			}
			set
			{
				this.partitionSubsetField = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001F708 File Offset: 0x0001D908
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001F710 File Offset: 0x0001D910
		[XmlAttribute]
		public DateTime LastUpdatedTime
		{
			get
			{
				return this.lastUpdatedTimeField;
			}
			set
			{
				this.lastUpdatedTimeField = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001F719 File Offset: 0x0001D919
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0001F721 File Offset: 0x0001D921
		[XmlIgnore]
		public bool LastUpdatedTimeSpecified
		{
			get
			{
				return this.lastUpdatedTimeFieldSpecified;
			}
			set
			{
				this.lastUpdatedTimeFieldSpecified = value;
			}
		}

		// Token: 0x04000378 RID: 888
		private XmlElement[] anyField;

		// Token: 0x04000379 RID: 889
		private string nameField;

		// Token: 0x0400037A RID: 890
		private string addressField;

		// Token: 0x0400037B RID: 891
		private int[] partitionSubsetField;

		// Token: 0x0400037C RID: 892
		private DateTime lastUpdatedTimeField;

		// Token: 0x0400037D RID: 893
		private bool lastUpdatedTimeFieldSpecified;
	}
}

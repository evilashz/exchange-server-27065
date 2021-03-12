using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B7 RID: 2231
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CompanyTagLevelMapsValueCompanyTagLevelMap
	{
		// Token: 0x17002746 RID: 10054
		// (get) Token: 0x06006E66 RID: 28262 RVA: 0x0017632C File Offset: 0x0017452C
		// (set) Token: 0x06006E67 RID: 28263 RVA: 0x00176334 File Offset: 0x00174534
		[XmlElement(Order = 0)]
		public MapValue Map
		{
			get
			{
				return this.mapField;
			}
			set
			{
				this.mapField = value;
			}
		}

		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x06006E68 RID: 28264 RVA: 0x0017633D File Offset: 0x0017453D
		// (set) Token: 0x06006E69 RID: 28265 RVA: 0x00176345 File Offset: 0x00174545
		[XmlArray(Order = 1)]
		[XmlArrayItem("ServicePlanLevelMap", IsNullable = false)]
		public ServicePlanLevelMapsValueServicePlanLevelMap[] ServicePlanLevelMaps
		{
			get
			{
				return this.servicePlanLevelMapsField;
			}
			set
			{
				this.servicePlanLevelMapsField = value;
			}
		}

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x06006E6A RID: 28266 RVA: 0x0017634E File Offset: 0x0017454E
		// (set) Token: 0x06006E6B RID: 28267 RVA: 0x00176356 File Offset: 0x00174556
		[XmlAttribute]
		public string CompanyTag
		{
			get
			{
				return this.companyTagField;
			}
			set
			{
				this.companyTagField = value;
			}
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x06006E6C RID: 28268 RVA: 0x0017635F File Offset: 0x0017455F
		// (set) Token: 0x06006E6D RID: 28269 RVA: 0x00176367 File Offset: 0x00174567
		[XmlAttribute]
		public uint Priority
		{
			get
			{
				return this.priorityField;
			}
			set
			{
				this.priorityField = value;
			}
		}

		// Token: 0x040047CF RID: 18383
		private MapValue mapField;

		// Token: 0x040047D0 RID: 18384
		private ServicePlanLevelMapsValueServicePlanLevelMap[] servicePlanLevelMapsField;

		// Token: 0x040047D1 RID: 18385
		private string companyTagField;

		// Token: 0x040047D2 RID: 18386
		private uint priorityField;
	}
}

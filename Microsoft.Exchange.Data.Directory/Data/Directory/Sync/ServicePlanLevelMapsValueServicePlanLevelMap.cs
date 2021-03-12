using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008B6 RID: 2230
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServicePlanLevelMapsValueServicePlanLevelMap
	{
		// Token: 0x17002744 RID: 10052
		// (get) Token: 0x06006E61 RID: 28257 RVA: 0x00176302 File Offset: 0x00174502
		// (set) Token: 0x06006E62 RID: 28258 RVA: 0x0017630A File Offset: 0x0017450A
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

		// Token: 0x17002745 RID: 10053
		// (get) Token: 0x06006E63 RID: 28259 RVA: 0x00176313 File Offset: 0x00174513
		// (set) Token: 0x06006E64 RID: 28260 RVA: 0x0017631B File Offset: 0x0017451B
		[XmlAttribute]
		public string PlanId
		{
			get
			{
				return this.planIdField;
			}
			set
			{
				this.planIdField = value;
			}
		}

		// Token: 0x040047CD RID: 18381
		private MapValue mapField;

		// Token: 0x040047CE RID: 18382
		private string planIdField;
	}
}

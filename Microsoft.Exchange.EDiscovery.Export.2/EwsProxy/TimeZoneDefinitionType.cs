using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000A7 RID: 167
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class TimeZoneDefinitionType
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001FADE File Offset: 0x0001DCDE
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0001FAE6 File Offset: 0x0001DCE6
		[XmlArrayItem("Period", IsNullable = false)]
		public PeriodType[] Periods
		{
			get
			{
				return this.periodsField;
			}
			set
			{
				this.periodsField = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001FAEF File Offset: 0x0001DCEF
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001FAF7 File Offset: 0x0001DCF7
		[XmlArrayItem("TransitionsGroup", IsNullable = false)]
		public ArrayOfTransitionsType[] TransitionsGroups
		{
			get
			{
				return this.transitionsGroupsField;
			}
			set
			{
				this.transitionsGroupsField = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001FB00 File Offset: 0x0001DD00
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0001FB08 File Offset: 0x0001DD08
		public ArrayOfTransitionsType Transitions
		{
			get
			{
				return this.transitionsField;
			}
			set
			{
				this.transitionsField = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001FB11 File Offset: 0x0001DD11
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001FB19 File Offset: 0x0001DD19
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001FB22 File Offset: 0x0001DD22
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001FB2A File Offset: 0x0001DD2A
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

		// Token: 0x0400035E RID: 862
		private PeriodType[] periodsField;

		// Token: 0x0400035F RID: 863
		private ArrayOfTransitionsType[] transitionsGroupsField;

		// Token: 0x04000360 RID: 864
		private ArrayOfTransitionsType transitionsField;

		// Token: 0x04000361 RID: 865
		private string idField;

		// Token: 0x04000362 RID: 866
		private string nameField;
	}
}

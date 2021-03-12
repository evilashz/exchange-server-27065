using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B5 RID: 181
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class OccurrenceItemIdType : BaseItemIdType
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x0001FD78 File Offset: 0x0001DF78
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x0001FD80 File Offset: 0x0001DF80
		[XmlAttribute]
		public string RecurringMasterId
		{
			get
			{
				return this.recurringMasterIdField;
			}
			set
			{
				this.recurringMasterIdField = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x0001FD89 File Offset: 0x0001DF89
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0001FD91 File Offset: 0x0001DF91
		[XmlAttribute]
		public string ChangeKey
		{
			get
			{
				return this.changeKeyField;
			}
			set
			{
				this.changeKeyField = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0001FD9A File Offset: 0x0001DF9A
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0001FDA2 File Offset: 0x0001DFA2
		[XmlAttribute]
		public int InstanceIndex
		{
			get
			{
				return this.instanceIndexField;
			}
			set
			{
				this.instanceIndexField = value;
			}
		}

		// Token: 0x04000554 RID: 1364
		private string recurringMasterIdField;

		// Token: 0x04000555 RID: 1365
		private string changeKeyField;

		// Token: 0x04000556 RID: 1366
		private int instanceIndexField;
	}
}

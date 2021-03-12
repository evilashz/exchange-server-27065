using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000177 RID: 375
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EffectiveRightsType
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00023A06 File Offset: 0x00021C06
		// (set) Token: 0x06001062 RID: 4194 RVA: 0x00023A0E File Offset: 0x00021C0E
		public bool CreateAssociated
		{
			get
			{
				return this.createAssociatedField;
			}
			set
			{
				this.createAssociatedField = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00023A17 File Offset: 0x00021C17
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x00023A1F File Offset: 0x00021C1F
		public bool CreateContents
		{
			get
			{
				return this.createContentsField;
			}
			set
			{
				this.createContentsField = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00023A28 File Offset: 0x00021C28
		// (set) Token: 0x06001066 RID: 4198 RVA: 0x00023A30 File Offset: 0x00021C30
		public bool CreateHierarchy
		{
			get
			{
				return this.createHierarchyField;
			}
			set
			{
				this.createHierarchyField = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00023A39 File Offset: 0x00021C39
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00023A41 File Offset: 0x00021C41
		public bool Delete
		{
			get
			{
				return this.deleteField;
			}
			set
			{
				this.deleteField = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00023A4A File Offset: 0x00021C4A
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x00023A52 File Offset: 0x00021C52
		public bool Modify
		{
			get
			{
				return this.modifyField;
			}
			set
			{
				this.modifyField = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00023A5B File Offset: 0x00021C5B
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x00023A63 File Offset: 0x00021C63
		public bool Read
		{
			get
			{
				return this.readField;
			}
			set
			{
				this.readField = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00023A6C File Offset: 0x00021C6C
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x00023A74 File Offset: 0x00021C74
		public bool ViewPrivateItems
		{
			get
			{
				return this.viewPrivateItemsField;
			}
			set
			{
				this.viewPrivateItemsField = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x00023A7D File Offset: 0x00021C7D
		// (set) Token: 0x06001070 RID: 4208 RVA: 0x00023A85 File Offset: 0x00021C85
		[XmlIgnore]
		public bool ViewPrivateItemsSpecified
		{
			get
			{
				return this.viewPrivateItemsFieldSpecified;
			}
			set
			{
				this.viewPrivateItemsFieldSpecified = value;
			}
		}

		// Token: 0x04000B33 RID: 2867
		private bool createAssociatedField;

		// Token: 0x04000B34 RID: 2868
		private bool createContentsField;

		// Token: 0x04000B35 RID: 2869
		private bool createHierarchyField;

		// Token: 0x04000B36 RID: 2870
		private bool deleteField;

		// Token: 0x04000B37 RID: 2871
		private bool modifyField;

		// Token: 0x04000B38 RID: 2872
		private bool readField;

		// Token: 0x04000B39 RID: 2873
		private bool viewPrivateItemsField;

		// Token: 0x04000B3A RID: 2874
		private bool viewPrivateItemsFieldSpecified;
	}
}

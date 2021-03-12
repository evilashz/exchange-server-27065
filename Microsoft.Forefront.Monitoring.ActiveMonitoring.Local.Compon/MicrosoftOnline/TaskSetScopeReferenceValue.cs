using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C8 RID: 200
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class TaskSetScopeReferenceValue
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001F305 File Offset: 0x0001D505
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001F30D File Offset: 0x0001D50D
		[XmlArrayItem("ScopeReference", IsNullable = false)]
		public ScopeReferencesScopeReference[] ScopeReferences
		{
			get
			{
				return this.scopeReferencesField;
			}
			set
			{
				this.scopeReferencesField = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001F316 File Offset: 0x0001D516
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x0001F31E File Offset: 0x0001D51E
		[XmlAttribute]
		public string TaskSetId
		{
			get
			{
				return this.taskSetIdField;
			}
			set
			{
				this.taskSetIdField = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001F327 File Offset: 0x0001D527
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001F32F File Offset: 0x0001D52F
		[XmlAttribute]
		public bool BuiltIn
		{
			get
			{
				return this.builtInField;
			}
			set
			{
				this.builtInField = value;
			}
		}

		// Token: 0x04000349 RID: 841
		private ScopeReferencesScopeReference[] scopeReferencesField;

		// Token: 0x0400034A RID: 842
		private string taskSetIdField;

		// Token: 0x0400034B RID: 843
		private bool builtInField;
	}
}

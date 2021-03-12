using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000200 RID: 512
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class SyncFolderHierarchyResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00025C33 File Offset: 0x00023E33
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x00025C3B File Offset: 0x00023E3B
		public string SyncState
		{
			get
			{
				return this.syncStateField;
			}
			set
			{
				this.syncStateField = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00025C44 File Offset: 0x00023E44
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x00025C4C File Offset: 0x00023E4C
		public bool IncludesLastFolderInRange
		{
			get
			{
				return this.includesLastFolderInRangeField;
			}
			set
			{
				this.includesLastFolderInRangeField = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00025C55 File Offset: 0x00023E55
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x00025C5D File Offset: 0x00023E5D
		[XmlIgnore]
		public bool IncludesLastFolderInRangeSpecified
		{
			get
			{
				return this.includesLastFolderInRangeFieldSpecified;
			}
			set
			{
				this.includesLastFolderInRangeFieldSpecified = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00025C66 File Offset: 0x00023E66
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x00025C6E File Offset: 0x00023E6E
		public SyncFolderHierarchyChangesType Changes
		{
			get
			{
				return this.changesField;
			}
			set
			{
				this.changesField = value;
			}
		}

		// Token: 0x04000E16 RID: 3606
		private string syncStateField;

		// Token: 0x04000E17 RID: 3607
		private bool includesLastFolderInRangeField;

		// Token: 0x04000E18 RID: 3608
		private bool includesLastFolderInRangeFieldSpecified;

		// Token: 0x04000E19 RID: 3609
		private SyncFolderHierarchyChangesType changesField;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200083E RID: 2110
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class Account : DirectoryObject
	{
		// Token: 0x060068C2 RID: 26818 RVA: 0x00171374 File Offset: 0x0016F574
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncAccountSchema.DisplayName, ref this.displayNameField);
		}

		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x060068C3 RID: 26819 RVA: 0x00171387 File Offset: 0x0016F587
		// (set) Token: 0x060068C4 RID: 26820 RVA: 0x0017138F File Offset: 0x0016F58F
		[XmlElement(Order = 0)]
		public DirectoryPropertyGuidSingle AccountId
		{
			get
			{
				return this.accountIdField;
			}
			set
			{
				this.accountIdField = value;
			}
		}

		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x060068C5 RID: 26821 RVA: 0x00171398 File Offset: 0x0016F598
		// (set) Token: 0x060068C6 RID: 26822 RVA: 0x001713A0 File Offset: 0x0016F5A0
		[XmlElement(Order = 1)]
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x060068C7 RID: 26823 RVA: 0x001713A9 File Offset: 0x0016F5A9
		// (set) Token: 0x060068C8 RID: 26824 RVA: 0x001713B1 File Offset: 0x0016F5B1
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040044DC RID: 17628
		private DirectoryPropertyGuidSingle accountIdField;

		// Token: 0x040044DD RID: 17629
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040044DE RID: 17630
		private XmlAttribute[] anyAttrField;
	}
}

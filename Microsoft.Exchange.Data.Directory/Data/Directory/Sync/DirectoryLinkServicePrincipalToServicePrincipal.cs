using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200084A RID: 2122
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DelegationEntry))]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryLinkServicePrincipalToServicePrincipal : DirectoryLink
	{
		// Token: 0x06006A1A RID: 27162 RVA: 0x00172677 File Offset: 0x00170877
		public DirectoryLinkServicePrincipalToServicePrincipal()
		{
			this.sourceClassField = DirectoryObjectClass.ServicePrincipal;
			this.targetClassField = DirectoryObjectClass.ServicePrincipal;
		}

		// Token: 0x170025B1 RID: 9649
		// (get) Token: 0x06006A1B RID: 27163 RVA: 0x0017268D File Offset: 0x0017088D
		// (set) Token: 0x06006A1C RID: 27164 RVA: 0x00172695 File Offset: 0x00170895
		[XmlAttribute]
		public DirectoryObjectClass SourceClass
		{
			get
			{
				return this.sourceClassField;
			}
			set
			{
				this.sourceClassField = value;
			}
		}

		// Token: 0x170025B2 RID: 9650
		// (get) Token: 0x06006A1D RID: 27165 RVA: 0x0017269E File Offset: 0x0017089E
		// (set) Token: 0x06006A1E RID: 27166 RVA: 0x001726A6 File Offset: 0x001708A6
		[XmlIgnore]
		public bool SourceClassSpecified
		{
			get
			{
				return this.sourceClassFieldSpecified;
			}
			set
			{
				this.sourceClassFieldSpecified = value;
			}
		}

		// Token: 0x170025B3 RID: 9651
		// (get) Token: 0x06006A1F RID: 27167 RVA: 0x001726AF File Offset: 0x001708AF
		// (set) Token: 0x06006A20 RID: 27168 RVA: 0x001726B7 File Offset: 0x001708B7
		[XmlAttribute]
		public DirectoryObjectClass TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x170025B4 RID: 9652
		// (get) Token: 0x06006A21 RID: 27169 RVA: 0x001726C0 File Offset: 0x001708C0
		// (set) Token: 0x06006A22 RID: 27170 RVA: 0x001726C8 File Offset: 0x001708C8
		[XmlIgnore]
		public bool TargetClassSpecified
		{
			get
			{
				return this.targetClassFieldSpecified;
			}
			set
			{
				this.targetClassFieldSpecified = value;
			}
		}

		// Token: 0x04004576 RID: 17782
		private DirectoryObjectClass sourceClassField;

		// Token: 0x04004577 RID: 17783
		private bool sourceClassFieldSpecified;

		// Token: 0x04004578 RID: 17784
		private DirectoryObjectClass targetClassField;

		// Token: 0x04004579 RID: 17785
		private bool targetClassFieldSpecified;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000897 RID: 2199
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryReferenceUserAndServicePrincipal))]
	[XmlInclude(typeof(DirectoryReferenceAddressList))]
	[XmlInclude(typeof(DirectoryReferenceAny))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public abstract class DirectoryReference
	{
		// Token: 0x06006DB7 RID: 28087
		public abstract DirectoryObjectClass GetTargetClass();

		// Token: 0x06006DB8 RID: 28088 RVA: 0x00175C89 File Offset: 0x00173E89
		public DirectoryReference()
		{
			this.targetDeletedField = false;
		}

		// Token: 0x17002717 RID: 10007
		// (get) Token: 0x06006DB9 RID: 28089 RVA: 0x00175C98 File Offset: 0x00173E98
		// (set) Token: 0x06006DBA RID: 28090 RVA: 0x00175CA0 File Offset: 0x00173EA0
		[XmlAttribute]
		[DefaultValue(false)]
		public bool TargetDeleted
		{
			get
			{
				return this.targetDeletedField;
			}
			set
			{
				this.targetDeletedField = value;
			}
		}

		// Token: 0x17002718 RID: 10008
		// (get) Token: 0x06006DBB RID: 28091 RVA: 0x00175CA9 File Offset: 0x00173EA9
		// (set) Token: 0x06006DBC RID: 28092 RVA: 0x00175CB1 File Offset: 0x00173EB1
		[XmlText]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04004789 RID: 18313
		private bool targetDeletedField;

		// Token: 0x0400478A RID: 18314
		private string valueField;
	}
}

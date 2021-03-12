using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000898 RID: 2200
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryReferenceAddressList : DirectoryReference
	{
		// Token: 0x06006DBD RID: 28093 RVA: 0x00175CBA File Offset: 0x00173EBA
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(this.TargetClass);
		}

		// Token: 0x17002719 RID: 10009
		// (get) Token: 0x06006DBE RID: 28094 RVA: 0x00175CC7 File Offset: 0x00173EC7
		// (set) Token: 0x06006DBF RID: 28095 RVA: 0x00175CCF File Offset: 0x00173ECF
		[XmlAttribute]
		public DirectoryObjectClassAddressList TargetClass
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

		// Token: 0x0400478B RID: 18315
		private DirectoryObjectClassAddressList targetClassField;
	}
}

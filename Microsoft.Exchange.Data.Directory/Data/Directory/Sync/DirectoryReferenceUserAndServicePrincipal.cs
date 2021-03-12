using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200089A RID: 2202
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryReferenceUserAndServicePrincipal : DirectoryReference
	{
		// Token: 0x06006DC5 RID: 28101 RVA: 0x00175D01 File Offset: 0x00173F01
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), this.TargetClass.ToString());
		}

		// Token: 0x1700271B RID: 10011
		// (get) Token: 0x06006DC6 RID: 28102 RVA: 0x00175D27 File Offset: 0x00173F27
		// (set) Token: 0x06006DC7 RID: 28103 RVA: 0x00175D2F File Offset: 0x00173F2F
		[XmlAttribute]
		public DirectoryObjectClassUserAndServicePrincipal TargetClass
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

		// Token: 0x0400478D RID: 18317
		private DirectoryObjectClassUserAndServicePrincipal targetClassField;
	}
}

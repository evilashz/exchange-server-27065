using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000899 RID: 2201
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryReferenceAny : DirectoryReference
	{
		// Token: 0x06006DC1 RID: 28097 RVA: 0x00175CE0 File Offset: 0x00173EE0
		public override DirectoryObjectClass GetTargetClass()
		{
			return this.TargetClass;
		}

		// Token: 0x1700271A RID: 10010
		// (get) Token: 0x06006DC2 RID: 28098 RVA: 0x00175CE8 File Offset: 0x00173EE8
		// (set) Token: 0x06006DC3 RID: 28099 RVA: 0x00175CF0 File Offset: 0x00173EF0
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

		// Token: 0x0400478C RID: 18316
		private DirectoryObjectClass targetClassField;
	}
}

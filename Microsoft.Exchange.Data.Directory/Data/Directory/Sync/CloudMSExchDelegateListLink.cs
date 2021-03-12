using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000845 RID: 2117
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class CloudMSExchDelegateListLink : DirectoryLinkUserToUser
	{
		// Token: 0x060068FB RID: 26875 RVA: 0x0017163B File Offset: 0x0016F83B
		public override DirectoryObjectClass GetSourceClass()
		{
			return base.SourceClass;
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x00171643 File Offset: 0x0016F843
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = objectClass;
			base.SourceClassSpecified = true;
		}

		// Token: 0x060068FD RID: 26877 RVA: 0x00171653 File Offset: 0x0016F853
		public override DirectoryObjectClass GetTargetClass()
		{
			return base.TargetClass;
		}

		// Token: 0x060068FE RID: 26878 RVA: 0x0017165B File Offset: 0x0016F85B
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = objectClass;
			base.TargetClassSpecified = true;
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200089E RID: 2206
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MSExchDelegateListLink : DirectoryLinkUserToUser
	{
		// Token: 0x06006DD8 RID: 28120 RVA: 0x00175E80 File Offset: 0x00174080
		public override DirectoryObjectClass GetSourceClass()
		{
			return base.SourceClass;
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x00175E88 File Offset: 0x00174088
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = objectClass;
			base.SourceClassSpecified = true;
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x00175E98 File Offset: 0x00174098
		public override DirectoryObjectClass GetTargetClass()
		{
			return base.TargetClass;
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x00175EA0 File Offset: 0x001740A0
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = objectClass;
			base.TargetClassSpecified = true;
		}
	}
}

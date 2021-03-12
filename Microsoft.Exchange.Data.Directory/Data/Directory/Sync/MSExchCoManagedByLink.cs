using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200089D RID: 2205
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class MSExchCoManagedByLink : DirectoryLinkGroupToPerson
	{
		// Token: 0x06006DD3 RID: 28115 RVA: 0x00175E22 File Offset: 0x00174022
		public override DirectoryObjectClass GetSourceClass()
		{
			return base.SourceClass;
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x00175E2A File Offset: 0x0017402A
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = objectClass;
			base.SourceClassSpecified = true;
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x00175E3A File Offset: 0x0017403A
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x00175E47 File Offset: 0x00174047
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

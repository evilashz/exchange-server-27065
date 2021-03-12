using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000856 RID: 2134
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class ManagedBy : DirectoryLinkGroupToPerson
	{
		// Token: 0x06006B18 RID: 27416 RVA: 0x0017337B File Offset: 0x0017157B
		public override DirectoryObjectClass GetSourceClass()
		{
			return base.SourceClass;
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x00173383 File Offset: 0x00171583
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = objectClass;
			base.SourceClassSpecified = true;
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x00173393 File Offset: 0x00171593
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x001733A0 File Offset: 0x001715A0
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

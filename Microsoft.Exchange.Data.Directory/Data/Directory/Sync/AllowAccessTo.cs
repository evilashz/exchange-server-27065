using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000841 RID: 2113
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AllowAccessTo : DirectoryLinkAllowAccessTo
	{
		// Token: 0x060068E3 RID: 26851 RVA: 0x0017147B File Offset: 0x0016F67B
		public override DirectoryObjectClass GetSourceClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.SourceClass.ToString());
		}

		// Token: 0x060068E4 RID: 26852 RVA: 0x001714A1 File Offset: 0x0016F6A1
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassContainsAllowAccessTo)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassContainsAllowAccessTo), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x001714D2 File Offset: 0x0016F6D2
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x001714F8 File Offset: 0x0016F6F8
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassCanHaveAllowAccessTo)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassCanHaveAllowAccessTo), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

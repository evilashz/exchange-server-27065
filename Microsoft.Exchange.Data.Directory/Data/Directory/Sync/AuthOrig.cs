using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000843 RID: 2115
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AuthOrig : DirectoryLinkAddressListObjectToPerson
	{
		// Token: 0x060068ED RID: 26861 RVA: 0x0017155B File Offset: 0x0016F75B
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x00171568 File Offset: 0x0016F768
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x00171599 File Offset: 0x0016F799
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x001715A6 File Offset: 0x0016F7A6
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

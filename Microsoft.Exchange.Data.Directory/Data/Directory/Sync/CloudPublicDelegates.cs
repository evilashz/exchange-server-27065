using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000847 RID: 2119
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class CloudPublicDelegates : DirectoryLinkAddressListObjectToAddressListObject
	{
		// Token: 0x06006905 RID: 26885 RVA: 0x0017169D File Offset: 0x0016F89D
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006906 RID: 26886 RVA: 0x001716AA File Offset: 0x0016F8AA
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006907 RID: 26887 RVA: 0x001716DB File Offset: 0x0016F8DB
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x06006908 RID: 26888 RVA: 0x00171701 File Offset: 0x0016F901
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

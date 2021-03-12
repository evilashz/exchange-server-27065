using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000861 RID: 2145
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class UnauthOrig : DirectoryLinkAddressListObjectToPerson
	{
		// Token: 0x06006B7C RID: 27516 RVA: 0x001739AC File Offset: 0x00171BAC
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006B7D RID: 27517 RVA: 0x001739B9 File Offset: 0x00171BB9
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006B7E RID: 27518 RVA: 0x001739EA File Offset: 0x00171BEA
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x001739F7 File Offset: 0x00171BF7
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

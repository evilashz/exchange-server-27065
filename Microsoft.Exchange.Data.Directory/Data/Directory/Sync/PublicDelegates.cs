using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000959 RID: 2393
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class PublicDelegates : DirectoryLinkAddressListObjectToGroupAndUser
	{
		// Token: 0x060070AB RID: 28843 RVA: 0x00177661 File Offset: 0x00175861
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x0017766E File Offset: 0x0017586E
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x0017769F File Offset: 0x0017589F
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x001776C5 File Offset: 0x001758C5
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassGroupAndUser)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassGroupAndUser), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

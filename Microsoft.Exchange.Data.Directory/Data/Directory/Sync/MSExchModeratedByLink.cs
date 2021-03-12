using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200089F RID: 2207
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class MSExchModeratedByLink : DirectoryLinkAddressListObjectToPerson
	{
		// Token: 0x06006DDD RID: 28125 RVA: 0x00175EB8 File Offset: 0x001740B8
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x00175EC5 File Offset: 0x001740C5
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x00175EF6 File Offset: 0x001740F6
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x00175F03 File Offset: 0x00174103
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

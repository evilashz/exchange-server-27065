using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200089C RID: 2204
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MSExchBypassModerationLink : DirectoryLinkAddressListObjectToPerson
	{
		// Token: 0x06006DCE RID: 28110 RVA: 0x00175D9E File Offset: 0x00173F9E
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x00175DAB File Offset: 0x00173FAB
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x00175DDC File Offset: 0x00173FDC
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x00175DE9 File Offset: 0x00173FE9
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200084F RID: 2127
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DLMemSubmitPerms : DirectoryLinkAddressListObjectToGroup
	{
		// Token: 0x06006A60 RID: 27232 RVA: 0x00172942 File Offset: 0x00170B42
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006A61 RID: 27233 RVA: 0x0017294F File Offset: 0x00170B4F
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x00172980 File Offset: 0x00170B80
		public override DirectoryObjectClass GetTargetClass()
		{
			return base.TargetClass;
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x00172988 File Offset: 0x00170B88
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = objectClass;
			base.TargetClassSpecified = true;
		}
	}
}

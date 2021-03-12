using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200084E RID: 2126
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DLMemRejectPerms : DirectoryLinkAddressListObjectToGroup
	{
		// Token: 0x06006A5B RID: 27227 RVA: 0x001728E4 File Offset: 0x00170AE4
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x001728F1 File Offset: 0x00170AF1
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x00172922 File Offset: 0x00170B22
		public override DirectoryObjectClass GetTargetClass()
		{
			return base.TargetClass;
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x0017292A File Offset: 0x00170B2A
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = objectClass;
			base.TargetClassSpecified = true;
		}
	}
}

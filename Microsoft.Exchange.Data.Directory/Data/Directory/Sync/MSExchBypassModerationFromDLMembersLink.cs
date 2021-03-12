using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200089B RID: 2203
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class MSExchBypassModerationFromDLMembersLink : DirectoryLinkAddressListObjectToGroup
	{
		// Token: 0x06006DC9 RID: 28105 RVA: 0x00175D40 File Offset: 0x00173F40
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006DCA RID: 28106 RVA: 0x00175D4D File Offset: 0x00173F4D
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassAddressList)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassAddressList), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006DCB RID: 28107 RVA: 0x00175D7E File Offset: 0x00173F7E
		public override DirectoryObjectClass GetTargetClass()
		{
			return base.TargetClass;
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x00175D86 File Offset: 0x00173F86
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = objectClass;
			base.TargetClassSpecified = true;
		}
	}
}

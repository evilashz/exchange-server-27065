using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000964 RID: 2404
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class RegisteredOwner : DirectoryLinkDeviceToUser
	{
		// Token: 0x060070B9 RID: 28857 RVA: 0x00177759 File Offset: 0x00175959
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObjectClass.Account;
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x0017775C File Offset: 0x0017595C
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x0017775E File Offset: 0x0017595E
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObjectClass.Account;
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x00177761 File Offset: 0x00175961
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
		}
	}
}

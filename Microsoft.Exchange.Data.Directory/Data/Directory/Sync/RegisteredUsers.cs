using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000965 RID: 2405
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class RegisteredUsers : DirectoryLinkDeviceToUser
	{
		// Token: 0x060070BE RID: 28862 RVA: 0x0017776B File Offset: 0x0017596B
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObjectClass.Account;
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x0017776E File Offset: 0x0017596E
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x00177770 File Offset: 0x00175970
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObjectClass.Company;
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x00177773 File Offset: 0x00175973
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
		}
	}
}

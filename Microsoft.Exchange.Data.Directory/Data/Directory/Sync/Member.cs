using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200085A RID: 2138
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Member : DirectoryLinkMemberObjectToAddressListObject
	{
		// Token: 0x06006B30 RID: 27440 RVA: 0x001734D3 File Offset: 0x001716D3
		public override DirectoryObjectClass GetSourceClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.SourceClass.ToString());
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x001734F9 File Offset: 0x001716F9
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassContainsMember)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassContainsMember), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x0017352A File Offset: 0x0017172A
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x00173550 File Offset: 0x00171750
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassCanBeMember)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassCanBeMember), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200085C RID: 2140
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class Owner : DirectoryLinkOwnerObjectToUserAndServicePrincipal
	{
		// Token: 0x06006B3A RID: 27450 RVA: 0x001735B3 File Offset: 0x001717B3
		public override DirectoryObjectClass GetSourceClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.SourceClass.ToString());
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x001735D9 File Offset: 0x001717D9
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassContainsOwner)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassContainsOwner), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x0017360A File Offset: 0x0017180A
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x00173630 File Offset: 0x00171830
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassUserAndServicePrincipal)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassUserAndServicePrincipal), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

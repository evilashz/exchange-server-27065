using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200085E RID: 2142
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class PendingMember : DirectoryLinkPendingMember
	{
		// Token: 0x06006B48 RID: 27464 RVA: 0x001736B5 File Offset: 0x001718B5
		public override DirectoryObjectClass GetSourceClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.SourceClass.ToString());
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x001736DB File Offset: 0x001718DB
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassContainsPendingMember)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassContainsPendingMember), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006B4A RID: 27466 RVA: 0x0017370C File Offset: 0x0017190C
		public override DirectoryObjectClass GetTargetClass()
		{
			return (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), base.TargetClass.ToString());
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x00173732 File Offset: 0x00171932
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassCanBePendingMember)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassCanBePendingMember), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

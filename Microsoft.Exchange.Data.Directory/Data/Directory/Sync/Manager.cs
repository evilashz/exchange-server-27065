using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000858 RID: 2136
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class Manager : DirectoryLinkPersonToPerson
	{
		// Token: 0x17002622 RID: 9762
		// (get) Token: 0x06006B22 RID: 27426 RVA: 0x00173403 File Offset: 0x00171603
		// (set) Token: 0x06006B23 RID: 27427 RVA: 0x0017340B File Offset: 0x0017160B
		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01")]
		public DateTime Timestamp { get; set; }

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x06006B24 RID: 27428 RVA: 0x00173414 File Offset: 0x00171614
		// (set) Token: 0x06006B25 RID: 27429 RVA: 0x0017341C File Offset: 0x0017161C
		[XmlIgnore]
		public bool TimestampSpecified { get; set; }

		// Token: 0x06006B26 RID: 27430 RVA: 0x00173425 File Offset: 0x00171625
		public override DirectoryObjectClass GetSourceClass()
		{
			return DirectoryObject.GetObjectClass(base.SourceClass);
		}

		// Token: 0x06006B27 RID: 27431 RVA: 0x00173432 File Offset: 0x00171632
		public override void SetSourceClass(DirectoryObjectClass objectClass)
		{
			base.SourceClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}

		// Token: 0x06006B28 RID: 27432 RVA: 0x00173463 File Offset: 0x00171663
		public override DirectoryObjectClass GetTargetClass()
		{
			return DirectoryObject.GetObjectClass(base.TargetClass);
		}

		// Token: 0x06006B29 RID: 27433 RVA: 0x00173470 File Offset: 0x00171670
		public override void SetTargetClass(DirectoryObjectClass objectClass)
		{
			base.TargetClass = (DirectoryObjectClassPerson)DirectoryLink.ConvertEnums(typeof(DirectoryObjectClassPerson), Enum.GetName(typeof(DirectoryObjectClass), objectClass));
		}
	}
}

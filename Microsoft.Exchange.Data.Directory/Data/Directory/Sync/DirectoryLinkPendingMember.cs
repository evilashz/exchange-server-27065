using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200085D RID: 2141
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(PendingMember))]
	[Serializable]
	public abstract class DirectoryLinkPendingMember : DirectoryLink
	{
		// Token: 0x17002628 RID: 9768
		// (get) Token: 0x06006B3F RID: 27455 RVA: 0x00173669 File Offset: 0x00171869
		// (set) Token: 0x06006B40 RID: 27456 RVA: 0x00173671 File Offset: 0x00171871
		[XmlAttribute]
		public DirectoryObjectClassContainsPendingMember SourceClass
		{
			get
			{
				return this.sourceClassField;
			}
			set
			{
				this.sourceClassField = value;
			}
		}

		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x06006B41 RID: 27457 RVA: 0x0017367A File Offset: 0x0017187A
		// (set) Token: 0x06006B42 RID: 27458 RVA: 0x00173682 File Offset: 0x00171882
		[XmlIgnore]
		public bool SourceClassSpecified
		{
			get
			{
				return this.sourceClassFieldSpecified;
			}
			set
			{
				this.sourceClassFieldSpecified = value;
			}
		}

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x06006B43 RID: 27459 RVA: 0x0017368B File Offset: 0x0017188B
		// (set) Token: 0x06006B44 RID: 27460 RVA: 0x00173693 File Offset: 0x00171893
		[XmlAttribute]
		public DirectoryObjectClassCanBePendingMember TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x1700262B RID: 9771
		// (get) Token: 0x06006B45 RID: 27461 RVA: 0x0017369C File Offset: 0x0017189C
		// (set) Token: 0x06006B46 RID: 27462 RVA: 0x001736A4 File Offset: 0x001718A4
		[XmlIgnore]
		public bool TargetClassSpecified
		{
			get
			{
				return this.targetClassFieldSpecified;
			}
			set
			{
				this.targetClassFieldSpecified = value;
			}
		}

		// Token: 0x040045ED RID: 17901
		private DirectoryObjectClassContainsPendingMember sourceClassField;

		// Token: 0x040045EE RID: 17902
		private bool sourceClassFieldSpecified;

		// Token: 0x040045EF RID: 17903
		private DirectoryObjectClassCanBePendingMember targetClassField;

		// Token: 0x040045F0 RID: 17904
		private bool targetClassFieldSpecified;
	}
}

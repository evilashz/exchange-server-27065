using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000852 RID: 2130
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(InvitedBy))]
	[Serializable]
	public abstract class DirectoryLinkInvitedBy : DirectoryLink
	{
		// Token: 0x17002613 RID: 9747
		// (get) Token: 0x06006AF5 RID: 27381 RVA: 0x00173259 File Offset: 0x00171459
		// (set) Token: 0x06006AF6 RID: 27382 RVA: 0x00173261 File Offset: 0x00171461
		[XmlAttribute]
		public DirectoryObjectClassContainsInvitedBy SourceClass
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

		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x06006AF7 RID: 27383 RVA: 0x0017326A File Offset: 0x0017146A
		// (set) Token: 0x06006AF8 RID: 27384 RVA: 0x00173272 File Offset: 0x00171472
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

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x06006AF9 RID: 27385 RVA: 0x0017327B File Offset: 0x0017147B
		// (set) Token: 0x06006AFA RID: 27386 RVA: 0x00173283 File Offset: 0x00171483
		[XmlAttribute]
		public DirectoryObjectClassCanHaveInvitedBy TargetClass
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

		// Token: 0x040045D8 RID: 17880
		private DirectoryObjectClassContainsInvitedBy sourceClassField;

		// Token: 0x040045D9 RID: 17881
		private bool sourceClassFieldSpecified;

		// Token: 0x040045DA RID: 17882
		private DirectoryObjectClassCanHaveInvitedBy targetClassField;
	}
}

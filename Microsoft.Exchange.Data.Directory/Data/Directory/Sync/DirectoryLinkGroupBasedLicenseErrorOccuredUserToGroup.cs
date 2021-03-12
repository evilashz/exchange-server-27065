using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000956 RID: 2390
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public abstract class DirectoryLinkGroupBasedLicenseErrorOccuredUserToGroup : DirectoryLink
	{
		// Token: 0x0600709D RID: 28829 RVA: 0x001775DC File Offset: 0x001757DC
		public DirectoryLinkGroupBasedLicenseErrorOccuredUserToGroup()
		{
			this.sourceClassField = DirectoryObjectClass.User;
			this.targetClassField = DirectoryObjectClass.Group;
		}

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x0600709E RID: 28830 RVA: 0x001775F3 File Offset: 0x001757F3
		// (set) Token: 0x0600709F RID: 28831 RVA: 0x001775FB File Offset: 0x001757FB
		[XmlAttribute]
		public DirectoryObjectClass SourceClass
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

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x060070A0 RID: 28832 RVA: 0x00177604 File Offset: 0x00175804
		// (set) Token: 0x060070A1 RID: 28833 RVA: 0x0017760C File Offset: 0x0017580C
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

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x060070A2 RID: 28834 RVA: 0x00177615 File Offset: 0x00175815
		// (set) Token: 0x060070A3 RID: 28835 RVA: 0x0017761D File Offset: 0x0017581D
		[XmlAttribute]
		public DirectoryObjectClass TargetClass
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

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x060070A4 RID: 28836 RVA: 0x00177626 File Offset: 0x00175826
		// (set) Token: 0x060070A5 RID: 28837 RVA: 0x0017762E File Offset: 0x0017582E
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

		// Token: 0x04004908 RID: 18696
		private DirectoryObjectClass sourceClassField;

		// Token: 0x04004909 RID: 18697
		private bool sourceClassFieldSpecified;

		// Token: 0x0400490A RID: 18698
		private DirectoryObjectClass targetClassField;

		// Token: 0x0400490B RID: 18699
		private bool targetClassFieldSpecified;
	}
}

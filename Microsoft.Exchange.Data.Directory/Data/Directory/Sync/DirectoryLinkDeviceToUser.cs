using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000963 RID: 2403
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(RegisteredUsers))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(RegisteredOwner))]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class DirectoryLinkDeviceToUser : DirectoryLink
	{
		// Token: 0x060070B0 RID: 28848 RVA: 0x001776FE File Offset: 0x001758FE
		public DirectoryLinkDeviceToUser()
		{
			this.sourceClassField = DirectoryObjectClass.Device;
			this.targetClassField = DirectoryObjectClass.User;
		}

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x060070B1 RID: 28849 RVA: 0x00177715 File Offset: 0x00175915
		// (set) Token: 0x060070B2 RID: 28850 RVA: 0x0017771D File Offset: 0x0017591D
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

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x060070B3 RID: 28851 RVA: 0x00177726 File Offset: 0x00175926
		// (set) Token: 0x060070B4 RID: 28852 RVA: 0x0017772E File Offset: 0x0017592E
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

		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x060070B5 RID: 28853 RVA: 0x00177737 File Offset: 0x00175937
		// (set) Token: 0x060070B6 RID: 28854 RVA: 0x0017773F File Offset: 0x0017593F
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

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x060070B7 RID: 28855 RVA: 0x00177748 File Offset: 0x00175948
		// (set) Token: 0x060070B8 RID: 28856 RVA: 0x00177750 File Offset: 0x00175950
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

		// Token: 0x04004929 RID: 18729
		private DirectoryObjectClass sourceClassField;

		// Token: 0x0400492A RID: 18730
		private bool sourceClassFieldSpecified;

		// Token: 0x0400492B RID: 18731
		private DirectoryObjectClass targetClassField;

		// Token: 0x0400492C RID: 18732
		private bool targetClassFieldSpecified;
	}
}

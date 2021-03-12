using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000842 RID: 2114
	[XmlInclude(typeof(AuthOrig))]
	[XmlInclude(typeof(UnauthOrig))]
	[XmlInclude(typeof(MSExchModeratedByLink))]
	[XmlInclude(typeof(MSExchBypassModerationLink))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public abstract class DirectoryLinkAddressListObjectToPerson : DirectoryLink
	{
		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x060068E8 RID: 26856 RVA: 0x00171531 File Offset: 0x0016F731
		// (set) Token: 0x060068E9 RID: 26857 RVA: 0x00171539 File Offset: 0x0016F739
		[XmlAttribute]
		public DirectoryObjectClassAddressList SourceClass
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

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x060068EA RID: 26858 RVA: 0x00171542 File Offset: 0x0016F742
		// (set) Token: 0x060068EB RID: 26859 RVA: 0x0017154A File Offset: 0x0016F74A
		[XmlAttribute]
		public DirectoryObjectClassPerson TargetClass
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

		// Token: 0x040044E8 RID: 17640
		private DirectoryObjectClassAddressList sourceClassField;

		// Token: 0x040044E9 RID: 17641
		private DirectoryObjectClassPerson targetClassField;
	}
}

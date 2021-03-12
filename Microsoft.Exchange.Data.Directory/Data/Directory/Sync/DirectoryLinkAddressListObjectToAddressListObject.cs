using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000846 RID: 2118
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlInclude(typeof(CloudPublicDelegates))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public abstract class DirectoryLinkAddressListObjectToAddressListObject : DirectoryLink
	{
		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x06006900 RID: 26880 RVA: 0x00171673 File Offset: 0x0016F873
		// (set) Token: 0x06006901 RID: 26881 RVA: 0x0017167B File Offset: 0x0016F87B
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

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x06006902 RID: 26882 RVA: 0x00171684 File Offset: 0x0016F884
		// (set) Token: 0x06006903 RID: 26883 RVA: 0x0017168C File Offset: 0x0016F88C
		[XmlAttribute]
		public DirectoryObjectClassAddressList TargetClass
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

		// Token: 0x040044EE RID: 17646
		private DirectoryObjectClassAddressList sourceClassField;

		// Token: 0x040044EF RID: 17647
		private DirectoryObjectClassAddressList targetClassField;
	}
}

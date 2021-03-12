using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000943 RID: 2371
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class LicenseUnitsDetailValue
	{
		// Token: 0x06007002 RID: 28674 RVA: 0x001770B0 File Offset: 0x001752B0
		public LicenseUnitsDetailValue()
		{
			this.lockedOutField = 0;
		}

		// Token: 0x170027D5 RID: 10197
		// (get) Token: 0x06007003 RID: 28675 RVA: 0x001770BF File Offset: 0x001752BF
		// (set) Token: 0x06007004 RID: 28676 RVA: 0x001770C7 File Offset: 0x001752C7
		[XmlAttribute]
		public int Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x170027D6 RID: 10198
		// (get) Token: 0x06007005 RID: 28677 RVA: 0x001770D0 File Offset: 0x001752D0
		// (set) Token: 0x06007006 RID: 28678 RVA: 0x001770D8 File Offset: 0x001752D8
		[XmlAttribute]
		public int Warning
		{
			get
			{
				return this.warningField;
			}
			set
			{
				this.warningField = value;
			}
		}

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06007007 RID: 28679 RVA: 0x001770E1 File Offset: 0x001752E1
		// (set) Token: 0x06007008 RID: 28680 RVA: 0x001770E9 File Offset: 0x001752E9
		[XmlAttribute]
		public int Suspended
		{
			get
			{
				return this.suspendedField;
			}
			set
			{
				this.suspendedField = value;
			}
		}

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x001770F2 File Offset: 0x001752F2
		// (set) Token: 0x0600700A RID: 28682 RVA: 0x001770FA File Offset: 0x001752FA
		[XmlAttribute]
		[DefaultValue(0)]
		public int LockedOut
		{
			get
			{
				return this.lockedOutField;
			}
			set
			{
				this.lockedOutField = value;
			}
		}

		// Token: 0x040048B1 RID: 18609
		private int enabledField;

		// Token: 0x040048B2 RID: 18610
		private int warningField;

		// Token: 0x040048B3 RID: 18611
		private int suspendedField;

		// Token: 0x040048B4 RID: 18612
		private int lockedOutField;
	}
}

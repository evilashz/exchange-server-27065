using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B4 RID: 2228
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AccountSettingsTypeSet
	{
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x0006C8B1 File Offset: 0x0006AAB1
		// (set) Token: 0x06002FCF RID: 12239 RVA: 0x0006C8B9 File Offset: 0x0006AAB9
		[XmlArrayItem("Filter", IsNullable = false)]
		public FiltersRequestTypeFilter[] Filters
		{
			get
			{
				return this.filtersField;
			}
			set
			{
				this.filtersField = value;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x0006C8C2 File Offset: 0x0006AAC2
		// (set) Token: 0x06002FD1 RID: 12241 RVA: 0x0006C8CA File Offset: 0x0006AACA
		[XmlArrayItem("List", IsNullable = false)]
		public ListsRequestTypeList[] Lists
		{
			get
			{
				return this.listsField;
			}
			set
			{
				this.listsField = value;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x0006C8D3 File Offset: 0x0006AAD3
		// (set) Token: 0x06002FD3 RID: 12243 RVA: 0x0006C8DB File Offset: 0x0006AADB
		public OptionsType Options
		{
			get
			{
				return this.optionsField;
			}
			set
			{
				this.optionsField = value;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x0006C8E4 File Offset: 0x0006AAE4
		// (set) Token: 0x06002FD5 RID: 12245 RVA: 0x0006C8EC File Offset: 0x0006AAEC
		public StringWithCharSetType UserSignature
		{
			get
			{
				return this.userSignatureField;
			}
			set
			{
				this.userSignatureField = value;
			}
		}

		// Token: 0x04002950 RID: 10576
		private FiltersRequestTypeFilter[] filtersField;

		// Token: 0x04002951 RID: 10577
		private ListsRequestTypeList[] listsField;

		// Token: 0x04002952 RID: 10578
		private OptionsType optionsField;

		// Token: 0x04002953 RID: 10579
		private StringWithCharSetType userSignatureField;
	}
}

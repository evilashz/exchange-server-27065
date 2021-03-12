using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F2 RID: 2290
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class SettingsAccountSettingsGet
	{
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x00073554 File Offset: 0x00071754
		// (set) Token: 0x0600315B RID: 12635 RVA: 0x0007355C File Offset: 0x0007175C
		[XmlArrayItem("Filter", IsNullable = false)]
		public FiltersResponseTypeFilter[] Filters
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

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x00073565 File Offset: 0x00071765
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x0007356D File Offset: 0x0007176D
		[XmlArrayItem("List", IsNullable = false)]
		public ListsGetResponseTypeList[] Lists
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

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x00073576 File Offset: 0x00071776
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x0007357E File Offset: 0x0007177E
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

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x00073587 File Offset: 0x00071787
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x0007358F File Offset: 0x0007178F
		public PropertiesType Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x00073598 File Offset: 0x00071798
		// (set) Token: 0x06003163 RID: 12643 RVA: 0x000735A0 File Offset: 0x000717A0
		public StringWithVersionType UserSignature
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

		// Token: 0x04002A7C RID: 10876
		private FiltersResponseTypeFilter[] filtersField;

		// Token: 0x04002A7D RID: 10877
		private ListsGetResponseTypeList[] listsField;

		// Token: 0x04002A7E RID: 10878
		private OptionsType optionsField;

		// Token: 0x04002A7F RID: 10879
		private PropertiesType propertiesField;

		// Token: 0x04002A80 RID: 10880
		private StringWithVersionType userSignatureField;
	}
}

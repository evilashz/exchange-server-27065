using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000340 RID: 832
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetDiscoverySearchConfigurationType : BaseRequestType
	{
		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000291D9 File Offset: 0x000273D9
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000291E1 File Offset: 0x000273E1
		public string SearchId
		{
			get
			{
				return this.searchIdField;
			}
			set
			{
				this.searchIdField = value;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x000291EA File Offset: 0x000273EA
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x000291F2 File Offset: 0x000273F2
		public bool ExpandGroupMembership
		{
			get
			{
				return this.expandGroupMembershipField;
			}
			set
			{
				this.expandGroupMembershipField = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x000291FB File Offset: 0x000273FB
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00029203 File Offset: 0x00027403
		[XmlIgnore]
		public bool ExpandGroupMembershipSpecified
		{
			get
			{
				return this.expandGroupMembershipFieldSpecified;
			}
			set
			{
				this.expandGroupMembershipFieldSpecified = value;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001AD3 RID: 6867 RVA: 0x0002920C File Offset: 0x0002740C
		// (set) Token: 0x06001AD4 RID: 6868 RVA: 0x00029214 File Offset: 0x00027414
		public bool InPlaceHoldConfigurationOnly
		{
			get
			{
				return this.inPlaceHoldConfigurationOnlyField;
			}
			set
			{
				this.inPlaceHoldConfigurationOnlyField = value;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x0002921D File Offset: 0x0002741D
		// (set) Token: 0x06001AD6 RID: 6870 RVA: 0x00029225 File Offset: 0x00027425
		[XmlIgnore]
		public bool InPlaceHoldConfigurationOnlySpecified
		{
			get
			{
				return this.inPlaceHoldConfigurationOnlyFieldSpecified;
			}
			set
			{
				this.inPlaceHoldConfigurationOnlyFieldSpecified = value;
			}
		}

		// Token: 0x040011E9 RID: 4585
		private string searchIdField;

		// Token: 0x040011EA RID: 4586
		private bool expandGroupMembershipField;

		// Token: 0x040011EB RID: 4587
		private bool expandGroupMembershipFieldSpecified;

		// Token: 0x040011EC RID: 4588
		private bool inPlaceHoldConfigurationOnlyField;

		// Token: 0x040011ED RID: 4589
		private bool inPlaceHoldConfigurationOnlyFieldSpecified;
	}
}

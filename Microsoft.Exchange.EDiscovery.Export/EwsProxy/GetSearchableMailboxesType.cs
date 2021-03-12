using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000342 RID: 834
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetSearchableMailboxesType : BaseRequestType
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0002930A File Offset: 0x0002750A
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00029312 File Offset: 0x00027512
		public string SearchFilter
		{
			get
			{
				return this.searchFilterField;
			}
			set
			{
				this.searchFilterField = value;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0002931B File Offset: 0x0002751B
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00029323 File Offset: 0x00027523
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

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0002932C File Offset: 0x0002752C
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00029334 File Offset: 0x00027534
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

		// Token: 0x040011FA RID: 4602
		private string searchFilterField;

		// Token: 0x040011FB RID: 4603
		private bool expandGroupMembershipField;

		// Token: 0x040011FC RID: 4604
		private bool expandGroupMembershipFieldSpecified;
	}
}

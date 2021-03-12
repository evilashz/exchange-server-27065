using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000457 RID: 1111
	[DataContract]
	public class TransportRuleFilter : SearchTextFilter
	{
		// Token: 0x170021DB RID: 8667
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000A9534 File Offset: 0x000A7734
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-TransportRule";
			}
		}

		// Token: 0x170021DC RID: 8668
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000A953B File Offset: 0x000A773B
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x170021DD RID: 8669
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000A9542 File Offset: 0x000A7742
		// (set) Token: 0x06003793 RID: 14227 RVA: 0x000A955D File Offset: 0x000A775D
		[DataMember]
		public string DlpPolicy
		{
			get
			{
				return ((string)base["DlpPolicy"]) ?? string.Empty;
			}
			set
			{
				base["DlpPolicy"] = value;
			}
		}

		// Token: 0x170021DE RID: 8670
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000A956B File Offset: 0x000A776B
		protected override string[] FilterableProperties
		{
			get
			{
				return TransportRuleFilter.filterableProperties;
			}
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000A9572 File Offset: 0x000A7772
		protected override SearchTextFilterType FilterType
		{
			get
			{
				return SearchTextFilterType.Contains;
			}
		}

		// Token: 0x040025E0 RID: 9696
		private static string[] filterableProperties = new string[]
		{
			"Description"
		};
	}
}

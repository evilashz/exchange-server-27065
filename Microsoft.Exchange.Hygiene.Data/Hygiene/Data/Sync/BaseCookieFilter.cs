using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000215 RID: 533
	internal abstract class BaseCookieFilter : ConfigurablePropertyBag
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00044DA1 File Offset: 0x00042FA1
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00044DB3 File Offset: 0x00042FB3
		public string Version
		{
			get
			{
				return this[BaseCookieSchema.VersionProp] as string;
			}
			set
			{
				this[BaseCookieSchema.VersionProp] = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00044DC1 File Offset: 0x00042FC1
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x00044DD3 File Offset: 0x00042FD3
		public string ServiceInstance
		{
			get
			{
				return this[BaseCookieSchema.ServiceInstanceProp] as string;
			}
			set
			{
				this[BaseCookieSchema.ServiceInstanceProp] = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x00044DE1 File Offset: 0x00042FE1
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x00044DF3 File Offset: 0x00042FF3
		public DateTime? LastUpdatedCutoffThreshold
		{
			get
			{
				return (DateTime?)this[BaseCookieSchema.LastUpdatedCutoffThresholdQueryProp];
			}
			set
			{
				this[BaseCookieSchema.LastUpdatedCutoffThresholdQueryProp] = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x00044E06 File Offset: 0x00043006
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x00044E18 File Offset: 0x00043018
		public DateTime? LastCompletedCutoffThreshold
		{
			get
			{
				return (DateTime?)this[BaseCookieSchema.LastCompletedCutoffThresholdQueryProp];
			}
			set
			{
				this[BaseCookieSchema.LastCompletedCutoffThresholdQueryProp] = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x00044E2B File Offset: 0x0004302B
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00044E34 File Offset: 0x00043034
		public List<QueryFilter> GetQueryFilters()
		{
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (PropertyDefinition propertyDefinition in this.GetPropertyDefinitions(true))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, propertyDefinition, this[propertyDefinition]));
			}
			return list;
		}
	}
}

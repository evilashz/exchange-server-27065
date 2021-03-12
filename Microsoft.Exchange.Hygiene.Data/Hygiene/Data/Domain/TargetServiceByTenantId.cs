using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	internal class TargetServiceByTenantId : ConfigurablePropertyBag
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00025536 File Offset: 0x00023736
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.TenantId);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00025543 File Offset: 0x00023743
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x00025555 File Offset: 0x00023755
		public Guid TenantId
		{
			get
			{
				return DomainSchema.GetGuidEmptyIfNull(this[DomainSchema.TenantId]);
			}
			set
			{
				this[DomainSchema.TenantId] = DomainSchema.GetNullIfGuidEmpty(value);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x00025568 File Offset: 0x00023768
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x0002557A File Offset: 0x0002377A
		public Dictionary<int, Dictionary<int, string>> Properties
		{
			get
			{
				return this[DomainSchema.PropertiesAsId] as Dictionary<int, Dictionary<int, string>>;
			}
			set
			{
				this[DomainSchema.PropertiesAsId] = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x00025588 File Offset: 0x00023788
		public IEnumerable<string> UpdatedDomainKeys
		{
			get
			{
				return this[DomainSchema.UpdatedDomains] as IEnumerable<string>;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002559A File Offset: 0x0002379A
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return TargetServiceByTenantId.propertydefinitions;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000255AC File Offset: 0x000237AC
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005F5 RID: 1525
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.TenantId,
			DomainSchema.PropertiesAsId
		};
	}
}

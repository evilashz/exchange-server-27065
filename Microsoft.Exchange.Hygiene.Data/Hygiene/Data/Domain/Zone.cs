using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	internal class Zone : ConfigurablePropertyBag
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00025D9B File Offset: 0x00023F9B
		public override ObjectId Identity
		{
			get
			{
				return DomainSchema.GetObjectId(this.ZoneId);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00025DA8 File Offset: 0x00023FA8
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00025DBA File Offset: 0x00023FBA
		public Guid ZoneId
		{
			get
			{
				return (Guid)this[DomainSchema.ZoneId];
			}
			set
			{
				this[DomainSchema.ZoneId] = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00025DCD File Offset: 0x00023FCD
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00025DDF File Offset: 0x00023FDF
		public string DomainName
		{
			get
			{
				return (string)this[DomainSchema.DomainName];
			}
			set
			{
				this[DomainSchema.DomainName] = DomainSchema.GetNullIfStringEmpty(value);
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00025DF2 File Offset: 0x00023FF2
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return Zone.propertydefinitions;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00025E04 File Offset: 0x00024004
		public override string ToString()
		{
			return this.ConvertToString();
		}

		// Token: 0x040005FA RID: 1530
		private static readonly PropertyDefinition[] propertydefinitions = new PropertyDefinition[]
		{
			DomainSchema.ZoneId,
			DomainSchema.DomainName
		};
	}
}

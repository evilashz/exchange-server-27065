using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200011F RID: 287
	internal class DIDomainCommon : DICommon
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00021A30 File Offset: 0x0001FC30
		public string DomainKey
		{
			get
			{
				return this[DomainSchema.DomainKey] as string;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00021A42 File Offset: 0x0001FC42
		public string DomainName
		{
			get
			{
				return this[DomainSchema.DomainName] as string;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00021A54 File Offset: 0x0001FC54
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>(base.GetPropertyDefinitions(false));
			list.AddRange(DIDomainCommon.definitions);
			return list;
		}

		// Token: 0x04000597 RID: 1431
		private static readonly PropertyDefinition[] definitions = new PropertyDefinition[]
		{
			DomainSchema.DomainKey,
			DomainSchema.DomainName
		};
	}
}

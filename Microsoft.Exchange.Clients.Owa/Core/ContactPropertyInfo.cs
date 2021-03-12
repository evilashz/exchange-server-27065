using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002B9 RID: 697
	public class ContactPropertyInfo
	{
		// Token: 0x06001B3C RID: 6972 RVA: 0x0009BA1F File Offset: 0x00099C1F
		public ContactPropertyInfo(PropertyDefinition propertyDefinition, string id, Strings.IDs label)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			this.label = label;
			this.id = id;
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0009BA4A File Offset: 0x00099C4A
		public Strings.IDs Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0009BA52 File Offset: 0x00099C52
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0009BA5A File Offset: 0x00099C5A
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x0400139A RID: 5018
		private Strings.IDs label;

		// Token: 0x0400139B RID: 5019
		private string id;

		// Token: 0x0400139C RID: 5020
		private PropertyDefinition propertyDefinition;
	}
}

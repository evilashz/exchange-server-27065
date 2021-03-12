using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	public sealed class ProxyAddressTemplateCollection : ProxyAddressBaseCollection<ProxyAddressTemplate>
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001259D File Offset: 0x0001079D
		public new static ProxyAddressTemplateCollection Empty
		{
			get
			{
				return ProxyAddressTemplateCollection.empty;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000125A4 File Offset: 0x000107A4
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x000125AC File Offset: 0x000107AC
		public new bool AutoPromotionDisabled
		{
			get
			{
				return base.AutoPromotionDisabled;
			}
			set
			{
				base.AutoPromotionDisabled = value;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000125B5 File Offset: 0x000107B5
		public ProxyAddressTemplateCollection()
		{
			this.AutoPromotionDisabled = true;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000125C4 File Offset: 0x000107C4
		public ProxyAddressTemplateCollection(object value)
		{
			this.AutoPromotionDisabled = true;
			this.Add(value);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000125DC File Offset: 0x000107DC
		public ProxyAddressTemplateCollection(ICollection values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.AutoPromotionDisabled = true;
			foreach (object item in values)
			{
				this.Add(item);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00012648 File Offset: 0x00010848
		public ProxyAddressTemplateCollection(Dictionary<string, object> table) : base(table)
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012651 File Offset: 0x00010851
		internal ProxyAddressTemplateCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
			this.AutoPromotionDisabled = true;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012663 File Offset: 0x00010863
		internal ProxyAddressTemplateCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
			this.AutoPromotionDisabled = true;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00012679 File Offset: 0x00010879
		public new static implicit operator ProxyAddressTemplateCollection(object[] array)
		{
			return new ProxyAddressTemplateCollection(false, null, array);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00012683 File Offset: 0x00010883
		protected override ProxyAddressTemplate ConvertInput(object item)
		{
			if (item is string)
			{
				return ProxyAddressTemplate.Parse((string)item);
			}
			return base.ConvertInput(item);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000126A0 File Offset: 0x000108A0
		public ProxyAddressTemplateCollection(Hashtable table) : base(table)
		{
		}

		// Token: 0x0400030B RID: 779
		private static ProxyAddressTemplateCollection empty = new ProxyAddressTemplateCollection(true, null, new ProxyAddressTemplate[0]);
	}
}

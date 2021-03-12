using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class ImportContactListResult : ConfigurableObject
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00007B8D File Offset: 0x00005D8D
		public ImportContactListResult(ObjectId mbxIdentity) : base(new SimpleProviderPropertyBag())
		{
			SyncUtilities.ThrowIfArgumentNull("mbxIdentity", mbxIdentity);
			this[SimpleProviderObjectSchema.Identity] = mbxIdentity;
			base.ResetChangeTracking();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007BB7 File Offset: 0x00005DB7
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00007BC9 File Offset: 0x00005DC9
		public int ContactsImported
		{
			get
			{
				return (int)this[ImportContactListResultSchema.ContactsImported];
			}
			internal set
			{
				this[ImportContactListResultSchema.ContactsImported] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007BDC File Offset: 0x00005DDC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ImportContactListResult.schema;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00007BE3 File Offset: 0x00005DE3
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000079 RID: 121
		private static readonly ImportContactListResultSchema schema = ObjectSchema.GetInstance<ImportContactListResultSchema>();
	}
}

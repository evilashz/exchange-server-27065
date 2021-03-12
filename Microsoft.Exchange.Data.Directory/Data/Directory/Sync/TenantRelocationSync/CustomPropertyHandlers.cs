using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F7 RID: 2039
	internal class CustomPropertyHandlers
	{
		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x0015F908 File Offset: 0x0015DB08
		// (set) Token: 0x060064C2 RID: 25794 RVA: 0x0015F910 File Offset: 0x0015DB10
		internal Dictionary<string, CustomPropertyHandlerBase> Handlers { get; private set; }

		// Token: 0x060064C3 RID: 25795 RVA: 0x0015F91C File Offset: 0x0015DB1C
		private CustomPropertyHandlers()
		{
			this.Handlers = new Dictionary<string, CustomPropertyHandlerBase>(StringComparer.InvariantCultureIgnoreCase);
			this.Handlers.Add(ADRecipientSchema.PoliciesIncluded.LdapDisplayName, StringObjectGuidHandler.Instance);
			this.Handlers.Add(ADRecipientSchema.PoliciesExcluded.LdapDisplayName, StringObjectGuidHandler.Instance);
			this.Handlers.Add("msExchTargetServerAdmins", BinaryObjectGuidHandler.Instance);
			this.Handlers.Add("msExchTargetServerViewOnlyAdmins", BinaryObjectGuidHandler.Instance);
			this.Handlers.Add("msExchTargetServerPartnerAdmins", BinaryObjectGuidHandler.Instance);
			this.Handlers.Add("msExchTargetServerPartnerViewOnlyAdmins", BinaryObjectGuidHandler.Instance);
		}

		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x0015F9C7 File Offset: 0x0015DBC7
		internal static CustomPropertyHandlers Instance
		{
			get
			{
				if (CustomPropertyHandlers.instance == null)
				{
					CustomPropertyHandlers.instance = new CustomPropertyHandlers();
				}
				return CustomPropertyHandlers.instance;
			}
		}

		// Token: 0x040042F5 RID: 17141
		private static CustomPropertyHandlers instance;
	}
}

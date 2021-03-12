using System;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000506 RID: 1286
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	internal sealed class MsoMainStreamCookieContainer : SyncServiceInstance
	{
		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x000DC56F File Offset: 0x000DA76F
		internal MultiValuedProperty<byte[]> MsoForwardSyncRecipientCookie
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie];
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000DC581 File Offset: 0x000DA781
		internal MultiValuedProperty<byte[]> MsoForwardSyncNonRecipientCookie
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie];
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000DC593 File Offset: 0x000DA793
		internal MultiValuedProperty<FullSyncObjectRequest> MsoForwardSyncObjectFullSyncRequests
		{
			get
			{
				return (MultiValuedProperty<FullSyncObjectRequest>)this[MsoMainStreamCookieContainerSchema.MsoForwardSyncObjectFullSyncRequests];
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x0600390C RID: 14604 RVA: 0x000DC5A5 File Offset: 0x000DA7A5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMSOSyncServiceInstance";
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000DC5AC File Offset: 0x000DA7AC
		internal override ADObjectSchema Schema
		{
			get
			{
				return MsoMainStreamCookieContainer.schema;
			}
		}

		// Token: 0x040026F6 RID: 9974
		private const string MostDerivedObjectClassInternal = "msExchMSOSyncServiceInstance";

		// Token: 0x040026F7 RID: 9975
		private static MsoMainStreamCookieContainerSchema schema = ObjectSchema.GetInstance<MsoMainStreamCookieContainerSchema>();
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005AD RID: 1453
	[Serializable]
	public sealed class SyncedAcceptedDomain : AcceptedDomain
	{
		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x000FC386 File Offset: 0x000FA586
		public MultiValuedProperty<string> SyncErrors
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncedAcceptedDomainSchema.SyncErrors];
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x000FC398 File Offset: 0x000FA598
		internal override ADObjectSchema Schema
		{
			get
			{
				return SyncedAcceptedDomain.SchemaObject;
			}
		}

		// Token: 0x04002D8A RID: 11658
		private static readonly SyncedAcceptedDomainSchema SchemaObject = ObjectSchema.GetInstance<SyncedAcceptedDomainSchema>();
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000520 RID: 1312
	[KnownType(typeof(ManagementRoleObject))]
	[DataContract]
	public class ManagementRoleObject : ManagementRoleRow
	{
		// Token: 0x06003EBA RID: 16058 RVA: 0x000BD30D File Offset: 0x000BB50D
		public ManagementRoleObject(ExchangeRole exchangeRole) : base(exchangeRole)
		{
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x000BD316 File Offset: 0x000BB516
		// (set) Token: 0x06003EBC RID: 16060 RVA: 0x000BD323 File Offset: 0x000BB523
		[DataMember]
		public string Description
		{
			get
			{
				return base.ExchangeRole.Description;
			}
			set
			{
				base.ExchangeRole.Description = value;
			}
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x000BD331 File Offset: 0x000BB531
		// (set) Token: 0x06003EBE RID: 16062 RVA: 0x000BD33E File Offset: 0x000BB53E
		public ScopeType ImplicitConfigWriteScope
		{
			get
			{
				return base.ExchangeRole.ImplicitConfigWriteScope;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x000BD345 File Offset: 0x000BB545
		// (set) Token: 0x06003EC0 RID: 16064 RVA: 0x000BD352 File Offset: 0x000BB552
		public ScopeType ImplicitRecipientWriteScope
		{
			get
			{
				return base.ExchangeRole.ImplicitRecipientWriteScope;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x000BD359 File Offset: 0x000BB559
		// (set) Token: 0x06003EC2 RID: 16066 RVA: 0x000BD370 File Offset: 0x000BB570
		[DataMember]
		public string ImplicitConfigWriteScopeName
		{
			get
			{
				return base.ExchangeRole.ImplicitConfigWriteScope.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x000BD377 File Offset: 0x000BB577
		// (set) Token: 0x06003EC4 RID: 16068 RVA: 0x000BD38E File Offset: 0x000BB58E
		[DataMember]
		public string ImplicitRecipientWriteScopeName
		{
			get
			{
				return base.ExchangeRole.ImplicitRecipientWriteScope.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}

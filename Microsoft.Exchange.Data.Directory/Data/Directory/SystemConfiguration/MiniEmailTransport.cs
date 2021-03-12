using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004E3 RID: 1251
	[Serializable]
	public class MiniEmailTransport : MiniObject
	{
		// Token: 0x060037F7 RID: 14327 RVA: 0x000D9714 File Offset: 0x000D7914
		static MiniEmailTransport()
		{
			ADEmailTransport ademailTransport = new ADEmailTransport();
			MiniEmailTransport.implicitFilter = ademailTransport.ImplicitFilter;
			MiniEmailTransport.mostDerivedClass = ademailTransport.MostDerivedObjectClass;
			MiniEmailTransport.schema = ObjectSchema.GetInstance<MiniEmailTransportSchema>();
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x000D974F File Offset: 0x000D794F
		public bool IsPop3
		{
			get
			{
				return base.ObjectClass.Contains(Pop3AdConfiguration.MostDerivedClass);
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000D9761 File Offset: 0x000D7961
		public bool IsImap4
		{
			get
			{
				return base.ObjectClass.Contains(Imap4AdConfiguration.MostDerivedClass);
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060037FB RID: 14331 RVA: 0x000D9773 File Offset: 0x000D7973
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ADEmailTransportSchema.Server];
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000D9785 File Offset: 0x000D7985
		public MultiValuedProperty<IPBinding> UnencryptedOrTLSBindings
		{
			get
			{
				return (MultiValuedProperty<IPBinding>)this[PopImapAdConfigurationSchema.UnencryptedOrTLSBindings];
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x000D9797 File Offset: 0x000D7997
		public MultiValuedProperty<IPBinding> SSLBindings
		{
			get
			{
				return (MultiValuedProperty<IPBinding>)this[PopImapAdConfigurationSchema.SSLBindings];
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000D97A9 File Offset: 0x000D79A9
		public MultiValuedProperty<ProtocolConnectionSettings> ExternalConnectionSettings
		{
			get
			{
				return (MultiValuedProperty<ProtocolConnectionSettings>)this[PopImapAdConfigurationSchema.ExternalConnectionSettings];
			}
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000D97BB File Offset: 0x000D79BB
		public MultiValuedProperty<ProtocolConnectionSettings> InternalConnectionSettings
		{
			get
			{
				return (MultiValuedProperty<ProtocolConnectionSettings>)this[PopImapAdConfigurationSchema.InternalConnectionSettings];
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x000D97CD File Offset: 0x000D79CD
		public LoginOptions LoginType
		{
			get
			{
				return (LoginOptions)this[PopImapAdConfigurationSchema.LoginType];
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000D97DF File Offset: 0x000D79DF
		internal override ADObjectSchema Schema
		{
			get
			{
				return MiniEmailTransport.schema;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x000D97E6 File Offset: 0x000D79E6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MiniEmailTransport.mostDerivedClass;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x000D97ED File Offset: 0x000D79ED
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return MiniEmailTransport.implicitFilter;
			}
		}

		// Token: 0x040025CF RID: 9679
		private static QueryFilter implicitFilter;

		// Token: 0x040025D0 RID: 9680
		private static ADObjectSchema schema;

		// Token: 0x040025D1 RID: 9681
		private static string mostDerivedClass;
	}
}

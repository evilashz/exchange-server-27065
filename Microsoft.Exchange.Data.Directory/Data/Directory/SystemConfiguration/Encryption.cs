using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000441 RID: 1089
	[Serializable]
	public class Encryption : ADLegacyVersionableObject
	{
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000C6311 File Offset: 0x000C4511
		internal override ADObjectSchema Schema
		{
			get
			{
				return Encryption.schema;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000C6318 File Offset: 0x000C4518
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Encryption.mostDerivedClass;
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000C6328 File Offset: 0x000C4528
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(EncryptionSchema.DefaultMessageFormat))
			{
				this[EncryptionSchema.DefaultMessageFormat] = true;
			}
			if (!base.IsModified(EncryptionSchema.EncryptAlgListNA))
			{
				this[EncryptionSchema.EncryptAlgListNA] = new MultiValuedProperty<string>(new string[]
				{
					Encryption.KeyManagementServiceCast64,
					Encryption.KeyManagementServiceDes
				});
			}
			if (!base.IsModified(EncryptionSchema.EncryptAlgListOther))
			{
				this[EncryptionSchema.EncryptAlgListOther] = new MultiValuedProperty<string>(new string[]
				{
					Encryption.KeyManagementServiceCast40
				});
			}
			if (!base.IsModified(EncryptionSchema.EncryptAlgSelectedNA))
			{
				this[EncryptionSchema.EncryptAlgSelectedNA] = Encryption.KeyManagementServiceDes;
			}
			if (!base.IsModified(EncryptionSchema.EncryptAlgSelectedOther))
			{
				this[EncryptionSchema.EncryptAlgSelectedOther] = Encryption.KeyManagementServiceCast40;
			}
			if (!base.IsModified(EncryptionSchema.SMimeAlgListNA))
			{
				this[EncryptionSchema.SMimeAlgListNA] = new MultiValuedProperty<string>(new string[]
				{
					Encryption.KeyManagementService3Des,
					Encryption.KeyManagementServiceDes,
					Encryption.KeyManagementService128BitsRC2,
					Encryption.KeyManagementService40BitsRC2,
					Encryption.KeyManagementService64BitsRC2
				});
			}
			if (!base.IsModified(EncryptionSchema.SMimeAlgListOther))
			{
				this[EncryptionSchema.SMimeAlgListOther] = new MultiValuedProperty<string>(new string[]
				{
					Encryption.KeyManagementService40BitsRC2
				});
			}
			if (!base.IsModified(EncryptionSchema.SMimeAlgSelectedNA))
			{
				this[EncryptionSchema.SMimeAlgSelectedNA] = Encryption.KeyManagementService3Des;
			}
			if (!base.IsModified(EncryptionSchema.SMimeAlgSelectedOther))
			{
				this[EncryptionSchema.SMimeAlgSelectedOther] = Encryption.KeyManagementService40BitsRC2;
			}
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x0400211C RID: 8476
		public static readonly string DefaultName = "Encryption";

		// Token: 0x0400211D RID: 8477
		public static readonly string KeyManagementServiceCast64 = "CAST-64";

		// Token: 0x0400211E RID: 8478
		public static readonly string KeyManagementServiceCast40 = "CAST-40";

		// Token: 0x0400211F RID: 8479
		public static readonly string KeyManagementServiceDes = "DES";

		// Token: 0x04002120 RID: 8480
		public static readonly string KeyManagementService3Des = "3DES";

		// Token: 0x04002121 RID: 8481
		public static readonly string KeyManagementService40BitsRC2 = "RC2-40";

		// Token: 0x04002122 RID: 8482
		public static readonly string KeyManagementService64BitsRC2 = "RC2-64";

		// Token: 0x04002123 RID: 8483
		public static readonly string KeyManagementService128BitsRC2 = "RC2-128";

		// Token: 0x04002124 RID: 8484
		private static EncryptionSchema schema = ObjectSchema.GetInstance<EncryptionSchema>();

		// Token: 0x04002125 RID: 8485
		private static string mostDerivedClass = "encryptionCfg";
	}
}

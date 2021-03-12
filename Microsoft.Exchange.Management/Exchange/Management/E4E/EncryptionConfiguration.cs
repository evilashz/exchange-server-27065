using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.E4E
{
	// Token: 0x0200032A RID: 810
	[Serializable]
	public class EncryptionConfiguration : ConfigurableObject
	{
		// Token: 0x06001B8A RID: 7050 RVA: 0x0007A8F8 File Offset: 0x00078AF8
		public EncryptionConfiguration() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0007A905 File Offset: 0x00078B05
		public EncryptionConfiguration(string imageBase64, string emailText, string portalText, string disclaimerText, bool otpEnabled) : base(new SimpleProviderPropertyBag())
		{
			this.ImageBase64 = imageBase64;
			this.EmailText = emailText;
			this.PortalText = portalText;
			this.DisclaimerText = disclaimerText;
			this.OTPEnabled = otpEnabled;
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0007A937 File Offset: 0x00078B37
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return EncryptionConfiguration.schema;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0007A93E File Offset: 0x00078B3E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0007A945 File Offset: 0x00078B45
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0007A960 File Offset: 0x00078B60
		internal string ImageBase64
		{
			get
			{
				if (this.Image != null)
				{
					return Convert.ToBase64String(this.Image);
				}
				return string.Empty;
			}
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					this.Image = Convert.FromBase64String(value);
					return;
				}
				this.Image = null;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0007A97E File Offset: 0x00078B7E
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0007A990 File Offset: 0x00078B90
		public byte[] Image
		{
			get
			{
				return (byte[])this[EncryptionConfigurationSchema.Image];
			}
			set
			{
				this[EncryptionConfigurationSchema.Image] = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0007A99E File Offset: 0x00078B9E
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x0007A9B0 File Offset: 0x00078BB0
		public string EmailText
		{
			get
			{
				return (string)this[EncryptionConfigurationSchema.EmailText];
			}
			set
			{
				this[EncryptionConfigurationSchema.EmailText] = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0007A9BE File Offset: 0x00078BBE
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x0007A9D0 File Offset: 0x00078BD0
		public string PortalText
		{
			get
			{
				return (string)this[EncryptionConfigurationSchema.PortalText];
			}
			set
			{
				this[EncryptionConfigurationSchema.PortalText] = value;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0007A9DE File Offset: 0x00078BDE
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x0007A9F0 File Offset: 0x00078BF0
		public string DisclaimerText
		{
			get
			{
				return (string)this[EncryptionConfigurationSchema.DisclaimerText];
			}
			set
			{
				this[EncryptionConfigurationSchema.DisclaimerText] = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0007A9FE File Offset: 0x00078BFE
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x0007AA10 File Offset: 0x00078C10
		public bool OTPEnabled
		{
			get
			{
				return (bool)this[EncryptionConfigurationSchema.OTPEnabled];
			}
			set
			{
				this[EncryptionConfigurationSchema.OTPEnabled] = value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0007AA23 File Offset: 0x00078C23
		public override ObjectId Identity
		{
			get
			{
				return (ObjectId)this[EncryptionConfigurationSchema.Identity];
			}
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0007AA38 File Offset: 0x00078C38
		internal static object IdentityGetter(IPropertyBag propertyBag)
		{
			byte[] image = (byte[])propertyBag[EncryptionConfigurationSchema.Image];
			string emailText = (string)propertyBag[EncryptionConfigurationSchema.EmailText];
			string portalText = (string)propertyBag[EncryptionConfigurationSchema.PortalText];
			string disclaimerText = (string)propertyBag[EncryptionConfigurationSchema.DisclaimerText];
			bool otpEnabled = (bool)propertyBag[EncryptionConfigurationSchema.OTPEnabled];
			return new OMEConfigurationId(image, emailText, portalText, disclaimerText, otpEnabled);
		}

		// Token: 0x04000BE1 RID: 3041
		private static EncryptionConfigurationSchema schema = ObjectSchema.GetInstance<EncryptionConfigurationSchema>();
	}
}

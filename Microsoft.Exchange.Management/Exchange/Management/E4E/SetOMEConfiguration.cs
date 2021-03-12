using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management.Automation;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.E4E
{
	// Token: 0x0200032D RID: 813
	[Cmdlet("Set", "OMEConfiguration", DefaultParameterSetName = "Identity")]
	public sealed class SetOMEConfiguration : SetTenantADTaskBase<OMEConfigurationIdParameter, EncryptionConfiguration, EncryptionConfiguration>
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0007AF1B File Offset: 0x0007911B
		// (set) Token: 0x06001BAE RID: 7086 RVA: 0x0007AF32 File Offset: 0x00079132
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0007AF45 File Offset: 0x00079145
		// (set) Token: 0x06001BB0 RID: 7088 RVA: 0x0007AF5C File Offset: 0x0007915C
		[Parameter(Mandatory = false)]
		public byte[] Image
		{
			get
			{
				return (byte[])base.Fields["Image"];
			}
			set
			{
				base.Fields["Image"] = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x0007AF6F File Offset: 0x0007916F
		// (set) Token: 0x06001BB2 RID: 7090 RVA: 0x0007AF86 File Offset: 0x00079186
		[Parameter(Mandatory = false)]
		public string EmailText
		{
			get
			{
				return (string)base.Fields["EmailText"];
			}
			set
			{
				base.Fields["EmailText"] = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0007AF99 File Offset: 0x00079199
		// (set) Token: 0x06001BB4 RID: 7092 RVA: 0x0007AFB0 File Offset: 0x000791B0
		[Parameter(Mandatory = false)]
		public string PortalText
		{
			get
			{
				return (string)base.Fields["PortalText"];
			}
			set
			{
				base.Fields["PortalText"] = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001BB5 RID: 7093 RVA: 0x0007AFC3 File Offset: 0x000791C3
		// (set) Token: 0x06001BB6 RID: 7094 RVA: 0x0007AFDA File Offset: 0x000791DA
		[Parameter(Mandatory = false)]
		public string DisclaimerText
		{
			get
			{
				return (string)base.Fields["DisclaimerText"];
			}
			set
			{
				base.Fields["DisclaimerText"] = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x0007AFED File Offset: 0x000791ED
		// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x0007B004 File Offset: 0x00079204
		[Parameter(Mandatory = false)]
		public bool OTPEnabled
		{
			get
			{
				return (bool)base.Fields["OTPEnabled"];
			}
			set
			{
				base.Fields["OTPEnabled"] = value;
			}
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0007B01C File Offset: 0x0007921C
		protected override IConfigDataProvider CreateSession()
		{
			if (this.Organization != null)
			{
				this.SetCurrentOrganizationId();
			}
			if (base.CurrentOrganizationId == null || base.CurrentOrganizationId.OrganizationalUnit == null || string.IsNullOrWhiteSpace(base.CurrentOrganizationId.OrganizationalUnit.Name))
			{
				base.WriteError(new LocalizedException(Strings.ErrorParameterRequired("Organization")), ErrorCategory.InvalidArgument, null);
			}
			string organizationRawIdentity;
			if (this.Organization == null)
			{
				organizationRawIdentity = base.CurrentOrganizationId.OrganizationalUnit.Name;
			}
			else
			{
				organizationRawIdentity = this.Organization.RawIdentity;
			}
			return new EncryptionConfigurationDataProvider(organizationRawIdentity);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0007B0B0 File Offset: 0x000792B0
		protected override IConfigurable PrepareDataObject()
		{
			EncryptionConfiguration encryptionConfiguration = (EncryptionConfiguration)base.PrepareDataObject();
			if (base.Fields.IsModified("Image"))
			{
				encryptionConfiguration.Image = this.Image;
				if (this.Image != null && this.Image.Length > 0)
				{
					if (this.Image.Length > 40960)
					{
						base.WriteError(new LocalizedException(Strings.ErrorImageFileBig(40)), ErrorCategory.InvalidArgument, null);
					}
					try
					{
						using (MemoryStream memoryStream = new MemoryStream(this.Image))
						{
							Bitmap bitmap = new Bitmap(memoryStream);
							if (bitmap.Height > 70 || bitmap.Width > 170)
							{
								int height;
								int width;
								if (bitmap.Height * 170 > bitmap.Width * 70)
								{
									height = 70;
									width = bitmap.Width * 70 / bitmap.Height;
								}
								else
								{
									width = 170;
									height = bitmap.Height * 170 / bitmap.Width;
								}
								bitmap = new Bitmap(bitmap, width, height);
							}
							using (MemoryStream memoryStream2 = new MemoryStream())
							{
								bitmap.Save(memoryStream2, ImageFormat.Png);
								encryptionConfiguration.Image = memoryStream2.ToArray();
							}
						}
					}
					catch (Exception)
					{
						base.WriteError(new LocalizedException(Strings.ErrorImageImport), ErrorCategory.InvalidArgument, null);
					}
				}
			}
			if (base.Fields.IsModified("EmailText"))
			{
				encryptionConfiguration.EmailText = AntiXssEncoder.HtmlEncode(this.EmailText, false);
			}
			if (base.Fields.IsModified("PortalText"))
			{
				encryptionConfiguration.PortalText = AntiXssEncoder.HtmlEncode(this.PortalText, false);
			}
			if (base.Fields.IsModified("DisclaimerText"))
			{
				encryptionConfiguration.DisclaimerText = AntiXssEncoder.HtmlEncode(this.DisclaimerText, false);
			}
			if (base.Fields.IsModified("OTPEnabled"))
			{
				encryptionConfiguration.OTPEnabled = this.OTPEnabled;
			}
			return encryptionConfiguration;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0007B2B0 File Offset: 0x000794B0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.Fields.IsModified("EmailText") && !string.IsNullOrWhiteSpace(this.EmailText) && this.EmailText.Length > 1024)
			{
				base.WriteError(new LocalizedException(Strings.ErrorEmailTextBig(1024)), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("PortalText") && !string.IsNullOrWhiteSpace(this.PortalText) && this.PortalText.Length > 128)
			{
				base.WriteError(new LocalizedException(Strings.ErrorPortalTextBig(128)), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("DisclaimerText") && !string.IsNullOrWhiteSpace(this.DisclaimerText) && this.DisclaimerText.Length > 1024)
			{
				base.WriteError(new LocalizedException(Strings.ErrorDisclaimerTextBig(1024)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0007B39C File Offset: 0x0007959C
		private void SetCurrentOrganizationId()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 285, "SetCurrentOrganizationId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\E4E\\SetEncryptionConfiguration.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
			base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
		}

		// Token: 0x04000BE3 RID: 3043
		private const int maxWidth = 170;

		// Token: 0x04000BE4 RID: 3044
		private const int maxHeight = 70;

		// Token: 0x04000BE5 RID: 3045
		private const int maxImageSize = 40960;

		// Token: 0x04000BE6 RID: 3046
		private const int maxEmailTextSize = 1024;

		// Token: 0x04000BE7 RID: 3047
		private const int maxPortalTextSize = 128;

		// Token: 0x04000BE8 RID: 3048
		private const int maxDisclaimerTextSize = 1024;
	}
}

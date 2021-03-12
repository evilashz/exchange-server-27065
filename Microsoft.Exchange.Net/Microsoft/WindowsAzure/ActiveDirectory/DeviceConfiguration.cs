using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A8 RID: 1448
	[DataServiceKey("objectId")]
	public class DeviceConfiguration : DirectoryObject
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x0002CEB8 File Offset: 0x0002B0B8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static DeviceConfiguration CreateDeviceConfiguration(string objectId, Collection<byte[]> publicIssuerCertificates, Collection<byte[]> cloudPublicIssuerCertificates)
		{
			DeviceConfiguration deviceConfiguration = new DeviceConfiguration();
			deviceConfiguration.objectId = objectId;
			if (publicIssuerCertificates == null)
			{
				throw new ArgumentNullException("publicIssuerCertificates");
			}
			deviceConfiguration.publicIssuerCertificates = publicIssuerCertificates;
			if (cloudPublicIssuerCertificates == null)
			{
				throw new ArgumentNullException("cloudPublicIssuerCertificates");
			}
			deviceConfiguration.cloudPublicIssuerCertificates = cloudPublicIssuerCertificates;
			return deviceConfiguration;
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0002CEFD File Offset: 0x0002B0FD
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x0002CF05 File Offset: 0x0002B105
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<byte[]> publicIssuerCertificates
		{
			get
			{
				return this._publicIssuerCertificates;
			}
			set
			{
				this._publicIssuerCertificates = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x0002CF0E File Offset: 0x0002B10E
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x0002CF16 File Offset: 0x0002B116
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<byte[]> cloudPublicIssuerCertificates
		{
			get
			{
				return this._cloudPublicIssuerCertificates;
			}
			set
			{
				this._cloudPublicIssuerCertificates = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0002CF1F File Offset: 0x0002B11F
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x0002CF27 File Offset: 0x0002B127
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? registrationQuota
		{
			get
			{
				return this._registrationQuota;
			}
			set
			{
				this._registrationQuota = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x0002CF30 File Offset: 0x0002B130
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x0002CF38 File Offset: 0x0002B138
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? maximumRegistrationInactivityPeriod
		{
			get
			{
				return this._maximumRegistrationInactivityPeriod;
			}
			set
			{
				this._maximumRegistrationInactivityPeriod = value;
			}
		}

		// Token: 0x0400198F RID: 6543
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<byte[]> _publicIssuerCertificates = new Collection<byte[]>();

		// Token: 0x04001990 RID: 6544
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<byte[]> _cloudPublicIssuerCertificates = new Collection<byte[]>();

		// Token: 0x04001991 RID: 6545
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _registrationQuota;

		// Token: 0x04001992 RID: 6546
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _maximumRegistrationInactivityPeriod;
	}
}

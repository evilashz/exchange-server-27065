using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C9 RID: 1481
	[DataServiceKey("objectId")]
	public class DeviceConfiguration : DirectoryObject
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x0002EC44 File Offset: 0x0002CE44
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0002EC89 File Offset: 0x0002CE89
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x0002EC91 File Offset: 0x0002CE91
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

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0002EC9A File Offset: 0x0002CE9A
		// (set) Token: 0x06001779 RID: 6009 RVA: 0x0002ECA2 File Offset: 0x0002CEA2
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

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0002ECAB File Offset: 0x0002CEAB
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x0002ECB3 File Offset: 0x0002CEB3
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		// (set) Token: 0x0600177D RID: 6013 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
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

		// Token: 0x04001AA5 RID: 6821
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<byte[]> _publicIssuerCertificates = new Collection<byte[]>();

		// Token: 0x04001AA6 RID: 6822
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<byte[]> _cloudPublicIssuerCertificates = new Collection<byte[]>();

		// Token: 0x04001AA7 RID: 6823
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _registrationQuota;

		// Token: 0x04001AA8 RID: 6824
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _maximumRegistrationInactivityPeriod;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F1 RID: 1521
	[DataServiceKey("objectId")]
	public class DeviceConfiguration : DirectoryObject
	{
		// Token: 0x06001A47 RID: 6727 RVA: 0x00030FCC File Offset: 0x0002F1CC
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

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00031011 File Offset: 0x0002F211
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x00031019 File Offset: 0x0002F219
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

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00031022 File Offset: 0x0002F222
		// (set) Token: 0x06001A4B RID: 6731 RVA: 0x0003102A File Offset: 0x0002F22A
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

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00031033 File Offset: 0x0002F233
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x0003103B File Offset: 0x0002F23B
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

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x00031044 File Offset: 0x0002F244
		// (set) Token: 0x06001A4F RID: 6735 RVA: 0x0003104C File Offset: 0x0002F24C
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

		// Token: 0x04001BF4 RID: 7156
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<byte[]> _publicIssuerCertificates = new Collection<byte[]>();

		// Token: 0x04001BF5 RID: 7157
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<byte[]> _cloudPublicIssuerCertificates = new Collection<byte[]>();

		// Token: 0x04001BF6 RID: 7158
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _registrationQuota;

		// Token: 0x04001BF7 RID: 7159
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _maximumRegistrationInactivityPeriod;
	}
}

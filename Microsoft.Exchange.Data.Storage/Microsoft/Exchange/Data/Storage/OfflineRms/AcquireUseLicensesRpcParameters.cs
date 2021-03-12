using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB7 RID: 2743
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcquireUseLicensesRpcParameters : LicensingRpcParameters
	{
		// Token: 0x06006401 RID: 25601 RVA: 0x001A7BB6 File Offset: 0x001A5DB6
		public AcquireUseLicensesRpcParameters(byte[] data) : base(data)
		{
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x001A7BC0 File Offset: 0x001A5DC0
		public AcquireUseLicensesRpcParameters(RmsClientManagerContext rmsClientManagerContext, XmlNode[] rightsAccountCertificate, XmlNode[] issuanceLicense, LicenseeIdentity[] licenseeIdentities) : base(rmsClientManagerContext)
		{
			if (rmsClientManagerContext == null)
			{
				throw new ArgumentNullException("rmsClientManagerContext");
			}
			if (rightsAccountCertificate == null || rightsAccountCertificate.Length <= 0)
			{
				throw new ArgumentNullException("rightsAccountCertificate");
			}
			if (issuanceLicense == null || issuanceLicense.Length <= 0)
			{
				throw new ArgumentNullException("issuanceLicense");
			}
			if (licenseeIdentities == null || licenseeIdentities.Length <= 0)
			{
				throw new ArgumentNullException("licenseeIdentities");
			}
			base.SetParameterValue("RightsAccountCertificateStringArray", DrmClientUtils.ConvertXmlNodeArrayToStringArray(rightsAccountCertificate));
			base.SetParameterValue("IssuanceLicenseStringArray", DrmClientUtils.ConvertXmlNodeArrayToStringArray(issuanceLicense));
			base.SetParameterValue("LicenseeIdentities", licenseeIdentities);
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06006403 RID: 25603 RVA: 0x001A7C50 File Offset: 0x001A5E50
		public XmlNode[] RightsAccountCertificate
		{
			get
			{
				if (this.rightsAccountCertificate == null)
				{
					string[] array = base.GetParameterValue("RightsAccountCertificateStringArray") as string[];
					if (array == null || array.Length <= 0)
					{
						throw new ArgumentNullException("rightsAccountCertificateStringArray");
					}
					if (!RMUtil.TryConvertCertChainStringArrayToXmlNodeArray(array, out this.rightsAccountCertificate))
					{
						throw new InvalidOperationException("Conversion from string array to XmlNode array failed for rightsAccountCertificate");
					}
				}
				return this.rightsAccountCertificate;
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06006404 RID: 25604 RVA: 0x001A7CAC File Offset: 0x001A5EAC
		public XmlNode[] IssuanceLicense
		{
			get
			{
				if (this.issuanceLicense == null)
				{
					string[] array = base.GetParameterValue("IssuanceLicenseStringArray") as string[];
					if (array == null || array.Length <= 0)
					{
						throw new ArgumentNullException("issuanceLicenseStringArray");
					}
					if (!RMUtil.TryConvertCertChainStringArrayToXmlNodeArray(array, out this.issuanceLicense))
					{
						throw new InvalidOperationException("Conversion from string array to XmlNode array failed for issuanceLicense");
					}
				}
				return this.issuanceLicense;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06006405 RID: 25605 RVA: 0x001A7D08 File Offset: 0x001A5F08
		public LicenseeIdentity[] LicenseeIdentities
		{
			get
			{
				if (this.licenseeIdentities == null)
				{
					this.licenseeIdentities = (base.GetParameterValue("LicenseeIdentities") as LicenseeIdentity[]);
					if (this.licenseeIdentities == null || this.licenseeIdentities.Length <= 0)
					{
						throw new ArgumentNullException("licenseeIdentities");
					}
				}
				return this.licenseeIdentities;
			}
		}

		// Token: 0x040038B7 RID: 14519
		private const string RightsAccountCertificateStringArrayParameterName = "RightsAccountCertificateStringArray";

		// Token: 0x040038B8 RID: 14520
		private const string IssuanceLicenseStringArrayParameterName = "IssuanceLicenseStringArray";

		// Token: 0x040038B9 RID: 14521
		private const string LicenseeIdentitiesParameterName = "LicenseeIdentities";

		// Token: 0x040038BA RID: 14522
		private XmlNode[] rightsAccountCertificate;

		// Token: 0x040038BB RID: 14523
		private XmlNode[] issuanceLicense;

		// Token: 0x040038BC RID: 14524
		private LicenseeIdentity[] licenseeIdentities;
	}
}

using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB2 RID: 2738
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcquireTenantLicensesRpcParameters : LicensingRpcParameters
	{
		// Token: 0x060063F1 RID: 25585 RVA: 0x001A7882 File Offset: 0x001A5A82
		public AcquireTenantLicensesRpcParameters(byte[] data) : base(data)
		{
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x001A788C File Offset: 0x001A5A8C
		public AcquireTenantLicensesRpcParameters(RmsClientManagerContext rmsClientManagerContext, string identity, XmlNode[] machineCertificateChain) : base(rmsClientManagerContext)
		{
			if (rmsClientManagerContext == null)
			{
				throw new ArgumentNullException("rmsClientManagerContext");
			}
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			if (machineCertificateChain == null || machineCertificateChain.Length <= 0)
			{
				throw new ArgumentNullException("machineCertificateChain");
			}
			base.SetParameterValue("Identity", identity);
			base.SetParameterValue("MachineCertificateChainStringArray", DrmClientUtils.ConvertXmlNodeArrayToStringArray(machineCertificateChain));
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x001A78F4 File Offset: 0x001A5AF4
		public XmlNode[] MachineCertificateChain
		{
			get
			{
				if (this.machineCertificateChain == null)
				{
					string[] array = base.GetParameterValue("MachineCertificateChainStringArray") as string[];
					if (array == null || array.Length <= 0)
					{
						throw new ArgumentNullException("machineCertificateChainStringArray");
					}
					if (!RMUtil.TryConvertCertChainStringArrayToXmlNodeArray(array, out this.machineCertificateChain))
					{
						throw new InvalidOperationException("Conversion from string array to XmlNode array failed for machineCertificateChain");
					}
				}
				return this.machineCertificateChain;
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x001A794D File Offset: 0x001A5B4D
		public string Identity
		{
			get
			{
				if (string.IsNullOrEmpty(this.identity))
				{
					this.identity = (base.GetParameterValue("Identity") as string);
				}
				return this.identity;
			}
		}

		// Token: 0x040038AB RID: 14507
		private const string MachineCertificateChainStringArrayParameterName = "MachineCertificateChainStringArray";

		// Token: 0x040038AC RID: 14508
		private const string IdentityParameterName = "Identity";

		// Token: 0x040038AD RID: 14509
		private XmlNode[] machineCertificateChain;

		// Token: 0x040038AE RID: 14510
		private string identity;
	}
}

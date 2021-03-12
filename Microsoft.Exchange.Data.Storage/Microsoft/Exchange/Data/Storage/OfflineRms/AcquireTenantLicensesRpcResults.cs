using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB4 RID: 2740
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AcquireTenantLicensesRpcResults : LicensingRpcResults
	{
		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x060063F8 RID: 25592 RVA: 0x001A79DC File Offset: 0x001A5BDC
		public XmlNode[] RacXml
		{
			get
			{
				if (this.racXml == null)
				{
					string[] array = base.GetParameterValue("RacStringArray") as string[];
					if (array == null || array.Length <= 0)
					{
						throw new ArgumentNullException("RacStringArray");
					}
					if (!RMUtil.TryConvertCertChainStringArrayToXmlNodeArray(array, out this.racXml))
					{
						throw new InvalidOperationException("Conversion from string array to XmlNode array failed for racXml");
					}
				}
				return this.racXml;
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x060063F9 RID: 25593 RVA: 0x001A7A38 File Offset: 0x001A5C38
		public XmlNode[] ClcXml
		{
			get
			{
				if (this.clcXml == null)
				{
					string[] array = base.GetParameterValue("ClcStringArray") as string[];
					if (array == null || array.Length <= 0)
					{
						throw new ArgumentNullException("ClcStringArray");
					}
					if (!RMUtil.TryConvertCertChainStringArrayToXmlNodeArray(array, out this.clcXml))
					{
						throw new InvalidOperationException("Conversion from string array to XmlNode array failed for clcXml");
					}
				}
				return this.clcXml;
			}
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x001A7A91 File Offset: 0x001A5C91
		public AcquireTenantLicensesRpcResults(byte[] data) : base(data)
		{
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x001A7A9C File Offset: 0x001A5C9C
		public AcquireTenantLicensesRpcResults(OverallRpcResult overallRpcResult, XmlNode[] rac, XmlNode[] clc) : base(overallRpcResult)
		{
			if (overallRpcResult == null)
			{
				throw new ArgumentNullException("overallRpcResult");
			}
			if (rac == null || rac.Length <= 0)
			{
				throw new ArgumentNullException("rac");
			}
			if (clc == null || clc.Length <= 0)
			{
				throw new ArgumentNullException("clc");
			}
			base.SetParameterValue("RacStringArray", DrmClientUtils.ConvertXmlNodeArrayToStringArray(rac));
			base.SetParameterValue("ClcStringArray", DrmClientUtils.ConvertXmlNodeArrayToStringArray(clc));
		}

		// Token: 0x040038B1 RID: 14513
		private const string RacStringArrayParameterName = "RacStringArray";

		// Token: 0x040038B2 RID: 14514
		private const string ClcStringArrayParameterName = "ClcStringArray";

		// Token: 0x040038B3 RID: 14515
		private XmlNode[] racXml;

		// Token: 0x040038B4 RID: 14516
		private XmlNode[] clcXml;
	}
}

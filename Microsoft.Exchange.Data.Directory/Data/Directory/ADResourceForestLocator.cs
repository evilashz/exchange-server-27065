using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000066 RID: 102
	internal static class ADResourceForestLocator
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0001B75C File Offset: 0x0001995C
		public static string InferResourceForestFromAccountForestIdentity(ADObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			return ADResourceForestLocator.InferResourceForestFromAccountPartition(objectId.PartitionFQDN);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001B778 File Offset: 0x00019978
		public static string InferResourceForestFromAccountPartition(string partitionFQDN)
		{
			if (partitionFQDN == null)
			{
				throw new ArgumentNullException("partitionFQDN");
			}
			Match match = ADResourceForestLocator.ProdAccountPartitionRegex.Match(partitionFQDN);
			if (match.Success)
			{
				if (!(match.Groups["number"].Value == "01"))
				{
					return string.Format("{0}prd{1}.prod.outlook.com", match.Groups["region"].Value.ToLower(), match.Groups["number"].Value);
				}
				if (string.Compare(match.Groups["region"].Value, "nam", StringComparison.CurrentCultureIgnoreCase) == 0)
				{
					return "prod.exchangelabs.com";
				}
				return string.Format("{0}prd01.prod.exchangelabs.com", match.Groups["region"].Value.ToLower());
			}
			else
			{
				match = ADResourceForestLocator.GallatinAccountPartitionRegex.Match(partitionFQDN);
				if (match.Success)
				{
					return string.Format("{0}pr{1}.prod.partner.outlook.cn", match.Groups["region"].Value.ToLower(), match.Groups["number"].Value);
				}
				match = ADResourceForestLocator.PdtAccountPartitionRegex.Match(partitionFQDN);
				if (match.Success)
				{
					return string.Format("{0}pdt{1}.sdf.exchangelabs.com", match.Groups["region"].Value.ToLower(), match.Groups["number"].Value);
				}
				match = ADResourceForestLocator.SdfAccountPartitionRegex.Match(partitionFQDN);
				if (match.Success)
				{
					return string.Format("{0}sdf{1}.sdf.exchangelabs.com", match.Groups["region"].Value.ToLower(), match.Groups["number"].Value);
				}
				if (ADResourceForestLocator.TdsAccountPartitionRegex.IsMatch(partitionFQDN))
				{
					return PartitionId.LocalForest.ForestFQDN;
				}
				throw new CannotGetForestInfoException(DirectoryStrings.UnknownAccountForest(partitionFQDN));
			}
		}

		// Token: 0x04000212 RID: 530
		private static readonly Regex ProdAccountPartitionRegex = new Regex("^(?<region>\\w{3})pr(?<number>\\d{2})a00\\d.prod.outlook.com$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04000213 RID: 531
		private static readonly Regex GallatinAccountPartitionRegex = new Regex("^(?<region>\\w{3})pr(?<number>\\d{2})a00\\d.prod.partner.outlook.cn$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04000214 RID: 532
		private static readonly Regex PdtAccountPartitionRegex = new Regex("^(?<region>\\w{3})pdt(?<number>\\d{2})a00\\d.sdf.exchangelabs.com$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04000215 RID: 533
		private static readonly Regex SdfAccountPartitionRegex = new Regex("^(?<region>\\w{3})sr(?<number>\\d{2})a00\\d{1,2}.sdf.exchangelabs.com$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x04000216 RID: 534
		private static readonly Regex TdsAccountPartitionRegex = new Regex("^[\\w-]+dom.extest.microsoft.com$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
	}
}

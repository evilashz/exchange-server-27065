using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public class ServiceProviderSettings
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x0002BE4D File Offset: 0x0002A04D
		private ServiceProviderSettings(string providerName, string providerUrl, List<IPRange> ipRanges, List<TlsCertificate> tlsCertifcates, string originalExpression)
		{
			this.ProviderName = providerName;
			this.ProviderUrl = providerUrl;
			this.IPRanges = ipRanges;
			this.Certificates = tlsCertifcates;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0002BE72 File Offset: 0x0002A072
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0002BE7A File Offset: 0x0002A07A
		public string ProviderName { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0002BE83 File Offset: 0x0002A083
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0002BE8B File Offset: 0x0002A08B
		public string ProviderUrl { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0002BE94 File Offset: 0x0002A094
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0002BE9C File Offset: 0x0002A09C
		public List<IPRange> IPRanges { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0002BEA5 File Offset: 0x0002A0A5
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0002BEAD File Offset: 0x0002A0AD
		public List<TlsCertificate> Certificates { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0002BEB6 File Offset: 0x0002A0B6
		public string Expression
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
		public override string ToString()
		{
			string format = "{0},{1},{2},{3}";
			object[] array = new object[4];
			array[0] = this.ProviderName;
			array[1] = this.ProviderUrl;
			object[] array2 = array;
			int num = 2;
			object obj;
			if (this.IPRanges == null)
			{
				obj = null;
			}
			else
			{
				obj = string.Join(";", from ipRange in this.IPRanges
				where ipRange != null
				select ipRange.ToString());
			}
			array2[num] = obj;
			object[] array3 = array;
			int num2 = 3;
			object obj2;
			if (this.Certificates == null)
			{
				obj2 = null;
			}
			else
			{
				obj2 = string.Join(";", from tlsCertificate in this.Certificates
				where tlsCertificate != null
				select tlsCertificate.ToString());
			}
			array3[num2] = obj2;
			return string.Format(format, array);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002C004 File Offset: 0x0002A204
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ServiceProviderSettings serviceProviderSettings = obj as ServiceProviderSettings;
			if (serviceProviderSettings == null)
			{
				return false;
			}
			if (!string.Equals(this.ProviderName, serviceProviderSettings.ProviderName, StringComparison.InvariantCultureIgnoreCase) || !string.Equals(this.ProviderUrl, serviceProviderSettings.ProviderUrl, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (this.IPRanges != null && serviceProviderSettings.IPRanges != null)
			{
				if (this.IPRanges.Count != serviceProviderSettings.IPRanges.Count)
				{
					return false;
				}
				using (List<IPRange>.Enumerator enumerator = serviceProviderSettings.IPRanges.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IPRange ipRangeToCompare = enumerator.Current;
						if (!this.IPRanges.Any((IPRange ipRange) => ipRange.Equals(ipRangeToCompare)))
						{
							return false;
						}
					}
				}
			}
			if ((this.IPRanges == null && serviceProviderSettings.IPRanges != null) || (this.IPRanges != null && serviceProviderSettings.IPRanges == null))
			{
				return false;
			}
			if (this.Certificates != null && serviceProviderSettings.Certificates != null)
			{
				if (this.Certificates.Count != serviceProviderSettings.Certificates.Count)
				{
					return false;
				}
				using (List<TlsCertificate>.Enumerator enumerator2 = serviceProviderSettings.Certificates.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						TlsCertificate certificateToCompare = enumerator2.Current;
						if (!this.Certificates.Any((TlsCertificate certificate) => certificate.Equals(certificateToCompare)))
						{
							return false;
						}
					}
				}
			}
			return (this.Certificates == null || serviceProviderSettings.Certificates != null) && (this.Certificates != null || serviceProviderSettings.Certificates == null);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002C1D0 File Offset: 0x0002A3D0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		public static ServiceProviderSettings Parse(string stringToParse)
		{
			ServiceProviderSettings result;
			string message;
			if (ServiceProviderSettings.TryParse(stringToParse, out result, out message))
			{
				return result;
			}
			throw new FormatException(message);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002C204 File Offset: 0x0002A404
		private static bool TryParse(string stringToParse, out ServiceProviderSettings settings, out string error)
		{
			settings = null;
			error = null;
			if (string.IsNullOrWhiteSpace(stringToParse))
			{
				error = "String passed is null or empty";
				return false;
			}
			string[] array = stringToParse.Split(new char[]
			{
				','
			});
			if (array.Length != 4)
			{
				error = "We need a CSV with 4 values - Provider name, Provider url, IP ranges and Certificate names";
				return false;
			}
			char[] separator = new char[]
			{
				';'
			};
			List<IPRange> list = null;
			List<TlsCertificate> list2 = null;
			bool flag = true;
			if (!string.IsNullOrWhiteSpace(array[2]))
			{
				list = new List<IPRange>();
				string[] array2 = array[2].Split(separator);
				foreach (string text in array2)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						IPRange item;
						if (IPRange.TryParse(text.Trim(), out item))
						{
							list.Add(item);
						}
						else
						{
							error = string.Format("{0}Invalid IP Range {1}.", error, text);
							flag = false;
						}
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(array[3]))
			{
				list2 = new List<TlsCertificate>();
				string[] array4 = array[3].Split(separator);
				foreach (string text2 in array4)
				{
					if (!string.IsNullOrWhiteSpace(text2))
					{
						TlsCertificate item2;
						if (TlsCertificate.TryParse(text2.Trim(), out item2))
						{
							list2.Add(item2);
						}
						else
						{
							error = string.Format("{0}Invalid Tls certificate {1}.", error, text2);
							flag = false;
						}
					}
				}
			}
			if (flag)
			{
				settings = new ServiceProviderSettings(array[0].Trim(), array[1].Trim(), list, list2, stringToParse);
			}
			return flag;
		}
	}
}

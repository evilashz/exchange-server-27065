using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000904 RID: 2308
	internal static class TaskCommon
	{
		// Token: 0x060051E8 RID: 20968 RVA: 0x00152F68 File Offset: 0x00151168
		private static string GetDomainFromSubject(string subject)
		{
			int num = subject.IndexOf("cn=", StringComparison.InvariantCultureIgnoreCase);
			string text;
			if (num > -1)
			{
				text = subject.Substring(num + "cn=".Length).Split(new char[]
				{
					','
				})[0].Trim();
			}
			else
			{
				text = subject;
			}
			if (text.StartsWith("*."))
			{
				text = text.Replace("*.", "mail.");
			}
			return text;
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x00152FD5 File Offset: 0x001511D5
		public static string GetDomainFromSubject(SmtpX509Identifier certificateIdentity)
		{
			return TaskCommon.GetDomainFromSubject(certificateIdentity.CertificateSubject);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00152FE4 File Offset: 0x001511E4
		public static bool CompareArrays<T>(T[] array1, T[] array2, bool allowPartialMatch)
		{
			if (!allowPartialMatch && array1.Length != array2.Length)
			{
				return false;
			}
			bool result = !allowPartialMatch;
			for (int i = 0; i < array1.Length; i++)
			{
				if (allowPartialMatch)
				{
					if (array2.Contains(array1[i]))
					{
						result = true;
						break;
					}
				}
				else
				{
					if (!array2.Contains(array1[i]))
					{
						result = false;
						break;
					}
					if (!array1.Contains(array2[i]))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00153050 File Offset: 0x00151250
		public static OrganizationRelationship GetOrganizationRelationship(ICommonSession session, string identity, IEnumerable<string> domains)
		{
			OrganizationRelationship organizationRelationship = null;
			IEnumerable<OrganizationRelationship> organizationRelationship2 = session.GetOrganizationRelationship();
			bool flag = false;
			foreach (OrganizationRelationship organizationRelationship3 in organizationRelationship2)
			{
				if (organizationRelationship3.Identity.ToString().Equals(identity, StringComparison.InvariantCultureIgnoreCase))
				{
					if (flag)
					{
						throw new LocalizedException(HybridStrings.ErrorMultipleMatchingOrgRelationships);
					}
					organizationRelationship = organizationRelationship3;
				}
				foreach (string domain in domains)
				{
					SmtpDomain smtpDomain = new SmtpDomain(domain);
					foreach (SmtpDomain smtpDomain2 in organizationRelationship3.DomainNames)
					{
						if (smtpDomain2.Domain.Equals(smtpDomain.Domain, StringComparison.InvariantCultureIgnoreCase))
						{
							if (flag)
							{
								throw new LocalizedException(HybridStrings.ErrorMultipleMatchingOrgRelationships);
							}
							organizationRelationship = organizationRelationship3;
							break;
						}
					}
				}
				if (organizationRelationship != null)
				{
					flag = true;
				}
			}
			return organizationRelationship;
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x001531BC File Offset: 0x001513BC
		public static bool ContainsSame<T>(IEnumerable<T> a, IEnumerable<T> b)
		{
			if (a == b)
			{
				return true;
			}
			int num = (a == null) ? 0 : a.Count<T>();
			int num2 = (b == null) ? 0 : b.Count<T>();
			if (num == 0 && num2 == 0)
			{
				return true;
			}
			if (a == null || b == null || num != num2)
			{
				return false;
			}
			using (IEnumerator<T> enumerator = a.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T aItem = enumerator.Current;
					if (!b.Any((T i) => i.Equals(aItem)))
					{
						return false;
					}
				}
			}
			using (IEnumerator<T> enumerator2 = b.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					T bItem = enumerator2.Current;
					if (!a.Any((T i) => i.Equals(bItem)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x001532CC File Offset: 0x001514CC
		public static bool AreEqual(SmtpX509Identifier a, SmtpX509Identifier b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x001532E3 File Offset: 0x001514E3
		public static bool AreEqual(ADObjectId a, ADObjectId b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x001532FA File Offset: 0x001514FA
		public static bool AreEqual(SmtpDomain a, SmtpDomain b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00153320 File Offset: 0x00151520
		public static SmtpX509Identifier GetSmtpX509Identifier(MultiValuedProperty<SmtpReceiveDomainCapabilities> tlsDomainCapabilities)
		{
			SmtpReceiveDomainCapabilities smtpReceiveDomainCapabilities = (from d in tlsDomainCapabilities
			where d.SmtpX509Identifier != null
			select d).FirstOrDefault<SmtpReceiveDomainCapabilities>();
			if (smtpReceiveDomainCapabilities != null)
			{
				return new SmtpX509Identifier(smtpReceiveDomainCapabilities.ToString());
			}
			return null;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00153366 File Offset: 0x00151566
		public static string ToStringOrNull(object value)
		{
			if (value != null)
			{
				return value.ToString();
			}
			return null;
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x00153374 File Offset: 0x00151574
		public static string GetOnPremOrgRelationshipName(IOrganizationConfig orgConfig)
		{
			return string.Format("On-premises to O365 - {0}", orgConfig.Guid.ToString());
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x001533A0 File Offset: 0x001515A0
		public static string GetTenantOrgRelationshipName(IOrganizationConfig orgConfig)
		{
			return string.Format("O365 to On-premises - {0}", orgConfig.Guid.ToString());
		}

		// Token: 0x04002FDF RID: 12255
		private const string OnPremOrgRelationshipNameTemplate = "On-premises to O365 - {0}";

		// Token: 0x04002FE0 RID: 12256
		private const string TenantOrgRelationshipNameTemplate = "O365 to On-premises - {0}";

		// Token: 0x04002FE1 RID: 12257
		public const string WellKnownRemoteDomainNameTemplate = "Hybrid Domain - {0}";
	}
}

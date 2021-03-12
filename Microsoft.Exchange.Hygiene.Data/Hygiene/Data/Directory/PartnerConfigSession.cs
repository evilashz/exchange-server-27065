using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000F3 RID: 243
	internal class PartnerConfigSession
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0001D7FB File Offset: 0x0001B9FB
		public PartnerConfigSession(string callerId)
		{
			if (string.IsNullOrEmpty(callerId))
			{
				throw new ArgumentNullException("callerId");
			}
			this.callerId = callerId;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001D81D File Offset: 0x0001BA1D
		internal PartnerConfigSession(string callerId, string partnerName) : this(callerId)
		{
			if (string.IsNullOrEmpty(partnerName))
			{
				throw new ArgumentNullException("partnerName");
			}
			this.partnerName = partnerName;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001D840 File Offset: 0x0001BA40
		public static IEnumerable<PartnerCertificate> FindAllPartnerCertificatesForAllCallerIds()
		{
			IConfigurable[] source = PartnerConfigSession.dataProvider.Find<PartnerCertificate>(null, null, false, null);
			return source.Cast<PartnerCertificate>();
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001D864 File Offset: 0x0001BA64
		public static IEnumerable<PartnerRole> FindAllPartnerRolesForAllCallerIds()
		{
			IConfigurable[] source = PartnerConfigSession.dataProvider.Find<PartnerRole>(null, null, false, null);
			return source.Cast<PartnerRole>();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001D888 File Offset: 0x0001BA88
		public void SavePartnerCertificate(string certificateSubjectName)
		{
			if (string.IsNullOrEmpty(certificateSubjectName))
			{
				throw new ArgumentNullException("certificateSubjectName");
			}
			PartnerCertificate partnerCertificate = new PartnerCertificate(this.callerId, certificateSubjectName);
			if (string.IsNullOrEmpty(this.partnerName))
			{
				partnerCertificate.PartnerName = this.partnerName;
			}
			ConfigurablePropertyBag configurablePropertyBag = partnerCertificate;
			if (configurablePropertyBag != null)
			{
				configurablePropertyBag[PartnerCertificate.PartnerCertificateIdDef] = this.GenerateNewGuid();
				configurablePropertyBag[PartnerCertificate.PartnerIdDef] = this.GenerateNewGuid();
				configurablePropertyBag[PartnerCertificate.CertificateIdDef] = this.GenerateNewGuid();
			}
			PartnerConfigSession.dataProvider.Save(partnerCertificate);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001D920 File Offset: 0x0001BB20
		public void DeletePartnerCertificate(string certificateSubjectName)
		{
			if (string.IsNullOrEmpty(certificateSubjectName))
			{
				throw new ArgumentNullException("certificateSubjectName");
			}
			PartnerConfigSession.dataProvider.Delete(new PartnerCertificate(this.callerId, certificateSubjectName));
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001D94B File Offset: 0x0001BB4B
		public void SavePartnerRole(string roleName)
		{
			if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentNullException("roleName");
			}
			PartnerConfigSession.dataProvider.Save(new PartnerRole(this.callerId, roleName));
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001D976 File Offset: 0x0001BB76
		public void DeletePartnerRole(string roleName)
		{
			if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentNullException("roleName");
			}
			PartnerConfigSession.dataProvider.Delete(new PartnerRole(this.callerId, roleName));
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		public IEnumerable<PartnerCertificate> FindPartnerCertificates()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, PartnerCertificate.PartnerCallerIdDef, this.callerId);
			IConfigurable[] source = PartnerConfigSession.dataProvider.Find<PartnerCertificate>(filter, null, false, null);
			return source.Cast<PartnerCertificate>();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		public IEnumerable<PartnerRole> FindPartnerRole()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, PartnerRole.PartnerCallerIdDef, this.callerId);
			IConfigurable[] source = PartnerConfigSession.dataProvider.Find<PartnerRole>(filter, null, false, null);
			return source.Cast<PartnerRole>();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001DA0C File Offset: 0x0001BC0C
		private Guid GenerateNewGuid()
		{
			return CombGuidGenerator.NewGuid();
		}

		// Token: 0x04000507 RID: 1287
		private static IConfigDataProvider dataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Directory);

		// Token: 0x04000508 RID: 1288
		private readonly string callerId;

		// Token: 0x04000509 RID: 1289
		private readonly string partnerName;
	}
}

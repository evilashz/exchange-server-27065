using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000297 RID: 663
	public class GlsOverrideCollection
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x0008AAE4 File Offset: 0x00088CE4
		public GlsOverrideCollection(string serializedValue)
		{
			this.tenantOverrides = new List<GlobalLocatorServiceTenant>();
			if (string.IsNullOrEmpty(serializedValue))
			{
				return;
			}
			string[] array = serializedValue.Split(new char[]
			{
				';'
			});
			try
			{
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						':'
					});
					if (array3.Length < 5)
					{
						throw new FormatException("Parts count");
					}
					GlobalLocatorServiceTenant globalLocatorServiceTenant = new GlobalLocatorServiceTenant();
					globalLocatorServiceTenant.ExternalDirectoryOrganizationId = Guid.Parse(array3[0]);
					globalLocatorServiceTenant.ResourceForest = array3[1];
					globalLocatorServiceTenant.AccountForest = array3[2];
					globalLocatorServiceTenant.TenantContainerCN = array3[3];
					globalLocatorServiceTenant.SmtpNextHopDomain = new SmtpDomain(array3[4]);
					if (!Fqdn.IsValidFqdn(globalLocatorServiceTenant.ResourceForest))
					{
						throw new ArgumentException("ResourceForest");
					}
					if (!Fqdn.IsValidFqdn(globalLocatorServiceTenant.AccountForest))
					{
						throw new ArgumentException("AccountForest");
					}
					this.tenantOverrides.Add(globalLocatorServiceTenant);
				}
			}
			catch (Exception innerException)
			{
				throw new FormatException("Format: '[tenant guid]:[resource forest]:[account forest]:[tenant CN]:[SmtpNextHopDomain];[tenant guid]:[resource forest]:[account forest]:[tenant CN]:[SmtpNextHopDomain]' \r\nExample: '038ea23e-d787-4595-9257-e78aca022e2c:rf1.com:af1.com:tenant1.com:pod50002ehf.exchangelabs.live-int.com;0339d953-85e6-4dbc-b0cf-446464c4ebfc:rf2.com:af2.com:tenant2.com:pod50002ehf.exchangelabs.live-int.com'", innerException);
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0008AC08 File Offset: 0x00088E08
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (GlobalLocatorServiceTenant globalLocatorServiceTenant in this.tenantOverrides)
			{
				if (!flag)
				{
					stringBuilder.Append(';');
					flag = false;
				}
				stringBuilder.AppendFormat("{0}:{1}:{2}:{3}:{4}", new object[]
				{
					globalLocatorServiceTenant.ExternalDirectoryOrganizationId,
					globalLocatorServiceTenant.ResourceForest,
					globalLocatorServiceTenant.AccountForest,
					globalLocatorServiceTenant.TenantContainerCN,
					globalLocatorServiceTenant.SmtpNextHopDomain
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0008ACDC File Offset: 0x00088EDC
		public bool TryGetTenantOverride(Guid externalDirectoryOrganizationId, out GlobalLocatorServiceTenant tenantOverride)
		{
			tenantOverride = this.tenantOverrides.Find((GlobalLocatorServiceTenant tenant) => tenant.ExternalDirectoryOrganizationId == externalDirectoryOrganizationId);
			return tenantOverride != null;
		}

		// Token: 0x04001286 RID: 4742
		private const string formatError = "Format: '[tenant guid]:[resource forest]:[account forest]:[tenant CN]:[SmtpNextHopDomain];[tenant guid]:[resource forest]:[account forest]:[tenant CN]:[SmtpNextHopDomain]' \r\nExample: '038ea23e-d787-4595-9257-e78aca022e2c:rf1.com:af1.com:tenant1.com:pod50002ehf.exchangelabs.live-int.com;0339d953-85e6-4dbc-b0cf-446464c4ebfc:rf2.com:af2.com:tenant2.com:pod50002ehf.exchangelabs.live-int.com'";

		// Token: 0x04001287 RID: 4743
		internal readonly List<GlobalLocatorServiceTenant> tenantOverrides;
	}
}

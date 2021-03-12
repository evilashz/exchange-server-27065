using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel.Pickers;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000349 RID: 841
	public class RecipientObjectResolver : AdObjectResolver, IRecipientObjectResolver
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x000908ED File Offset: 0x0008EAED
		private RecipientObjectResolver()
		{
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000908F5 File Offset: 0x0008EAF5
		// (set) Token: 0x06002F66 RID: 12134 RVA: 0x000908FC File Offset: 0x0008EAFC
		internal static IRecipientObjectResolver Instance { get; set; } = new RecipientObjectResolver();

		// Token: 0x06002F67 RID: 12135 RVA: 0x00090914 File Offset: 0x0008EB14
		public IEnumerable<RecipientObjectResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<RecipientObjectResolverRow>(identities, RecipientObjectResolverRow.Properties, (ADRawEntry e) => new RecipientObjectResolverRow(e))
			orderby row.DisplayName
			select row;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0009097C File Offset: 0x0008EB7C
		public IEnumerable<PeopleRecipientObject> ResolvePeople(IEnumerable<ADObjectId> identities)
		{
			return from row in base.ResolveObjects<PeopleRecipientObject>(identities, PeopleRecipientObject.Properties, (ADRawEntry e) => new PeopleRecipientObject(e))
			orderby row.DisplayName
			select row;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000909E0 File Offset: 0x0008EBE0
		public IEnumerable<ADRecipient> ResolveProxyAddresses(IEnumerable<ProxyAddress> proxyAddresses)
		{
			if (proxyAddresses != null && proxyAddresses.Any<ProxyAddress>())
			{
				IRecipientSession recipientSession = (IRecipientSession)this.CreateAdSession();
				return from recipient in recipientSession.FindByProxyAddresses(proxyAddresses.ToArray<ProxyAddress>())
				select recipient.Data;
			}
			return null;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00090A4C File Offset: 0x0008EC4C
		public IEnumerable<ADRecipient> ResolveLegacyDNs(IEnumerable<string> legacyDNs)
		{
			if (legacyDNs != null && legacyDNs.Any<string>())
			{
				IRecipientSession recipientSession = (IRecipientSession)this.CreateAdSession();
				return from recipient in recipientSession.FindADRecipientsByLegacyExchangeDNs(legacyDNs.ToArray<string>())
				where recipient.Data != null
				select recipient.Data;
			}
			return null;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x00090AD4 File Offset: 0x0008ECD4
		public IEnumerable<ADRecipient> ResolveSmtpAddress(IEnumerable<string> addresses)
		{
			if (addresses != null && addresses.Any<string>())
			{
				return from recipient in this.ResolveProxyAddresses(from address in addresses
				select ProxyAddress.Parse(address))
				where recipient != null
				select recipient;
			}
			return null;
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x00090B47 File Offset: 0x0008ED47
		public IEnumerable<Identity> ResolveOrganizationUnitIdentity(IEnumerable<ADObjectId> identities)
		{
			return from row in identities
			select row.ToIdentity(row.ToCanonicalName());
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x00090D60 File Offset: 0x0008EF60
		public IEnumerable<RecipientObjectResolverRow> ResolveSmtpAddress(SmtpAddress[] smtpAddresses)
		{
			IEnumerable<ADRecipient> adRecipients = this.ResolveProxyAddresses(from address in smtpAddresses
			select ProxyAddress.Parse(address.ToString()));
			if (adRecipients != null)
			{
				foreach (ADRecipient adRecipient in adRecipients)
				{
					if (adRecipient != null)
					{
						yield return new RecipientObjectResolverRow(adRecipient);
					}
				}
			}
			yield break;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x00090D84 File Offset: 0x0008EF84
		public IEnumerable<AcePermissionRecipientRow> ResolveSecurityPrincipalId(IEnumerable<SecurityPrincipalIdParameter> sidPrincipalId)
		{
			if (sidPrincipalId != null && sidPrincipalId.Any<SecurityPrincipalIdParameter>())
			{
				IRecipientSession recipientSession = (IRecipientSession)this.CreateAdSession();
				List<AcePermissionRecipientRow> list = new List<AcePermissionRecipientRow>();
				foreach (SecurityPrincipalIdParameter securityPrincipalIdParameter in sidPrincipalId)
				{
					SecurityIdentifier securityIdentifier = securityPrincipalIdParameter.SecurityIdentifier;
					if (!securityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
					{
						MiniRecipient miniRecipient = recipientSession.FindMiniRecipientBySid<MiniRecipient>(securityIdentifier, AcePermissionRecipientRow.Properties.AsEnumerable<PropertyDefinition>());
						if (miniRecipient != null)
						{
							Identity identity;
							if (miniRecipient.MasterAccountSid == securityIdentifier)
							{
								identity = new Identity(miniRecipient.Guid.ToString(), securityPrincipalIdParameter.ToString());
							}
							else
							{
								identity = new Identity(miniRecipient.Guid.ToString(), string.IsNullOrEmpty(miniRecipient.DisplayName) ? miniRecipient.Name : miniRecipient.DisplayName);
							}
							list.Add(new AcePermissionRecipientRow(identity));
						}
					}
				}
				return list;
			}
			return new List<AcePermissionRecipientRow>();
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x00090EA4 File Offset: 0x0008F0A4
		public List<SecurityIdentifier> ConvertGuidsToSid(string[] rawGuids)
		{
			List<Guid> list = new List<Guid>();
			foreach (string input in rawGuids)
			{
				Guid guid;
				if (!Guid.TryParse(input, out guid))
				{
					throw new FaultException(string.Format("Guid {0} is invalid", guid));
				}
				list.Add(guid);
			}
			if (list != null && list.Any<Guid>())
			{
				IRecipientSession session = (IRecipientSession)this.CreateAdSession();
				List<SecurityIdentifier> list2 = new List<SecurityIdentifier>();
				foreach (Guid guid2 in list)
				{
					SecurityPrincipalIdParameter securityPrincipalIdParameter = new SecurityPrincipalIdParameter(guid2.ToString());
					IEnumerable<ADRecipient> objects = securityPrincipalIdParameter.GetObjects<ADRecipient>(null, session);
					using (IEnumerator<ADRecipient> enumerator2 = objects.GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							ADRecipient adrecipient = enumerator2.Current;
							list2.Add(adrecipient.MasterAccountSid ?? ((IADSecurityPrincipal)adrecipient).Sid);
							if (enumerator2.MoveNext())
							{
								throw new Exception(Strings.ErrorUserNotUnique(guid2.ToString()));
							}
						}
					}
				}
				return list2;
			}
			return new List<SecurityIdentifier>();
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x00091004 File Offset: 0x0008F204
		internal override IDirectorySession CreateAdSession()
		{
			IDirectorySession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, base.TenantSessionSetting, 499, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Pickers\\RecipientObjectResolver.cs");
			tenantOrRootOrgRecipientSession.SessionSettings.IncludeInactiveMailbox = true;
			return tenantOrRootOrgRecipientSession;
		}
	}
}

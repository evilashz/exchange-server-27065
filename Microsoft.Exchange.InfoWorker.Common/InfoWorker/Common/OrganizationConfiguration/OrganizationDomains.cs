using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.RecipientAPI;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x0200014C RID: 332
	internal class OrganizationDomains
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00027088 File Offset: 0x00025288
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x00027095 File Offset: 0x00025295
		public IList<Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain> AcceptedDomains
		{
			get
			{
				return this.acceptedDomains.AcceptedDomains;
			}
			internal set
			{
				this.acceptedDomains.AcceptedDomains = (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain[])value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000270A8 File Offset: 0x000252A8
		public IList<SmtpDomainWithSubdomains> InternalDomains
		{
			get
			{
				if (this.internalDomains == null || this.cookie != this.acceptedDomains.AcceptedDomains)
				{
					this.cookie = this.acceptedDomains.AcceptedDomains;
					this.BuildInternalDomainList();
				}
				return this.internalDomains;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000270E2 File Offset: 0x000252E2
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x000270EA File Offset: 0x000252EA
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x06000915 RID: 2325 RVA: 0x000270F3 File Offset: 0x000252F3
		private OrganizationDomains()
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000270FC File Offset: 0x000252FC
		public OrganizationDomains(OrganizationId organizationId)
		{
			this.OrganizationId = organizationId;
			this.acceptedDomains = new PerTenantAcceptedDomainCollection(organizationId);
			this.remoteDomains = new PerTenantRemoteDomainCollection(organizationId);
			this.isInternalResolver = new IsInternalResolver(organizationId, new IsInternalResolver.GetAcceptedDomainCollectionDelegate(this.GetAcceptedDomainCollection), new IsInternalResolver.GetRemoteDomainCollectionDelegate(this.GetRemoteDomainCollection));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00027154 File Offset: 0x00025354
		public OrganizationDomains(OrganizationId organizationId, TimeSpan timeoutInterval)
		{
			this.OrganizationId = organizationId;
			this.acceptedDomains = new PerTenantAcceptedDomainCollection(organizationId, timeoutInterval);
			this.remoteDomains = new PerTenantRemoteDomainCollection(organizationId, timeoutInterval);
			this.isInternalResolver = new IsInternalResolver(organizationId, new IsInternalResolver.GetAcceptedDomainCollectionDelegate(this.GetAcceptedDomainCollection), new IsInternalResolver.GetRemoteDomainCollectionDelegate(this.GetRemoteDomainCollection));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000271AC File Offset: 0x000253AC
		public OrganizationDomains(IList<SmtpDomainWithSubdomains> internalDomains)
		{
			this.acceptedDomains = new PerTenantAcceptedDomainCollection(OrganizationId.ForestWideOrgId);
			this.internalDomains = internalDomains;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000271CC File Offset: 0x000253CC
		public DomainType GetAcceptedDomainType(string domain)
		{
			bool flag;
			AcceptedDomainCollection acceptedDomainCollection = this.GetAcceptedDomainCollection(this.OrganizationId, out flag);
			AcceptedDomainEntry acceptedDomainEntry = (AcceptedDomainEntry)acceptedDomainCollection.Find(domain);
			if (acceptedDomainEntry == null)
			{
				return DomainType.External;
			}
			switch (acceptedDomainEntry.DomainType)
			{
			case AcceptedDomainType.Authoritative:
				return DomainType.Authoritative;
			case AcceptedDomainType.ExternalRelay:
				return DomainType.External;
			case AcceptedDomainType.InternalRelay:
				return DomainType.InternalRelay;
			default:
				return DomainType.Unknown;
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002721C File Offset: 0x0002541C
		public bool IsInternal(string domainName)
		{
			bool result;
			try
			{
				RoutingDomain domain = new RoutingDomain(domainName);
				result = this.isInternalResolver.IsInternal(domain);
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00027258 File Offset: 0x00025458
		public void Initialize()
		{
			this.acceptedDomains.Initialize();
			this.remoteDomains.Initialize();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00027270 File Offset: 0x00025470
		private void BuildInternalDomainList()
		{
			List<SmtpDomainWithSubdomains> list = new List<SmtpDomainWithSubdomains>(this.acceptedDomains.AcceptedDomains.Length);
			foreach (Microsoft.Exchange.Data.Directory.SystemConfiguration.AcceptedDomain acceptedDomain in this.acceptedDomains.AcceptedDomains)
			{
				if (acceptedDomain.DomainType != AcceptedDomainType.ExternalRelay)
				{
					list.Add(acceptedDomain.DomainName);
				}
			}
			this.internalDomains = list;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000272EC File Offset: 0x000254EC
		private AcceptedDomainCollection GetAcceptedDomainCollection(OrganizationId organizationId, out bool scopedToOrganization)
		{
			scopedToOrganization = false;
			if (organizationId != this.OrganizationId)
			{
				return null;
			}
			if (this.acceptedDomains.AcceptedDomains != null && 0 < this.acceptedDomains.AcceptedDomains.Length)
			{
				AcceptedDomainEntry[] acceptedDomainEntryList = (from domain in this.acceptedDomains.AcceptedDomains
				where domain != null
				select new AcceptedDomainEntry(domain, organizationId)).ToArray<AcceptedDomainEntry>();
				return new AcceptedDomainMap(acceptedDomainEntryList);
			}
			return new AcceptedDomainMap(null);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000273A4 File Offset: 0x000255A4
		private RemoteDomainCollection GetRemoteDomainCollection(OrganizationId organizationId)
		{
			if (this.remoteDomains.RemoteDomains != null && 0 < this.remoteDomains.RemoteDomains.Length)
			{
				RemoteDomainEntry[] remoteDomainEntryList = (from domain in this.remoteDomains.RemoteDomains
				where domain != null
				select new RemoteDomainEntry(domain)).ToArray<RemoteDomainEntry>();
				return new RemoteDomainMap(remoteDomainEntryList);
			}
			return new RemoteDomainMap(null);
		}

		// Token: 0x04000716 RID: 1814
		private PerTenantAcceptedDomainCollection acceptedDomains;

		// Token: 0x04000717 RID: 1815
		private PerTenantRemoteDomainCollection remoteDomains;

		// Token: 0x04000718 RID: 1816
		private object cookie;

		// Token: 0x04000719 RID: 1817
		private IList<SmtpDomainWithSubdomains> internalDomains;

		// Token: 0x0400071A RID: 1818
		private IsInternalResolver isInternalResolver;
	}
}

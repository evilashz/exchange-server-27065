using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DAB RID: 3499
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicUrlKey : SharingAnonymousIdentityCacheKey, IEquatable<PublicUrlKey>
	{
		// Token: 0x0600782D RID: 30765 RVA: 0x00212704 File Offset: 0x00210904
		internal PublicUrlKey(PublicUrl url) : base(PublicUrlKey.GetHashCode(url))
		{
			this.smtpAddress = url.SmtpAddress;
			this.urlId = url.Identity;
			this.filterBySmtpAddress = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, new SmtpProxyAddress(this.smtpAddress.ToString(), false).ToString());
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x00212764 File Offset: 0x00210964
		private static int GetHashCode(PublicUrl url)
		{
			Util.ThrowOnNullArgument(url, "url");
			return (url.SmtpAddress.ToString() + url.Identity).GetHashCode();
		}

		// Token: 0x0600782F RID: 30767 RVA: 0x002127A0 File Offset: 0x002109A0
		public override string Lookup(out SecurityIdentifier sid)
		{
			sid = null;
			ADSessionSettings sessionSettings;
			try
			{
				sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(this.smtpAddress.Domain);
			}
			catch (CannotResolveTenantNameException arg)
			{
				ExTraceGlobals.SharingTracer.TraceError<SmtpAddress, CannotResolveTenantNameException>((long)this.GetHashCode(), "PublicUrlKey.Lookup(): Cannot resolve tenant name for address {0}.Error: {1}", this.smtpAddress, arg);
				return null;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 79, "Lookup", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Sharing\\PublicUrlKey.cs");
			ADUser[] array = tenantOrRootOrgRecipientSession.FindADUser(null, QueryScope.SubTree, this.filterBySmtpAddress, null, 1);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.SharingTracer.TraceError<SmtpAddress>((long)this.GetHashCode(), "PublicUrlKey.Lookup(): Cannot find ADUser object for smtp address {0}.", this.smtpAddress);
				return null;
			}
			ADUser aduser = array[0];
			sid = aduser.Sid;
			string folder = aduser.SharingAnonymousIdentities.GetFolder(this.urlId);
			if (string.IsNullOrEmpty(folder))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<SmtpAddress, string>((long)this.GetHashCode(), "PublicUrlKey.Lookup(): Not found sharing anonymous identity for user {0} with url id {1}.", this.smtpAddress, this.urlId);
			}
			else
			{
				ExTraceGlobals.SharingTracer.TraceDebug<SmtpAddress, string, string>((long)this.GetHashCode(), "PublicUrlKey.Lookup(): Get sharing anonymous identity for user {0} with url id {1}: Folder Id = {2}.", this.smtpAddress, this.urlId, folder);
			}
			return folder;
		}

		// Token: 0x06007830 RID: 30768 RVA: 0x002128C8 File Offset: 0x00210AC8
		public bool Equals(PublicUrlKey other)
		{
			return !(other == null) && (object.ReferenceEquals(this, other) || (StringComparer.OrdinalIgnoreCase.Equals(this.urlId, other.urlId) && this.smtpAddress == other.smtpAddress));
		}

		// Token: 0x06007831 RID: 30769 RVA: 0x00212916 File Offset: 0x00210B16
		protected override bool InternalEquals(object obj)
		{
			return this.Equals(obj as PublicUrlKey);
		}

		// Token: 0x04005332 RID: 21298
		private readonly SmtpAddress smtpAddress;

		// Token: 0x04005333 RID: 21299
		private readonly string urlId;

		// Token: 0x04005334 RID: 21300
		private readonly QueryFilter filterBySmtpAddress;
	}
}

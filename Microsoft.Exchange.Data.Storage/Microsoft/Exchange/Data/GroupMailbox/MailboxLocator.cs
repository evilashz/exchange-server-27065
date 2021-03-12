using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007FF RID: 2047
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MailboxLocator : IMailboxLocator
	{
		// Token: 0x06004C56 RID: 19542 RVA: 0x0013C644 File Offset: 0x0013A844
		protected MailboxLocator(IRecipientSession adSession)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			this.adSession = adSession;
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0013C65E File Offset: 0x0013A85E
		protected MailboxLocator(IRecipientSession adSession, string externalDirectoryObjectId, string legacyDn) : this(adSession)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("legacyDn", legacyDn);
			this.externalDirectoryObjectId = externalDirectoryObjectId;
			this.legacyDn = legacyDn;
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06004C58 RID: 19544
		public abstract string LocatorType { get; }

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06004C59 RID: 19545 RVA: 0x0013C680 File Offset: 0x0013A880
		// (set) Token: 0x06004C5A RID: 19546 RVA: 0x0013C688 File Offset: 0x0013A888
		public string ExternalId
		{
			get
			{
				return this.externalDirectoryObjectId;
			}
			set
			{
				this.externalDirectoryObjectId = value;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06004C5B RID: 19547 RVA: 0x0013C691 File Offset: 0x0013A891
		// (set) Token: 0x06004C5C RID: 19548 RVA: 0x0013C699 File Offset: 0x0013A899
		public string LegacyDn
		{
			get
			{
				return this.legacyDn;
			}
			set
			{
				ArgumentValidator.ThrowIfNullOrWhiteSpace("value", value);
				this.legacyDn = value;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06004C5D RID: 19549 RVA: 0x0013C6B0 File Offset: 0x0013A8B0
		public Guid MailboxGuid
		{
			get
			{
				Guid result = Guid.Empty;
				if (this.adUser != null)
				{
					result = this.adUser.ExchangeGuid;
				}
				else
				{
					MailboxLocator.Tracer.TraceError((long)this.GetHashCode(), "MailboxLocator::get_MailboxGuid: adUser was null at the time");
				}
				return result;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06004C5E RID: 19550 RVA: 0x0013C6F0 File Offset: 0x0013A8F0
		public string IdentityHash
		{
			get
			{
				if (this.identityHash == null)
				{
					using (SHA1 sha = SHA1.Create())
					{
						string s = (this.legacyDn + this.externalDirectoryObjectId).ToLower();
						byte[] bytes = Encoding.Unicode.GetBytes(s);
						byte[] inArray = sha.ComputeHash(bytes);
						this.identityHash = Convert.ToBase64String(inArray);
					}
				}
				return this.identityHash;
			}
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0013C764 File Offset: 0x0013A964
		public ADUser FindAdUser()
		{
			if (this.adUser == null)
			{
				try
				{
					ADUser aduser = this.FindByLegacyDN();
					this.InitializeFromAd(aduser);
				}
				catch (NonUniqueRecipientException innerException)
				{
					throw new MailboxNotFoundException(ServerStrings.NonUniqueRecipientError, innerException);
				}
			}
			return this.adUser;
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0013C7C8 File Offset: 0x0013A9C8
		public string[] FindAlternateLegacyDNs()
		{
			return (from address in this.FindAdUser().EmailAddresses
			where address.Prefix == ProxyAddressPrefix.X500
			select address.AddressString).ToArray<string>();
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0013C829 File Offset: 0x0013AA29
		public virtual bool IsValidReplicationTarget()
		{
			throw new NotImplementedException("Replication to this locator is not yet supported");
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0013C838 File Offset: 0x0013AA38
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("{Type:\"");
			stringBuilder.Append(this.LocatorType);
			stringBuilder.Append("\", ExternalDirectoryObjectId:\"");
			stringBuilder.Append(this.ExternalId);
			stringBuilder.Append("\"}");
			return stringBuilder.ToString();
		}

		// Token: 0x06004C63 RID: 19555
		protected abstract bool IsValidAdUser(ADUser adUser);

		// Token: 0x06004C64 RID: 19556 RVA: 0x0013C894 File Offset: 0x0013AA94
		protected void InitializeFromAd(ProxyAddress proxyAddress)
		{
			ArgumentValidator.ThrowIfNull("smtpAddress", proxyAddress);
			ADUser aduser = this.adSession.FindByProxyAddress(proxyAddress) as ADUser;
			if (aduser == null)
			{
				throw new MailboxNotFoundException(ServerStrings.InvalidAddressError(proxyAddress.AddressString));
			}
			this.InitializeFromAd(aduser);
		}

		// Token: 0x06004C65 RID: 19557 RVA: 0x0013C8DC File Offset: 0x0013AADC
		protected void InitializeFromAd(ADUser adUser)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			if (!this.IsValidAdUser(adUser))
			{
				throw new MailboxWrongTypeException(adUser.ExternalDirectoryObjectId, adUser.RecipientTypeDetails.ToString());
			}
			this.adUser = adUser;
			this.externalDirectoryObjectId = adUser.ExternalDirectoryObjectId;
			this.legacyDn = adUser.LegacyExchangeDN;
		}

		// Token: 0x06004C66 RID: 19558 RVA: 0x0013C938 File Offset: 0x0013AB38
		private ADUser FindByLegacyDN()
		{
			MailboxLocator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MailboxLocator::FindByLegacyDN. Retrieving AD User by LegacyDN={0}", this.legacyDn);
			ADUser aduser = this.adSession.FindByLegacyExchangeDN(this.LegacyDn) as ADUser;
			if (aduser == null)
			{
				throw new MailboxNotFoundException(ServerStrings.InvalidAddressError(this.LegacyDn));
			}
			return aduser;
		}

		// Token: 0x040029B0 RID: 10672
		protected static readonly Trace Tracer = ExTraceGlobals.MailboxLocatorTracer;

		// Token: 0x040029B1 RID: 10673
		private readonly IRecipientSession adSession;

		// Token: 0x040029B2 RID: 10674
		private string externalDirectoryObjectId;

		// Token: 0x040029B3 RID: 10675
		private string legacyDn;

		// Token: 0x040029B4 RID: 10676
		private ADUser adUser;

		// Token: 0x040029B5 RID: 10677
		private string identityHash;
	}
}

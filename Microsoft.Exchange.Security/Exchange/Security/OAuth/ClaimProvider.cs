using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000AD RID: 173
	internal abstract class ClaimProvider
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x0002DADE File Offset: 0x0002BCDE
		protected ClaimProvider(OrganizationId organizationId)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002DAED File Offset: 0x0002BCED
		public static ClaimProvider Create(ADUser adUser)
		{
			return new ClaimProvider.ADUserClaimProvider(adUser);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002DAF5 File Offset: 0x0002BCF5
		public static ClaimProvider Create(MiniRecipient miniRecipient)
		{
			return new ClaimProvider.MiniRecipientClaimProvider(miniRecipient);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0002DAFD File Offset: 0x0002BCFD
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0002DB05 File Offset: 0x0002BD05
		public bool IsAllowedToIncludeNameId { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0002DB0E File Offset: 0x0002BD0E
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0002DB18 File Offset: 0x0002BD18
		public bool IncludeNameIdOnly
		{
			get
			{
				return this.includeNameIdOnly;
			}
			set
			{
				this.includeNameIdOnly = value;
				this.upnClaim = (this.smtpClaim = (this.sipClaim = null));
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002DB48 File Offset: 0x0002BD48
		public Dictionary<string, string> GetClaims()
		{
			if (AuthCommon.IsWindowsLiveIDEnabled)
			{
				this.FindNameIdClaimForOnline();
			}
			else
			{
				this.FindNameIdClaimForOnPremise();
			}
			if (!this.IncludeNameIdOnly)
			{
				if (AuthCommon.IsWindowsLiveIDEnabled)
				{
					this.FindUPNClaimForOnline();
				}
				else
				{
					this.FindUPNClaimForOnPremise();
				}
				this.FindSmtpClaim();
				this.FindSipClaim();
			}
			else if (string.IsNullOrEmpty(this.nameidClaim))
			{
				throw new OAuthTokenRequestFailedException(OAuthOutboundErrorCodes.EmptyNameIdClaim, null, null);
			}
			return this.GetUserClaimsHelper();
		}

		// Token: 0x060005FA RID: 1530
		protected abstract void FindNameIdClaimForOnPremise();

		// Token: 0x060005FB RID: 1531
		protected abstract void FindUPNClaimForOnPremise();

		// Token: 0x060005FC RID: 1532
		protected abstract void FindNameIdClaimForOnline();

		// Token: 0x060005FD RID: 1533
		protected abstract void FindUPNClaimForOnline();

		// Token: 0x060005FE RID: 1534
		protected abstract void FindSmtpClaim();

		// Token: 0x060005FF RID: 1535
		protected abstract void FindSipClaim();

		// Token: 0x06000600 RID: 1536 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
		private Dictionary<string, string> GetUserClaimsHelper()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(5);
			if (!string.IsNullOrEmpty(this.nameidClaim) && this.IsAllowedToIncludeNameId)
			{
				dictionary.Add(Constants.ClaimTypes.NameIdentifier, this.nameidClaim);
			}
			if (!string.IsNullOrEmpty(this.upnClaim))
			{
				dictionary.Add(Constants.ClaimTypes.Upn, this.upnClaim);
			}
			if (!string.IsNullOrEmpty(this.smtpClaim))
			{
				dictionary.Add(Constants.ClaimTypes.Smtp, this.smtpClaim);
			}
			if (!string.IsNullOrEmpty(this.sipClaim))
			{
				dictionary.Add(Constants.ClaimTypes.Sip, this.sipClaim);
			}
			if (dictionary.Count == 0)
			{
				throw new OAuthTokenRequestFailedException(OAuthOutboundErrorCodes.EmptyClaimsForUser, null, null);
			}
			if (!string.IsNullOrEmpty(this.nameidClaim) && this.IsAllowedToIncludeNameId)
			{
				dictionary.Add(Constants.ClaimTypes.Nii, this.niiClaim);
			}
			return dictionary;
		}

		// Token: 0x040005BF RID: 1471
		protected readonly OrganizationId organizationId;

		// Token: 0x040005C0 RID: 1472
		protected string niiClaim;

		// Token: 0x040005C1 RID: 1473
		protected string nameidClaim;

		// Token: 0x040005C2 RID: 1474
		protected string upnClaim;

		// Token: 0x040005C3 RID: 1475
		protected string smtpClaim;

		// Token: 0x040005C4 RID: 1476
		protected string sipClaim;

		// Token: 0x040005C5 RID: 1477
		private bool includeNameIdOnly;

		// Token: 0x020000AE RID: 174
		private class ADUserClaimProvider : ClaimProvider
		{
			// Token: 0x06000601 RID: 1537 RVA: 0x0002DC81 File Offset: 0x0002BE81
			public ADUserClaimProvider(ADUser user) : base(user.OrganizationId)
			{
				this.adUser = user;
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x0002DC98 File Offset: 0x0002BE98
			protected override void FindNameIdClaimForOnPremise()
			{
				SecurityIdentifier securityIdentifier = this.adUser.IsLinked ? this.adUser.MasterAccountSid : this.adUser.Sid;
				if (securityIdentifier != null)
				{
					this.niiClaim = Constants.NiiClaimValues.ActiveDirectory;
					this.nameidClaim = securityIdentifier.ToString();
				}
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x0002DCEC File Offset: 0x0002BEEC
			protected override void FindNameIdClaimForOnline()
			{
				SmtpAddress windowsLiveID = this.adUser.WindowsLiveID;
				if (windowsLiveID == SmtpAddress.Empty)
				{
					return;
				}
				LiveIdInstanceType? liveIdInstanceType = DomainPropertyCache.Singleton.Get(new SmtpDomainWithSubdomains(windowsLiveID.Domain)).LiveIdInstanceType;
				if (liveIdInstanceType != null && liveIdInstanceType.Value == LiveIdInstanceType.Business)
				{
					this.niiClaim = Constants.NiiClaimValues.BusinessLiveId;
					this.nameidClaim = this.adUser.NetID.ToString();
				}
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0002DD63 File Offset: 0x0002BF63
			protected override void FindUPNClaimForOnPremise()
			{
				this.upnClaim = this.adUser.UserPrincipalName;
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0002DD78 File Offset: 0x0002BF78
			protected override void FindUPNClaimForOnline()
			{
				SmtpAddress windowsLiveID = this.adUser.WindowsLiveID;
				this.upnClaim = this.adUser.WindowsLiveID.ToString();
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
			protected override void FindSmtpClaim()
			{
				SmtpAddress primarySmtpAddress = this.adUser.PrimarySmtpAddress;
				if (primarySmtpAddress != SmtpAddress.Empty)
				{
					this.smtpClaim = primarySmtpAddress.ToString();
				}
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0002DDFC File Offset: 0x0002BFFC
			protected override void FindSipClaim()
			{
				ProxyAddressCollection emailAddresses = this.adUser.EmailAddresses;
				if (emailAddresses != null)
				{
					ProxyAddress proxyAddress = emailAddresses.Find((ProxyAddress p) => p.Prefix == ProxyAddressPrefix.SIP);
					if (proxyAddress != null)
					{
						this.sipClaim = proxyAddress.AddressString;
					}
				}
			}

			// Token: 0x040005C7 RID: 1479
			private readonly ADUser adUser;
		}

		// Token: 0x020000AF RID: 175
		private class MiniRecipientClaimProvider : ClaimProvider
		{
			// Token: 0x06000609 RID: 1545 RVA: 0x0002DE51 File Offset: 0x0002C051
			public MiniRecipientClaimProvider(MiniRecipient miniRecipient) : base(miniRecipient.OrganizationId)
			{
				this.miniRecipient = miniRecipient;
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0002DE68 File Offset: 0x0002C068
			protected override void FindNameIdClaimForOnPremise()
			{
				if (this.miniRecipient.MasterAccountSid != null)
				{
					this.niiClaim = Constants.NiiClaimValues.ActiveDirectory;
					this.nameidClaim = this.miniRecipient.MasterAccountSid.ToString();
					return;
				}
				if (this.miniRecipient.Sid != null)
				{
					this.niiClaim = Constants.NiiClaimValues.ActiveDirectory;
					this.nameidClaim = this.miniRecipient.Sid.ToString();
				}
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0002DEE0 File Offset: 0x0002C0E0
			protected override void FindNameIdClaimForOnline()
			{
				SmtpAddress windowsLiveID = this.miniRecipient.WindowsLiveID;
				if (windowsLiveID == SmtpAddress.Empty)
				{
					return;
				}
				LiveIdInstanceType? liveIdInstanceType = DomainPropertyCache.Singleton.Get(new SmtpDomainWithSubdomains(windowsLiveID.Domain)).LiveIdInstanceType;
				if (liveIdInstanceType != null && liveIdInstanceType.Value == LiveIdInstanceType.Business)
				{
					this.niiClaim = Constants.NiiClaimValues.BusinessLiveId;
					this.nameidClaim = this.miniRecipient.NetID.ToString();
				}
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x0002DF57 File Offset: 0x0002C157
			protected override void FindUPNClaimForOnPremise()
			{
				this.upnClaim = this.miniRecipient.UserPrincipalName;
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0002DF6C File Offset: 0x0002C16C
			protected override void FindUPNClaimForOnline()
			{
				SmtpAddress windowsLiveID = this.miniRecipient.WindowsLiveID;
				if (windowsLiveID != SmtpAddress.Empty)
				{
					this.upnClaim = windowsLiveID.ToString();
				}
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x0002DFA8 File Offset: 0x0002C1A8
			protected override void FindSmtpClaim()
			{
				this.smtpClaim = this.miniRecipient.PrimarySmtpAddress.ToString();
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
			protected override void FindSipClaim()
			{
			}

			// Token: 0x040005C9 RID: 1481
			private readonly MiniRecipient miniRecipient;
		}
	}
}

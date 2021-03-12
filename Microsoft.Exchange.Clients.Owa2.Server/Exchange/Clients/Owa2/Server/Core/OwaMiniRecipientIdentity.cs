using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A3 RID: 163
	public sealed class OwaMiniRecipientIdentity : OwaIdentity
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x00013A00 File Offset: 0x00011C00
		private OwaMiniRecipientIdentity(OWAMiniRecipient owaMiniRecipient)
		{
			base.OwaMiniRecipient = owaMiniRecipient;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00013A0F File Offset: 0x00011C0F
		private OwaMiniRecipientIdentity(ProxyAddress proxyAddress)
		{
			this.proxyAddress = proxyAddress;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00013A1E File Offset: 0x00011C1E
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00013A21 File Offset: 0x00011C21
		public override SecurityIdentifier UserSid
		{
			get
			{
				return base.OwaMiniRecipient.Sid;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00013A2E File Offset: 0x00011C2E
		public override string UniqueId
		{
			get
			{
				return this.proxyAddress.ToString();
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00013A3B File Offset: 0x00011C3B
		public override string AuthenticationType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00013A3E File Offset: 0x00011C3E
		public ProxyAddress ProxyAddress
		{
			get
			{
				return this.proxyAddress;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00013A46 File Offset: 0x00011C46
		public override bool IsPartial
		{
			get
			{
				return base.OwaMiniRecipient == null;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00013A51 File Offset: 0x00011C51
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00013A54 File Offset: 0x00011C54
		public static OwaMiniRecipientIdentity CreateFromOWAMiniRecipient(OWAMiniRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<SecurityIdentifier>(0L, "OwaMiniRecipientIdentity.CreateFromOWAMiniRecipient for recipient with Sid:{0}", recipient.Sid);
			return new OwaMiniRecipientIdentity(recipient);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00013A84 File Offset: 0x00011C84
		public static OwaMiniRecipientIdentity CreateFromProxyAddress(string emailString)
		{
			if (emailString == null)
			{
				throw new ArgumentNullException("emailString");
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "OwaMiniRecipientIdentity.CreateFromProxyAddress for emailString:{0}", emailString);
			ProxyAddress proxyAddress = null;
			try
			{
				proxyAddress = ProxyAddress.Parse(emailString);
			}
			catch (ArgumentNullException)
			{
				proxyAddress = null;
			}
			if (proxyAddress == null || proxyAddress.GetType() != typeof(SmtpProxyAddress))
			{
				throw new OwaExplicitLogonException(string.Format("{0} is not a valid SMTP address", emailString), string.Format(Strings.GetLocalizedString(-13616305), emailString));
			}
			return new OwaMiniRecipientIdentity(proxyAddress);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00013B18 File Offset: 0x00011D18
		public override string SafeGetRenderableName()
		{
			return this.proxyAddress.ToString();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00013B28 File Offset: 0x00011D28
		public override bool IsEqualsTo(OwaIdentity otherIdentity)
		{
			if (otherIdentity == null)
			{
				return false;
			}
			OwaMiniRecipientIdentity owaMiniRecipientIdentity = otherIdentity as OwaMiniRecipientIdentity;
			if (owaMiniRecipientIdentity == null)
			{
				throw new OwaInvalidOperationException("Comparing OwaMiniRecipientIdentity with identities of another type is not supported");
			}
			return owaMiniRecipientIdentity.ProxyAddress == this.proxyAddress;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00013B60 File Offset: 0x00011D60
		public override string GetLogonName()
		{
			return null;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00013B64 File Offset: 0x00011D64
		public void UpgradePartialIdentity()
		{
			OWAMiniRecipient owaminiRecipient = null;
			SmtpAddress smtpAddress = new SmtpAddress(this.proxyAddress.AddressString);
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "OwaMiniRecipientIdentity.UpgradePartialIdentity for smtp: {0}", this.proxyAddress.AddressString);
			IRecipientSession recipientSession = UserContextUtilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, smtpAddress.Domain, null);
			Exception ex = null;
			try
			{
				owaminiRecipient = recipientSession.FindMiniRecipientByProxyAddress<OWAMiniRecipient>(this.proxyAddress, OWAMiniRecipientSchema.AdditionalProperties);
				if (owaminiRecipient == null)
				{
					throw new OwaExplicitLogonException(string.Format("The address {0} is an object in AD database but it is not an user", this.proxyAddress), Strings.GetLocalizedString(-1332692688), ex);
				}
			}
			catch (NonUniqueRecipientException ex2)
			{
				ex = ex2;
			}
			if (owaminiRecipient == null || ex != null)
			{
				throw new OwaExplicitLogonException(string.Format("Couldn't find a match for {0}", this.proxyAddress.ToString()), string.Format(Strings.GetLocalizedString(-13616305), this.proxyAddress), ex);
			}
			base.OwaMiniRecipient = owaminiRecipient;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00013C40 File Offset: 0x00011E40
		internal override MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return null;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00013C43 File Offset: 0x00011E43
		internal override MailboxSession CreateInstantSearchMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return null;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00013C46 File Offset: 0x00011E46
		internal override MailboxSession CreateDelegateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00013C49 File Offset: 0x00011E49
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaMiniRecipientIdentity.InternalCreateExchangePrincipal");
			return ExchangePrincipal.FromMiniRecipient(base.OwaMiniRecipient);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00013C67 File Offset: 0x00011E67
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaMiniRecipientIdentity>(this);
		}

		// Token: 0x0400038D RID: 909
		private ProxyAddress proxyAddress;
	}
}

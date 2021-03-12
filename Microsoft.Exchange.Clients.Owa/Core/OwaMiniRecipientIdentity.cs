using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001E9 RID: 489
	public sealed class OwaMiniRecipientIdentity : OwaIdentity
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x000627B8 File Offset: 0x000609B8
		private OwaMiniRecipientIdentity(OWAMiniRecipient owaMiniRecipient)
		{
			this.owaMiniRecipient = owaMiniRecipient;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000627C7 File Offset: 0x000609C7
		private OwaMiniRecipientIdentity(ProxyAddress proxyAddress)
		{
			this.proxyAddress = proxyAddress;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000627D6 File Offset: 0x000609D6
		public static OwaMiniRecipientIdentity CreateFromOWAMiniRecipient(OWAMiniRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("user", "AD User cannot be null");
			}
			return new OwaMiniRecipientIdentity(recipient);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000627F4 File Offset: 0x000609F4
		public static OwaMiniRecipientIdentity CreateFromProxyAddress(string emailString)
		{
			if (emailString == null)
			{
				throw new ArgumentNullException("emailString");
			}
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
				throw new OwaExplicitLogonException(string.Format("{0} is not a valid SMTP address", emailString), string.Format(LocalizedStrings.GetNonEncoded(-13616305), emailString));
			}
			return new OwaMiniRecipientIdentity(proxyAddress);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00062878 File Offset: 0x00060A78
		public void UpgradePartialIdentity()
		{
			OWAMiniRecipient owaminiRecipient = null;
			SmtpAddress smtpAddress = new SmtpAddress(this.proxyAddress.AddressString);
			IRecipientSession recipientSession = Utilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, smtpAddress.Domain);
			Exception ex = null;
			try
			{
				owaminiRecipient = recipientSession.FindMiniRecipientByProxyAddress<OWAMiniRecipient>(this.proxyAddress, OWAMiniRecipientSchema.AdditionalProperties);
				if (owaminiRecipient == null)
				{
					throw new OwaExplicitLogonException(string.Format("The address {0} is an object in AD database but it is not an user", this.proxyAddress), LocalizedStrings.GetNonEncoded(-1332692688), ex);
				}
			}
			catch (NonUniqueRecipientException ex2)
			{
				ex = ex2;
			}
			base.LastRecipientSessionDCServerName = recipientSession.LastUsedDc;
			if (owaminiRecipient == null || ex != null)
			{
				throw new OwaExplicitLogonException(string.Format("Couldn't find a match for {0}", this.proxyAddress.ToString()), string.Format(LocalizedStrings.GetNonEncoded(-13616305), this.proxyAddress), ex);
			}
			this.owaMiniRecipient = owaminiRecipient;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00062944 File Offset: 0x00060B44
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				base.ThrowNotSupported("WindowsIdentity");
				return null;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00062952 File Offset: 0x00060B52
		public override SecurityIdentifier UserSid
		{
			get
			{
				return this.owaMiniRecipient.Sid;
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0006295F File Offset: 0x00060B5F
		public override string GetLogonName()
		{
			base.ThrowNotSupported("LogonName");
			return null;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0006296D File Offset: 0x00060B6D
		public override string SafeGetRenderableName()
		{
			return this.proxyAddress.ToString();
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0006297A File Offset: 0x00060B7A
		public override string UniqueId
		{
			get
			{
				return this.proxyAddress.ToString();
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00062987 File Offset: 0x00060B87
		public override string AuthenticationType
		{
			get
			{
				base.ThrowNotSupported("AuthenticationType");
				return null;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00062995 File Offset: 0x00060B95
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				base.ThrowNotSupported("ClientSecurityContext");
				return null;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000629A3 File Offset: 0x00060BA3
		public override bool IsPartial
		{
			get
			{
				return this.owaMiniRecipient == null;
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000629AE File Offset: 0x00060BAE
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			return ExchangePrincipal.FromMiniRecipient(this.owaMiniRecipient);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000629BB File Offset: 0x00060BBB
		internal override UncSession CreateUncSession(DocumentLibraryObjectId objectId)
		{
			base.ThrowNotSupported("CreateUncSession");
			return null;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000629C9 File Offset: 0x00060BC9
		internal override SharepointSession CreateSharepointSession(DocumentLibraryObjectId objectId)
		{
			base.ThrowNotSupported("CreateSharepointSession");
			return null;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000629D7 File Offset: 0x00060BD7
		internal override MailboxSession CreateMailboxSession(IExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			base.ThrowNotSupported("CreateMailboxSession");
			return null;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000629E5 File Offset: 0x00060BE5
		internal override MailboxSession CreateWebPartMailboxSession(IExchangePrincipal mailBoxExchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest)
		{
			base.ThrowNotSupported("CreateWebPartMailboxSession");
			return null;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000629F4 File Offset: 0x00060BF4
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

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00062A2C File Offset: 0x00060C2C
		public ProxyAddress ProxyAddress
		{
			get
			{
				return this.proxyAddress;
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00062A34 File Offset: 0x00060C34
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaMiniRecipientIdentity>(this);
		}

		// Token: 0x04000AA5 RID: 2725
		private ProxyAddress proxyAddress;
	}
}

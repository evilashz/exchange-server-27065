using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000174 RID: 372
	public abstract class OwaIdentity : DisposeTrackableBase
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0005870A File Offset: 0x0005690A
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00058712 File Offset: 0x00056912
		internal string LastRecipientSessionDCServerName { get; set; }

		// Token: 0x06000D15 RID: 3349 RVA: 0x0005871C File Offset: 0x0005691C
		internal static OwaIdentity CreateOwaIdentityFromSmtpAddress(OwaIdentity logonIdentity, string smtpAddress, out ExchangePrincipal logonExchangePrincipal, out bool isExplicitLogon, out bool isAlternateMailbox)
		{
			OwaIdentity owaIdentity = null;
			isAlternateMailbox = false;
			isExplicitLogon = false;
			logonExchangePrincipal = null;
			try
			{
				logonExchangePrincipal = logonIdentity.CreateExchangePrincipal();
				Guid? alternateMailbox = OwaAlternateMailboxIdentity.GetAlternateMailbox(logonExchangePrincipal, smtpAddress);
				if (alternateMailbox != null)
				{
					owaIdentity = OwaAlternateMailboxIdentity.Create(logonIdentity, logonExchangePrincipal, alternateMailbox.Value);
					isAlternateMailbox = true;
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "The request is under alternate mailbox: {0}", smtpAddress);
				}
				else
				{
					isExplicitLogon = true;
				}
			}
			catch (AdUserNotFoundException)
			{
				isExplicitLogon = true;
			}
			catch (UserHasNoMailboxException)
			{
				isExplicitLogon = true;
			}
			if (isExplicitLogon)
			{
				if (owaIdentity != null)
				{
					owaIdentity.Dispose();
					owaIdentity = null;
				}
				owaIdentity = OwaMiniRecipientIdentity.CreateFromProxyAddress(smtpAddress);
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "The request is under explicit logon: {0}", smtpAddress);
			}
			return owaIdentity;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x000587D0 File Offset: 0x000569D0
		public virtual OrganizationProperties UserOrganizationProperties
		{
			get
			{
				if (this.userOrganizationProperties == null)
				{
					OWAMiniRecipient owaminiRecipient = this.GetOWAMiniRecipient();
					if (!OrganizationPropertyCache.TryGetOrganizationProperties(owaminiRecipient.OrganizationId, out this.userOrganizationProperties))
					{
						throw new OwaADObjectNotFoundException("The organization does not exist in AD. OrgId:" + owaminiRecipient.OrganizationId);
					}
				}
				return this.userOrganizationProperties;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000D17 RID: 3351
		public abstract WindowsIdentity WindowsIdentity { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000D18 RID: 3352
		public abstract SecurityIdentifier UserSid { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000D19 RID: 3353
		public abstract string AuthenticationType { get; }

		// Token: 0x06000D1A RID: 3354
		public abstract string GetLogonName();

		// Token: 0x06000D1B RID: 3355
		public abstract string SafeGetRenderableName();

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000D1C RID: 3356
		public abstract string UniqueId { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000D1D RID: 3357
		internal abstract ClientSecurityContext ClientSecurityContext { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000D1E RID: 3358
		public abstract bool IsPartial { get; }

		// Token: 0x06000D1F RID: 3359
		internal abstract ExchangePrincipal InternalCreateExchangePrincipal();

		// Token: 0x06000D20 RID: 3360
		internal abstract MailboxSession CreateMailboxSession(IExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest);

		// Token: 0x06000D21 RID: 3361
		internal abstract MailboxSession CreateWebPartMailboxSession(IExchangePrincipal mailBoxExchangePrincipal, CultureInfo cultureInfo, HttpRequest clientRequest);

		// Token: 0x06000D22 RID: 3362
		internal abstract UncSession CreateUncSession(DocumentLibraryObjectId objectId);

		// Token: 0x06000D23 RID: 3363
		internal abstract SharepointSession CreateSharepointSession(DocumentLibraryObjectId objectId);

		// Token: 0x06000D24 RID: 3364 RVA: 0x0005881B File Offset: 0x00056A1B
		public virtual void Refresh(OwaIdentity identity)
		{
			if (identity.GetType() != base.GetType())
			{
				throw new OwaInvalidOperationException(string.Format("Type of passed in identity does not match current identity.  Expected {0} but got {1}.", base.GetType(), identity.GetType()));
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0005884C File Offset: 0x00056A4C
		public virtual SmtpAddress PrimarySmtpAddress
		{
			get
			{
				if (this.owaMiniRecipient == null)
				{
					return default(SmtpAddress);
				}
				return this.owaMiniRecipient.PrimarySmtpAddress;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00058878 File Offset: 0x00056A78
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x000588E1 File Offset: 0x00056AE1
		public virtual string DomainName
		{
			get
			{
				if (string.IsNullOrEmpty(this.domainName))
				{
					string text = this.SafeGetRenderableName();
					if (!string.IsNullOrEmpty(text))
					{
						int num = text.IndexOf('\\');
						if (num > 0)
						{
							return text.Substring(0, num);
						}
						if (text.IndexOf('@') >= 0)
						{
							SmtpAddress smtpAddress = new SmtpAddress(text);
							if (smtpAddress.IsValidAddress)
							{
								return smtpAddress.Domain;
							}
						}
					}
				}
				return this.domainName;
			}
			set
			{
				this.domainName = value;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000588EA File Offset: 0x00056AEA
		public virtual bool IsEqualsTo(OwaIdentity otherIdentity)
		{
			return otherIdentity != null && otherIdentity.UserSid.Equals(this.UserSid);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00058902 File Offset: 0x00056B02
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00058904 File Offset: 0x00056B04
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaIdentity>(this);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0005890C File Offset: 0x00056B0C
		internal ExchangePrincipal CreateExchangePrincipal()
		{
			ExchangePrincipal result = null;
			try
			{
				OwaDiagnostics.TracePfd(18057, "Creating new ExchangePrincipal", new object[0]);
				if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					string text = null;
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						text = current.Name;
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "<n/a>";
					}
					string arg = this.SafeGetRenderableName();
					ExTraceGlobals.CoreTracer.TraceDebug<string, string>(0L, "Using accout {0} to bind to ExchangePrincipal object for user {1}", text, arg);
				}
				result = this.InternalCreateExchangePrincipal();
			}
			catch (ObjectNotFoundException ex)
			{
				bool flag = false;
				DataValidationException ex2 = ex.InnerException as DataValidationException;
				if (ex2 != null)
				{
					PropertyValidationError propertyValidationError = ex2.Error as PropertyValidationError;
					if (propertyValidationError != null && propertyValidationError.PropertyDefinition == MiniRecipientSchema.Languages)
					{
						OWAMiniRecipient owaminiRecipient = this.FixCorruptOWAMiniRecipientCultureEntry();
						if (owaminiRecipient != null)
						{
							try
							{
								result = ExchangePrincipal.FromMiniRecipient(owaminiRecipient, RemotingOptions.AllowCrossSite);
								flag = true;
							}
							catch (ObjectNotFoundException)
							{
							}
						}
					}
				}
				if (!flag)
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00058A10 File Offset: 0x00056C10
		internal OWAMiniRecipient FixCorruptOWAMiniRecipientCultureEntry()
		{
			if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "User {0} has corrupt culture, setting client culture to empty", this.SafeGetRenderableName());
			}
			IRecipientSession recipientSession = Utilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, this.DomainName);
			ADUser aduser = recipientSession.FindBySid(this.UserSid) as ADUser;
			this.LastRecipientSessionDCServerName = recipientSession.LastUsedDc;
			if (aduser != null)
			{
				aduser.Languages = new MultiValuedProperty<CultureInfo>();
				if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Saving culture for User {0}, setting client culture to empty", this.SafeGetRenderableName());
				}
				recipientSession.Save(aduser);
				return recipientSession.FindMiniRecipientBySid<OWAMiniRecipient>(this.UserSid, OWAMiniRecipientSchema.AdditionalProperties);
			}
			return null;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00058AC0 File Offset: 0x00056CC0
		protected internal void ThrowNotSupported(string methodName)
		{
			string message = string.Format("This type of identity ({0}) doesn't support {1}", base.GetType().ToString(), methodName);
			throw new OwaNotSupportedException(message);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00058AEC File Offset: 0x00056CEC
		public ADRecipient CreateADRecipientBySid()
		{
			IRecipientSession recipientSession = Utilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, this.DomainName);
			ADRecipient adrecipient = recipientSession.FindBySid(this.UserSid);
			this.LastRecipientSessionDCServerName = recipientSession.LastUsedDc;
			if (adrecipient == null)
			{
				throw new OwaADObjectNotFoundException();
			}
			return adrecipient;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00058B2A File Offset: 0x00056D2A
		public OWAMiniRecipient GetOWAMiniRecipient()
		{
			if (this.owaMiniRecipient == null)
			{
				this.owaMiniRecipient = this.CreateOWAMiniRecipientBySid();
			}
			return this.owaMiniRecipient;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00058B48 File Offset: 0x00056D48
		public OWAMiniRecipient CreateOWAMiniRecipientBySid()
		{
			IRecipientSession recipientSession = Utilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, this.DomainName);
			OWAMiniRecipient owaminiRecipient = recipientSession.FindMiniRecipientBySid<OWAMiniRecipient>(this.UserSid, OWAMiniRecipientSchema.AdditionalProperties);
			this.LastRecipientSessionDCServerName = recipientSession.LastUsedDc;
			if (owaminiRecipient == null)
			{
				throw new OwaADObjectNotFoundException();
			}
			return owaminiRecipient;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00058B8B File Offset: 0x00056D8B
		public bool IsCrossForest(SecurityIdentifier masterAccountSid)
		{
			return this.UserSid != null && this.UserSid.Equals(masterAccountSid);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00058BAC File Offset: 0x00056DAC
		protected ADUser CreateADUserBySid()
		{
			ADUser aduser = this.CreateADRecipientBySid() as ADUser;
			if (aduser == null)
			{
				throw new OwaExplicitLogonException(string.Format("The SID {0} is an object in AD database but it is not an user", this.UserSid), LocalizedStrings.GetNonEncoded(-1332692688), null);
			}
			return aduser;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00058BEC File Offset: 0x00056DEC
		internal IADOrgPerson CreateADOrgPersonForWebPartUserBySid()
		{
			IADOrgPerson iadorgPerson = this.CreateADRecipientBySid() as IADOrgPerson;
			if (iadorgPerson == null)
			{
				throw new OwaExplicitLogonException(string.Format("The SID {0} is an object in AD database but it is not an ADOrgPerson, which is required for web part delegate access", this.UserSid), LocalizedStrings.GetNonEncoded(-1332692688), null);
			}
			return iadorgPerson;
		}

		// Token: 0x0400091B RID: 2331
		protected OWAMiniRecipient owaMiniRecipient;

		// Token: 0x0400091C RID: 2332
		protected OrganizationProperties userOrganizationProperties;

		// Token: 0x0400091D RID: 2333
		protected string domainName;
	}
}

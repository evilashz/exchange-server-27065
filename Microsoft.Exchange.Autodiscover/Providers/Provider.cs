using System;
using System.Globalization;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover.Providers
{
	// Token: 0x02000018 RID: 24
	internal abstract class Provider
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000054D7 File Offset: 0x000036D7
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000054DF File Offset: 0x000036DF
		private protected RequestData RequestData { protected get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000054E8 File Offset: 0x000036E8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000054F0 File Offset: 0x000036F0
		private protected ProviderAttribute[] Attributes { protected get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000054F9 File Offset: 0x000036F9
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005501 File Offset: 0x00003701
		private protected ADRecipient Caller { protected get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000550A File Offset: 0x0000370A
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005512 File Offset: 0x00003712
		private protected ADRecipient RequestedRecipient { protected get; private set; }

		// Token: 0x060000BE RID: 190 RVA: 0x0000551C File Offset: 0x0000371C
		protected Provider(RequestData requestData)
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug((long)this.GetHashCode(), "[Provider.base()] Timestamp=\"{0}\";CompterNameHash=\"{1}\";EmailAddress=\"{2}\";LegacyDN=\"{3}\";UserSID=\"{4}\";", new object[]
			{
				requestData.Timestamp,
				requestData.ComputerNameHash,
				requestData.EMailAddress,
				requestData.LegacyDN,
				requestData.User.Identity.GetSecurityIdentifier()
			});
			this.RequestData = requestData;
			Type type = base.GetType();
			Type typeFromHandle = typeof(ProviderAttribute);
			this.Attributes = (ProviderAttribute[])type.GetCustomAttributes(typeFromHandle, false);
			this.Caller = (HttpContext.Current.Items["CallerRecipient"] as ADRecipient);
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("Caller", (this.Caller == null) ? "null" : this.Caller.PrimarySmtpAddress.ToString());
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005606 File Offset: 0x00003806
		public virtual void GenerateResponseXml(XmlWriter xmlFragment)
		{
			if (!this.WriteRedirectXml(xmlFragment))
			{
				ExTraceGlobals.FrameworkTracer.TraceDebug((long)this.GetHashCode(), "[base.GenerateResponseXml()] 'redirectOrError=false; 'Calling provider's WriteConfigXml()'");
				this.WriteConfigXml(xmlFragment);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000562E File Offset: 0x0000382E
		public virtual string Get302RedirectUrl()
		{
			return null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005634 File Offset: 0x00003834
		protected virtual bool WriteRedirectXml(XmlWriter xmlFragment)
		{
			string ns = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006";
			Common.StartEnvelope(xmlFragment);
			xmlFragment.WriteStartElement("Response", ns);
			xmlFragment.WriteStartElement("User", ns);
			xmlFragment.WriteElementString("DisplayName", ns, "John Doe");
			xmlFragment.WriteEndElement();
			xmlFragment.WriteStartElement("Account", ns);
			xmlFragment.WriteElementString("AccountType", ns, "email");
			xmlFragment.WriteElementString("Action", ns, "redirect");
			xmlFragment.WriteElementString("RedirectURL", ns, "http://autodiscover.redirect.com");
			RequestDetailsLoggerBase<RequestDetailsLogger>.Current.SetRedirectionType(RedirectionType.UrlRedirect);
			xmlFragment.WriteEndElement();
			xmlFragment.WriteEndElement();
			Common.EndEnvelope(xmlFragment);
			ExTraceGlobals.FrameworkTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[base.WriteRedirectXml()] redirectUrl=\"{0}\";Assembly=\"{1}\"", "http://autodiscover.redirect.com", base.GetType().AssemblyQualifiedName);
			Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_InfoProvRedirectionResponse, Common.PeriodicKey, new object[]
			{
				"http://autodiscover.redirect.com",
				this.RequestData.EMailAddress,
				this.RequestData.LegacyDN,
				base.GetType().AssemblyQualifiedName
			});
			return true;
		}

		// Token: 0x060000C2 RID: 194
		protected abstract void WriteConfigXml(XmlWriter xmlFragment);

		// Token: 0x060000C3 RID: 195 RVA: 0x00005908 File Offset: 0x00003B08
		protected void ResolveRequestedADRecipient()
		{
			string resolveMethod = "Unknown";
			try
			{
				if (this.Caller != null)
				{
					if (!string.IsNullOrEmpty(this.RequestData.LegacyDN) && string.Equals(this.Caller.LegacyExchangeDN, this.RequestData.LegacyDN, StringComparison.OrdinalIgnoreCase))
					{
						this.RequestedRecipient = this.Caller;
						resolveMethod = "CallerByLegacyDN";
						return;
					}
					if (!string.IsNullOrEmpty(this.RequestData.LegacyDN) && this.Caller.EmailAddresses != null)
					{
						string x500 = "x500:" + this.RequestData.LegacyDN;
						ProxyAddress a = this.Caller.EmailAddresses.Find((ProxyAddress x) => string.Equals(x.ToString(), x500, StringComparison.OrdinalIgnoreCase));
						if (a != null)
						{
							this.RequestedRecipient = this.Caller;
							resolveMethod = "CallerByX500";
							return;
						}
					}
					if (!string.IsNullOrEmpty(this.RequestData.EMailAddress) && SmtpAddress.IsValidSmtpAddress(this.RequestData.EMailAddress))
					{
						SmtpProxyAddress smtpProxy = new SmtpProxyAddress(this.RequestData.EMailAddress, true);
						ProxyAddress a2 = this.Caller.EmailAddresses.Find((ProxyAddress x) => x.Equals(smtpProxy));
						if (a2 != null)
						{
							this.RequestedRecipient = this.Caller;
							resolveMethod = "CallerByProxy";
							return;
						}
					}
					if (AutodiscoverCommonUserSettings.HasLocalArchive(this.Caller) && AutodiscoverCommonUserSettings.IsEmailAddressTargetingArchive(this.Caller as ADUser, this.RequestData.EMailAddress))
					{
						this.RequestedRecipient = this.Caller;
						resolveMethod = "CallerByArchive";
						return;
					}
				}
				if (this.Caller == null)
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.NoADLookupForUser.Enabled)
					{
						goto IL_285;
					}
				}
				try
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.Current.TrackLatency(ServiceLatencyMetadata.RequestedUserADLatency, delegate()
					{
						IRecipientSession callerScopedRecipientSession = this.GetCallerScopedRecipientSession();
						if (!string.IsNullOrEmpty(this.RequestData.LegacyDN))
						{
							this.RequestedRecipient = callerScopedRecipientSession.FindByLegacyExchangeDN(this.RequestData.LegacyDN);
							if (this.RequestedRecipient != null)
							{
								resolveMethod = "FoundByLegacyDN";
							}
						}
						if (this.RequestedRecipient == null && this.RequestData.EMailAddress != null && SmtpAddress.IsValidSmtpAddress(this.RequestData.EMailAddress))
						{
							Guid guid;
							if (AutodiscoverCommonUserSettings.TryGetExchangeGuidFromEmailAddress(this.RequestData.EMailAddress, out guid))
							{
								this.RequestedRecipient = callerScopedRecipientSession.FindByExchangeGuidIncludingArchive(guid);
								ADUser aduser = this.RequestedRecipient as ADUser;
								if (aduser != null && aduser.ArchiveGuid.Equals(guid) && RemoteMailbox.IsRemoteMailbox(aduser.RecipientTypeDetails) && aduser.ArchiveDatabase == null)
								{
									this.RequestedRecipient = null;
								}
								if (this.RequestedRecipient != null)
								{
									resolveMethod = "FoundByGUID";
								}
							}
							if (this.RequestedRecipient == null)
							{
								SmtpProxyAddress proxyAddress = new SmtpProxyAddress(this.RequestData.EMailAddress, true);
								this.RequestedRecipient = callerScopedRecipientSession.FindByProxyAddress(proxyAddress);
								if (this.RequestedRecipient != null)
								{
									resolveMethod = "FoundBySMTP";
								}
							}
						}
					});
				}
				catch (LocalizedException ex)
				{
					ExTraceGlobals.FrameworkTracer.TraceError<string, string>(0L, "[UpdateCacheCallback()] 'LocalizedException' Message=\"{0}\";StackTrace=\"{1}\"", ex.Message, ex.StackTrace);
					Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_ErrWebException, Common.PeriodicKey, new object[]
					{
						ex.Message,
						ex.StackTrace
					});
					resolveMethod = "Exception";
				}
				IL_285:;
			}
			finally
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericInfo("ResolveMethod", resolveMethod);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005BE8 File Offset: 0x00003DE8
		private IRecipientSession GetCallerScopedRecipientSession()
		{
			ADSessionSettings adsessionSettings = (this.Caller == null) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.Caller.OrganizationId);
			adsessionSettings.IncludeInactiveMailbox = true;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, this.GetQueryBaseDN(), CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, adsessionSettings, 386, "GetCallerScopedRecipientSession", "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\Common\\Provider.cs");
			tenantOrRootOrgRecipientSession.ServerTimeout = new TimeSpan?(Common.RecipientLookupTimeout);
			tenantOrRootOrgRecipientSession.SessionSettings.AccountingObject = this.RequestData.Budget;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005C74 File Offset: 0x00003E74
		private ADObjectId GetQueryBaseDN()
		{
			ADUser aduser = this.Caller as ADUser;
			if (aduser == null)
			{
				return null;
			}
			return aduser.QueryBaseDN;
		}
	}
}

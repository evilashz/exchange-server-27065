using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A2 RID: 162
	public sealed class OwaCompositeIdentity : OwaIdentity
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0001363C File Offset: 0x0001183C
		private OwaCompositeIdentity(OwaIdentity primaryIdentity, IEnumerable<OwaIdentity> secondaryIdentities)
		{
			if (primaryIdentity == null)
			{
				throw new ArgumentNullException("primaryIdentity", "The primary identity must not be null!");
			}
			this.primaryIdentity = primaryIdentity;
			base.UserOrganizationId = primaryIdentity.UserOrganizationId;
			base.OwaMiniRecipient = primaryIdentity.OwaMiniRecipient;
			IReadOnlyList<OwaIdentity> readOnlyList;
			if (secondaryIdentities != null)
			{
				readOnlyList = (from i in secondaryIdentities
				orderby i.UserSid
				select i).ToArray<OwaIdentity>();
			}
			else
			{
				readOnlyList = new OwaIdentity[0];
			}
			this.secondaryIdentities = readOnlyList;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000136BC File Offset: 0x000118BC
		public static OwaCompositeIdentity CreateFromCompositeIdentity(CompositeIdentity compositeIdentity)
		{
			if (compositeIdentity == null)
			{
				throw new ArgumentNullException("compositeIdentity", "You must specify the source CompositeIdentity.");
			}
			OwaIdentity owaIdentity = OwaIdentity.GetOwaIdentity(compositeIdentity.PrimaryIdentity);
			if (owaIdentity == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaIdentity::CreateFromCompositeIdentity] - failed to resolve primary identity.");
				throw new OwaIdentityException("Cannot create security context for the specified composite identity. Failed to resolve the primary identity.");
			}
			OwaIdentity[] array = new OwaIdentity[compositeIdentity.SecondaryIdentitiesCount];
			int num = 0;
			foreach (IIdentity identity in compositeIdentity.SecondaryIdentities)
			{
				array[num] = OwaIdentity.GetOwaIdentity(identity);
				if (array[num] == null)
				{
					ExTraceGlobals.CoreCallTracer.TraceError(0L, string.Format("[OwaIdentity::CreateFromCompositeIdentity] - failed to resolve secondary identity {0}.", num));
					throw new OwaIdentityException(string.Format("Cannot create security context for the specified composite identity. Failed to resolve a secondary identity {0}.", num));
				}
				num++;
			}
			return new OwaCompositeIdentity(owaIdentity, array);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x000137A0 File Offset: 0x000119A0
		internal static OwaIdentity CreateFromAuthZClientInfo(AuthZClientInfo authZClientInfo)
		{
			if (authZClientInfo == null)
			{
				throw new ArgumentNullException("authZClientInfo", "You must specify the source AuthZClientInfo.");
			}
			OwaIdentity owaIdentity = OwaClientSecurityContextIdentity.CreateFromClientSecurityContext(authZClientInfo.ClientSecurityContext, authZClientInfo.PrimarySmtpAddress, "OverrideClientSecurityContext");
			if (owaIdentity == null)
			{
				ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaIdentity::CreateFromAuthZClientInfo] - was unable to create the security context for " + authZClientInfo.PrimarySmtpAddress);
				throw new OwaIdentityException("Cannot create security context for the specified identity. Failed to resolve the identity " + authZClientInfo.PrimarySmtpAddress);
			}
			if (authZClientInfo.SecondaryClientInfoItems.Count > 0)
			{
				OwaIdentity[] array = new OwaIdentity[authZClientInfo.SecondaryClientInfoItems.Count];
				int num = 0;
				foreach (AuthZClientInfo authZClientInfo2 in authZClientInfo.SecondaryClientInfoItems)
				{
					array[num] = OwaClientSecurityContextIdentity.CreateFromClientSecurityContext(authZClientInfo2.ClientSecurityContext, authZClientInfo2.PrimarySmtpAddress, "OverrideClientSecurityContext");
					if (array[num] == null)
					{
						ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaIdentity::CreateFromAuthZClientInfo] - was unable to create the security context for composite identity. Failed to resolve secondary identity " + authZClientInfo2.PrimarySmtpAddress);
						throw new OwaIdentityException(string.Format("Cannot create security context for the specified composite identity. Failed to resolve the secondary identity {0}: {1}.", num, authZClientInfo2.PrimarySmtpAddress));
					}
					num++;
				}
				owaIdentity = new OwaCompositeIdentity(owaIdentity, array);
			}
			return owaIdentity;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000138D4 File Offset: 0x00011AD4
		public IReadOnlyList<OwaIdentity> SecondaryIdentities
		{
			get
			{
				return this.secondaryIdentities;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000138DC File Offset: 0x00011ADC
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				return this.primaryIdentity.WindowsIdentity;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x000138E9 File Offset: 0x00011AE9
		public override SecurityIdentifier UserSid
		{
			get
			{
				return this.primaryIdentity.UserSid;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x000138F6 File Offset: 0x00011AF6
		public override string AuthenticationType
		{
			get
			{
				return this.primaryIdentity.AuthenticationType;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00013903 File Offset: 0x00011B03
		public override string UniqueId
		{
			get
			{
				return this.primaryIdentity.UniqueId;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00013910 File Offset: 0x00011B10
		public override bool IsPartial
		{
			get
			{
				return this.primaryIdentity.IsPartial;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001391D File Offset: 0x00011B1D
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.primaryIdentity.ClientSecurityContext;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001392A File Offset: 0x00011B2A
		public override string GetLogonName()
		{
			return this.primaryIdentity.GetLogonName();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00013937 File Offset: 0x00011B37
		public override string SafeGetRenderableName()
		{
			return this.primaryIdentity.SafeGetRenderableName();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00013944 File Offset: 0x00011B44
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			return this.primaryIdentity.InternalCreateExchangePrincipal();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00013951 File Offset: 0x00011B51
		internal override MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.primaryIdentity.CreateMailboxSession(exchangePrincipal, cultureInfo);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00013960 File Offset: 0x00011B60
		internal override MailboxSession CreateInstantSearchMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.primaryIdentity.CreateInstantSearchMailboxSession(exchangePrincipal, cultureInfo);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001396F File Offset: 0x00011B6F
		internal override MailboxSession CreateDelegateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.primaryIdentity.CreateDelegateMailboxSession(exchangePrincipal, cultureInfo);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00013980 File Offset: 0x00011B80
		public override bool IsEqualsTo(OwaIdentity otherIdentity)
		{
			OwaCompositeIdentity owaCompositeIdentity = otherIdentity as OwaCompositeIdentity;
			if (owaCompositeIdentity == null)
			{
				return false;
			}
			bool flag = otherIdentity.UserSid.Equals(this.UserSid);
			if (flag)
			{
				if (this.secondaryIdentities.Count != this.secondaryIdentities.Count)
				{
					return false;
				}
				for (int i = 0; i < this.secondaryIdentities.Count; i++)
				{
					if (!this.secondaryIdentities[i].IsEqualsTo(owaCompositeIdentity.secondaryIdentities[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400038A RID: 906
		private readonly OwaIdentity primaryIdentity;

		// Token: 0x0400038B RID: 907
		private readonly IReadOnlyList<OwaIdentity> secondaryIdentities;
	}
}

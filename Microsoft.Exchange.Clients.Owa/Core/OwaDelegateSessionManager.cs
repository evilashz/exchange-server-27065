using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200017F RID: 383
	internal sealed class OwaDelegateSessionManager
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x0005AE91 File Offset: 0x00059091
		internal OwaDelegateSessionManager(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
			this.exchangePrincipals = new Dictionary<string, ExchangePrincipal>();
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0005AEBC File Offset: 0x000590BC
		internal bool TryGetExchangePrincipal(string legacyDN, out ExchangePrincipal exchangePrincipal)
		{
			bool result = false;
			exchangePrincipal = null;
			if (string.IsNullOrEmpty(legacyDN) || !Utilities.IsValidLegacyDN(legacyDN))
			{
				return result;
			}
			if (this.exchangePrincipals.TryGetValue(legacyDN.ToUpperInvariant(), out exchangePrincipal))
			{
				result = true;
			}
			else
			{
				try
				{
					ADSessionSettings adSettings = Utilities.CreateScopedADSessionSettings(this.userContext.LogonIdentity.DomainName);
					exchangePrincipal = ExchangePrincipal.FromLegacyDN(adSettings, legacyDN, RemotingOptions.AllowCrossSite);
					result = true;
					if (!this.exchangePrincipals.ContainsKey(exchangePrincipal.LegacyDn.ToUpperInvariant()))
					{
						this.exchangePrincipals.Add(exchangePrincipal.LegacyDn.ToUpperInvariant(), exchangePrincipal);
					}
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "OwaDelegateSessionManager.TryGetExchangePrincipal. Unable to get ExchangePrincipal from legacy DN {0}. Exception: {1}.", legacyDN, ex.Message);
				}
				catch (StorageTransientException ex2)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "OwaDelegateSessionManager.TryGetExchangePrincipal. Unable to get ExchangePrincipal from legacy DN {0}. Exception: {1}.", legacyDN, ex2.Message);
				}
			}
			return result;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0005AFB0 File Offset: 0x000591B0
		public void RemoveInvalidExchangePrincipal(string legacyDN)
		{
			if (this.exchangePrincipals != null && this.exchangePrincipals.ContainsKey(legacyDN.ToUpperInvariant()))
			{
				this.exchangePrincipals.Remove(legacyDN.ToUpperInvariant());
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0005AFDF File Offset: 0x000591DF
		public void ClearAllExchangePrincipals()
		{
			if (this.exchangePrincipals != null)
			{
				this.exchangePrincipals.Clear();
				this.exchangePrincipals = null;
			}
		}

		// Token: 0x04000993 RID: 2451
		private Dictionary<string, ExchangePrincipal> exchangePrincipals;

		// Token: 0x04000994 RID: 2452
		private UserContext userContext;
	}
}

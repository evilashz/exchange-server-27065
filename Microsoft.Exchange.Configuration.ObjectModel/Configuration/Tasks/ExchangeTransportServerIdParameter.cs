using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	public abstract class ExchangeTransportServerIdParameter : ADIdParameter
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x0002ABFD File Offset: 0x00028DFD
		public ExchangeTransportServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0002AC06 File Offset: 0x00028E06
		public ExchangeTransportServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002AC0F File Offset: 0x00028E0F
		public ExchangeTransportServerIdParameter()
		{
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0002AC17 File Offset: 0x00028E17
		protected ExchangeTransportServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002AC20 File Offset: 0x00028E20
		protected bool MatchServer(ADObjectId identity)
		{
			if (this.identityPassedIn == null)
			{
				return true;
			}
			if (this.identityPassedIn.InternalADObjectId == null)
			{
				return ExchangeTransportServerIdParameter.MatchServerName(identity.Name, this.identityPassedIn.RawIdentity) || ExchangeTransportServerIdParameter.MatchServerName(identity.Parent.Parent.Name, this.identityPassedIn.RawIdentity);
			}
			if (this.identityPassedIn.InternalADObjectId.Name == identity.Name)
			{
				return ExchangeTransportServerIdParameter.MatchServerName(identity.Parent.Parent.Name, this.identityPassedIn.InternalADObjectId.Parent.Parent.Name);
			}
			return ExchangeTransportServerIdParameter.MatchServerName(identity.Parent.Parent.Name, this.identityPassedIn.InternalADObjectId.Name);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		private static bool MatchServerName(string serverName, string serverIdentity)
		{
			if (serverIdentity.StartsWith("*") && serverIdentity.EndsWith("*"))
			{
				if (serverIdentity.Length > 2)
				{
					return serverName.IndexOf(serverIdentity.Substring(1, serverIdentity.Length - 2), StringComparison.OrdinalIgnoreCase) > -1;
				}
			}
			else if (serverIdentity.StartsWith("*"))
			{
				if (serverIdentity.Length > 1)
				{
					return serverName.EndsWith(serverIdentity.Substring(1), StringComparison.OrdinalIgnoreCase);
				}
			}
			else
			{
				if (serverIdentity.EndsWith("*"))
				{
					return CultureInfo.InvariantCulture.CompareInfo.IsPrefix(serverName, serverIdentity.Substring(0, serverIdentity.Length - 1), CompareOptions.IgnoreCase);
				}
				if (string.Equals(serverName, serverIdentity, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				int num = serverIdentity.IndexOf(".");
				return num > 0 && string.Equals(serverName, serverIdentity.Substring(0, num), StringComparison.OrdinalIgnoreCase);
			}
			return true;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002ADC4 File Offset: 0x00028FC4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(MailboxTransportServer) && typeof(T) != typeof(FrontendTransportServer))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			List<T> list = new List<T>();
			foreach (T item in wrapper)
			{
				if (this.MatchServer((ADObjectId)item.Identity))
				{
					list.Add(item);
				}
			}
			if (list.Count == 0 && this.identityPassedIn != null)
			{
				notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.identityPassedIn.RawIdentity));
			}
			return list;
		}

		// Token: 0x04000315 RID: 789
		private const string WildcardCharacter = "*";

		// Token: 0x04000316 RID: 790
		protected ADIdParameter identityPassedIn;
	}
}

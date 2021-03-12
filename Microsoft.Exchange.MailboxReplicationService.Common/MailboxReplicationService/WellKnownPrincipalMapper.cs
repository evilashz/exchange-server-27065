using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E1 RID: 225
	internal class WellKnownPrincipalMapper
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00010819 File Offset: 0x0000EA19
		public WellKnownPrincipalMapper()
		{
			this.guidToSidMap = new Dictionary<Guid, SecurityIdentifier>();
			this.sidToGuidMap = new Dictionary<SecurityIdentifier, Guid>();
		}

		// Token: 0x170002D7 RID: 727
		public Guid this[SecurityIdentifier sid]
		{
			get
			{
				Guid result;
				if (this.sidToGuidMap.TryGetValue(sid, out result))
				{
					return result;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x170002D8 RID: 728
		public SecurityIdentifier this[Guid guid]
		{
			get
			{
				SecurityIdentifier result;
				if (this.guidToSidMap.TryGetValue(guid, out result))
				{
					return result;
				}
				return null;
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00010888 File Offset: 0x0000EA88
		public void Initialize(IRecipientSession session)
		{
			if (this.initialized)
			{
				return;
			}
			lock (this.locker)
			{
				if (!this.initialized)
				{
					try
					{
						this.AddMapping(WellKnownPrincipalMapper.ExchangeServers, session.GetWellKnownExchangeGroupSid(WellKnownPrincipalMapper.ExchangeServers));
					}
					catch (ADExternalException)
					{
					}
					this.initialized = true;
				}
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00010904 File Offset: 0x0000EB04
		private void AddMapping(Guid guid, SecurityIdentifier sid)
		{
			if (sid != null && guid != Guid.Empty)
			{
				this.guidToSidMap[guid] = sid;
				this.sidToGuidMap[sid] = guid;
			}
		}

		// Token: 0x0400050B RID: 1291
		public static readonly Guid ExchangeServers = new Guid("00fa592b-68a2-43ea-83ba-89b4971b6863");

		// Token: 0x0400050C RID: 1292
		private Dictionary<Guid, SecurityIdentifier> guidToSidMap;

		// Token: 0x0400050D RID: 1293
		private Dictionary<SecurityIdentifier, Guid> sidToGuidMap;

		// Token: 0x0400050E RID: 1294
		private bool initialized;

		// Token: 0x0400050F RID: 1295
		private object locker = new object();
	}
}

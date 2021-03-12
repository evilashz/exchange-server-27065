using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000180 RID: 384
	internal class PrincipalMapper : LookupTable<MappedPrincipal>
	{
		// Token: 0x06000E7C RID: 3708 RVA: 0x00021130 File Offset: 0x0001F330
		public PrincipalMapper(IMailbox mailbox)
		{
			this.mailbox = mailbox;
			this.byMailboxGuid = new PrincipalMapper.MailboxGuidIndex();
			this.bySid = new PrincipalMapper.SidIndex();
			this.byX500Proxy = new PrincipalMapper.X500ProxyIndex();
			base.RegisterIndex(this.byMailboxGuid);
			base.RegisterIndex(this.bySid);
			base.RegisterIndex(this.byX500Proxy);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0002118F File Offset: 0x0001F38F
		public PrincipalMapper.X500ProxyIndex ByX500Proxy
		{
			get
			{
				return this.byX500Proxy;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00021197 File Offset: 0x0001F397
		public PrincipalMapper.MailboxGuidIndex ByMailboxGuid
		{
			get
			{
				return this.byMailboxGuid;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0002119F File Offset: 0x0001F39F
		public PrincipalMapper.SidIndex BySid
		{
			get
			{
				return this.bySid;
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000211A7 File Offset: 0x0001F3A7
		public void AddLegDN(string legDN)
		{
			this.ByX500Proxy.AddKey(legDN);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000211B8 File Offset: 0x0001F3B8
		public string LookupLegDnByExProxy(string exProxy)
		{
			MappedPrincipal mappedPrincipal = this.ByX500Proxy[exProxy];
			if (mappedPrincipal == null)
			{
				return this.ScrambleLegDN(exProxy);
			}
			return mappedPrincipal.LegacyDN;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000211E3 File Offset: 0x0001F3E3
		public void AddSid(SecurityIdentifier objectSid)
		{
			this.BySid.AddKey(objectSid);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000211F4 File Offset: 0x0001F3F4
		public SecurityIdentifier LookupSidByMailboxGuid(Guid mailboxGuid)
		{
			MappedPrincipal mappedPrincipal = this.ByMailboxGuid[mailboxGuid];
			if (mappedPrincipal == null)
			{
				return null;
			}
			return mappedPrincipal.ObjectSid;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002121C File Offset: 0x0001F41C
		public SecurityIdentifier LookupSidByExProxy(string exProxy)
		{
			MappedPrincipal mappedPrincipal = this.ByX500Proxy[exProxy];
			if (mappedPrincipal == null)
			{
				return null;
			}
			return mappedPrincipal.ObjectSid;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00021244 File Offset: 0x0001F444
		private string ScrambleLegDN(string legDN)
		{
			return string.Format("{0}_unmapped{1}", legDN, Guid.NewGuid().ToString("N"));
		}

		// Token: 0x04000824 RID: 2084
		private IMailbox mailbox;

		// Token: 0x04000825 RID: 2085
		private PrincipalMapper.X500ProxyIndex byX500Proxy;

		// Token: 0x04000826 RID: 2086
		private PrincipalMapper.MailboxGuidIndex byMailboxGuid;

		// Token: 0x04000827 RID: 2087
		private PrincipalMapper.SidIndex bySid;

		// Token: 0x02000181 RID: 385
		public class X500ProxyIndex : LookupIndex<string, MappedPrincipal>
		{
			// Token: 0x06000E86 RID: 3718 RVA: 0x00021270 File Offset: 0x0001F470
			protected override ICollection<string> RetrieveKeys(MappedPrincipal data)
			{
				HashSet<string> hashSet = new HashSet<string>(this.GetEqualityComparer());
				if (!string.IsNullOrEmpty(data.LegacyDN))
				{
					hashSet.Add(data.LegacyDN);
				}
				if (data.ProxyAddresses != null)
				{
					foreach (string proxyAddressString in data.ProxyAddresses)
					{
						ProxyAddress proxyAddress = ProxyAddress.Parse(proxyAddressString);
						if (proxyAddress != null && proxyAddress.Prefix == ProxyAddressPrefix.X500 && !hashSet.Contains(proxyAddress.AddressString))
						{
							hashSet.Add(proxyAddress.AddressString);
						}
					}
				}
				return hashSet;
			}

			// Token: 0x06000E87 RID: 3719 RVA: 0x00021308 File Offset: 0x0001F508
			protected override MappedPrincipal[] LookupKeys(string[] keys)
			{
				MappedPrincipal[] array = new MappedPrincipal[keys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					array[i] = new MappedPrincipal();
					array[i].LegacyDN = keys[i];
				}
				return ((PrincipalMapper)base.Owner).mailbox.ResolvePrincipals(array);
			}

			// Token: 0x06000E88 RID: 3720 RVA: 0x00021355 File Offset: 0x0001F555
			protected override IEqualityComparer<string> GetEqualityComparer()
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x02000182 RID: 386
		public class MailboxGuidIndex : LookupIndex<Guid, MappedPrincipal>
		{
			// Token: 0x06000E8A RID: 3722 RVA: 0x00021364 File Offset: 0x0001F564
			protected override ICollection<Guid> RetrieveKeys(MappedPrincipal data)
			{
				if (data.MailboxGuid != Guid.Empty)
				{
					return new Guid[]
					{
						data.MailboxGuid
					};
				}
				return null;
			}

			// Token: 0x06000E8B RID: 3723 RVA: 0x000213A0 File Offset: 0x0001F5A0
			protected override MappedPrincipal[] LookupKeys(Guid[] keys)
			{
				MappedPrincipal[] array = new MappedPrincipal[keys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					array[i] = new MappedPrincipal();
					array[i].MailboxGuid = keys[i];
				}
				return ((PrincipalMapper)base.Owner).mailbox.ResolvePrincipals(array);
			}
		}

		// Token: 0x02000183 RID: 387
		public class SidIndex : LookupIndex<SecurityIdentifier, MappedPrincipal>
		{
			// Token: 0x06000E8D RID: 3725 RVA: 0x00021400 File Offset: 0x0001F600
			protected override ICollection<SecurityIdentifier> RetrieveKeys(MappedPrincipal data)
			{
				if (data.ObjectSid != null)
				{
					return new SecurityIdentifier[]
					{
						data.ObjectSid
					};
				}
				return null;
			}

			// Token: 0x06000E8E RID: 3726 RVA: 0x00021430 File Offset: 0x0001F630
			protected override MappedPrincipal[] LookupKeys(SecurityIdentifier[] keys)
			{
				MappedPrincipal[] array = new MappedPrincipal[keys.Length];
				for (int i = 0; i < keys.Length; i++)
				{
					array[i] = new MappedPrincipal();
					array[i].ObjectSid = keys[i];
				}
				return ((PrincipalMapper)base.Owner).mailbox.ResolvePrincipals(array);
			}
		}
	}
}

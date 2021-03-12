using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000063 RID: 99
	public class CompositeIdentity : GenericSidIdentity, IIdentity, IEnumerable<IdentityRef>, IEnumerable
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public CompositeIdentity(IdentityRef primaryIdentity, IEnumerable<IdentityRef> secondaryIdentities) : base(primaryIdentity.Identity.GetSafeName(true), primaryIdentity.Identity.AuthenticationType, primaryIdentity.Identity.GetSecurityIdentifier())
		{
			this.ValidateIdentities(primaryIdentity, secondaryIdentities);
			this.primaryIdentity = primaryIdentity;
			this.secondaryIdentities = (secondaryIdentities ?? ((IEnumerable<IdentityRef>)new IdentityRef[0])).ToArray<IdentityRef>();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001AA43 File Offset: 0x00018C43
		public GenericIdentity PrimaryIdentity
		{
			get
			{
				return this.primaryIdentity.Identity;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001AA50 File Offset: 0x00018C50
		public int SecondaryIdentitiesCount
		{
			get
			{
				return this.secondaryIdentities.Length;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0001AA62 File Offset: 0x00018C62
		public IEnumerable<GenericIdentity> SecondaryIdentities
		{
			get
			{
				return from ir in this.secondaryIdentities
				select ir.Identity;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0001AA94 File Offset: 0x00018C94
		public SecurityIdentifier CanarySid
		{
			get
			{
				IdentityRef identityRef;
				if (!this.primaryIdentity.IsUsedForCanary)
				{
					identityRef = this.secondaryIdentities.FirstOrDefault((IdentityRef si) => si.IsUsedForCanary);
				}
				else
				{
					identityRef = this.primaryIdentity;
				}
				IdentityRef identityRef2 = identityRef;
				if (identityRef2 == null)
				{
					identityRef2 = this.primaryIdentity;
				}
				return identityRef2.Identity.GetSecurityIdentifier();
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001AAF4 File Offset: 0x00018CF4
		public GenericIdentity GetSecondaryIdentityAt(int index)
		{
			if (index < 0 || index >= this.SecondaryIdentitiesCount)
			{
				throw new ArgumentOutOfRangeException("index", string.Format(CultureInfo.InvariantCulture, "Invalid index specified ({0}). There are {1} secondary identities available.", new object[]
				{
					index,
					this.SecondaryIdentitiesCount
				}));
			}
			return this.secondaryIdentities[index].Identity;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001AB54 File Offset: 0x00018D54
		public IEnumerator<IdentityRef> GetEnumerator()
		{
			return this.GetAllIdentities().GetEnumerator();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001AB61 File Offset: 0x00018D61
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001AB6C File Offset: 0x00018D6C
		private void ValidateIdentities(IdentityRef primaryIdentity, IEnumerable<IdentityRef> secondaryIdentities)
		{
			if (primaryIdentity == null)
			{
				throw new ArgumentNullException("primaryIdentity", "The primary identity must not be null!");
			}
			int num = Convert.ToInt32(primaryIdentity.Authority == AuthenticationAuthority.MSA);
			if (secondaryIdentities != null)
			{
				foreach (IdentityRef identityRef in secondaryIdentities)
				{
					num += Convert.ToInt32(identityRef.Authority == AuthenticationAuthority.MSA);
				}
			}
			if (num > 1)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "There must not be more than one {0} identity present.", new object[]
				{
					AuthenticationAuthority.MSA
				}));
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		private IEnumerable<IdentityRef> GetAllIdentities()
		{
			yield return this.primaryIdentity;
			if (this.secondaryIdentities != null)
			{
				foreach (IdentityRef secondaryIdentity in this.secondaryIdentities)
				{
					yield return secondaryIdentity;
				}
			}
			yield break;
		}

		// Token: 0x0400034A RID: 842
		private readonly IdentityRef primaryIdentity;

		// Token: 0x0400034B RID: 843
		private readonly IdentityRef[] secondaryIdentities;
	}
}

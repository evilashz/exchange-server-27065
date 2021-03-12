using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001FD RID: 509
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoPrincipal
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0004DD87 File Offset: 0x0004BF87
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x0004DD8F File Offset: 0x0004BF8F
		public ICollection<string> EmailAddresses { get; set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x0004DD98 File Offset: 0x0004BF98
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x06001272 RID: 4722 RVA: 0x0004DDD0 File Offset: 0x0004BFD0
		public IEnumerable<string> GetEmailAddressDomains()
		{
			if (this.EmailAddresses == null || this.EmailAddresses.Count == 0)
			{
				return Array<string>.Empty;
			}
			return from address in this.EmailAddresses
			where SmtpAddress.IsValidSmtpAddress(address)
			select SmtpAddress.Parse(address).Domain;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004DE50 File Offset: 0x0004C050
		public bool IsSame(PhotoPrincipal other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (this.EmailAddresses == null || this.EmailAddresses.Count == 0)
			{
				return false;
			}
			if (other.EmailAddresses == null || other.EmailAddresses.Count == 0)
			{
				return false;
			}
			return (from s in this.EmailAddresses.Intersect(other.EmailAddresses, StringComparer.OrdinalIgnoreCase)
			where !string.IsNullOrEmpty(s)
			select s).Any<string>();
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004DED8 File Offset: 0x0004C0D8
		public bool InSameOrganization(PhotoPrincipal other)
		{
			return other != null && (object.ReferenceEquals(this, other) || (!(this.OrganizationId == null) && !(other.OrganizationId == null) && this.OrganizationId.Equals(other.OrganizationId)));
		}
	}
}

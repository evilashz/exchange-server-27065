using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000508 RID: 1288
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct OscNetworkMoniker : IEquatable<OscNetworkMoniker>
	{
		// Token: 0x060037B2 RID: 14258 RVA: 0x000E0CA4 File Offset: 0x000DEEA4
		internal OscNetworkMoniker(Guid providerGuid, string networkId, string userId)
		{
			Util.ThrowOnNullOrEmptyArgument(userId, "userId");
			if (string.IsNullOrEmpty(networkId))
			{
				this.moniker = string.Format("{0}-{1}", OscNetworkMoniker.ToStringEnclosedByCurlyBracesLowerCase(providerGuid), userId).ToLowerInvariant();
				return;
			}
			this.moniker = string.Format("{0}-{1}-{2}", OscNetworkMoniker.ToStringEnclosedByCurlyBracesLowerCase(providerGuid), networkId, userId).ToLowerInvariant();
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000E0CFE File Offset: 0x000DEEFE
		internal OscNetworkMoniker(string moniker)
		{
			Util.ThrowOnNullOrEmptyArgument(moniker, "moniker");
			this.moniker = moniker;
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000E0D12 File Offset: 0x000DEF12
		public bool Equals(OscNetworkMoniker other)
		{
			return this.moniker.Equals(other.moniker, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000E0D27 File Offset: 0x000DEF27
		public bool Equals(string other)
		{
			return this.moniker.Equals(other, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000E0D36 File Offset: 0x000DEF36
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (other is string)
			{
				return this.Equals((string)other);
			}
			return other is OscNetworkMoniker && this.Equals((OscNetworkMoniker)other);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000E0D68 File Offset: 0x000DEF68
		public override int GetHashCode()
		{
			return this.moniker.GetHashCode();
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000E0D75 File Offset: 0x000DEF75
		public override string ToString()
		{
			return this.moniker.ToString();
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000E0D82 File Offset: 0x000DEF82
		private static string ToStringEnclosedByCurlyBracesLowerCase(Guid g)
		{
			return g.ToString("B", CultureInfo.InvariantCulture).ToLowerInvariant();
		}

		// Token: 0x04001D97 RID: 7575
		private readonly string moniker;
	}
}

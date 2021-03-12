using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x02000029 RID: 41
	public abstract class RoutingKeyBase : IRoutingKey, IEquatable<IRoutingKey>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009D RID: 157
		public abstract RoutingItemType RoutingItemType { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009E RID: 158
		public abstract string Value { get; }

		// Token: 0x0600009F RID: 159 RVA: 0x000034B4 File Offset: 0x000016B4
		public static bool operator ==(RoutingKeyBase firstRoutingKey, RoutingKeyBase secondRoutingKey)
		{
			return object.ReferenceEquals(firstRoutingKey, secondRoutingKey) || (!object.ReferenceEquals(firstRoutingKey, null) && !object.ReferenceEquals(secondRoutingKey, null) && firstRoutingKey.RoutingItemType == secondRoutingKey.RoutingItemType && firstRoutingKey.Value.Equals(secondRoutingKey.Value, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003502 File Offset: 0x00001702
		public static bool operator !=(RoutingKeyBase firstRoutingKey, RoutingKeyBase secondRoutingKey)
		{
			return !(firstRoutingKey == secondRoutingKey);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000350E File Offset: 0x0000170E
		public bool Equals(IRoutingKey other)
		{
			return other as RoutingKeyBase == this;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000351C File Offset: 0x0000171C
		public override bool Equals(object obj)
		{
			return this.Equals((IRoutingKey)obj);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000352A File Offset: 0x0000172A
		public override int GetHashCode()
		{
			return this.RoutingItemType.GetHashCode() ^ this.Value.GetHashCode();
		}
	}
}

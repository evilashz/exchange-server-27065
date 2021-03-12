using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingEntries
{
	// Token: 0x0200001B RID: 27
	public abstract class RoutingEntryBase : IRoutingEntry, IEquatable<IRoutingEntry>
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000076 RID: 118
		public abstract IRoutingDestination Destination { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000077 RID: 119
		public abstract IRoutingKey Key { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000078 RID: 120
		public abstract long Timestamp { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x00003134 File Offset: 0x00001334
		public static bool operator ==(RoutingEntryBase firstRoutingEntry, RoutingEntryBase secondRoutingEntry)
		{
			return object.ReferenceEquals(firstRoutingEntry, secondRoutingEntry) || (!object.ReferenceEquals(firstRoutingEntry, null) && !object.ReferenceEquals(secondRoutingEntry, null) && firstRoutingEntry.Key.Equals(secondRoutingEntry.Key) && firstRoutingEntry.Destination.Equals(secondRoutingEntry.Destination));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003186 File Offset: 0x00001386
		public static bool operator !=(RoutingEntryBase firstRoutingEntry, RoutingEntryBase secondRoutingEntry)
		{
			return !(firstRoutingEntry == secondRoutingEntry);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003192 File Offset: 0x00001392
		public bool Equals(IRoutingEntry other)
		{
			return other as RoutingEntryBase == this;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031A0 File Offset: 0x000013A0
		public override bool Equals(object obj)
		{
			return this.Equals((IRoutingEntry)obj);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031AE File Offset: 0x000013AE
		public override int GetHashCode()
		{
			return this.Key.GetHashCode() ^ this.Destination.GetHashCode();
		}
	}
}

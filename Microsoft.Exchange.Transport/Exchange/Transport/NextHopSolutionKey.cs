using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200018C RID: 396
	internal struct NextHopSolutionKey : IEquatable<NextHopSolutionKey>
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x00046B76 File Offset: 0x00044D76
		public NextHopSolutionKey(DeliveryType deliveryType, string nextHopDomain, Guid nextHopConnector)
		{
			this = new NextHopSolutionKey(new NextHopType(deliveryType), nextHopDomain, nextHopConnector);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00046B86 File Offset: 0x00044D86
		public NextHopSolutionKey(NextHopType nextHopType, string nextHopDomain, Guid nextHopConnector)
		{
			this.nextHopType = nextHopType;
			this.nextHopDomain = nextHopDomain.ToLowerInvariant();
			this.nextHopConnector = nextHopConnector;
			this.propertyBag = null;
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00046BA9 File Offset: 0x00044DA9
		public NextHopSolutionKey(DeliveryType deliveryType, string nextHopDomain, Guid nextHopConnector, string nextHopTlsDomain)
		{
			this = new NextHopSolutionKey(new NextHopType(deliveryType), nextHopDomain, nextHopConnector, nextHopTlsDomain);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00046BBB File Offset: 0x00044DBB
		public NextHopSolutionKey(NextHopType nextHopType, string nextHopDomain, Guid nextHopConnector, string nextHopTlsDomain)
		{
			this = new NextHopSolutionKey(nextHopType, nextHopDomain, nextHopConnector);
			if (!string.IsNullOrEmpty(nextHopTlsDomain))
			{
				this.SetProperty<string>(NextHopSolutionKeyObjectSchema.TlsDomain, nextHopTlsDomain);
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00046BDC File Offset: 0x00044DDC
		public NextHopSolutionKey(DeliveryType deliveryType, string nextHopDomain, Guid nextHopConnector, bool isLocalDeliveryGroupRelay)
		{
			this = new NextHopSolutionKey(deliveryType, nextHopDomain, nextHopConnector);
			if (isLocalDeliveryGroupRelay)
			{
				this.SetProperty<bool>(NextHopSolutionKeyObjectSchema.IsLocalDeliveryGroupRelay, isLocalDeliveryGroupRelay);
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00046BF8 File Offset: 0x00044DF8
		public NextHopSolutionKey(DeliveryType deliveryType, string nextHopDomain, Guid nextHopConnector, RequiredTlsAuthLevel? requiredAuthLevel, string nextHopTlsDomain, string overrideSource)
		{
			this = new NextHopSolutionKey(deliveryType, nextHopDomain, nextHopConnector, nextHopTlsDomain);
			if (requiredAuthLevel != null)
			{
				this.SetProperty<RequiredTlsAuthLevel?>(NextHopSolutionKeyObjectSchema.TlsAuthLevel, requiredAuthLevel);
			}
			if (overrideSource != null)
			{
				this.SetProperty<string>(NextHopSolutionKeyObjectSchema.OverrideSource, overrideSource);
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00046C2C File Offset: 0x00044E2C
		public NextHopSolutionKey(DeliveryType deliveryType, string nextHopDomain, Guid nextHopConnector, RequiredTlsAuthLevel? requiredAuthLevel, string nextHopTlsDomain, RiskLevel riskLevel, int outboundIPPool, string overrideSource)
		{
			this = new NextHopSolutionKey(deliveryType, nextHopDomain, nextHopConnector, requiredAuthLevel, nextHopTlsDomain, overrideSource);
			if (riskLevel != RiskLevel.Normal)
			{
				this.SetProperty<int>(NextHopSolutionKeyObjectSchema.RiskLevel, (int)riskLevel);
			}
			if (outboundIPPool != 0)
			{
				this.SetProperty<int>(NextHopSolutionKeyObjectSchema.OutboundIPPool, outboundIPPool);
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00046C60 File Offset: 0x00044E60
		public NextHopSolutionKey(RoutedMessageQueue queue)
		{
			this.nextHopType = queue.Key.NextHopType;
			this.nextHopDomain = queue.Key.NextHopDomain;
			this.nextHopConnector = queue.Key.NextHopConnector;
			this.propertyBag = null;
			if (!string.IsNullOrEmpty(queue.Key.NextHopTlsDomain))
			{
				this.SetProperty<string>(NextHopSolutionKeyObjectSchema.TlsDomain, queue.Key.NextHopTlsDomain);
			}
			if (queue.Key.IsLocalDeliveryGroupRelay)
			{
				this.SetProperty<bool>(NextHopSolutionKeyObjectSchema.IsLocalDeliveryGroupRelay, queue.Key.IsLocalDeliveryGroupRelay);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00046D0A File Offset: 0x00044F0A
		public NextHopType NextHopType
		{
			get
			{
				return this.nextHopType;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00046D12 File Offset: 0x00044F12
		public string NextHopDomain
		{
			get
			{
				return this.nextHopDomain;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00046D1A File Offset: 0x00044F1A
		public Guid NextHopConnector
		{
			get
			{
				return this.nextHopConnector;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00046D22 File Offset: 0x00044F22
		public string NextHopTlsDomain
		{
			get
			{
				return this.GetProperty<string>(NextHopSolutionKeyObjectSchema.TlsDomain);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00046D2F File Offset: 0x00044F2F
		public bool IsLocalDeliveryGroupRelay
		{
			get
			{
				return this.GetProperty<bool>(NextHopSolutionKeyObjectSchema.IsLocalDeliveryGroupRelay);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00046D3C File Offset: 0x00044F3C
		public RequiredTlsAuthLevel? TlsAuthLevel
		{
			get
			{
				return this.GetProperty<RequiredTlsAuthLevel?>(NextHopSolutionKeyObjectSchema.TlsAuthLevel);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00046D49 File Offset: 0x00044F49
		public RiskLevel RiskLevel
		{
			get
			{
				return (RiskLevel)this.GetProperty<int>(NextHopSolutionKeyObjectSchema.RiskLevel);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00046D56 File Offset: 0x00044F56
		public int OutboundIPPool
		{
			get
			{
				return this.GetProperty<int>(NextHopSolutionKeyObjectSchema.OutboundIPPool);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00046D63 File Offset: 0x00044F63
		public string OverrideSource
		{
			get
			{
				return this.GetProperty<string>(NextHopSolutionKeyObjectSchema.OverrideSource);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00046D70 File Offset: 0x00044F70
		public bool IsEmpty
		{
			get
			{
				return this.nextHopType.Equals(NextHopType.Empty) && string.IsNullOrEmpty(this.nextHopDomain) && this.nextHopConnector == Guid.Empty && string.IsNullOrEmpty(this.NextHopTlsDomain);
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00046DC0 File Offset: 0x00044FC0
		public static NextHopSolutionKey CreateShadowNextHopSolutionKey(string serverFqdn, NextHopSolutionKey other)
		{
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			Guid nextHopGuid = other.NextHopType.IsSmtpConnectorDeliveryType ? other.NextHopConnector : Guid.Empty;
			return NextHopSolutionKey.CreateShadowNextHopSolutionKey(serverFqdn, nextHopGuid, other.NextHopTlsDomain);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00046E0E File Offset: 0x0004500E
		public static NextHopSolutionKey CreateShadowNextHopSolutionKey(string serverFqdn, Guid nextHopGuid)
		{
			if (string.IsNullOrEmpty("serverFqdn"))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			return NextHopSolutionKey.CreateShadowNextHopSolutionKey(serverFqdn, nextHopGuid, null);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00046E2F File Offset: 0x0004502F
		public static NextHopSolutionKey CreateShadowNextHopSolutionKey(string serverFqdn, Guid nextHopGuid, string tlsDomain)
		{
			return new NextHopSolutionKey(NextHopType.ShadowRedundancy, serverFqdn, nextHopGuid, tlsDomain);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00046E40 File Offset: 0x00045040
		public override int GetHashCode()
		{
			return this.NextHopConnector.GetHashCode() ^ this.NextHopDomain.GetHashCode() ^ this.NextHopType.GetHashCode() ^ this.NextHopTlsDomain.GetHashCode() ^ this.IsLocalDeliveryGroupRelay.GetHashCode() ^ this.TlsAuthLevel.GetHashCode() ^ this.RiskLevel.GetHashCode() ^ this.OutboundIPPool.GetHashCode() ^ this.OverrideSource.GetHashCode();
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00046EDF File Offset: 0x000450DF
		public override bool Equals(object obj)
		{
			return obj is NextHopSolutionKey && this.Equals((NextHopSolutionKey)obj);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00046EF8 File Offset: 0x000450F8
		public bool Equals(NextHopSolutionKey other)
		{
			return this.NextHopType.Equals(other.NextHopType) && this.NextHopConnector == other.NextHopConnector && this.NextHopDomain.Equals(other.NextHopDomain, StringComparison.Ordinal) && this.NextHopTlsDomain.Equals(other.NextHopTlsDomain) && this.IsLocalDeliveryGroupRelay == other.IsLocalDeliveryGroupRelay && this.TlsAuthLevel == other.TlsAuthLevel && this.RiskLevel == other.RiskLevel && this.OutboundIPPool == other.OutboundIPPool && this.OverrideSource.Equals(other.OverrideSource, StringComparison.Ordinal);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00046FD8 File Offset: 0x000451D8
		public override string ToString()
		{
			return string.Format("{0} ({1}) {2} {3} {4} {5} {6} {7} {8}", new object[]
			{
				this.nextHopType,
				this.nextHopDomain,
				this.nextHopConnector,
				this.NextHopTlsDomain,
				this.IsLocalDeliveryGroupRelay ? "local-relay" : string.Empty,
				this.TlsAuthLevel,
				this.RiskLevel,
				this.OutboundIPPool,
				this.OverrideSource
			});
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00047070 File Offset: 0x00045270
		public string ToShortString()
		{
			string text = string.Empty;
			if (this.TlsAuthLevel != null)
			{
				text = ((int)this.TlsAuthLevel.Value).ToString(CultureInfo.InvariantCulture);
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				(int)this.NextHopType.NextHopCategory,
				(int)this.NextHopType.DeliveryType
			});
			return string.Format(CultureInfo.InvariantCulture, "({0}):{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8};", new object[]
			{
				this.nextHopDomain,
				this.NextHopTlsDomain,
				text2,
				this.nextHopConnector.ToString().Substring(0, 6),
				this.IsLocalDeliveryGroupRelay ? "1" : "0",
				text,
				(int)this.RiskLevel,
				this.OutboundIPPool,
				this.OverrideSource
			});
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00047192 File Offset: 0x00045392
		private void SetProperty<T>(ProviderPropertyDefinition property, T value)
		{
			if (this.propertyBag == null)
			{
				this.propertyBag = new NextHopSolutionKeyPropertyBag();
			}
			this.propertyBag[property] = value;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000471B9 File Offset: 0x000453B9
		private T GetProperty<T>(ProviderPropertyDefinition property)
		{
			if (this.propertyBag != null)
			{
				return (T)((object)this.propertyBag[property]);
			}
			return (T)((object)property.DefaultValue);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000471E0 File Offset: 0x000453E0
		private MultiValuedProperty<T> GetMultiValuedProperty<T>(ProviderPropertyDefinition property)
		{
			if (this.propertyBag != null)
			{
				return (MultiValuedProperty<T>)this.propertyBag[property];
			}
			return MultiValuedProperty<T>.Empty;
		}

		// Token: 0x0400093D RID: 2365
		public static readonly NextHopSolutionKey Empty = new NextHopSolutionKey(NextHopType.Empty, string.Empty, Guid.Empty);

		// Token: 0x0400093E RID: 2366
		public static readonly NextHopSolutionKey Submission = new NextHopSolutionKey(NextHopType.Empty, "Submission", Guid.Empty);

		// Token: 0x0400093F RID: 2367
		public static readonly NextHopSolutionKey Unreachable = new NextHopSolutionKey(NextHopType.Unreachable, "Unreachable", Guid.Empty);

		// Token: 0x04000940 RID: 2368
		private readonly NextHopType nextHopType;

		// Token: 0x04000941 RID: 2369
		private readonly string nextHopDomain;

		// Token: 0x04000942 RID: 2370
		private readonly Guid nextHopConnector;

		// Token: 0x04000943 RID: 2371
		private NextHopSolutionKeyPropertyBag propertyBag;
	}
}

using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E04 RID: 3588
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeviceIdentity : IComparable<DeviceIdentity>, IEquatable<DeviceIdentity>, IEquatable<string>
	{
		// Token: 0x06007BA5 RID: 31653 RVA: 0x00221E6E File Offset: 0x0022006E
		public DeviceIdentity(string deviceId, string deviceType, string protocol)
		{
			this.Initialize(deviceId, deviceType, protocol);
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x00221E80 File Offset: 0x00220080
		public DeviceIdentity(string deviceId, string deviceType, MobileClientType mobileClientType)
		{
			string protocol;
			if (!DeviceIdentity.TryGetProtocol(mobileClientType, out protocol))
			{
				throw new ArgumentException("Unsupported MobileClientType value: " + mobileClientType);
			}
			this.Initialize(deviceId, deviceType, protocol);
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x00221EBC File Offset: 0x002200BC
		public DeviceIdentity(string compositeIdentity)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("CompositeIdentity", compositeIdentity);
			this.CompositeKey = compositeIdentity;
			string protocol;
			string deviceType;
			string deviceId;
			DeviceIdentity.ParseSyncFolderName(compositeIdentity, out protocol, out deviceType, out deviceId);
			this.Protocol = protocol;
			this.DeviceType = deviceType;
			this.DeviceId = deviceId;
			DeviceIdentity.VerifyPart(this.DeviceId, "DeviceId");
			DeviceIdentity.VerifyPart(this.DeviceType, "DeviceType");
			ArgumentValidator.ThrowIfNullOrEmpty("Protocol", this.Protocol);
			this.IsDnMangled = this.DeviceId.Contains("\n");
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x00221F48 File Offset: 0x00220148
		public static DeviceIdentity FromMobileDevice(MobileDevice mobileDevice)
		{
			return new DeviceIdentity(mobileDevice.DeviceId, mobileDevice.DeviceType, (mobileDevice.ClientType == MobileClientType.EAS) ? "AirSync" : "MOWA");
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06007BA9 RID: 31657 RVA: 0x00221F70 File Offset: 0x00220170
		// (set) Token: 0x06007BAA RID: 31658 RVA: 0x00221F78 File Offset: 0x00220178
		public bool IsDnMangled { get; private set; }

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x00221F81 File Offset: 0x00220181
		// (set) Token: 0x06007BAC RID: 31660 RVA: 0x00221F89 File Offset: 0x00220189
		public string DeviceId { get; private set; }

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x00221F92 File Offset: 0x00220192
		// (set) Token: 0x06007BAE RID: 31662 RVA: 0x00221F9A File Offset: 0x0022019A
		public string DeviceType { get; private set; }

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x06007BAF RID: 31663 RVA: 0x00221FA3 File Offset: 0x002201A3
		// (set) Token: 0x06007BB0 RID: 31664 RVA: 0x00221FAB File Offset: 0x002201AB
		public string Protocol { get; private set; }

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06007BB1 RID: 31665 RVA: 0x00221FB4 File Offset: 0x002201B4
		// (set) Token: 0x06007BB2 RID: 31666 RVA: 0x00221FBC File Offset: 0x002201BC
		public string CompositeKey { get; private set; }

		// Token: 0x06007BB3 RID: 31667 RVA: 0x00221FC5 File Offset: 0x002201C5
		public bool TryGetMobileClientType(out MobileClientType mobileClientType)
		{
			return DeviceIdentity.TryGetMobileClientType(this.Protocol, out mobileClientType);
		}

		// Token: 0x06007BB4 RID: 31668 RVA: 0x00221FD3 File Offset: 0x002201D3
		public static bool TryGetMobileClientType(string protocol, out MobileClientType mobileClientType)
		{
			mobileClientType = MobileClientType.EAS;
			if (string.Equals(protocol, "AirSync", StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if (string.Equals(protocol, "MOWA", StringComparison.OrdinalIgnoreCase))
			{
				mobileClientType = MobileClientType.MOWA;
				return true;
			}
			return false;
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x00221FFC File Offset: 0x002201FC
		public static bool TryGetProtocol(MobileClientType mobileClientType, out string protocol)
		{
			switch (mobileClientType)
			{
			case MobileClientType.EAS:
				protocol = "AirSync";
				return true;
			case MobileClientType.MOWA:
				protocol = "MOWA";
				return true;
			default:
				protocol = null;
				return false;
			}
		}

		// Token: 0x06007BB6 RID: 31670 RVA: 0x00222033 File Offset: 0x00220233
		public bool IsDeviceId(string deviceId)
		{
			return this.DeviceId.Equals(deviceId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007BB7 RID: 31671 RVA: 0x00222042 File Offset: 0x00220242
		public bool IsDeviceType(string deviceType)
		{
			return this.DeviceType.Equals(deviceType, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007BB8 RID: 31672 RVA: 0x00222051 File Offset: 0x00220251
		public bool IsProtocol(string protocol)
		{
			return string.Equals(this.Protocol, protocol, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x00222060 File Offset: 0x00220260
		public static string BuildCompositeKey(string protocol, string deviceType, string deviceId)
		{
			return string.Format("{0}-{1}-{2}", protocol, deviceType, deviceId);
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x00222070 File Offset: 0x00220270
		public static void ParseSyncFolderName(string combinedName, out string protocol, out string deviceType, out string deviceId)
		{
			protocol = null;
			deviceType = null;
			deviceId = null;
			int num = combinedName.LastIndexOf('-');
			if (num < 0 || num >= combinedName.Length - 1)
			{
				throw new InvalidOperationException(string.Format("[DeviceId] SyncStateStorage has an invalid name: '{0}'", combinedName));
			}
			deviceId = combinedName.Substring(num + 1);
			combinedName = combinedName.Substring(0, num);
			num = combinedName.LastIndexOf('-');
			if (num < 0 || num >= combinedName.Length - 1)
			{
				throw new InvalidOperationException(string.Format("[DeviceType] SyncStateStorage has an invalid name: '{0}'", combinedName));
			}
			deviceType = combinedName.Substring(num + 1);
			protocol = combinedName.Substring(0, num);
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x00222102 File Offset: 0x00220302
		public override string ToString()
		{
			return this.CompositeKey;
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x0022210A File Offset: 0x0022030A
		public override int GetHashCode()
		{
			return this.CompositeKey.GetHashCode();
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x00222118 File Offset: 0x00220318
		public override bool Equals(object obj)
		{
			if (!(obj is DeviceIdentity))
			{
				return false;
			}
			DeviceIdentity deviceIdentity = (DeviceIdentity)obj;
			return deviceIdentity.CompositeKey.Equals(this.CompositeKey, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x00222148 File Offset: 0x00220348
		public bool Equals(string deviceId, string deviceType)
		{
			return this.IsDeviceId(deviceId) && this.IsDeviceType(deviceType);
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x0022215C File Offset: 0x0022035C
		public bool Equals(string deviceId, string deviceType, string protocol)
		{
			return this.IsDeviceId(deviceId) && this.IsDeviceType(deviceType) && this.IsProtocol(protocol);
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x00222179 File Offset: 0x00220379
		private static void VerifyPart(string toCheck, string part)
		{
			if (string.IsNullOrEmpty(toCheck))
			{
				throw new ArgumentException(string.Format("{0} cannot be null or empty.", part));
			}
			if (toCheck.Contains("-"))
			{
				throw new InvalidOperationException(string.Format("{0} cannot contain hyphens.  Supplied value - '{1}'", part, toCheck));
			}
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x002221B4 File Offset: 0x002203B4
		private void Initialize(string deviceId, string deviceType, string protocol)
		{
			DeviceIdentity.VerifyPart(deviceId, "DeviceId");
			DeviceIdentity.VerifyPart(deviceType, "DeviceType");
			ArgumentValidator.ThrowIfNullOrEmpty("Protocol", protocol);
			this.DeviceId = deviceId;
			this.DeviceType = deviceType;
			this.Protocol = protocol;
			this.IsDnMangled = (deviceId != null && deviceId.Contains("\n"));
			this.CompositeKey = DeviceIdentity.BuildCompositeKey(protocol, deviceType, deviceId);
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x0022221C File Offset: 0x0022041C
		int IComparable<DeviceIdentity>.CompareTo(DeviceIdentity other)
		{
			if (other == null)
			{
				return -1;
			}
			return this.CompositeKey.CompareTo(other.CompositeKey);
		}

		// Token: 0x06007BC3 RID: 31683 RVA: 0x00222234 File Offset: 0x00220434
		bool IEquatable<DeviceIdentity>.Equals(DeviceIdentity other)
		{
			return other != null && other.CompositeKey.Equals(this.CompositeKey);
		}

		// Token: 0x06007BC4 RID: 31684 RVA: 0x0022224C File Offset: 0x0022044C
		bool IEquatable<string>.Equals(string other)
		{
			return this.CompositeKey.Equals(other);
		}

		// Token: 0x040054EA RID: 21738
		private const string deviceIdPart = "DeviceId";

		// Token: 0x040054EB RID: 21739
		private const string deviceTypePart = "DeviceType";

		// Token: 0x040054EC RID: 21740
		private const string protocolPart = "Protocol";

		// Token: 0x040054ED RID: 21741
		private const string compositeIdentityPart = "CompositeIdentity";

		// Token: 0x040054EE RID: 21742
		public const string AirSyncProtocol = "AirSync";

		// Token: 0x040054EF RID: 21743
		public const string MOWAProtocol = "MOWA";
	}
}

using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200005F RID: 95
	internal class DeviceAccessRuleData
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0001E989 File Offset: 0x0001CB89
		public DeviceAccessRuleData(ActiveSyncDeviceAccessRule deviceAccessRule)
		{
			this.Identity = deviceAccessRule.OriginalId;
			this.QueryString = deviceAccessRule.QueryString;
			this.Characteristic = deviceAccessRule.Characteristic;
			this.AccessLevel = deviceAccessRule.AccessLevel;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001E9C1 File Offset: 0x0001CBC1
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001E9C9 File Offset: 0x0001CBC9
		public ADObjectId Identity { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001E9D2 File Offset: 0x0001CBD2
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001E9DA File Offset: 0x0001CBDA
		public string QueryString { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001E9E3 File Offset: 0x0001CBE3
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001E9EB File Offset: 0x0001CBEB
		public DeviceAccessCharacteristic Characteristic { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001E9FC File Offset: 0x0001CBFC
		public DeviceAccessLevel AccessLevel { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001EA08 File Offset: 0x0001CC08
		public DeviceAccessState AccessState
		{
			get
			{
				switch (this.AccessLevel)
				{
				case DeviceAccessLevel.Allow:
					return DeviceAccessState.Allowed;
				case DeviceAccessLevel.Block:
					return DeviceAccessState.Blocked;
				case DeviceAccessLevel.Quarantine:
					return DeviceAccessState.Quarantined;
				default:
					return DeviceAccessState.Unknown;
				}
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public override string ToString()
		{
			return string.Format("Identity: {0}, Characteristic: {1}, QueryString: {2}, AccessLevel: {3}", new object[]
			{
				this.Identity,
				this.Characteristic,
				this.QueryString,
				this.AccessLevel
			});
		}
	}
}

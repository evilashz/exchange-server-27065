using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000289 RID: 649
	internal class EasDeviceBudgetKey : SidBudgetKey
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x0008D1B0 File Offset: 0x0008B3B0
		public EasDeviceBudgetKey(SecurityIdentifier sid, string deviceId, string deviceType, ADSessionSettings settings) : base(sid, BudgetType.Eas, false, settings)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("deviceId", deviceId);
			ArgumentValidator.ThrowIfNullOrEmpty("deviceType", deviceType);
			this.DeviceId = deviceId;
			this.DeviceType = deviceType;
			this.cachedHashCode = (sid.GetHashCode() ^ deviceId.GetHashCode() ^ deviceType.GetHashCode());
			this.cachedToString = string.Format("{0}_{1}_{2}", base.NtAccount, this.DeviceId, this.DeviceType);
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0008D228 File Offset: 0x0008B428
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x0008D230 File Offset: 0x0008B430
		public string DeviceId { get; private set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0008D239 File Offset: 0x0008B439
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0008D241 File Offset: 0x0008B441
		public string DeviceType { get; private set; }

		// Token: 0x060017E9 RID: 6121 RVA: 0x0008D24A File Offset: 0x0008B44A
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0008D254 File Offset: 0x0008B454
		public override bool Equals(object obj)
		{
			EasDeviceBudgetKey easDeviceBudgetKey = obj as EasDeviceBudgetKey;
			return !(easDeviceBudgetKey == null) && (object.ReferenceEquals(obj, this) || (easDeviceBudgetKey.Sid.Equals(base.Sid) && easDeviceBudgetKey.DeviceId == this.DeviceId && easDeviceBudgetKey.DeviceType == this.DeviceType));
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0008D2B7 File Offset: 0x0008B4B7
		public override string ToString()
		{
			return this.cachedToString;
		}

		// Token: 0x04000EA5 RID: 3749
		private readonly int cachedHashCode;

		// Token: 0x04000EA6 RID: 3750
		private readonly string cachedToString;
	}
}

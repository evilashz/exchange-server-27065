using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000061 RID: 97
	internal static class AuditHelper
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000A810 File Offset: 0x00008A10
		public static void ApplyAuditProperties(IPropertyBag propertyBag, Guid auditId = default(Guid), string userId = null)
		{
			if (propertyBag == null)
			{
				throw new ArgumentNullException("propertyBag");
			}
			if (auditId == Guid.Empty)
			{
				if (propertyBag[AuditHelper.AuditIdProp] != null)
				{
					auditId = (Guid)propertyBag[AuditHelper.AuditIdProp];
				}
				if (auditId == Guid.Empty)
				{
					auditId = Guid.NewGuid();
				}
			}
			if (string.IsNullOrEmpty(userId))
			{
				userId = (string)propertyBag[AuditHelper.UserIdProp];
				if (string.IsNullOrEmpty(userId))
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						userId = string.Format("Unknown-{0}-{1}", currentProcess.ProcessName, Environment.MachineName);
					}
				}
			}
			propertyBag[AuditHelper.AuditIdProp] = auditId;
			propertyBag[AuditHelper.UserIdProp] = userId;
		}

		// Token: 0x0400023E RID: 574
		public static readonly HygienePropertyDefinition UserIdProp = new HygienePropertyDefinition("AuditUserId", typeof(string), string.Empty, ExchangeObjectVersion.Exchange2003, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400023F RID: 575
		public static readonly HygienePropertyDefinition AuditIdProp = new HygienePropertyDefinition("AuditId", typeof(Guid), Guid.Empty, ExchangeObjectVersion.Exchange2003, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000240 RID: 576
		public static readonly string UnknownUserId = "Unknown";

		// Token: 0x04000241 RID: 577
		public static readonly string ForwardSyncUserId = "ForwardSync";

		// Token: 0x04000242 RID: 578
		public static readonly string Migrate1415SyncUserId = "Migrate1415";

		// Token: 0x04000243 RID: 579
		public static readonly string SecurityUserId = "Security";
	}
}

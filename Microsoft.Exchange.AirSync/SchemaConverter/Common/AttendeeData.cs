using System;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	internal struct AttendeeData
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x0005C9E9 File Offset: 0x0005ABE9
		public AttendeeData(string emailAddress, string displayName)
		{
			AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.CommonTracer, null, "AttendeeData Created email={0} displayname={1}", emailAddress, displayName);
			this.emailAddress = emailAddress;
			this.displayName = displayName;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0005CA0B File Offset: 0x0005AC0B
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x0005CA13 File Offset: 0x0005AC13
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0005CA1C File Offset: 0x0005AC1C
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x0005CA24 File Offset: 0x0005AC24
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x04000ACD RID: 2765
		private string displayName;

		// Token: 0x04000ACE RID: 2766
		private string emailAddress;
	}
}

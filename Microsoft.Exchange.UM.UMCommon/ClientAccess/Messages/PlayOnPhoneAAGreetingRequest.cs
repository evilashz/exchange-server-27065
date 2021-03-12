using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	public class PlayOnPhoneAAGreetingRequest : PlayOnPhoneRequest
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00025B08 File Offset: 0x00023D08
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00025B10 File Offset: 0x00023D10
		public Guid AAIdentity
		{
			get
			{
				return this.aaGuid;
			}
			set
			{
				this.aaGuid = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00025B19 File Offset: 0x00023D19
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00025B21 File Offset: 0x00023D21
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00025B2A File Offset: 0x00023D2A
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00025B32 File Offset: 0x00023D32
		public string UserRecordingTheGreeting
		{
			get
			{
				return this.user;
			}
			set
			{
				this.user = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00025B3B File Offset: 0x00023D3B
		internal UMDialPlan DialPlan
		{
			get
			{
				if (this.dp == null)
				{
					this.GetAAAndDialPlan();
				}
				return this.dp;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00025B51 File Offset: 0x00023D51
		internal UMAutoAttendant AutoAttendant
		{
			get
			{
				if (this.aa == null)
				{
					this.GetAAAndDialPlan();
				}
				return this.aa;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00025B67 File Offset: 0x00023D67
		internal override string GetFriendlyName()
		{
			return Strings.PlayOnPhoneAAGreetingRequest;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00025B74 File Offset: 0x00023D74
		private void GetAAAndDialPlan()
		{
			Guid aaidentity = this.AAIdentity;
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromTenantGuid(base.TenantGuid);
			UMAutoAttendant autoAttendantFromId = iadsystemConfigurationLookup.GetAutoAttendantFromId(new ADObjectId(this.AAIdentity));
			if (autoAttendantFromId == null)
			{
				throw new InvalidUMAutoAttendantException();
			}
			this.aa = autoAttendantFromId;
			this.dp = iadsystemConfigurationLookup.GetDialPlanFromId(autoAttendantFromId.UMDialPlan);
		}

		// Token: 0x0400055E RID: 1374
		private Guid aaGuid;

		// Token: 0x0400055F RID: 1375
		private string fileName;

		// Token: 0x04000560 RID: 1376
		private UMDialPlan dp;

		// Token: 0x04000561 RID: 1377
		private UMAutoAttendant aa;

		// Token: 0x04000562 RID: 1378
		private string user;
	}
}

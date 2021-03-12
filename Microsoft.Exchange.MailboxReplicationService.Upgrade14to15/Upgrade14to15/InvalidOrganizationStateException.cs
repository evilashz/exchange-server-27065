using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000EC RID: 236
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidOrganizationStateException : MigrationTransientException
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		public InvalidOrganizationStateException(string org, string servicePlan, ExchangeObjectVersion version, bool isUpgrading, bool isPiloting, bool isUpgradeInProgress) : base(UpgradeHandlerStrings.InvalidOrganizationState(org, servicePlan, version, isUpgrading, isPiloting, isUpgradeInProgress))
		{
			this.org = org;
			this.servicePlan = servicePlan;
			this.version = version;
			this.isUpgrading = isUpgrading;
			this.isPiloting = isPiloting;
			this.isUpgradeInProgress = isUpgradeInProgress;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public InvalidOrganizationStateException(string org, string servicePlan, ExchangeObjectVersion version, bool isUpgrading, bool isPiloting, bool isUpgradeInProgress, Exception innerException) : base(UpgradeHandlerStrings.InvalidOrganizationState(org, servicePlan, version, isUpgrading, isPiloting, isUpgradeInProgress), innerException)
		{
			this.org = org;
			this.servicePlan = servicePlan;
			this.version = version;
			this.isUpgrading = isUpgrading;
			this.isPiloting = isPiloting;
			this.isUpgradeInProgress = isUpgradeInProgress;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000FE3C File Offset: 0x0000E03C
		protected InvalidOrganizationStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.org = (string)info.GetValue("org", typeof(string));
			this.servicePlan = (string)info.GetValue("servicePlan", typeof(string));
			this.version = (ExchangeObjectVersion)info.GetValue("version", typeof(ExchangeObjectVersion));
			this.isUpgrading = (bool)info.GetValue("isUpgrading", typeof(bool));
			this.isPiloting = (bool)info.GetValue("isPiloting", typeof(bool));
			this.isUpgradeInProgress = (bool)info.GetValue("isUpgradeInProgress", typeof(bool));
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000FF14 File Offset: 0x0000E114
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("org", this.org);
			info.AddValue("servicePlan", this.servicePlan);
			info.AddValue("version", this.version);
			info.AddValue("isUpgrading", this.isUpgrading);
			info.AddValue("isPiloting", this.isPiloting);
			info.AddValue("isUpgradeInProgress", this.isUpgradeInProgress);
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0000FF8F File Offset: 0x0000E18F
		public string Org
		{
			get
			{
				return this.org;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0000FF97 File Offset: 0x0000E197
		public string ServicePlan
		{
			get
			{
				return this.servicePlan;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x0000FF9F File Offset: 0x0000E19F
		public ExchangeObjectVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0000FFA7 File Offset: 0x0000E1A7
		public bool IsUpgrading
		{
			get
			{
				return this.isUpgrading;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0000FFAF File Offset: 0x0000E1AF
		public bool IsPiloting
		{
			get
			{
				return this.isPiloting;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0000FFB7 File Offset: 0x0000E1B7
		public bool IsUpgradeInProgress
		{
			get
			{
				return this.isUpgradeInProgress;
			}
		}

		// Token: 0x0400039B RID: 923
		private readonly string org;

		// Token: 0x0400039C RID: 924
		private readonly string servicePlan;

		// Token: 0x0400039D RID: 925
		private readonly ExchangeObjectVersion version;

		// Token: 0x0400039E RID: 926
		private readonly bool isUpgrading;

		// Token: 0x0400039F RID: 927
		private readonly bool isPiloting;

		// Token: 0x040003A0 RID: 928
		private readonly bool isUpgradeInProgress;
	}
}

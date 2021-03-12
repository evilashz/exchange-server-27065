using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	internal class SetupServerSettings : ADServerSettings, ICloneable, IEquatable<SetupServerSettings>
	{
		// Token: 0x06001156 RID: 4438 RVA: 0x00053D09 File Offset: 0x00051F09
		private SetupServerSettings()
		{
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00053D11 File Offset: 0x00051F11
		internal static SetupServerSettings CreateSetupServerSettings()
		{
			return new SetupServerSettings();
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00053D18 File Offset: 0x00051F18
		protected override bool EnforceIsUpdatableByADSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00053D1B File Offset: 0x00051F1B
		internal override void SetPreferredGlobalCatalog(string partitionFqdn, ADServerInfo serverInfo)
		{
			throw new NotSupportedException("SetPreferredGlobalCatalog passing ADServerInfo is not supported on SetupServerSettings");
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00053D27 File Offset: 0x00051F27
		internal override void SetConfigurationDomainController(string partitionFqdn, ADServerInfo serverInfo)
		{
			throw new NotSupportedException("SetConfigurationDomainController passing ADServerInfo is not supported on SetupServerSettings");
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00053D33 File Offset: 0x00051F33
		internal override void AddPreferredDC(ADServerInfo serverInfo)
		{
			throw new NotSupportedException("AddPreferredDC passing ADServerInfo is not supported on SetupServerSettings");
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00053D3F File Offset: 0x00051F3F
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00053D42 File Offset: 0x00051F42
		internal override ADObjectId RecipientViewRoot
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("RecipientViewRoot setter is not supported on SetupServerSettings");
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00053D4E File Offset: 0x00051F4E
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00053D51 File Offset: 0x00051F51
		internal override bool ViewEntireForest
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException("ViewEntireForest setter is not supported on SetupServerSettings");
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00053D5D File Offset: 0x00051F5D
		internal override bool IsFailoverRequired()
		{
			return false;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00053D60 File Offset: 0x00051F60
		internal override bool TryFailover(out ADServerSettings newServerSettings, out string failToFailOverReason, bool forestWideAffinityRequested = false)
		{
			throw new NotSupportedException("TryFailover setter is not supported on SetupServerSettings");
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00053D6C File Offset: 0x00051F6C
		internal override void MarkDcDown(Fqdn fqdn)
		{
			throw new NotSupportedException("MarkDcDown setter is not supported on SetupServerSettings");
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00053D78 File Offset: 0x00051F78
		internal override void MarkDcUp(Fqdn fqdn)
		{
			throw new NotSupportedException("MarkDcUp setter is not supported on SetupServerSettings");
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00053D84 File Offset: 0x00051F84
		public override object Clone()
		{
			SetupServerSettings setupServerSettings = new SetupServerSettings();
			this.CopyTo(setupServerSettings);
			return setupServerSettings;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00053D9F File Offset: 0x00051F9F
		public bool Equals(SetupServerSettings other)
		{
			return base.Equals(other);
		}
	}
}

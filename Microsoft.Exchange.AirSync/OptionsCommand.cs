using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000F6 RID: 246
	internal class OptionsCommand : Command
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x0004A97E File Offset: 0x00048B7E
		internal OptionsCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfOptions;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0004A991 File Offset: 0x00048B91
		internal override bool RequiresPolicyCheck
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0004A994 File Offset: 0x00048B94
		internal override int MinVersion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0004A997 File Offset: 0x00048B97
		protected override bool ShouldOpenSyncState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0004A99A File Offset: 0x00048B9A
		protected sealed override string RootNodeName
		{
			get
			{
				return "Invalid";
			}
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0004A9A1 File Offset: 0x00048BA1
		internal override Command.ExecutionState ExecuteCommand()
		{
			this.AddHeaders();
			base.XmlResponse = null;
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0004A9B1 File Offset: 0x00048BB1
		protected override bool HandleQuarantinedState()
		{
			throw new InvalidOperationException("Options command should always be allowed!");
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0004A9C0 File Offset: 0x00048BC0
		private void AddHeaders()
		{
			if (base.User.IsConsumerOrganizationUser)
			{
				base.AddHeadersForConsumerOrgUser();
			}
			else
			{
				base.AddHeadersForEnterpriseOrgUser();
			}
			base.Context.Response.AppendHeader("Public", "OPTIONS,POST");
			base.Context.Response.AppendHeader("Allow", "OPTIONS,POST");
		}
	}
}

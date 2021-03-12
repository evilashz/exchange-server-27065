using System;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Ceres.SearchCore.Admin;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x0200001E RID: 30
	internal sealed class IndexSeeder : FastManagementClient, IIndexSeederSource, IIndexSeederTarget, IDisposable
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000B7DF File Offset: 0x000099DF
		internal IndexSeeder(string catalog)
		{
			Util.ThrowOnNullOrEmptyArgument(catalog, "catalog");
			this.catalog = catalog;
			base.DiagnosticsSession.ComponentName = "IndexSeeder";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.IndexManagementTracer;
			base.ConnectManagementAgents();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000B81F File Offset: 0x00009A1F
		protected override int ManagementPortOffset
		{
			get
			{
				return 63;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B823 File Offset: 0x00009A23
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IndexSeeder>(this);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B854 File Offset: 0x00009A54
		public string SeedToEndPoint(string seedingEndPoint, string reason)
		{
			Util.ThrowOnNullOrEmptyArgument(seedingEndPoint, "seedingEndPoint");
			if (string.IsNullOrEmpty(reason))
			{
				reason = "Seeding reason not specified";
			}
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Seed-EndPoint({0}) - EndPoint={1}, Reason={2}", new object[]
			{
				this.catalog,
				seedingEndPoint,
				reason
			});
			FailureCode failureCode = new FailureCode(0, reason, 0);
			return this.PerformFastOperation<string>(() => this.seedingService.SeedToEndPointWithReason(seedingEndPoint, failureCode), "SeedToEndPoint");
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000B90C File Offset: 0x00009B0C
		public int GetProgress(string identifier)
		{
			Util.ThrowOnNullOrEmptyArgument(identifier, "identifier");
			int num = this.PerformFastOperation<int>(() => this.seedingService.GetProgress(identifier), "GetProgress");
			if (num < 0)
			{
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Get-Progress({0}) - Return Value={1}", new object[]
				{
					this.catalog,
					num
				});
			}
			else if (num == 100)
			{
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Get-Progress({0}) - Seeding Completed", new object[]
				{
					this.catalog
				});
			}
			return num;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public void Cancel(string identifier, string reason)
		{
			Util.ThrowOnNullOrEmptyArgument(identifier, "identifier");
			if (string.IsNullOrEmpty(reason))
			{
				reason = "Cancel reason not specified";
			}
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Abort-Seeding({0}) - Reason={1}", new object[]
			{
				this.catalog,
				reason
			});
			FailureCode failureCode = new FailureCode(0, reason, 0);
			base.PerformFastOperation(delegate()
			{
				this.seedingService.AbortWithReason(identifier, failureCode);
			}, "Abort");
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000BA72 File Offset: 0x00009C72
		public string GetSeedingEndPoint()
		{
			return this.PerformFastOperation<string>(() => this.seedingService.GetSeedingEndPoint(), "GetSeedingEndPoint");
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000BA8B File Offset: 0x00009C8B
		protected override void InternalConnectManagementAgents(WcfManagementClient client)
		{
			this.seedingService = client.GetManagementAgent<ISeedingManagementAgent>("SeedingAgent-" + this.catalog + "/Single");
		}

		// Token: 0x040000D5 RID: 213
		private readonly string catalog;

		// Token: 0x040000D6 RID: 214
		private volatile ISeedingManagementAgent seedingService;
	}
}

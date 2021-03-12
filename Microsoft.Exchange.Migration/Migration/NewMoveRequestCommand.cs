using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NewMoveRequestCommand : NewMoveRequestCommandBase
	{
		// Token: 0x06001157 RID: 4439 RVA: 0x000495E4 File Offset: 0x000477E4
		public NewMoveRequestCommand(bool whatIf) : base("New-MoveRequest", NewMoveRequestCommand.ExceptionsToIgnore)
		{
			base.WhatIf = whatIf;
		}

		// Token: 0x1700052C RID: 1324
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x000495FD File Offset: 0x000477FD
		public bool Remote
		{
			set
			{
				base.AddParameter("Remote", value);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x00049610 File Offset: 0x00047810
		public bool Outbound
		{
			set
			{
				base.AddParameter("Outbound", value);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00049623 File Offset: 0x00047823
		public bool PrimaryOnly
		{
			set
			{
				base.AddParameter("PrimaryOnly", value);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00049636 File Offset: 0x00047836
		public bool ArchiveOnly
		{
			set
			{
				base.AddParameter("ArchiveOnly", value);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x00049649 File Offset: 0x00047849
		public string TargetDatabase
		{
			set
			{
				base.AddParameter("TargetDatabase", value);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00049657 File Offset: 0x00047857
		public string ArchiveTargetDatabase
		{
			set
			{
				base.AddParameter("ArchiveTargetDatabase", value);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x00049665 File Offset: 0x00047865
		public string RemoteTargetDatabase
		{
			set
			{
				base.AddParameter("RemoteTargetDatabase", value);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00049673 File Offset: 0x00047873
		public string RemoteArchiveTargetDatabase
		{
			set
			{
				base.AddParameter("RemoteArchiveTargetDatabase", value);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x00049681 File Offset: 0x00047881
		public bool IgnoreTenantMigrationPolicies
		{
			set
			{
				base.AddParameter("IgnoreTenantMigrationPolicies", value);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00049694 File Offset: 0x00047894
		public string TargetDeliveryDomain
		{
			set
			{
				base.AddParameter("TargetDeliveryDomain", value);
			}
		}

		// Token: 0x04000603 RID: 1539
		public const string CmdletName = "New-MoveRequest";

		// Token: 0x04000604 RID: 1540
		private static readonly Type[] ExceptionsToIgnore = new Type[0];
	}
}

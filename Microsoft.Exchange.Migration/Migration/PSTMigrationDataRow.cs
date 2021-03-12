using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B4 RID: 180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSTMigrationDataRow : IMigrationDataRow
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00029B78 File Offset: 0x00027D78
		public PSTMigrationDataRow(int rowIndex, string pstFilePath, SmtpAddress targetMailboxAddress, MigrationMailboxType targetMailboxType, string sourceRootFolder, string targetRootFolder)
		{
			MigrationUtil.ThrowOnNullArgument(pstFilePath, "pstFilePath");
			if (rowIndex < 1)
			{
				throw new ArgumentException("RowIndex should not be less than 1");
			}
			this.CursorPosition = rowIndex;
			this.PSTFilePath = pstFilePath;
			this.TargetMailboxAddress = targetMailboxAddress;
			this.TargetMailboxType = targetMailboxType;
			this.SourceRootFolder = sourceRootFolder;
			this.TargetRootFolder = targetRootFolder;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00029BD2 File Offset: 0x00027DD2
		public MigrationType MigrationType
		{
			get
			{
				return MigrationType.PSTImport;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00029BD6 File Offset: 0x00027DD6
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00029BD9 File Offset: 0x00027DD9
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00029BE1 File Offset: 0x00027DE1
		public int CursorPosition { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00029BEA File Offset: 0x00027DEA
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00029BF2 File Offset: 0x00027DF2
		public string PSTFilePath { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00029BFB File Offset: 0x00027DFB
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00029C03 File Offset: 0x00027E03
		public SmtpAddress TargetMailboxAddress { get; private set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00029C0C File Offset: 0x00027E0C
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00029C14 File Offset: 0x00027E14
		public MigrationMailboxType TargetMailboxType { get; private set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00029C1D File Offset: 0x00027E1D
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00029C25 File Offset: 0x00027E25
		public string SourceRootFolder { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00029C2E File Offset: 0x00027E2E
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00029C36 File Offset: 0x00027E36
		public string TargetRootFolder { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00029C40 File Offset: 0x00027E40
		public string Identifier
		{
			get
			{
				string arg = this.PSTFilePath;
				int num = this.PSTFilePath.LastIndexOf("\\");
				if (num != -1)
				{
					arg = this.PSTFilePath.Substring(num + 1);
				}
				return string.Format("{0}_{1}:{2}", this.TargetMailboxAddress.ToString().ToLowerInvariant(), arg, this.CursorPosition.ToString());
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00029CAC File Offset: 0x00027EAC
		public string LocalMailboxIdentifier
		{
			get
			{
				return this.TargetMailboxAddress.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00029CD2 File Offset: 0x00027ED2
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00029CD5 File Offset: 0x00027ED5
		public string RemoteIdentifier
		{
			get
			{
				return null;
			}
		}
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A5 RID: 1189
	public enum DataMoveReplicationConstraintParameter
	{
		// Token: 0x040024A3 RID: 9379
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintNone)]
		None,
		// Token: 0x040024A4 RID: 9380
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintSecondCopy)]
		SecondCopy,
		// Token: 0x040024A5 RID: 9381
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintSecondDatacenter)]
		SecondDatacenter = 3,
		// Token: 0x040024A6 RID: 9382
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintAllDatacenters)]
		AllDatacenters,
		// Token: 0x040024A7 RID: 9383
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintAllCopies)]
		AllCopies,
		// Token: 0x040024A8 RID: 9384
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintCINoReplication)]
		CINoReplication = 65536,
		// Token: 0x040024A9 RID: 9385
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintCISecondCopy)]
		CISecondCopy,
		// Token: 0x040024AA RID: 9386
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintCISecondDatacenter)]
		CISecondDatacenter = 65539,
		// Token: 0x040024AB RID: 9387
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintCIAllDatacenters)]
		CIAllDatacenters,
		// Token: 0x040024AC RID: 9388
		[LocDescription(DirectoryStrings.IDs.DataMoveReplicationConstraintCIAllCopies)]
		CIAllCopies
	}
}

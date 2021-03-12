using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000013 RID: 19
	public static class DxStoreConstants
	{
		// Token: 0x04000029 RID: 41
		public const string ServiceNamespace = "http://www.outlook.com/highavailability/dxstore/v1/";

		// Token: 0x0400002A RID: 42
		public const string DefaultGroupIdentifier = "B1563499-EA40-4101-A9E6-59A8EB26FF1E";

		// Token: 0x0400002B RID: 43
		public const string DxStoreAccessName = "Access";

		// Token: 0x0400002C RID: 44
		public const string DxStoreInstanceName = "Instance";

		// Token: 0x0400002D RID: 45
		public const string DxStoreManagerName = "Manager";

		// Token: 0x0400002E RID: 46
		public const string RootKeyName = "\\";

		// Token: 0x0400002F RID: 47
		public const string SnapshotElementName = "SnapshotRoot";

		// Token: 0x04000030 RID: 48
		public const string ContainerRootTag = "Root";

		// Token: 0x04000031 RID: 49
		public const string PrivateSectionLabel = "Private";

		// Token: 0x04000032 RID: 50
		public const string PublicSectionLabel = "Public";

		// Token: 0x04000033 RID: 51
		public const string DefaultGroupNameProperty = "DefaultGroupName";

		// Token: 0x04000034 RID: 52
		public const int DefaultEndPointPortNumber = 808;

		// Token: 0x04000035 RID: 53
		public const string DefaultEndPointProtocolName = "net.tcp";

		// Token: 0x04000036 RID: 54
		public const string DefaultManagerBaseKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\DxStore";

		// Token: 0x04000037 RID: 55
		public const string DefaultSnapshotFileName = "DxStoreSnapshot.xml";

		// Token: 0x04000038 RID: 56
		public const int InstanceProcessExitDelayInMs = 500;
	}
}

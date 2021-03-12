using System;

namespace Microsoft.Exchange.AirSync.Wbxml
{
	// Token: 0x020002A6 RID: 678
	internal abstract class WbxmlSchema
	{
		// Token: 0x060018A9 RID: 6313 RVA: 0x000920E0 File Offset: 0x000902E0
		public WbxmlSchema()
		{
			throw new WbxmlException("WbxmlSchema should never be created.  Create one of the derived classes.");
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000920F2 File Offset: 0x000902F2
		protected WbxmlSchema(int airSyncVersion)
		{
			this.version = airSyncVersion;
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00092101 File Offset: 0x00090301
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060018AC RID: 6316
		public abstract string GetName(int tag);

		// Token: 0x060018AD RID: 6317
		public abstract string GetNameSpace(int tag);

		// Token: 0x060018AE RID: 6318
		public abstract int GetTag(string nameSpace, string name);

		// Token: 0x060018AF RID: 6319
		public abstract bool IsTagSecure(int tag);

		// Token: 0x060018B0 RID: 6320
		public abstract bool IsTagAnOpaqueBlob(int tag);

		// Token: 0x0400118A RID: 4490
		private int version;
	}
}

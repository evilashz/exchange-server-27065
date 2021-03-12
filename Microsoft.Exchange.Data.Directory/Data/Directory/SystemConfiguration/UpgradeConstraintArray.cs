using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005EC RID: 1516
	[Serializable]
	public class UpgradeConstraintArray : XMLSerializableBase
	{
		// Token: 0x060047F5 RID: 18421 RVA: 0x001094BE File Offset: 0x001076BE
		public UpgradeConstraintArray() : this(null)
		{
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x001094C7 File Offset: 0x001076C7
		public UpgradeConstraintArray(UpgradeConstraint[] array)
		{
			this.UpgradeConstraints = array;
		}

		// Token: 0x170017B2 RID: 6066
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x001094D6 File Offset: 0x001076D6
		// (set) Token: 0x060047F8 RID: 18424 RVA: 0x001094DE File Offset: 0x001076DE
		[XmlArray("UpgradeConstraints")]
		[XmlArrayItem("UpgradeConstraint")]
		public UpgradeConstraint[] UpgradeConstraints { get; set; }
	}
}

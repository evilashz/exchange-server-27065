using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000561 RID: 1377
	[Serializable]
	public class RelocationConstraintArray : XMLSerializableBase
	{
		// Token: 0x06003DE5 RID: 15845 RVA: 0x000EB8F4 File Offset: 0x000E9AF4
		public RelocationConstraintArray() : this(null)
		{
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000EB8FD File Offset: 0x000E9AFD
		public RelocationConstraintArray(RelocationConstraint[] array)
		{
			this.RelocationConstraints = array;
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000EB90C File Offset: 0x000E9B0C
		// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000EB914 File Offset: 0x000E9B14
		[XmlArrayItem("RelocationConstraint")]
		[XmlArray("RelocationConstraintsCollection")]
		public RelocationConstraint[] RelocationConstraints { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E4 RID: 484
	[Serializable]
	public sealed class EsentColumnInRelationshipException : EsentObsoleteException
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00013582 File Offset: 0x00011782
		public EsentColumnInRelationshipException() : base("Cannot delete, column participates in relationship", JET_err.ColumnInRelationship)
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00013594 File Offset: 0x00011794
		private EsentColumnInRelationshipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

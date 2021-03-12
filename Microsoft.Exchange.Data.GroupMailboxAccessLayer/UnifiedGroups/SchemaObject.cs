using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000053 RID: 83
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Directory")]
	public class SchemaObject
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00010122 File Offset: 0x0000E322
		protected SchemaObject()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001012A File Offset: 0x0000E32A
		protected SchemaObject(Guid schemaTypeId)
		{
			this.TypeId = schemaTypeId;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00010139 File Offset: 0x0000E339
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00010141 File Offset: 0x0000E341
		[DataMember]
		public Guid TypeId { get; protected set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0001014A File Offset: 0x0000E34A
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00010152 File Offset: 0x0000E352
		[DataMember]
		public bool IsModified { get; internal set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001015B File Offset: 0x0000E35B
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00010163 File Offset: 0x0000E363
		[DataMember]
		public bool IsInitialized { get; internal set; }

		// Token: 0x060002B2 RID: 690 RVA: 0x0001016C File Offset: 0x0000E36C
		internal virtual void ClearChanges()
		{
			this.IsModified = false;
		}
	}
}

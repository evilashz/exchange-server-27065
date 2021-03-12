using System;
using System.Runtime.Serialization;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	public class FilteringPredicate : IExtensibleDataObject, IVerifiable
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000C1CE File Offset: 0x0000A3CE
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000C1D6 File Offset: 0x0000A3D6
		[DataMember(EmitDefaultValue = false)]
		public string Component { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000C1DF File Offset: 0x0000A3DF
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000C1E7 File Offset: 0x0000A3E7
		[DataMember(EmitDefaultValue = false)]
		public string Tenant { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		[DataMember(EmitDefaultValue = false)]
		public string User { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000C201 File Offset: 0x0000A401
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000C209 File Offset: 0x0000A409
		[DataMember(EmitDefaultValue = false)]
		public string Operation { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000C212 File Offset: 0x0000A412
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000C21A File Offset: 0x0000A41A
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C223 File Offset: 0x0000A423
		public virtual void Initialize()
		{
			this.Component = "*";
			this.Tenant = "*";
			this.User = "*";
			this.Operation = "*";
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000C251 File Offset: 0x0000A451
		public virtual void Validate()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C253 File Offset: 0x0000A453
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
}

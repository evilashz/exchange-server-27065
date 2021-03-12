using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003FB RID: 1019
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RestoreUserError", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class RestoreUserError : IExtensibleDataObject
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0008D7C7 File Offset: 0x0008B9C7
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0008D7CF File Offset: 0x0008B9CF
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0008D7D8 File Offset: 0x0008B9D8
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x0008D7E0 File Offset: 0x0008B9E0
		[DataMember]
		public Guid? ConflictingObjectId
		{
			get
			{
				return this.ConflictingObjectIdField;
			}
			set
			{
				this.ConflictingObjectIdField = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0008D7E9 File Offset: 0x0008B9E9
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x0008D7F1 File Offset: 0x0008B9F1
		[DataMember]
		public string CurrentValue
		{
			get
			{
				return this.CurrentValueField;
			}
			set
			{
				this.CurrentValueField = value;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0008D7FA File Offset: 0x0008B9FA
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0008D802 File Offset: 0x0008BA02
		[DataMember]
		public string ErrorId
		{
			get
			{
				return this.ErrorIdField;
			}
			set
			{
				this.ErrorIdField = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x0008D80B File Offset: 0x0008BA0B
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x0008D813 File Offset: 0x0008BA13
		[DataMember]
		public string ErrorType
		{
			get
			{
				return this.ErrorTypeField;
			}
			set
			{
				this.ErrorTypeField = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0008D81C File Offset: 0x0008BA1C
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x0008D824 File Offset: 0x0008BA24
		[DataMember]
		public string ObjectType
		{
			get
			{
				return this.ObjectTypeField;
			}
			set
			{
				this.ObjectTypeField = value;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x0008D82D File Offset: 0x0008BA2D
		// (set) Token: 0x06001945 RID: 6469 RVA: 0x0008D835 File Offset: 0x0008BA35
		[DataMember]
		public string SuggestedValue
		{
			get
			{
				return this.SuggestedValueField;
			}
			set
			{
				this.SuggestedValueField = value;
			}
		}

		// Token: 0x040011A7 RID: 4519
		private ExtensionDataObject extensionDataField;

		// Token: 0x040011A8 RID: 4520
		private Guid? ConflictingObjectIdField;

		// Token: 0x040011A9 RID: 4521
		private string CurrentValueField;

		// Token: 0x040011AA RID: 4522
		private string ErrorIdField;

		// Token: 0x040011AB RID: 4523
		private string ErrorTypeField;

		// Token: 0x040011AC RID: 4524
		private string ObjectTypeField;

		// Token: 0x040011AD RID: 4525
		private string SuggestedValueField;
	}
}

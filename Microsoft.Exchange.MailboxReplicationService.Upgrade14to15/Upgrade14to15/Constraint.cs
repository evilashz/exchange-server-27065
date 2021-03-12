using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200007D RID: 125
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Constraint", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	public class Constraint : IExtensibleDataObject
	{
		// Token: 0x06000307 RID: 775 RVA: 0x00004018 File Offset: 0x00002218
		public Constraint(string constraintName, string owner, DateTime fixByDate, ConstraintStatus status, bool isBlocking, string comment)
		{
			this.ConstraintName = constraintName;
			this.Owner = owner;
			this.FixByDate = fixByDate;
			this.Status = status;
			this.IsBlocking = isBlocking;
			this.Comment = comment;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000404D File Offset: 0x0000224D
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00004055 File Offset: 0x00002255
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000405E File Offset: 0x0000225E
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00004066 File Offset: 0x00002266
		[DataMember]
		public string Comment
		{
			get
			{
				return this.CommentField;
			}
			set
			{
				this.CommentField = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000406F File Offset: 0x0000226F
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00004077 File Offset: 0x00002277
		[DataMember]
		public string ConstraintName
		{
			get
			{
				return this.ConstraintNameField;
			}
			set
			{
				this.ConstraintNameField = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00004080 File Offset: 0x00002280
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00004088 File Offset: 0x00002288
		[DataMember]
		public DateTime FixByDate
		{
			get
			{
				return this.FixByDateField;
			}
			set
			{
				this.FixByDateField = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00004091 File Offset: 0x00002291
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00004099 File Offset: 0x00002299
		[DataMember]
		public bool IsBlocking
		{
			get
			{
				return this.IsBlockingField;
			}
			set
			{
				this.IsBlockingField = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000040A2 File Offset: 0x000022A2
		// (set) Token: 0x06000313 RID: 787 RVA: 0x000040AA File Offset: 0x000022AA
		[DataMember]
		public string Owner
		{
			get
			{
				return this.OwnerField;
			}
			set
			{
				this.OwnerField = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000040B3 File Offset: 0x000022B3
		// (set) Token: 0x06000315 RID: 789 RVA: 0x000040BB File Offset: 0x000022BB
		[DataMember]
		public ConstraintStatus Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x04000162 RID: 354
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000163 RID: 355
		private string CommentField;

		// Token: 0x04000164 RID: 356
		private string ConstraintNameField;

		// Token: 0x04000165 RID: 357
		private DateTime FixByDateField;

		// Token: 0x04000166 RID: 358
		private bool IsBlockingField;

		// Token: 0x04000167 RID: 359
		private string OwnerField;

		// Token: 0x04000168 RID: 360
		private ConstraintStatus StatusField;
	}
}

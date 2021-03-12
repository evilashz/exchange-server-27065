using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000242 RID: 578
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class TaskType : ItemType
	{
		// Token: 0x04000F4A RID: 3914
		public int ActualWork;

		// Token: 0x04000F4B RID: 3915
		[XmlIgnore]
		public bool ActualWorkSpecified;

		// Token: 0x04000F4C RID: 3916
		public DateTime AssignedTime;

		// Token: 0x04000F4D RID: 3917
		[XmlIgnore]
		public bool AssignedTimeSpecified;

		// Token: 0x04000F4E RID: 3918
		public string BillingInformation;

		// Token: 0x04000F4F RID: 3919
		public int ChangeCount;

		// Token: 0x04000F50 RID: 3920
		[XmlIgnore]
		public bool ChangeCountSpecified;

		// Token: 0x04000F51 RID: 3921
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Companies;

		// Token: 0x04000F52 RID: 3922
		public DateTime CompleteDate;

		// Token: 0x04000F53 RID: 3923
		[XmlIgnore]
		public bool CompleteDateSpecified;

		// Token: 0x04000F54 RID: 3924
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Contacts;

		// Token: 0x04000F55 RID: 3925
		public TaskDelegateStateType DelegationState;

		// Token: 0x04000F56 RID: 3926
		[XmlIgnore]
		public bool DelegationStateSpecified;

		// Token: 0x04000F57 RID: 3927
		public string Delegator;

		// Token: 0x04000F58 RID: 3928
		public DateTime DueDate;

		// Token: 0x04000F59 RID: 3929
		[XmlIgnore]
		public bool DueDateSpecified;

		// Token: 0x04000F5A RID: 3930
		public int IsAssignmentEditable;

		// Token: 0x04000F5B RID: 3931
		[XmlIgnore]
		public bool IsAssignmentEditableSpecified;

		// Token: 0x04000F5C RID: 3932
		public bool IsComplete;

		// Token: 0x04000F5D RID: 3933
		[XmlIgnore]
		public bool IsCompleteSpecified;

		// Token: 0x04000F5E RID: 3934
		public bool IsRecurring;

		// Token: 0x04000F5F RID: 3935
		[XmlIgnore]
		public bool IsRecurringSpecified;

		// Token: 0x04000F60 RID: 3936
		public bool IsTeamTask;

		// Token: 0x04000F61 RID: 3937
		[XmlIgnore]
		public bool IsTeamTaskSpecified;

		// Token: 0x04000F62 RID: 3938
		public string Mileage;

		// Token: 0x04000F63 RID: 3939
		public string Owner;

		// Token: 0x04000F64 RID: 3940
		public double PercentComplete;

		// Token: 0x04000F65 RID: 3941
		[XmlIgnore]
		public bool PercentCompleteSpecified;

		// Token: 0x04000F66 RID: 3942
		public TaskRecurrenceType Recurrence;

		// Token: 0x04000F67 RID: 3943
		public DateTime StartDate;

		// Token: 0x04000F68 RID: 3944
		[XmlIgnore]
		public bool StartDateSpecified;

		// Token: 0x04000F69 RID: 3945
		public TaskStatusType Status;

		// Token: 0x04000F6A RID: 3946
		[XmlIgnore]
		public bool StatusSpecified;

		// Token: 0x04000F6B RID: 3947
		public string StatusDescription;

		// Token: 0x04000F6C RID: 3948
		public int TotalWork;

		// Token: 0x04000F6D RID: 3949
		[XmlIgnore]
		public bool TotalWorkSpecified;
	}
}

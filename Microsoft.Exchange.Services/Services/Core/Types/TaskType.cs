using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000673 RID: 1651
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Task")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TaskType : ItemType
	{
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000B7B51 File Offset: 0x000B5D51
		// (set) Token: 0x06003270 RID: 12912 RVA: 0x000B7B63 File Offset: 0x000B5D63
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 1)]
		public int ActualWork
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(TaskSchema.ActualWork);
			}
			set
			{
				base.PropertyBag[TaskSchema.ActualWork] = value;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000B7B7B File Offset: 0x000B5D7B
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x000B7B8D File Offset: 0x000B5D8D
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ActualWorkSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.ActualWork);
			}
			set
			{
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x000B7B8F File Offset: 0x000B5D8F
		// (set) Token: 0x06003274 RID: 12916 RVA: 0x000B7BA1 File Offset: 0x000B5DA1
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 2)]
		public string AssignedTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.AssignedTime);
			}
			set
			{
				base.PropertyBag[TaskSchema.AssignedTime] = value;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000B7BB4 File Offset: 0x000B5DB4
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x000B7BC6 File Offset: 0x000B5DC6
		[XmlIgnore]
		[IgnoreDataMember]
		public bool AssignedTimeSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.AssignedTime);
			}
			set
			{
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000B7BC8 File Offset: 0x000B5DC8
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000B7BDA File Offset: 0x000B5DDA
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 3)]
		public string BillingInformation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.BillingInformation);
			}
			set
			{
				base.PropertyBag[TaskSchema.BillingInformation] = value;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000B7BED File Offset: 0x000B5DED
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x000B7BFF File Offset: 0x000B5DFF
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 4)]
		public int ChangeCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(TaskSchema.ChangeCount);
			}
			set
			{
				base.PropertyBag[TaskSchema.ChangeCount] = value;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000B7C17 File Offset: 0x000B5E17
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000B7C29 File Offset: 0x000B5E29
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ChangeCountSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.ChangeCount);
			}
			set
			{
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000B7C2B File Offset: 0x000B5E2B
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000B7C3D File Offset: 0x000B5E3D
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 5)]
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Companies
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(TaskSchema.Companies);
			}
			set
			{
				base.PropertyBag[TaskSchema.Companies] = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000B7C50 File Offset: 0x000B5E50
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000B7C62 File Offset: 0x000B5E62
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 6)]
		public string CompleteDate
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.CompleteDate);
			}
			set
			{
				base.PropertyBag[TaskSchema.CompleteDate] = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000B7C75 File Offset: 0x000B5E75
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000B7C87 File Offset: 0x000B5E87
		[XmlIgnore]
		[IgnoreDataMember]
		public bool CompleteDateSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.CompleteDate);
			}
			set
			{
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000B7C89 File Offset: 0x000B5E89
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000B7C9B File Offset: 0x000B5E9B
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 7)]
		public string[] Contacts
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(TaskSchema.Contacts);
			}
			set
			{
				base.PropertyBag[TaskSchema.Contacts] = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000B7CAE File Offset: 0x000B5EAE
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000B7CC5 File Offset: 0x000B5EC5
		[XmlElement]
		[IgnoreDataMember]
		public TaskDelegateStateType DelegationState
		{
			get
			{
				if (!this.DelegationStateSpecified)
				{
					return TaskDelegateStateType.NoMatch;
				}
				return EnumUtilities.Parse<TaskDelegateStateType>(this.DelegationStateString);
			}
			set
			{
				base.PropertyBag[TaskSchema.DelegationState] = value;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000B7CDD File Offset: 0x000B5EDD
		// (set) Token: 0x06003288 RID: 12936 RVA: 0x000B7CEF File Offset: 0x000B5EEF
		[DataMember(Name = "DelegationState", EmitDefaultValue = false, Order = 8)]
		[XmlIgnore]
		public string DelegationStateString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.DelegationState);
			}
			set
			{
				base.PropertyBag[TaskSchema.DelegationState] = value;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000B7D02 File Offset: 0x000B5F02
		// (set) Token: 0x0600328A RID: 12938 RVA: 0x000B7D0F File Offset: 0x000B5F0F
		[IgnoreDataMember]
		[XmlIgnore]
		public bool DelegationStateSpecified
		{
			get
			{
				return base.IsSet(TaskSchema.DelegationState);
			}
			set
			{
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000B7D11 File Offset: 0x000B5F11
		// (set) Token: 0x0600328C RID: 12940 RVA: 0x000B7D23 File Offset: 0x000B5F23
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 9)]
		public string Delegator
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.Delegator);
			}
			set
			{
				base.PropertyBag[TaskSchema.Delegator] = value;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000B7D36 File Offset: 0x000B5F36
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x000B7D48 File Offset: 0x000B5F48
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 10)]
		[DateTimeString]
		public string DueDate
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.DueDate);
			}
			set
			{
				base.PropertyBag[TaskSchema.DueDate] = value;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x000B7D5B File Offset: 0x000B5F5B
		// (set) Token: 0x06003290 RID: 12944 RVA: 0x000B7D6D File Offset: 0x000B5F6D
		[IgnoreDataMember]
		[XmlIgnore]
		public bool DueDateSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.DueDate);
			}
			set
			{
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000B7D6F File Offset: 0x000B5F6F
		// (set) Token: 0x06003292 RID: 12946 RVA: 0x000B7D81 File Offset: 0x000B5F81
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 11)]
		public int IsAssignmentEditable
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(TaskSchema.IsAssignmentEditable);
			}
			set
			{
				base.PropertyBag[TaskSchema.IsAssignmentEditable] = value;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000B7D99 File Offset: 0x000B5F99
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x000B7DAB File Offset: 0x000B5FAB
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsAssignmentEditableSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.IsAssignmentEditable);
			}
			set
			{
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000B7DAD File Offset: 0x000B5FAD
		// (set) Token: 0x06003296 RID: 12950 RVA: 0x000B7DBF File Offset: 0x000B5FBF
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 12)]
		public bool IsComplete
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool>(TaskSchema.IsComplete);
			}
			set
			{
				base.PropertyBag[TaskSchema.IsComplete] = value;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000B7DD7 File Offset: 0x000B5FD7
		// (set) Token: 0x06003298 RID: 12952 RVA: 0x000B7DE9 File Offset: 0x000B5FE9
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsCompleteSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.IsComplete);
			}
			set
			{
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000B7DEB File Offset: 0x000B5FEB
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x000B7DFD File Offset: 0x000B5FFD
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 13)]
		public bool IsRecurring
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool>(TaskSchema.IsRecurring);
			}
			set
			{
				base.PropertyBag[TaskSchema.IsRecurring] = value;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000B7E15 File Offset: 0x000B6015
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x000B7E27 File Offset: 0x000B6027
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsRecurringSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.IsRecurring);
			}
			set
			{
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000B7E29 File Offset: 0x000B6029
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x000B7E3B File Offset: 0x000B603B
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 14)]
		public bool IsTeamTask
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool>(TaskSchema.IsTeamTask);
			}
			set
			{
				base.PropertyBag[TaskSchema.IsTeamTask] = value;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000B7E53 File Offset: 0x000B6053
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x000B7E65 File Offset: 0x000B6065
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsTeamTaskSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.IsTeamTask);
			}
			set
			{
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000B7E67 File Offset: 0x000B6067
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000B7E79 File Offset: 0x000B6079
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 15)]
		public string Mileage
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.Mileage);
			}
			set
			{
				base.PropertyBag[TaskSchema.Mileage] = value;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000B7E8C File Offset: 0x000B608C
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x000B7E9E File Offset: 0x000B609E
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 16)]
		public string Owner
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.Owner);
			}
			set
			{
				base.PropertyBag[TaskSchema.Owner] = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000B7EB1 File Offset: 0x000B60B1
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x000B7EC3 File Offset: 0x000B60C3
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 17)]
		public string PercentComplete
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.PercentComplete);
			}
			set
			{
				base.PropertyBag[TaskSchema.PercentComplete] = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060032A7 RID: 12967 RVA: 0x000B7ED6 File Offset: 0x000B60D6
		// (set) Token: 0x060032A8 RID: 12968 RVA: 0x000B7EE8 File Offset: 0x000B60E8
		[XmlIgnore]
		[IgnoreDataMember]
		public bool PercentCompleteSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.PercentComplete);
			}
			set
			{
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000B7EEA File Offset: 0x000B60EA
		// (set) Token: 0x060032AA RID: 12970 RVA: 0x000B7EFC File Offset: 0x000B60FC
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 18)]
		public TaskRecurrenceType Recurrence
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<TaskRecurrenceType>(TaskSchema.Recurrence);
			}
			set
			{
				base.PropertyBag[TaskSchema.Recurrence] = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000B7F0F File Offset: 0x000B610F
		// (set) Token: 0x060032AC RID: 12972 RVA: 0x000B7F21 File Offset: 0x000B6121
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 19)]
		public string StartDate
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.StartDate);
			}
			set
			{
				base.PropertyBag[TaskSchema.StartDate] = value;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x000B7F34 File Offset: 0x000B6134
		// (set) Token: 0x060032AE RID: 12974 RVA: 0x000B7F46 File Offset: 0x000B6146
		[XmlIgnore]
		[IgnoreDataMember]
		public bool StartDateSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.StartDate);
			}
			set
			{
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000B7F48 File Offset: 0x000B6148
		// (set) Token: 0x060032B0 RID: 12976 RVA: 0x000B7F5F File Offset: 0x000B615F
		[XmlElement]
		[IgnoreDataMember]
		public TaskStatusType Status
		{
			get
			{
				if (!this.StatusSpecified)
				{
					return TaskStatusType.NotStarted;
				}
				return EnumUtilities.Parse<TaskStatusType>(this.StatusString);
			}
			set
			{
				base.PropertyBag[TaskSchema.Status] = value;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060032B1 RID: 12977 RVA: 0x000B7F77 File Offset: 0x000B6177
		// (set) Token: 0x060032B2 RID: 12978 RVA: 0x000B7F89 File Offset: 0x000B6189
		[XmlIgnore]
		[DataMember(Name = "Status", EmitDefaultValue = false, Order = 20)]
		public string StatusString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.Status);
			}
			set
			{
				base.PropertyBag[TaskSchema.Status] = value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060032B3 RID: 12979 RVA: 0x000B7F9C File Offset: 0x000B619C
		// (set) Token: 0x060032B4 RID: 12980 RVA: 0x000B7FA9 File Offset: 0x000B61A9
		[IgnoreDataMember]
		[XmlIgnore]
		public bool StatusSpecified
		{
			get
			{
				return base.IsSet(TaskSchema.Status);
			}
			set
			{
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000B7FAB File Offset: 0x000B61AB
		// (set) Token: 0x060032B6 RID: 12982 RVA: 0x000B7FBD File Offset: 0x000B61BD
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 21)]
		public string StatusDescription
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.StatusDescription);
			}
			set
			{
				base.PropertyBag[TaskSchema.StatusDescription] = value;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000B7FD0 File Offset: 0x000B61D0
		// (set) Token: 0x060032B8 RID: 12984 RVA: 0x000B7FE2 File Offset: 0x000B61E2
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 22)]
		public int TotalWork
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int>(TaskSchema.TotalWork);
			}
			set
			{
				base.PropertyBag[TaskSchema.TotalWork] = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000B7FFA File Offset: 0x000B61FA
		// (set) Token: 0x060032BA RID: 12986 RVA: 0x000B800C File Offset: 0x000B620C
		[XmlIgnore]
		[IgnoreDataMember]
		public bool TotalWorkSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.TotalWork);
			}
			set
			{
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060032BB RID: 12987 RVA: 0x000B800E File Offset: 0x000B620E
		// (set) Token: 0x060032BC RID: 12988 RVA: 0x000B8020 File Offset: 0x000B6220
		[DataMember(EmitDefaultValue = false, Order = 23)]
		[XmlIgnore]
		public ModernReminderType[] ModernReminders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ModernReminderType[]>(TaskSchema.ModernReminders);
			}
			set
			{
				base.PropertyBag[TaskSchema.ModernReminders] = value;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060032BD RID: 12989 RVA: 0x000B8033 File Offset: 0x000B6233
		// (set) Token: 0x060032BE RID: 12990 RVA: 0x000B8045 File Offset: 0x000B6245
		[DateTimeString]
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 24)]
		public string DoItTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(TaskSchema.DoItTime);
			}
			set
			{
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000B8047 File Offset: 0x000B6247
		// (set) Token: 0x060032C0 RID: 12992 RVA: 0x000B8059 File Offset: 0x000B6259
		[XmlIgnore]
		[IgnoreDataMember]
		public bool DoItTimeSpecified
		{
			get
			{
				return base.PropertyBag.Contains(TaskSchema.DoItTime);
			}
			set
			{
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000B805B File Offset: 0x000B625B
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.Task;
			}
		}
	}
}

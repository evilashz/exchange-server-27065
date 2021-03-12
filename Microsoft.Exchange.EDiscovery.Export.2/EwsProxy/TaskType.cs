using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000161 RID: 353
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class TaskType : ItemType
	{
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x000236A4 File Offset: 0x000218A4
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x000236AC File Offset: 0x000218AC
		public int ActualWork
		{
			get
			{
				return this.actualWorkField;
			}
			set
			{
				this.actualWorkField = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000236B5 File Offset: 0x000218B5
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x000236BD File Offset: 0x000218BD
		[XmlIgnore]
		public bool ActualWorkSpecified
		{
			get
			{
				return this.actualWorkFieldSpecified;
			}
			set
			{
				this.actualWorkFieldSpecified = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x000236C6 File Offset: 0x000218C6
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x000236CE File Offset: 0x000218CE
		public DateTime AssignedTime
		{
			get
			{
				return this.assignedTimeField;
			}
			set
			{
				this.assignedTimeField = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x000236D7 File Offset: 0x000218D7
		// (set) Token: 0x06001001 RID: 4097 RVA: 0x000236DF File Offset: 0x000218DF
		[XmlIgnore]
		public bool AssignedTimeSpecified
		{
			get
			{
				return this.assignedTimeFieldSpecified;
			}
			set
			{
				this.assignedTimeFieldSpecified = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x000236E8 File Offset: 0x000218E8
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x000236F0 File Offset: 0x000218F0
		public string BillingInformation
		{
			get
			{
				return this.billingInformationField;
			}
			set
			{
				this.billingInformationField = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x000236F9 File Offset: 0x000218F9
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x00023701 File Offset: 0x00021901
		public int ChangeCount
		{
			get
			{
				return this.changeCountField;
			}
			set
			{
				this.changeCountField = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0002370A File Offset: 0x0002190A
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x00023712 File Offset: 0x00021912
		[XmlIgnore]
		public bool ChangeCountSpecified
		{
			get
			{
				return this.changeCountFieldSpecified;
			}
			set
			{
				this.changeCountFieldSpecified = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0002371B File Offset: 0x0002191B
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x00023723 File Offset: 0x00021923
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Companies
		{
			get
			{
				return this.companiesField;
			}
			set
			{
				this.companiesField = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0002372C File Offset: 0x0002192C
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x00023734 File Offset: 0x00021934
		public DateTime CompleteDate
		{
			get
			{
				return this.completeDateField;
			}
			set
			{
				this.completeDateField = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0002373D File Offset: 0x0002193D
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x00023745 File Offset: 0x00021945
		[XmlIgnore]
		public bool CompleteDateSpecified
		{
			get
			{
				return this.completeDateFieldSpecified;
			}
			set
			{
				this.completeDateFieldSpecified = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0002374E File Offset: 0x0002194E
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x00023756 File Offset: 0x00021956
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Contacts
		{
			get
			{
				return this.contactsField;
			}
			set
			{
				this.contactsField = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0002375F File Offset: 0x0002195F
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x00023767 File Offset: 0x00021967
		public TaskDelegateStateType DelegationState
		{
			get
			{
				return this.delegationStateField;
			}
			set
			{
				this.delegationStateField = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x00023770 File Offset: 0x00021970
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x00023778 File Offset: 0x00021978
		[XmlIgnore]
		public bool DelegationStateSpecified
		{
			get
			{
				return this.delegationStateFieldSpecified;
			}
			set
			{
				this.delegationStateFieldSpecified = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00023781 File Offset: 0x00021981
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00023789 File Offset: 0x00021989
		public string Delegator
		{
			get
			{
				return this.delegatorField;
			}
			set
			{
				this.delegatorField = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00023792 File Offset: 0x00021992
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0002379A File Offset: 0x0002199A
		public DateTime DueDate
		{
			get
			{
				return this.dueDateField;
			}
			set
			{
				this.dueDateField = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000237A3 File Offset: 0x000219A3
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000237AB File Offset: 0x000219AB
		[XmlIgnore]
		public bool DueDateSpecified
		{
			get
			{
				return this.dueDateFieldSpecified;
			}
			set
			{
				this.dueDateFieldSpecified = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000237B4 File Offset: 0x000219B4
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000237BC File Offset: 0x000219BC
		public int IsAssignmentEditable
		{
			get
			{
				return this.isAssignmentEditableField;
			}
			set
			{
				this.isAssignmentEditableField = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000237C5 File Offset: 0x000219C5
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000237CD File Offset: 0x000219CD
		[XmlIgnore]
		public bool IsAssignmentEditableSpecified
		{
			get
			{
				return this.isAssignmentEditableFieldSpecified;
			}
			set
			{
				this.isAssignmentEditableFieldSpecified = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x000237D6 File Offset: 0x000219D6
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x000237DE File Offset: 0x000219DE
		public bool IsComplete
		{
			get
			{
				return this.isCompleteField;
			}
			set
			{
				this.isCompleteField = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000237E7 File Offset: 0x000219E7
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x000237EF File Offset: 0x000219EF
		[XmlIgnore]
		public bool IsCompleteSpecified
		{
			get
			{
				return this.isCompleteFieldSpecified;
			}
			set
			{
				this.isCompleteFieldSpecified = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x000237F8 File Offset: 0x000219F8
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x00023800 File Offset: 0x00021A00
		public bool IsRecurring
		{
			get
			{
				return this.isRecurringField;
			}
			set
			{
				this.isRecurringField = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00023809 File Offset: 0x00021A09
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x00023811 File Offset: 0x00021A11
		[XmlIgnore]
		public bool IsRecurringSpecified
		{
			get
			{
				return this.isRecurringFieldSpecified;
			}
			set
			{
				this.isRecurringFieldSpecified = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0002381A File Offset: 0x00021A1A
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x00023822 File Offset: 0x00021A22
		public bool IsTeamTask
		{
			get
			{
				return this.isTeamTaskField;
			}
			set
			{
				this.isTeamTaskField = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0002382B File Offset: 0x00021A2B
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x00023833 File Offset: 0x00021A33
		[XmlIgnore]
		public bool IsTeamTaskSpecified
		{
			get
			{
				return this.isTeamTaskFieldSpecified;
			}
			set
			{
				this.isTeamTaskFieldSpecified = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0002383C File Offset: 0x00021A3C
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x00023844 File Offset: 0x00021A44
		public string Mileage
		{
			get
			{
				return this.mileageField;
			}
			set
			{
				this.mileageField = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0002384D File Offset: 0x00021A4D
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00023855 File Offset: 0x00021A55
		public string Owner
		{
			get
			{
				return this.ownerField;
			}
			set
			{
				this.ownerField = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0002385E File Offset: 0x00021A5E
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x00023866 File Offset: 0x00021A66
		public double PercentComplete
		{
			get
			{
				return this.percentCompleteField;
			}
			set
			{
				this.percentCompleteField = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0002386F File Offset: 0x00021A6F
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x00023877 File Offset: 0x00021A77
		[XmlIgnore]
		public bool PercentCompleteSpecified
		{
			get
			{
				return this.percentCompleteFieldSpecified;
			}
			set
			{
				this.percentCompleteFieldSpecified = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00023880 File Offset: 0x00021A80
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x00023888 File Offset: 0x00021A88
		public TaskRecurrenceType Recurrence
		{
			get
			{
				return this.recurrenceField;
			}
			set
			{
				this.recurrenceField = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00023891 File Offset: 0x00021A91
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x00023899 File Offset: 0x00021A99
		public DateTime StartDate
		{
			get
			{
				return this.startDateField;
			}
			set
			{
				this.startDateField = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000238A2 File Offset: 0x00021AA2
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x000238AA File Offset: 0x00021AAA
		[XmlIgnore]
		public bool StartDateSpecified
		{
			get
			{
				return this.startDateFieldSpecified;
			}
			set
			{
				this.startDateFieldSpecified = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000238B3 File Offset: 0x00021AB3
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x000238BB File Offset: 0x00021ABB
		public TaskStatusType Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x000238C4 File Offset: 0x00021AC4
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x000238CC File Offset: 0x00021ACC
		[XmlIgnore]
		public bool StatusSpecified
		{
			get
			{
				return this.statusFieldSpecified;
			}
			set
			{
				this.statusFieldSpecified = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000238D5 File Offset: 0x00021AD5
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x000238DD File Offset: 0x00021ADD
		public string StatusDescription
		{
			get
			{
				return this.statusDescriptionField;
			}
			set
			{
				this.statusDescriptionField = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x000238E6 File Offset: 0x00021AE6
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x000238EE File Offset: 0x00021AEE
		public int TotalWork
		{
			get
			{
				return this.totalWorkField;
			}
			set
			{
				this.totalWorkField = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x000238F7 File Offset: 0x00021AF7
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x000238FF File Offset: 0x00021AFF
		[XmlIgnore]
		public bool TotalWorkSpecified
		{
			get
			{
				return this.totalWorkFieldSpecified;
			}
			set
			{
				this.totalWorkFieldSpecified = value;
			}
		}

		// Token: 0x04000AF8 RID: 2808
		private int actualWorkField;

		// Token: 0x04000AF9 RID: 2809
		private bool actualWorkFieldSpecified;

		// Token: 0x04000AFA RID: 2810
		private DateTime assignedTimeField;

		// Token: 0x04000AFB RID: 2811
		private bool assignedTimeFieldSpecified;

		// Token: 0x04000AFC RID: 2812
		private string billingInformationField;

		// Token: 0x04000AFD RID: 2813
		private int changeCountField;

		// Token: 0x04000AFE RID: 2814
		private bool changeCountFieldSpecified;

		// Token: 0x04000AFF RID: 2815
		private string[] companiesField;

		// Token: 0x04000B00 RID: 2816
		private DateTime completeDateField;

		// Token: 0x04000B01 RID: 2817
		private bool completeDateFieldSpecified;

		// Token: 0x04000B02 RID: 2818
		private string[] contactsField;

		// Token: 0x04000B03 RID: 2819
		private TaskDelegateStateType delegationStateField;

		// Token: 0x04000B04 RID: 2820
		private bool delegationStateFieldSpecified;

		// Token: 0x04000B05 RID: 2821
		private string delegatorField;

		// Token: 0x04000B06 RID: 2822
		private DateTime dueDateField;

		// Token: 0x04000B07 RID: 2823
		private bool dueDateFieldSpecified;

		// Token: 0x04000B08 RID: 2824
		private int isAssignmentEditableField;

		// Token: 0x04000B09 RID: 2825
		private bool isAssignmentEditableFieldSpecified;

		// Token: 0x04000B0A RID: 2826
		private bool isCompleteField;

		// Token: 0x04000B0B RID: 2827
		private bool isCompleteFieldSpecified;

		// Token: 0x04000B0C RID: 2828
		private bool isRecurringField;

		// Token: 0x04000B0D RID: 2829
		private bool isRecurringFieldSpecified;

		// Token: 0x04000B0E RID: 2830
		private bool isTeamTaskField;

		// Token: 0x04000B0F RID: 2831
		private bool isTeamTaskFieldSpecified;

		// Token: 0x04000B10 RID: 2832
		private string mileageField;

		// Token: 0x04000B11 RID: 2833
		private string ownerField;

		// Token: 0x04000B12 RID: 2834
		private double percentCompleteField;

		// Token: 0x04000B13 RID: 2835
		private bool percentCompleteFieldSpecified;

		// Token: 0x04000B14 RID: 2836
		private TaskRecurrenceType recurrenceField;

		// Token: 0x04000B15 RID: 2837
		private DateTime startDateField;

		// Token: 0x04000B16 RID: 2838
		private bool startDateFieldSpecified;

		// Token: 0x04000B17 RID: 2839
		private TaskStatusType statusField;

		// Token: 0x04000B18 RID: 2840
		private bool statusFieldSpecified;

		// Token: 0x04000B19 RID: 2841
		private string statusDescriptionField;

		// Token: 0x04000B1A RID: 2842
		private int totalWorkField;

		// Token: 0x04000B1B RID: 2843
		private bool totalWorkFieldSpecified;
	}
}

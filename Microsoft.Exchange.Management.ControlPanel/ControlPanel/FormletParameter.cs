using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200042F RID: 1071
	[KnownType(typeof(HiddenParameter))]
	[KnownType(typeof(IncidentReportContentParameter))]
	[DataContract]
	[KnownType(typeof(PeopleParameter))]
	[KnownType(typeof(TimePeriodParameter))]
	[KnownType(typeof(CallerIdsParameter))]
	[KnownType(typeof(ExtensionsDialedParameter))]
	[KnownType(typeof(KeyMappingsParameter))]
	[KnownType(typeof(StringParameter))]
	[KnownType(typeof(EnhancedEnumParameter))]
	[KnownType(typeof(EnumParameter))]
	[KnownType(typeof(NumberRangeParameter))]
	[KnownType(typeof(DateRangeParameter))]
	[KnownType(typeof(StringArrayParameter))]
	[KnownType(typeof(FolderParameter))]
	[KnownType(typeof(OUPickerParameter))]
	[KnownType(typeof(ObjectArrayParameter))]
	[KnownType(typeof(ObjectParameter))]
	[KnownType(typeof(BooleanParameter))]
	[KnownType(typeof(JournalRuleScopeParameter))]
	[KnownType(typeof(NotificationPhoneNumberParameter))]
	[KnownType(typeof(NumberEnumParameter))]
	[KnownType(typeof(NumberParameter))]
	[KnownType(typeof(ByteQuantifiedSizeParameter))]
	[KnownType(typeof(ADAttributeParameter))]
	[KnownType(typeof(ObjectsParameter))]
	[KnownType(typeof(DLPParameter))]
	[KnownType(typeof(SenderNotifyParameter))]
	public abstract class FormletParameter
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x000A6D34 File Offset: 0x000A4F34
		public FormletParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, string[] taskParameterNames)
		{
			this.Name = name;
			this.locDialogTitle = dialogTitle;
			this.locDialogLabel = dialogLabel;
			this.TaskParameterNames = taskParameterNames;
			this.EditorType = base.GetType().Name + "Editor";
			this.FormletType = null;
			this.RequiredField = true;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000A6D90 File Offset: 0x000A4F90
		public FormletParameter(string name, LocalizedString displayName, LocalizedString description) : this(name, displayName, description, new string[]
		{
			name
		})
		{
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000A6DB2 File Offset: 0x000A4FB2
		// (set) Token: 0x060035A1 RID: 13729 RVA: 0x000A6DBA File Offset: 0x000A4FBA
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000A6DC3 File Offset: 0x000A4FC3
		// (set) Token: 0x060035A3 RID: 13731 RVA: 0x000A6DCB File Offset: 0x000A4FCB
		public string[] TaskParameterNames { get; private set; }

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x060035A4 RID: 13732 RVA: 0x000A6DD4 File Offset: 0x000A4FD4
		// (set) Token: 0x060035A5 RID: 13733 RVA: 0x000A6DE7 File Offset: 0x000A4FE7
		[DataMember]
		public virtual string DialogTitle
		{
			get
			{
				return this.locDialogTitle.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x000A6DEE File Offset: 0x000A4FEE
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x000A6E01 File Offset: 0x000A5001
		[DataMember]
		public virtual string DialogLabel
		{
			get
			{
				return this.locDialogLabel.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000A6E08 File Offset: 0x000A5008
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x000A6E10 File Offset: 0x000A5010
		[DataMember]
		public string EditorType { get; protected set; }

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000A6E19 File Offset: 0x000A5019
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x000A6E21 File Offset: 0x000A5021
		public Type FormletType { get; protected set; }

		// Token: 0x060035AC RID: 13740 RVA: 0x000A6E2C File Offset: 0x000A502C
		internal static int GetIntFieldValue(Type strongType, string fieldName, int defaultValue)
		{
			FieldInfo field = strongType.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
			if (!(field == null))
			{
				return (int)field.GetValue(null);
			}
			return defaultValue;
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000A6E5C File Offset: 0x000A505C
		internal static string GetStringFieldValue(Type strongType, string fieldName, string defaultValue)
		{
			FieldInfo field = strongType.GetField(fieldName, BindingFlags.Static | BindingFlags.Public);
			if (!(field == null))
			{
				return field.GetValue(null).ToString();
			}
			return defaultValue;
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000A6E8A File Offset: 0x000A508A
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x000A6E9D File Offset: 0x000A509D
		[DataMember(EmitDefaultValue = false)]
		public string NoSelectionText
		{
			get
			{
				return this.noSelectionText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000A6EA4 File Offset: 0x000A50A4
		// (set) Token: 0x060035B1 RID: 13745 RVA: 0x000A6EAC File Offset: 0x000A50AC
		[DataMember(EmitDefaultValue = false)]
		public bool ExactMatch { get; protected set; }

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000A6EB5 File Offset: 0x000A50B5
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000A6EBD File Offset: 0x000A50BD
		[DataMember(EmitDefaultValue = false)]
		public bool RequiredField { get; set; }

		// Token: 0x040025A2 RID: 9634
		private LocalizedString locDialogTitle;

		// Token: 0x040025A3 RID: 9635
		private LocalizedString locDialogLabel;

		// Token: 0x040025A4 RID: 9636
		protected LocalizedString noSelectionText;
	}
}

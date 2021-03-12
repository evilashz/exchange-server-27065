using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ControlPanel.DataContracts;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000461 RID: 1121
	[DataContract]
	[KnownType(typeof(UMCallAnsweringRule))]
	public class UMCallAnsweringRule : RuleRow
	{
		// Token: 0x060038FB RID: 14587 RVA: 0x000ADC98 File Offset: 0x000ABE98
		public UMCallAnsweringRule(UMCallAnsweringRule rule) : base(rule)
		{
			this.Rule = rule;
			base.DescriptionObject = rule.Description;
			base.ConditionDescriptions = base.DescriptionObject.ConditionDescriptions.ToArray();
			base.ActionDescriptions = base.DescriptionObject.ActionDescriptions.ToArray();
			base.ExceptionDescriptions = base.DescriptionObject.ExceptionDescriptions.ToArray();
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000ADD01 File Offset: 0x000ABF01
		// (set) Token: 0x060038FD RID: 14589 RVA: 0x000ADD09 File Offset: 0x000ABF09
		public UMCallAnsweringRule Rule { get; private set; }

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000ADD14 File Offset: 0x000ABF14
		// (set) Token: 0x060038FF RID: 14591 RVA: 0x000ADD3E File Offset: 0x000ABF3E
		[DataMember(EmitDefaultValue = false)]
		public bool? CheckAutomaticReplies
		{
			get
			{
				if (!this.Rule.CheckAutomaticReplies)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000ADD45 File Offset: 0x000ABF45
		// (set) Token: 0x06003901 RID: 14593 RVA: 0x000ADD66 File Offset: 0x000ABF66
		[DataMember(EmitDefaultValue = false)]
		public TimeOfDayItem TimeOfDay
		{
			get
			{
				if (this.Rule.TimeOfDay == null)
				{
					return null;
				}
				return new TimeOfDayItem(this.Rule.TimeOfDay);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000ADD75 File Offset: 0x000ABF75
		// (set) Token: 0x06003903 RID: 14595 RVA: 0x000ADDA9 File Offset: 0x000ABFA9
		[DataMember(EmitDefaultValue = false)]
		public Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem[] CallerIds
		{
			get
			{
				return Array.ConvertAll<Microsoft.Exchange.Data.CallerIdItem, Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem>(this.Rule.CallerIds.ToArray(), (Microsoft.Exchange.Data.CallerIdItem x) => new Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem(x));
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000ADDB0 File Offset: 0x000ABFB0
		// (set) Token: 0x06003905 RID: 14597 RVA: 0x000ADDC2 File Offset: 0x000ABFC2
		[DataMember(EmitDefaultValue = false)]
		public string[] ExtensionsDialed
		{
			get
			{
				return this.Rule.ExtensionsDialed.ToArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000ADDCC File Offset: 0x000ABFCC
		// (set) Token: 0x06003907 RID: 14599 RVA: 0x000ADE16 File Offset: 0x000AC016
		[DataMember(EmitDefaultValue = false)]
		public string[] ScheduleStatus
		{
			get
			{
				FreeBusyStatusEnum scheduleStatus = (FreeBusyStatusEnum)this.Rule.ScheduleStatus;
				if (scheduleStatus == FreeBusyStatusEnum.None)
				{
					return null;
				}
				return scheduleStatus.ToString().Replace(" ", string.Empty).Split(new char[]
				{
					','
				});
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x06003908 RID: 14600 RVA: 0x000ADE25 File Offset: 0x000AC025
		// (set) Token: 0x06003909 RID: 14601 RVA: 0x000ADE59 File Offset: 0x000AC059
		[DataMember(EmitDefaultValue = false)]
		public Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping[] KeyMappings
		{
			get
			{
				return Array.ConvertAll<Microsoft.Exchange.Data.KeyMapping, Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping>(this.Rule.KeyMappings.ToArray(), (Microsoft.Exchange.Data.KeyMapping x) => new Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping(x));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x0600390A RID: 14602 RVA: 0x000ADE60 File Offset: 0x000AC060
		// (set) Token: 0x0600390B RID: 14603 RVA: 0x000ADE8A File Offset: 0x000AC08A
		[DataMember(EmitDefaultValue = false)]
		public bool? CallersCanInterruptGreeting
		{
			get
			{
				if (!this.Rule.CallersCanInterruptGreeting)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}
	}
}

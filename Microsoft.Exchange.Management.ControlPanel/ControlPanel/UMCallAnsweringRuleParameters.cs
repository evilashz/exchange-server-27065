using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ControlPanel.DataContracts;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000464 RID: 1124
	[DataContract]
	public abstract class UMCallAnsweringRuleParameters : SetObjectProperties
	{
		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000ADEAF File Offset: 0x000AC0AF
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000ADEB6 File Offset: 0x000AC0B6
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x000ADEC8 File Offset: 0x000AC0C8
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000ADED6 File Offset: 0x000AC0D6
		// (set) Token: 0x06003916 RID: 14614 RVA: 0x000ADEE8 File Offset: 0x000AC0E8
		[DataMember]
		public bool? CheckAutomaticReplies
		{
			get
			{
				return (bool?)base["CheckAutomaticReplies"];
			}
			set
			{
				base["CheckAutomaticReplies"] = (value ?? false);
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x000ADF1C File Offset: 0x000AC11C
		// (set) Token: 0x06003918 RID: 14616 RVA: 0x000ADF74 File Offset: 0x000AC174
		[DataMember]
		public string[] ScheduleStatus
		{
			get
			{
				string text = (string)base["ScheduleStatus"];
				if (text == null)
				{
					return null;
				}
				string[] array = text.Split(new char[]
				{
					','
				});
				if (array.Length == 1 && array[0] == FreeBusyStatusEnum.None.ToString())
				{
					return null;
				}
				return array;
			}
			set
			{
				if (value != null)
				{
					FreeBusyStatusEnum freeBusyStatusEnum = FreeBusyStatusEnum.None;
					for (int i = 0; i < value.Length; i++)
					{
						string value2 = value[i];
						freeBusyStatusEnum |= (FreeBusyStatusEnum)Enum.Parse(typeof(FreeBusyStatusEnum), value2);
					}
					base["ScheduleStatus"] = freeBusyStatusEnum;
					return;
				}
				base["ScheduleStatus"] = null;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x000ADFD8 File Offset: 0x000AC1D8
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x000AE026 File Offset: 0x000AC226
		[DataMember]
		public Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem[] CallerIds
		{
			get
			{
				Microsoft.Exchange.Data.CallerIdItem[] array = (Microsoft.Exchange.Data.CallerIdItem[])base["CallerIds"];
				if (array != null)
				{
					return Array.ConvertAll<Microsoft.Exchange.Data.CallerIdItem, Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem>(array, (Microsoft.Exchange.Data.CallerIdItem x) => new Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem(x));
				}
				return null;
			}
			set
			{
				string cmdletParameterName = "CallerIds";
				object value2;
				if (value == null)
				{
					value2 = null;
				}
				else
				{
					value2 = Array.ConvertAll<Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem, Microsoft.Exchange.Data.CallerIdItem>(value, (Microsoft.Exchange.Management.ControlPanel.DataContracts.CallerIdItem x) => x.ToTaskObject());
				}
				base[cmdletParameterName] = value2;
			}
		}

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000AE05C File Offset: 0x000AC25C
		// (set) Token: 0x0600391C RID: 14620 RVA: 0x000AE06E File Offset: 0x000AC26E
		[DataMember]
		public string[] ExtensionsDialed
		{
			get
			{
				return (string[])base["ExtensionsDialed"];
			}
			set
			{
				base["ExtensionsDialed"] = value;
			}
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000AE07C File Offset: 0x000AC27C
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x000AE0A5 File Offset: 0x000AC2A5
		[DataMember]
		public TimeOfDayItem TimeOfDay
		{
			get
			{
				TimeOfDay timeOfDay = (TimeOfDay)base["TimeOfDay"];
				if (timeOfDay != null)
				{
					return new TimeOfDayItem(timeOfDay);
				}
				return null;
			}
			set
			{
				base["TimeOfDay"] = ((value != null) ? value.ToTaskObject() : null);
			}
		}

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x000AE0C8 File Offset: 0x000AC2C8
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x000AE116 File Offset: 0x000AC316
		[DataMember]
		public Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping[] KeyMappings
		{
			get
			{
				Microsoft.Exchange.Data.KeyMapping[] array = (Microsoft.Exchange.Data.KeyMapping[])base["KeyMappings"];
				if (array != null)
				{
					return Array.ConvertAll<Microsoft.Exchange.Data.KeyMapping, Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping>(array, (Microsoft.Exchange.Data.KeyMapping x) => new Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping(x));
				}
				return null;
			}
			set
			{
				string cmdletParameterName = "KeyMappings";
				object value2;
				if (value == null)
				{
					value2 = null;
				}
				else
				{
					value2 = Array.ConvertAll<Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping, Microsoft.Exchange.Data.KeyMapping>(value, (Microsoft.Exchange.Management.ControlPanel.DataContracts.KeyMapping x) => x.ToTaskObject());
				}
				base[cmdletParameterName] = value2;
			}
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000AE14C File Offset: 0x000AC34C
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x000AE160 File Offset: 0x000AC360
		[DataMember]
		public bool? CallersCanInterruptGreeting
		{
			get
			{
				return (bool?)base["CallersCanInterruptGreeting"];
			}
			set
			{
				base["CallersCanInterruptGreeting"] = (value ?? false);
			}
		}
	}
}

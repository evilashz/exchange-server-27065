using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C80 RID: 3200
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class MailboxOofStateProperty : SmartPropertyDefinition
	{
		// Token: 0x06007036 RID: 28726 RVA: 0x001F0D24 File Offset: 0x001EEF24
		public MailboxOofStateProperty() : base("MailboxOofState", typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MailboxOofStateInternal, PropertyDependencyType.AllRead),
			new PropertyDependency(MailboxSchema.OofScheduleStart, PropertyDependencyType.NeedForRead),
			new PropertyDependency(MailboxSchema.OofScheduleEnd, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007037 RID: 28727 RVA: 0x001F0D80 File Offset: 0x001EEF80
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			bool flag = propertyBag.GetValueOrDefault<bool>(InternalSchema.MailboxOofStateInternal, false);
			ExDateTime valueOrDefault = propertyBag.GetValueOrDefault<ExDateTime>(MailboxSchema.OofScheduleStart, Util.Date1601Utc);
			ExDateTime valueOrDefault2 = propertyBag.GetValueOrDefault<ExDateTime>(MailboxSchema.OofScheduleEnd, Util.Date1601Utc);
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (flag)
			{
				if (valueOrDefault2 != Util.Date1601Utc && valueOrDefault2 < utcNow)
				{
					flag = false;
				}
			}
			else if (valueOrDefault != Util.Date1601Utc && valueOrDefault2 != Util.Date1601Utc && valueOrDefault <= utcNow && valueOrDefault2 > utcNow)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x001F0E18 File Offset: 0x001EF018
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MailboxOofStateInternal, (bool)value);
			propertyBag.SetValueWithFixup(MailboxSchema.OofScheduleStart, Util.Date1601Utc);
			propertyBag.SetValueWithFixup(MailboxSchema.OofScheduleEnd, Util.Date1601Utc);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C33 RID: 3123
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class IsOutOfDateProperty : SmartPropertyDefinition
	{
		// Token: 0x06006ED2 RID: 28370 RVA: 0x001DCE98 File Offset: 0x001DB098
		internal IsOutOfDateProperty() : base("IsOutOfDate", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[0])
		{
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x001DCEBC File Offset: 0x001DB0BC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MeetingMessage meetingMessage = propertyBag.Context.StoreObject as MeetingMessage;
			if (meetingMessage != null)
			{
				return meetingMessage.IsOutOfDate();
			}
			return new PropertyError(this, PropertyErrorCode.GetCalculatedPropertyError);
		}
	}
}

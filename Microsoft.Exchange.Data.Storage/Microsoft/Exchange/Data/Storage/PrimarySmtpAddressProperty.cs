using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CA7 RID: 3239
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class PrimarySmtpAddressProperty : SmartPropertyDefinition
	{
		// Token: 0x060070E3 RID: 28899 RVA: 0x001F4F8B File Offset: 0x001F318B
		internal PrimarySmtpAddressProperty() : base("PrimarySmtpAddressProperty", typeof(string), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, PrimarySmtpAddressProperty.dependantProps)
		{
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x001F4FB0 File Offset: 0x001F31B0
		private static PropertyDependency[] GetDependencies(ContactEmailSlotParticipantProperty[] sourceProps)
		{
			List<PropertyDependency> list = new List<PropertyDependency>();
			foreach (ContactEmailSlotParticipantProperty contactEmailSlotParticipantProperty in sourceProps)
			{
				if (contactEmailSlotParticipantProperty == null)
				{
					throw new InvalidProgramException("Initialization of sourceProps failed due to an ordering issue.");
				}
				foreach (PropertyDependency propertyDependency in contactEmailSlotParticipantProperty.Dependencies)
				{
					if ((propertyDependency.Type & PropertyDependencyType.NeedForRead) == PropertyDependencyType.NeedForRead)
					{
						list.Add(new PropertyDependency(propertyDependency.Property, PropertyDependencyType.NeedForRead));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x001F5030 File Offset: 0x001F3230
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			foreach (ContactEmailSlotParticipantProperty propertyDefinition in PrimarySmtpAddressProperty.sourceProps)
			{
				Participant participant = propertyBag.GetValue(propertyDefinition) as Participant;
				if (participant != null && participant.RoutingType == "EX")
				{
					string text = participant.TryGetProperty(ParticipantSchema.SmtpAddress) as string;
					if (!string.IsNullOrEmpty(text))
					{
						return text;
					}
				}
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x04004E8B RID: 20107
		private static ContactEmailSlotParticipantProperty[] sourceProps = new ContactEmailSlotParticipantProperty[]
		{
			InternalSchema.ContactEmail1,
			InternalSchema.ContactEmail2,
			InternalSchema.ContactEmail3
		};

		// Token: 0x04004E8C RID: 20108
		private static PropertyDependency[] dependantProps = PrimarySmtpAddressProperty.GetDependencies(PrimarySmtpAddressProperty.sourceProps);
	}
}

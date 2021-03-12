using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200073E RID: 1854
	[Serializable]
	public class InvalidParticipantException : CorruptDataException
	{
		// Token: 0x06004806 RID: 18438 RVA: 0x00130885 File Offset: 0x0012EA85
		public InvalidParticipantException(LocalizedString message, ParticipantValidationStatus validationStatus) : this(message, validationStatus, null)
		{
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x00130890 File Offset: 0x0012EA90
		public InvalidParticipantException(LocalizedString message, ParticipantValidationStatus validationStatus, Exception innerException) : base(InvalidParticipantException.CreateMessage(message, validationStatus), innerException)
		{
			EnumValidator.ThrowIfInvalid<ParticipantValidationStatus>(validationStatus, "validationStatus");
			this.validationStatus = validationStatus;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x001308B2 File Offset: 0x0012EAB2
		protected InvalidParticipantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.validationStatus = (ParticipantValidationStatus)info.GetValue("validationStatus", typeof(ParticipantValidationStatus));
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x001308DC File Offset: 0x0012EADC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("validationStatus", this.validationStatus);
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x001308FC File Offset: 0x0012EAFC
		public ParticipantValidationStatus ValidationStatus
		{
			get
			{
				return this.validationStatus;
			}
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x00130904 File Offset: 0x0012EB04
		private static LocalizedString CreateMessage(LocalizedString message, ParticipantValidationStatus status)
		{
			return ServerStrings.InvalidParticipant(message, status);
		}

		// Token: 0x0400273B RID: 10043
		private const string ValidationStatusLabel = "validationStatus";

		// Token: 0x0400273C RID: 10044
		private readonly ParticipantValidationStatus validationStatus;
	}
}

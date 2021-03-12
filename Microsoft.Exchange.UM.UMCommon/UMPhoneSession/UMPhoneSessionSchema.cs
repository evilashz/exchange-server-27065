using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMPhoneSession
{
	// Token: 0x0200012F RID: 303
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UMPhoneSessionSchema : SimpleProviderObjectSchema
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00025DB4 File Offset: 0x00023FB4
		public static SimpleProviderPropertyDefinition CallState
		{
			get
			{
				return UMPhoneSessionSchema.sessionState;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00025DBB File Offset: 0x00023FBB
		public static SimpleProviderPropertyDefinition EventCause
		{
			get
			{
				return UMPhoneSessionSchema.sessionEventCause;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00025DC2 File Offset: 0x00023FC2
		public static SimpleProviderPropertyDefinition OperationResult
		{
			get
			{
				return UMPhoneSessionSchema.sessionResult;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00025DC9 File Offset: 0x00023FC9
		public static SimpleProviderPropertyDefinition PhoneNumber
		{
			get
			{
				return UMPhoneSessionSchema.phoneNumber;
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00025DD0 File Offset: 0x00023FD0
		private static SimpleProviderPropertyDefinition CreatePropertyDefinition(string propertyName, Type propertyType, object defaultValue)
		{
			return new SimpleProviderPropertyDefinition(propertyName, ExchangeObjectVersion.Exchange2010, propertyType, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x04000566 RID: 1382
		public const string UMMailboxParameterName = "UMMailbox";

		// Token: 0x04000567 RID: 1383
		public const string PhoneNumberParameterName = "PhoneNumber";

		// Token: 0x04000568 RID: 1384
		public const string DefaultVoicemailGreetingParameterName = "DefaultVoicemailGreeting";

		// Token: 0x04000569 RID: 1385
		public const string AwayVoicemailGreetingParameterName = "AwayVoicemailGreeting";

		// Token: 0x0400056A RID: 1386
		public const string CallAnsweringRuleIdParameterName = "CallAnsweringRuleId";

		// Token: 0x0400056B RID: 1387
		private static SimpleProviderPropertyDefinition sessionState = UMPhoneSessionSchema.CreatePropertyDefinition("CallState", typeof(UMCallState), UMCallState.Disconnected);

		// Token: 0x0400056C RID: 1388
		private static SimpleProviderPropertyDefinition sessionEventCause = UMPhoneSessionSchema.CreatePropertyDefinition("EventCause", typeof(UMEventCause), UMEventCause.None);

		// Token: 0x0400056D RID: 1389
		private static SimpleProviderPropertyDefinition sessionResult = UMPhoneSessionSchema.CreatePropertyDefinition("OperationResult", typeof(UMOperationResult), UMOperationResult.Failure);

		// Token: 0x0400056E RID: 1390
		private static SimpleProviderPropertyDefinition phoneNumber = UMPhoneSessionSchema.CreatePropertyDefinition("PhoneNumber", typeof(string), string.Empty);
	}
}

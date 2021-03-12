using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000860 RID: 2144
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "FaultId", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	internal enum FaultId
	{
		// Token: 0x040027DA RID: 10202
		[EnumMember]
		InvalidParameterSpecified = 1,
		// Token: 0x040027DB RID: 10203
		[EnumMember]
		NullOrEmptyParameterSpecified,
		// Token: 0x040027DC RID: 10204
		[EnumMember]
		CompanyExistsUnderThisReseller = 100,
		// Token: 0x040027DD RID: 10205
		[EnumMember]
		CompanyExistsOutsideThisReseller,
		// Token: 0x040027DE RID: 10206
		[EnumMember]
		CompanyDoesNotExist,
		// Token: 0x040027DF RID: 10207
		[EnumMember]
		ParentCompanySpecifiedDoesNotExist,
		// Token: 0x040027E0 RID: 10208
		[EnumMember]
		CompanyIsDisabled,
		// Token: 0x040027E1 RID: 10209
		[EnumMember]
		CannotDisableCompany,
		// Token: 0x040027E2 RID: 10210
		[EnumMember]
		ParentCompanyIdNotSpecified,
		// Token: 0x040027E3 RID: 10211
		[EnumMember]
		InvalidCompanyIdSpecified,
		// Token: 0x040027E4 RID: 10212
		[EnumMember]
		CanNotCreateResellerCompany,
		// Token: 0x040027E5 RID: 10213
		[EnumMember]
		CanNotDeleteEnabledCompany,
		// Token: 0x040027E6 RID: 10214
		[EnumMember]
		NullOrEmptyCompanyNameSpecified,
		// Token: 0x040027E7 RID: 10215
		[EnumMember]
		NoParentCompany,
		// Token: 0x040027E8 RID: 10216
		[EnumMember]
		CompanyConfigurationNotFound,
		// Token: 0x040027E9 RID: 10217
		[EnumMember]
		InvalidCompanyGuidSpecified,
		// Token: 0x040027EA RID: 10218
		[EnumMember]
		CompanyGuidIsNotUnique,
		// Token: 0x040027EB RID: 10219
		[EnumMember]
		CompanyNameShouldOnlyBeAscii,
		// Token: 0x040027EC RID: 10220
		[EnumMember]
		DomainExistUnderThisCompany = 200,
		// Token: 0x040027ED RID: 10221
		[EnumMember]
		DomainExistOutsideThisCompany,
		// Token: 0x040027EE RID: 10222
		[EnumMember]
		DomainDoesNotExist,
		// Token: 0x040027EF RID: 10223
		[EnumMember]
		DomainIsDisabled,
		// Token: 0x040027F0 RID: 10224
		[EnumMember]
		CannotDisabledDomain,
		// Token: 0x040027F1 RID: 10225
		[EnumMember]
		DomainNameValidationFailed,
		// Token: 0x040027F2 RID: 10226
		[EnumMember]
		NullOrEmptyDomainNameSpecified,
		// Token: 0x040027F3 RID: 10227
		[EnumMember]
		ShouldNotEnableNonValidatedDomain,
		// Token: 0x040027F4 RID: 10228
		[EnumMember]
		CanNotAddDomainToReseller,
		// Token: 0x040027F5 RID: 10229
		[EnumMember]
		CanNotDeleteEnabledDomain,
		// Token: 0x040027F6 RID: 10230
		[EnumMember]
		DomainGuidIsNotUnique,
		// Token: 0x040027F7 RID: 10231
		[EnumMember]
		SmtpProfileAndMailServerCanNotBeSpecifiedTogether,
		// Token: 0x040027F8 RID: 10232
		[EnumMember]
		SmtpProfileWithTheSameNameExists = 300,
		// Token: 0x040027F9 RID: 10233
		[EnumMember]
		SpecifiedSmtpProfileDoesNotExist,
		// Token: 0x040027FA RID: 10234
		[EnumMember]
		AccessDenied,
		// Token: 0x040027FB RID: 10235
		[EnumMember]
		FailedToAddIP,
		// Token: 0x040027FC RID: 10236
		[EnumMember]
		FailedToRemoveIP,
		// Token: 0x040027FD RID: 10237
		[EnumMember]
		IPValidationFailed,
		// Token: 0x040027FE RID: 10238
		[EnumMember]
		SmtpProfileNameTooLong,
		// Token: 0x040027FF RID: 10239
		[EnumMember]
		InvalidSmtpProfileName,
		// Token: 0x04002800 RID: 10240
		[EnumMember]
		InvalidSmtpProfile,
		// Token: 0x04002801 RID: 10241
		[EnumMember]
		InvalidIPList,
		// Token: 0x04002802 RID: 10242
		[EnumMember]
		IpAlreadyInOurSystem,
		// Token: 0x04002803 RID: 10243
		[EnumMember]
		InboundIPOrSmtpProfileShouldBeSpecified,
		// Token: 0x04002804 RID: 10244
		[EnumMember]
		FailedToCreateCompany = 400,
		// Token: 0x04002805 RID: 10245
		[EnumMember]
		FailedToCreateDomain,
		// Token: 0x04002806 RID: 10246
		[EnumMember]
		UnknownError,
		// Token: 0x04002807 RID: 10247
		[EnumMember]
		InvalidOperation,
		// Token: 0x04002808 RID: 10248
		[EnumMember]
		InvalidTargetActionSpecified,
		// Token: 0x04002809 RID: 10249
		[EnumMember]
		CannotApplySettingAtResellerLevel,
		// Token: 0x0400280A RID: 10250
		[EnumMember]
		CannotApplySettingAtCompanyLevel,
		// Token: 0x0400280B RID: 10251
		[EnumMember]
		SubscriptionNotSetupAtResellerLevel,
		// Token: 0x0400280C RID: 10252
		[EnumMember]
		FailedToSaveSubscription,
		// Token: 0x0400280D RID: 10253
		[EnumMember]
		BatchSizeExceededLimit,
		// Token: 0x0400280E RID: 10254
		[EnumMember]
		UnableToConnectToDatabase,
		// Token: 0x0400280F RID: 10255
		[EnumMember]
		ConnectorDoesNotExists,
		// Token: 0x04002810 RID: 10256
		[EnumMember]
		ConnectorNotInResellerScope
	}
}

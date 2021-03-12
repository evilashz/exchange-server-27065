using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000040 RID: 64
	internal enum KnownError
	{
		// Token: 0x04000141 RID: 321
		NA,
		// Token: 0x04000142 RID: 322
		ErrorDiscoverySearchesDisabledException,
		// Token: 0x04000143 RID: 323
		ErrorSearchQueryCannotBeEmpty,
		// Token: 0x04000144 RID: 324
		ErrorNoMailboxSpecifiedForSearchOperation,
		// Token: 0x04000145 RID: 325
		ErrorInvalidSearchQuerySyntax,
		// Token: 0x04000146 RID: 326
		ErrorQueryLanguageNotValid,
		// Token: 0x04000147 RID: 327
		ErrorSortByPropertyIsNotFoundOrNotSupported,
		// Token: 0x04000148 RID: 328
		ErrorInvalidPropertyForSortBy,
		// Token: 0x04000149 RID: 329
		ErrorInvalidSearchId,
		// Token: 0x0400014A RID: 330
		ErrorSearchTimedOut,
		// Token: 0x0400014B RID: 331
		TooManyMailboxQueryObjects,
		// Token: 0x0400014C RID: 332
		TooManyMailboxesException,
		// Token: 0x0400014D RID: 333
		TooManyKeywordsException,
		// Token: 0x0400014E RID: 334
		ErrorRecipientTypeNotSupported,
		// Token: 0x0400014F RID: 335
		ErrorMailboxVersionNotSupported,
		// Token: 0x04000150 RID: 336
		ErrorNoPermissionToSearchOrHoldMailbox,
		// Token: 0x04000151 RID: 337
		ErrorSearchableObjectNotFound,
		// Token: 0x04000152 RID: 338
		ErrorWildcardAndGroupExpansionNotAllowed,
		// Token: 0x04000153 RID: 339
		ErrorSuffixSearchNotAllowed
	}
}

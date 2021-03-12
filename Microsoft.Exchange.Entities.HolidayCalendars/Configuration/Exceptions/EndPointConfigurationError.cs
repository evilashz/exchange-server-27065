using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions
{
	// Token: 0x0200000B RID: 11
	public enum EndPointConfigurationError : uint
	{
		// Token: 0x04000014 RID: 20
		UnableToFetchListOfCultures = 1U,
		// Token: 0x04000015 RID: 21
		UnableToFetchCalendarVersions,
		// Token: 0x04000016 RID: 22
		UrlDidNotResolveToHttpRequest,
		// Token: 0x04000017 RID: 23
		UrlSchemeNotSupported,
		// Token: 0x04000018 RID: 24
		VersionNumberError
	}
}

using System;

namespace Microsoft.Exchange.Entities.HolidayCalendars.Configuration.Exceptions
{
	// Token: 0x0200000D RID: 13
	public class EndpointConfigurationException : HolidayCalendarException
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000289C File Offset: 0x00000A9C
		public EndpointConfigurationException(EndPointConfigurationError error, string message, params object[] args) : base("Message: '{0}', Error: '{1}'", new object[]
		{
			string.Format(message, args),
			error.ToString()
		})
		{
			this.Error = error;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000028DC File Offset: 0x00000ADC
		public EndpointConfigurationException(EndPointConfigurationError error, Exception innerException, string message, params object[] args) : base("Message: '{0}', Error: '{1}' InnerException:'{2}'", new object[]
		{
			string.Format(message, args),
			error.ToString(),
			innerException
		})
		{
			this.Error = error;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002920 File Offset: 0x00000B20
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002928 File Offset: 0x00000B28
		public EndPointConfigurationError Error { get; private set; }
	}
}

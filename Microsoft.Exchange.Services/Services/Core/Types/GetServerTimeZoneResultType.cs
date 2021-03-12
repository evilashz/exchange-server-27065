using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005DD RID: 1501
	public class GetServerTimeZoneResultType : ResponseMessage
	{
		// Token: 0x06002D37 RID: 11575 RVA: 0x000B1E9B File Offset: 0x000B009B
		internal GetServerTimeZoneResultType(bool returnFullTimeZoneData, TimeZoneDefinitionType[] timeZoneDefinitions)
		{
			this.returnFullTimeZoneData = returnFullTimeZoneData;
			this.timeZoneDefinitions = timeZoneDefinitions;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000B1EB4 File Offset: 0x000B00B4
		public TimeZoneDefinitionType[] ToTimeZoneDefinitionType()
		{
			foreach (TimeZoneDefinitionType timeZoneDefinitionType in this.timeZoneDefinitions)
			{
				timeZoneDefinitionType.Render(this.returnFullTimeZoneData, EWSSettings.ClientCulture);
			}
			return this.timeZoneDefinitions;
		}

		// Token: 0x04001B1D RID: 6941
		private bool returnFullTimeZoneData;

		// Token: 0x04001B1E RID: 6942
		private TimeZoneDefinitionType[] timeZoneDefinitions;
	}
}

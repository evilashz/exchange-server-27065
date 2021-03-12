using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000327 RID: 807
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetTimeZone : ServiceCommand<TimeZoneConfiguration>
	{
		// Token: 0x06001AC7 RID: 6855 RVA: 0x0006540E File Offset: 0x0006360E
		public GetTimeZone(CallContext callContext, bool needTimeZoneList) : base(callContext)
		{
			this.needTimeZoneList = needTimeZoneList;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0006541E File Offset: 0x0006361E
		protected override TimeZoneConfiguration InternalExecute()
		{
			return GetTimeZone.GetSetting(base.CallContext, this.needTimeZoneList);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00065434 File Offset: 0x00063634
		public static TimeZoneConfiguration GetSetting(CallContext callContext, bool needTimeZoneList)
		{
			TimeZoneConfiguration timeZoneConfiguration = new TimeZoneConfiguration();
			if (needTimeZoneList)
			{
				List<TimeZoneEntry> list = new List<TimeZoneEntry>();
				foreach (ExTimeZone timezone in ExTimeZoneEnumerator.Instance)
				{
					list.Add(new TimeZoneEntry(timezone));
				}
				timeZoneConfiguration.TimeZoneList = list.ToArray();
			}
			UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.TimeZone);
			UserOptionsType userOptionsType = new UserOptionsType();
			userOptionsType.Load(callContext, new UserConfigurationPropertyDefinition[]
			{
				propertyDefinition
			});
			timeZoneConfiguration.CurrentTimeZone = userOptionsType.TimeZone;
			return timeZoneConfiguration;
		}

		// Token: 0x04000EDB RID: 3803
		private readonly bool needTimeZoneList;
	}
}

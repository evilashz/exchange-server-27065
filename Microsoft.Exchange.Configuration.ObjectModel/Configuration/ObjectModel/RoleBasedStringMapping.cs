using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000262 RID: 610
	internal static class RoleBasedStringMapping
	{
		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0004EA69 File Offset: 0x0004CC69
		private static ExEventLog EventLog
		{
			get
			{
				if (RoleBasedStringMapping.eventlog == null)
				{
					RoleBasedStringMapping.eventlog = new ExEventLog(ExTraceGlobals.RoleBasedStringMappingTracer.Category, "MSExchange Configuration Object Model - String Interface Packs");
				}
				return RoleBasedStringMapping.eventlog;
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0004EA90 File Offset: 0x0004CC90
		internal static CultureInfo GetRoleBasedCultureInfo(CultureInfo parentCulture, ICollection<RoleType> roleTypesCollection)
		{
			CultureInfo result;
			try
			{
				if (roleTypesCollection is List<RoleType>)
				{
					List<RoleType> list = (List<RoleType>)roleTypesCollection;
				}
				else
				{
					new List<RoleType>(roleTypesCollection);
				}
				string text = null;
				CultureInfo cultureInfo;
				if (text == null)
				{
					cultureInfo = parentCulture;
				}
				else
				{
					cultureInfo = SipCultureInfoFactory.CreateInstance(parentCulture, text);
				}
				result = cultureInfo;
			}
			catch (Exception ex)
			{
				RoleBasedStringMapping.EventLog.LogEvent(TaskEventLogConstants.Tuple_RoleBasedStringMappingFailure, null, new object[]
				{
					ex.GetType().Name,
					ex.ToString()
				});
				result = parentCulture;
			}
			return result;
		}

		// Token: 0x04000664 RID: 1636
		private static ExEventLog eventlog;
	}
}

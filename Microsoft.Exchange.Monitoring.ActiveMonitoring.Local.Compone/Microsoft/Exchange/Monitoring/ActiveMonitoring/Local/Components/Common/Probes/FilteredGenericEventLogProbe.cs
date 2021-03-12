using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Common.Probes
{
	// Token: 0x020000CD RID: 205
	public class FilteredGenericEventLogProbe : GenericEventLogProbe
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x00028150 File Offset: 0x00026350
		static FilteredGenericEventLogProbe()
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
			FilteredEventSection section = (FilteredEventSection)configuration.GetSection("filteredEvents");
			IEnumerable<string> collection = from key in section.ControlPanelRedEvent4.AllKeys
			select section.ControlPanelRedEvent4[key].Value;
			FilteredGenericEventLogProbe.filteredEventHashKeys = new HashSet<string>(collection);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000281B8 File Offset: 0x000263B8
		protected override void OnRedEvent(CentralEventLogWatcher.EventRecordMini redEvent)
		{
			string empty = string.Empty;
			string redEventString = redEvent.CustomizedProperties[2];
			FilteredGenericEventLogProbe.RedEventException exception = new FilteredGenericEventLogProbe.RedEventException(redEventString);
			if (WatsonExceptionReport.TryStringHashFromStackTrace(exception, false, out empty))
			{
				if (FilteredGenericEventLogProbe.filteredEventHashKeys.Contains(empty))
				{
					return;
				}
				redEvent.CustomizedProperties[0] = SuppressingPiiData.Redact(redEvent.CustomizedProperties[0]);
				base.Result.StateAttribute12 = string.Format("Exception StackTrace Hash Key: {0}", empty);
			}
			base.OnRedEvent(redEvent);
		}

		// Token: 0x04000468 RID: 1128
		private const int StackTrace = 2;

		// Token: 0x04000469 RID: 1129
		private const int UserInformation = 0;

		// Token: 0x0400046A RID: 1130
		private static readonly HashSet<string> filteredEventHashKeys;

		// Token: 0x020000CE RID: 206
		private class RedEventException : Exception
		{
			// Token: 0x060006C4 RID: 1732 RVA: 0x0002823A File Offset: 0x0002643A
			public RedEventException(string redEventString)
			{
				this.stackTrace = redEventString;
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00028254 File Offset: 0x00026454
			public override string StackTrace
			{
				get
				{
					return this.stackTrace;
				}
			}

			// Token: 0x0400046B RID: 1131
			private readonly string stackTrace = string.Empty;
		}
	}
}

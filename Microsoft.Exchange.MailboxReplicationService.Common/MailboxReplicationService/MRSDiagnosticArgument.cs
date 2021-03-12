using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200016C RID: 364
	internal class MRSDiagnosticArgument : DiagnosableArgument
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x0001FFE2 File Offset: 0x0001E1E2
		public MRSDiagnosticArgument(string argument)
		{
			base.Initialize(argument);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["job"] = typeof(string);
			schema["reservations"] = typeof(Guid);
			schema["resources"] = typeof(string);
			schema["healthstats"] = typeof(string);
			schema["unhealthy"] = typeof(string);
			schema["workloads"] = typeof(bool);
			schema["queues"] = typeof(string);
			schema["pickupresults"] = typeof(string);
			schema["maxsize"] = typeof(int);
			schema["requesttype"] = typeof(MRSRequestType);
			schema["binaryversions"] = typeof(string);
			schema["showtimeslots"] = typeof(bool);
		}

		// Token: 0x040007E9 RID: 2025
		public const string JobArgument = "job";

		// Token: 0x040007EA RID: 2026
		public const string ReservationsArgument = "reservations";

		// Token: 0x040007EB RID: 2027
		public const string ResourcesArgument = "resources";

		// Token: 0x040007EC RID: 2028
		public const string HealthStatsArgument = "healthstats";

		// Token: 0x040007ED RID: 2029
		public const string UnhealthyArgument = "unhealthy";

		// Token: 0x040007EE RID: 2030
		public const string WorkloadsArgument = "workloads";

		// Token: 0x040007EF RID: 2031
		public const string QueuesArgument = "queues";

		// Token: 0x040007F0 RID: 2032
		public const string PickupResultsArgument = "pickupresults";

		// Token: 0x040007F1 RID: 2033
		public const string MaxSizeArgument = "maxsize";

		// Token: 0x040007F2 RID: 2034
		public const string RequestTypeArgument = "requesttype";

		// Token: 0x040007F3 RID: 2035
		public const string BinaryVersionsArgument = "binaryversions";

		// Token: 0x040007F4 RID: 2036
		public const string ShowTimeSlots = "showtimeslots";
	}
}

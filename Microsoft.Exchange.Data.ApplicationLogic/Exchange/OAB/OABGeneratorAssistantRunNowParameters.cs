using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200015B RID: 347
	internal sealed class OABGeneratorAssistantRunNowParameters
	{
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003A5C4 File Offset: 0x000387C4
		public static bool TryParse(string input, out OABGeneratorAssistantRunNowParameters output)
		{
			string[] array = input.Split(new char[]
			{
				','
			});
			if (array.Length != 3)
			{
				OABGeneratorAssistantRunNowParameters.Tracer.TraceError<string>(0L, "OABGeneratorAssistantRunNowParameters.FromParametersString: ignoring on-demand request due malformed string parameter: {0}.", input);
				output = null;
				return false;
			}
			PartitionId partitionId;
			try
			{
				partitionId = new PartitionId(array[0]);
			}
			catch (ArgumentException arg)
			{
				OABGeneratorAssistantRunNowParameters.Tracer.TraceError<string, ArgumentException>(0L, "OABGeneratorAssistantRunNowParameters.FromParametersString: ignoring on-demand request due malformed PartitionId in string parameter: {0}. Exception: {1}", input, arg);
				output = null;
				return false;
			}
			Exception ex = null;
			Guid objectGuid = Guid.Empty;
			try
			{
				objectGuid = new Guid(array[1]);
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			catch (OverflowException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				OABGeneratorAssistantRunNowParameters.Tracer.TraceError<string, Exception>(0L, "OABGeneratorAssistantRunNowParameters.FromParametersString: ignoring on-demand request due malformed GUID in string parameter: {0}. Exception: {1}", input, ex);
				output = null;
				return false;
			}
			output = new OABGeneratorAssistantRunNowParameters
			{
				PartitionId = partitionId,
				ObjectGuid = objectGuid,
				Description = array[2]
			};
			return true;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003A6BC File Offset: 0x000388BC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.PartitionId.ForestFQDN,
				",",
				this.ObjectGuid.ToString(),
				",",
				this.Description
			});
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0003A714 File Offset: 0x00038914
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0003A71C File Offset: 0x0003891C
		public PartitionId PartitionId { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0003A725 File Offset: 0x00038925
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0003A72D File Offset: 0x0003892D
		public Guid ObjectGuid { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003A736 File Offset: 0x00038936
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0003A73E File Offset: 0x0003893E
		public string Description { get; set; }

		// Token: 0x04000773 RID: 1907
		private static readonly Trace Tracer = ExTraceGlobals.RunNowTracer;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200018F RID: 399
	internal class RawLogBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x00032C8C File Offset: 0x00030E8C
		public RawLogBatch()
		{
			this.RawLogLines = new List<byte[][]>();
			this.identity = Guid.NewGuid().ToString();
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00032CC3 File Offset: 0x00030EC3
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00032CD0 File Offset: 0x00030ED0
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x00032CE2 File Offset: 0x00030EE2
		public string MachineName
		{
			get
			{
				return (string)this[RawLogBatch.MachineNameProperty];
			}
			set
			{
				this[RawLogBatch.MachineNameProperty] = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00032CF0 File Offset: 0x00030EF0
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00032D02 File Offset: 0x00030F02
		public string FileName
		{
			get
			{
				return (string)this[RawLogBatch.FileNameProperty];
			}
			set
			{
				this[RawLogBatch.FileNameProperty] = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00032D10 File Offset: 0x00030F10
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x00032D22 File Offset: 0x00030F22
		public string LogPrefix
		{
			get
			{
				return (string)this[RawLogBatch.LogPrefixProperty];
			}
			set
			{
				this[RawLogBatch.LogPrefixProperty] = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00032D30 File Offset: 0x00030F30
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x00032D42 File Offset: 0x00030F42
		public string LogVersion
		{
			get
			{
				return (string)this[RawLogBatch.LogVersionProperty];
			}
			set
			{
				this[RawLogBatch.LogVersionProperty] = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00032D50 File Offset: 0x00030F50
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x00032D62 File Offset: 0x00030F62
		public List<byte[][]> RawLogLines
		{
			get
			{
				return (List<byte[][]>)this[RawLogBatch.RawLogLinesProperty];
			}
			set
			{
				this[RawLogBatch.RawLogLinesProperty] = value;
			}
		}

		// Token: 0x04000799 RID: 1945
		internal static readonly HygienePropertyDefinition MachineNameProperty = new HygienePropertyDefinition("MachineName", typeof(string));

		// Token: 0x0400079A RID: 1946
		internal static readonly HygienePropertyDefinition FileNameProperty = new HygienePropertyDefinition("FileName", typeof(string));

		// Token: 0x0400079B RID: 1947
		internal static readonly HygienePropertyDefinition LogPrefixProperty = new HygienePropertyDefinition("LogPrefix", typeof(string));

		// Token: 0x0400079C RID: 1948
		internal static readonly HygienePropertyDefinition LogVersionProperty = new HygienePropertyDefinition("LogVersion", typeof(string));

		// Token: 0x0400079D RID: 1949
		internal static readonly HygienePropertyDefinition RawLogLinesProperty = new HygienePropertyDefinition("RawLogLines", typeof(IList<byte[][]>));

		// Token: 0x0400079E RID: 1950
		private readonly string identity;
	}
}

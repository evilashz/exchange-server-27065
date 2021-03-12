using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Optics
{
	// Token: 0x020001B5 RID: 437
	internal class OpticsLogBatch : ConfigurablePropertyBag
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x00037F38 File Offset: 0x00036138
		public OpticsLogBatch()
		{
			this.identity = new ConfigObjectId(Guid.NewGuid().ToString());
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00037F69 File Offset: 0x00036169
		public override ObjectId Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00037F71 File Offset: 0x00036171
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x00037F83 File Offset: 0x00036183
		public string LogType
		{
			get
			{
				return (string)this[OpticsLogBatch.LogTypeProperty];
			}
			set
			{
				this[OpticsLogBatch.LogTypeProperty] = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00037F91 File Offset: 0x00036191
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00037FA3 File Offset: 0x000361A3
		public byte[] VersionMask
		{
			get
			{
				return (byte[])this[OpticsLogBatch.VersionMaskProperty];
			}
			set
			{
				this[OpticsLogBatch.VersionMaskProperty] = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00037FB1 File Offset: 0x000361B1
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00037FC3 File Offset: 0x000361C3
		public byte[] Data
		{
			get
			{
				return (byte[])this[OpticsLogBatch.DataProperty];
			}
			set
			{
				this[OpticsLogBatch.DataProperty] = value;
			}
		}

		// Token: 0x040008C0 RID: 2240
		public static readonly HygienePropertyDefinition LogTypeProperty = new HygienePropertyDefinition("log-type", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008C1 RID: 2241
		public static readonly HygienePropertyDefinition VersionMaskProperty = new HygienePropertyDefinition("version-mask", typeof(byte[]));

		// Token: 0x040008C2 RID: 2242
		public static readonly HygienePropertyDefinition DataProperty = new HygienePropertyDefinition("data", typeof(byte[]));

		// Token: 0x040008C3 RID: 2243
		private ObjectId identity;
	}
}

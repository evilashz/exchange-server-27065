using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ReliableActions
{
	// Token: 0x02000B0E RID: 2830
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class ActionInfo
	{
		// Token: 0x060066B6 RID: 26294 RVA: 0x001B3C57 File Offset: 0x001B1E57
		public ActionInfo(Guid id, DateTime timestamp, string commandType, string rawData)
		{
			this.Version = 1;
			this.Id = id;
			this.Timestamp = timestamp;
			this.CommandType = commandType;
			this.RawData = rawData;
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x060066B7 RID: 26295 RVA: 0x001B3C83 File Offset: 0x001B1E83
		// (set) Token: 0x060066B8 RID: 26296 RVA: 0x001B3C8B File Offset: 0x001B1E8B
		[DataMember]
		public int Version { get; private set; }

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x001B3C94 File Offset: 0x001B1E94
		// (set) Token: 0x060066BA RID: 26298 RVA: 0x001B3C9C File Offset: 0x001B1E9C
		[DataMember]
		public Guid Id { get; private set; }

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x001B3CA5 File Offset: 0x001B1EA5
		// (set) Token: 0x060066BC RID: 26300 RVA: 0x001B3CAD File Offset: 0x001B1EAD
		[DataMember]
		public DateTime Timestamp { get; private set; }

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x060066BD RID: 26301 RVA: 0x001B3CB6 File Offset: 0x001B1EB6
		// (set) Token: 0x060066BE RID: 26302 RVA: 0x001B3CBE File Offset: 0x001B1EBE
		[DataMember]
		public string RawData { get; private set; }

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x060066BF RID: 26303 RVA: 0x001B3CC7 File Offset: 0x001B1EC7
		// (set) Token: 0x060066C0 RID: 26304 RVA: 0x001B3CCF File Offset: 0x001B1ECF
		[DataMember]
		public string CommandType { get; private set; }

		// Token: 0x04003A3D RID: 14909
		public const int CurrentVersion = 1;
	}
}

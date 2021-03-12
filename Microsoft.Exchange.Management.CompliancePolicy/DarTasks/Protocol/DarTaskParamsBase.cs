using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol
{
	// Token: 0x02000010 RID: 16
	[KnownType(typeof(DarTaskAggregateParams))]
	[DataContract]
	[KnownType(typeof(DarTaskParams))]
	public class DarTaskParamsBase
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000034F4 File Offset: 0x000016F4
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000034FC File Offset: 0x000016FC
		[DataMember]
		public string TaskId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003505 File Offset: 0x00001705
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000350D File Offset: 0x0000170D
		[DataMember]
		public byte[] TenantId { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003516 File Offset: 0x00001716
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000351E File Offset: 0x0000171E
		[DataMember]
		public string TaskType { get; set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00003528 File Offset: 0x00001728
		public static T FromBytes<T>(byte[] data) where T : DarTaskParamsBase
		{
			string @string = Encoding.UTF8.GetString(data);
			return JsonConverter.Deserialize<T>(@string, null);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003548 File Offset: 0x00001748
		public byte[] ToBytes()
		{
			return Encoding.UTF8.GetBytes(JsonConverter.Serialize<DarTaskParamsBase>(this, null));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000355B File Offset: 0x0000175B
		public override string ToString()
		{
			return JsonConverter.Serialize<DarTaskParamsBase>(this, null);
		}
	}
}

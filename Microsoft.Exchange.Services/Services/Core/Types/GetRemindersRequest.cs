using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000449 RID: 1097
	[XmlType(TypeName = "GetRemindersType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetRemindersRequest : BaseRequest
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000A190C File Offset: 0x0009FB0C
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x000A1914 File Offset: 0x0009FB14
		[XmlElement("BeginTime", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[IgnoreDataMember]
		public DateTime BeginTime { get; set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x000A191D File Offset: 0x0009FB1D
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x000A1925 File Offset: 0x0009FB25
		[XmlIgnore]
		[DataMember(Name = "BeginTime", IsRequired = false)]
		public string BeginTimeString { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000A192E File Offset: 0x0009FB2E
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x000A1936 File Offset: 0x0009FB36
		[IgnoreDataMember]
		[XmlElement("EndTime", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public DateTime EndTime { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x000A193F File Offset: 0x0009FB3F
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x000A1947 File Offset: 0x0009FB47
		[DataMember(Name = "EndTime", IsRequired = false)]
		[XmlIgnore]
		public string EndTimeString { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x000A1950 File Offset: 0x0009FB50
		// (set) Token: 0x06002031 RID: 8241 RVA: 0x000A1958 File Offset: 0x0009FB58
		[DataMember]
		[XmlElement("MaxItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public int MaxItems { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x000A1961 File Offset: 0x0009FB61
		// (set) Token: 0x06002033 RID: 8243 RVA: 0x000A1969 File Offset: 0x0009FB69
		[XmlElement("ReminderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember]
		public ReminderTypes ReminderType { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x000A1972 File Offset: 0x0009FB72
		// (set) Token: 0x06002035 RID: 8245 RVA: 0x000A197A File Offset: 0x0009FB7A
		[XmlElement("ReminderGroupType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember]
		public ReminderGroupType? ReminderGroupType { get; set; }

		// Token: 0x06002036 RID: 8246 RVA: 0x000A1983 File Offset: 0x0009FB83
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetReminders(callContext, this);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000A198C File Offset: 0x0009FB8C
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000A198F File Offset: 0x0009FB8F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}

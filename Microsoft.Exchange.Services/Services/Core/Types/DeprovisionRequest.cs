using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000414 RID: 1044
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DeprovisionRequest : BaseRequest
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0009F9DB File Offset: 0x0009DBDB
		// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x0009F9E3 File Offset: 0x0009DBE3
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public string DeviceID { get; set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0009F9EC File Offset: 0x0009DBEC
		// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x0009F9F4 File Offset: 0x0009DBF4
		[DataMember(EmitDefaultValue = false, IsRequired = true)]
		public string DeviceType { get; set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0009F9FD File Offset: 0x0009DBFD
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x0009FA05 File Offset: 0x0009DC05
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public bool HasPAL { get; set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x0009FA0E File Offset: 0x0009DC0E
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x0009FA16 File Offset: 0x0009DC16
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public bool SpecifyProtocol { get; set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0009FA1F File Offset: 0x0009DC1F
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0009FA27 File Offset: 0x0009DC27
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string Protocol { get; set; }

		// Token: 0x06001DEE RID: 7662 RVA: 0x0009FA30 File Offset: 0x0009DC30
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new Deprovision(callContext, this);
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0009FA39 File Offset: 0x0009DC39
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0009FA3C File Offset: 0x0009DC3C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}

using System;
using System.Data.Linq.Mapping;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003C2 RID: 962
	[DataServiceKey("ConnectorName")]
	[Serializable]
	public class ServiceDeliveryReport : FfoReportObject
	{
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x0008CF69 File Offset: 0x0008B169
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x0008CF71 File Offset: 0x0008B171
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		[Column(Name = "Organization")]
		public string Organization { get; internal set; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0008CF7A File Offset: 0x0008B17A
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x0008CF82 File Offset: 0x0008B182
		[Column(Name = "IsAcceptedDomain")]
		[DalConversion("DefaultSerializer", "IsAcceptedDomain", new string[]
		{

		})]
		public bool IsAcceptedDomain { get; internal set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x0008CF8B File Offset: 0x0008B18B
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x0008CF93 File Offset: 0x0008B193
		[Column(Name = "IsOnPremMailbox")]
		[DalConversion("DefaultSerializer", "IsOnPremMailbox", new string[]
		{

		})]
		public bool IsOnPremMailbox { get; internal set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x0008CF9C File Offset: 0x0008B19C
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x0008CFA4 File Offset: 0x0008B1A4
		[DalConversion("DefaultSerializer", "ConnectorName", new string[]
		{

		})]
		[Column(Name = "ConnectorName")]
		public string ConnectorName { get; internal set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x0008CFAD File Offset: 0x0008B1AD
		// (set) Token: 0x060022A6 RID: 8870 RVA: 0x0008CFB5 File Offset: 0x0008B1B5
		[Column(Name = "SmartHost")]
		[DalConversion("DefaultSerializer", "IPAddress", new string[]
		{

		})]
		public string SmartHost { get; internal set; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0008CFBE File Offset: 0x0008B1BE
		// (set) Token: 0x060022A8 RID: 8872 RVA: 0x0008CFC6 File Offset: 0x0008B1C6
		[Column(Name = "IsListeningOnPort25")]
		[DalConversion("DefaultSerializer", "IsListeningOnPort25", new string[]
		{

		})]
		public bool IsListeningOnPort25 { get; internal set; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0008CFCF File Offset: 0x0008B1CF
		// (set) Token: 0x060022AA RID: 8874 RVA: 0x0008CFD7 File Offset: 0x0008B1D7
		[Column(Name = "IsSuccessfullyReceivingMail")]
		[DalConversion("DefaultSerializer", "IsSuccessfullyReceivingMail", new string[]
		{

		})]
		public bool IsSuccessfullyReceivingMail { get; internal set; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0008CFE0 File Offset: 0x0008B1E0
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x0008CFE8 File Offset: 0x0008B1E8
		[ODataInput("Recipient")]
		[DalConversion("ValueFromTask", "Recipient", new string[]
		{

		})]
		public string Recipient { get; internal set; }
	}
}

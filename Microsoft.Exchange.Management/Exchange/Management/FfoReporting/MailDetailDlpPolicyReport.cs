using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003B3 RID: 947
	[Serializable]
	public class MailDetailDlpPolicyReport : DetailObject
	{
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0008C3FC File Offset: 0x0008A5FC
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x0008C404 File Offset: 0x0008A604
		[DalConversion("OrganizationFromTask", "Organization", new string[]
		{

		})]
		public string Organization { get; private set; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0008C40D File Offset: 0x0008A60D
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0008C415 File Offset: 0x0008A615
		[DalConversion("DefaultSerializer", "Received", new string[]
		{

		})]
		public DateTime Date { get; private set; }

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0008C41E File Offset: 0x0008A61E
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x0008C426 File Offset: 0x0008A626
		[ODataInput("MessageId")]
		[DalConversion("DefaultSerializer", "ClientMessageId", new string[]
		{

		})]
		public string MessageId { get; private set; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0008C42F File Offset: 0x0008A62F
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x0008C437 File Offset: 0x0008A637
		[Redact]
		[DalConversion("DefaultSerializer", "Domain", new string[]
		{

		})]
		[ODataInput("Domain")]
		public string Domain { get; private set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x0008C440 File Offset: 0x0008A640
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x0008C448 File Offset: 0x0008A648
		[DalConversion("DefaultSerializer", "MessageSubject", new string[]
		{

		})]
		[Redact]
		public string Subject { get; private set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x0008C451 File Offset: 0x0008A651
		// (set) Token: 0x0600214F RID: 8527 RVA: 0x0008C459 File Offset: 0x0008A659
		[DalConversion("DefaultSerializer", "MessageSize", new string[]
		{

		})]
		public int MessageSize { get; private set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x0008C462 File Offset: 0x0008A662
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x0008C46A File Offset: 0x0008A66A
		[DalConversion("DefaultSerializer", "Direction", new string[]
		{

		})]
		[ODataInput("Direction")]
		public string Direction { get; private set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0008C473 File Offset: 0x0008A673
		// (set) Token: 0x06002153 RID: 8531 RVA: 0x0008C47B File Offset: 0x0008A67B
		[ODataInput("SenderAddress")]
		[Redact(RedactAs = typeof(SmtpAddress))]
		[DalConversion("DefaultSerializer", "SenderAddress", new string[]
		{

		})]
		public string SenderAddress { get; private set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x0008C484 File Offset: 0x0008A684
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x0008C48C File Offset: 0x0008A68C
		[ODataInput("RecipientAddress")]
		[DalConversion("DefaultSerializer", "RecipientAddress", new string[]
		{

		})]
		[Redact(RedactAs = typeof(SmtpAddress))]
		public string RecipientAddress { get; private set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0008C495 File Offset: 0x0008A695
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x0008C49D File Offset: 0x0008A69D
		[DalConversion("DefaultSerializer", "PolicyName", new string[]
		{

		})]
		[ODataInput("DlpPolicy")]
		public string DlpPolicy { get; private set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0008C4A6 File Offset: 0x0008A6A6
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x0008C4AE File Offset: 0x0008A6AE
		[DalConversion("DefaultSerializer", "TransportRuleName", new string[]
		{

		})]
		[ODataInput("TransportRule")]
		public string TransportRule { get; private set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0008C4B7 File Offset: 0x0008A6B7
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0008C4BF File Offset: 0x0008A6BF
		[Redact]
		[DalConversion("DefaultSerializer", "ClassificationSndoverride", new string[]
		{

		})]
		public string UserAction { get; private set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x0008C4C8 File Offset: 0x0008A6C8
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x0008C4D0 File Offset: 0x0008A6D0
		[Redact]
		[DalConversion("DefaultSerializer", "ClassificationJustification", new string[]
		{

		})]
		public string Justification { get; private set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0008C4D9 File Offset: 0x0008A6D9
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0008C4E1 File Offset: 0x0008A6E1
		[Redact]
		[DalConversion("DefaultSerializer", "DataClassification", new string[]
		{

		})]
		public string SensitiveInformationType { get; private set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0008C4EA File Offset: 0x0008A6EA
		// (set) Token: 0x06002161 RID: 8545 RVA: 0x0008C4F2 File Offset: 0x0008A6F2
		[DalConversion("DefaultSerializer", "ClassificationCount", new string[]
		{

		})]
		public int SensitiveInformationCount { get; private set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0008C4FB File Offset: 0x0008A6FB
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0008C503 File Offset: 0x0008A703
		[DalConversion("DefaultSerializer", "ClassificationConfidence", new string[]
		{

		})]
		public int SensitiveInformationConfidence { get; private set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0008C50C File Offset: 0x0008A70C
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x0008C514 File Offset: 0x0008A714
		[ODataInput("EventType")]
		[DalConversion("DefaultSerializer", "EventType", new string[]
		{

		})]
		public string EventType { get; private set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0008C51D File Offset: 0x0008A71D
		// (set) Token: 0x06002167 RID: 8551 RVA: 0x0008C525 File Offset: 0x0008A725
		[ODataInput("Action")]
		[DalConversion("DefaultSerializer", "Action", new string[]
		{

		})]
		public string Action { get; private set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0008C52E File Offset: 0x0008A72E
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x0008C536 File Offset: 0x0008A736
		[DalConversion("DefaultSerializer", "InternalMessageId", new string[]
		{

		})]
		public Guid MessageTraceId { get; private set; }
	}
}

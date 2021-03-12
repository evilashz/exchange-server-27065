using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000016 RID: 22
	internal class MailSubmissionResult
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00008645 File Offset: 0x00006845
		public MailSubmissionResult()
		{
			this.RemoteHostName = string.Empty;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008658 File Offset: 0x00006858
		public MailSubmissionResult(uint ec)
		{
			this.RemoteHostName = string.Empty;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000866B File Offset: 0x0000686B
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00008673 File Offset: 0x00006873
		public string DiagnosticInfo { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000867C File Offset: 0x0000687C
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00008684 File Offset: 0x00006884
		public uint ErrorCode { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000868D File Offset: 0x0000688D
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00008695 File Offset: 0x00006895
		public string From { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000869E File Offset: 0x0000689E
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000086A6 File Offset: 0x000068A6
		public string MessageId { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000086AF File Offset: 0x000068AF
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000086B7 File Offset: 0x000068B7
		public string Sender { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000086C0 File Offset: 0x000068C0
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000086C8 File Offset: 0x000068C8
		public string Subject { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000086D1 File Offset: 0x000068D1
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000086D9 File Offset: 0x000068D9
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000086E2 File Offset: 0x000068E2
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000086EA File Offset: 0x000068EA
		public Guid ExternalOrganizationId { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000086F3 File Offset: 0x000068F3
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000086FB File Offset: 0x000068FB
		public string[] RecipientAddresses { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00008704 File Offset: 0x00006904
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000870C File Offset: 0x0000690C
		public Guid NetworkMessageId { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00008715 File Offset: 0x00006915
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000871D File Offset: 0x0000691D
		public string RemoteHostName { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00008726 File Offset: 0x00006926
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000872E File Offset: 0x0000692E
		public IPAddress OriginalClientIPAddress { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00008737 File Offset: 0x00006937
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000873F File Offset: 0x0000693F
		public TimeSpan QuarantineTimeSpan { get; set; }
	}
}

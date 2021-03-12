using System;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.FfoQuarantine
{
	// Token: 0x02000026 RID: 38
	[Cmdlet("Get", "QuarantineMessage", DefaultParameterSetName = "Summary")]
	[OutputType(new Type[]
	{
		typeof(QuarantineMessage)
	})]
	public sealed class GetQuarantineMessage : Task
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000534C File Offset: 0x0000354C
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005354 File Offset: 0x00003554
		[Parameter(ParameterSetName = "Details", Mandatory = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNullOrEmpty]
		public string Identity { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000535D File Offset: 0x0000355D
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005365 File Offset: 0x00003565
		[Parameter]
		public OrganizationIdParameter Organization { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000536E File Offset: 0x0000356E
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005376 File Offset: 0x00003576
		[Parameter(ParameterSetName = "Summary")]
		public DateTime? StartReceivedDate { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000537F File Offset: 0x0000357F
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00005387 File Offset: 0x00003587
		[Parameter(ParameterSetName = "Summary")]
		public DateTime? EndReceivedDate { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005390 File Offset: 0x00003590
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00005398 File Offset: 0x00003598
		[Parameter(ParameterSetName = "Summary")]
		public string[] Domain { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000053A1 File Offset: 0x000035A1
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000053A9 File Offset: 0x000035A9
		[Parameter(ParameterSetName = "Summary")]
		public QuarantineMessageDirectionEnum? Direction { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000053B2 File Offset: 0x000035B2
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000053BA File Offset: 0x000035BA
		[Parameter(ParameterSetName = "Summary")]
		[ValidateLength(1, 320)]
		public string MessageId { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000053C3 File Offset: 0x000035C3
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000053CB File Offset: 0x000035CB
		[Parameter]
		[ValidateLength(1, 320)]
		public string[] SenderAddress { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000053D4 File Offset: 0x000035D4
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000053DC File Offset: 0x000035DC
		[Parameter(ParameterSetName = "Summary")]
		[ValidateLength(1, 320)]
		public string[] RecipientAddress { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000053E5 File Offset: 0x000035E5
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000053ED File Offset: 0x000035ED
		[ValidateLength(1, 320)]
		[Parameter(ParameterSetName = "Summary")]
		public string Subject { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000053F6 File Offset: 0x000035F6
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000053FE File Offset: 0x000035FE
		[Parameter(ParameterSetName = "Summary")]
		public QuarantineMessageTypeEnum? Type { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005407 File Offset: 0x00003607
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000540F File Offset: 0x0000360F
		[Parameter(ParameterSetName = "Summary")]
		[ValidateNotNullOrEmpty]
		public bool? Reported { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005418 File Offset: 0x00003618
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00005420 File Offset: 0x00003620
		[Parameter(ParameterSetName = "Summary")]
		public DateTime? StartExpiresDate { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005429 File Offset: 0x00003629
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00005431 File Offset: 0x00003631
		[Parameter(ParameterSetName = "Summary")]
		public DateTime? EndExpiresDate { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000543A File Offset: 0x0000363A
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00005442 File Offset: 0x00003642
		[Parameter(ParameterSetName = "Summary")]
		public int? Page { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000544B File Offset: 0x0000364B
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005453 File Offset: 0x00003653
		[Parameter(ParameterSetName = "Summary")]
		public int? PageSize { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000545C File Offset: 0x0000365C
		internal new OrganizationId ExecutingUserOrganizationId
		{
			get
			{
				return base.ExecutingUserOrganizationId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005464 File Offset: 0x00003664
		internal new OrganizationId CurrentOrganizationId
		{
			get
			{
				return base.CurrentOrganizationId;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000546C File Offset: 0x0000366C
		protected sealed override void InternalProcessRecord()
		{
			SystemProbe.Trace(GetQuarantineMessage.ComponentName, SystemProbe.Status.Pass, "Entering InternalProcessRecord", new object[0]);
			try
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.ManagementHelper");
				Type type = assembly.GetType("Microsoft.Exchange.Hygiene.ManagementHelper.FfoQuarantine.GetQuarantineMessageHelper");
				MethodInfo method = type.GetMethod("InternalProcessRecordHelper", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(GetQuarantineMessage)
				}, null);
				method.Invoke(null, new object[]
				{
					this
				});
			}
			catch (TargetInvocationException ex)
			{
				SystemProbe.Trace(GetQuarantineMessage.ComponentName, SystemProbe.Status.Fail, "TargetInvocationException in InternalProcessRecord: {0}", new object[]
				{
					ex.ToString()
				});
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				throw;
			}
			catch (Exception ex2)
			{
				SystemProbe.Trace(GetQuarantineMessage.ComponentName, SystemProbe.Status.Fail, "Unhandled Exception in InternalProcessRecord: {0}", new object[]
				{
					ex2.ToString()
				});
				throw;
			}
			SystemProbe.Trace(GetQuarantineMessage.ComponentName, SystemProbe.Status.Pass, "Exiting InternalProcessRecord", new object[0]);
		}

		// Token: 0x04000044 RID: 68
		private static readonly string ComponentName = "GetQuarantineMessage";
	}
}

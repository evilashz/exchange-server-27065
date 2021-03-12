using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000043 RID: 67
	internal class DataProviderCallLogEvent : ILogEvent
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000754A File Offset: 0x0000574A
		public DataProviderCallLogEvent(string eventId, UserContext userContext)
		{
			this.userContext = userContext;
			this.EventId = eventId;
			this.fileSize = -1;
			this.spCallsBuilder = new StringBuilder();
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00007572 File Offset: 0x00005772
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000757A File Offset: 0x0000577A
		public string EventId { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007583 File Offset: 0x00005783
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000758B File Offset: 0x0000578B
		public WebHeaderCollection ErrorResponseHeaders { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00007594 File Offset: 0x00005794
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000759C File Offset: 0x0000579C
		public AttachmentResultCode ResultCode { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000075A5 File Offset: 0x000057A5
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000075AD File Offset: 0x000057AD
		public int? NumberOfItems { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000075B6 File Offset: 0x000057B6
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000075BE File Offset: 0x000057BE
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = this.errorMessage + value + "| ";
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000075D8 File Offset: 0x000057D8
		public void Reset()
		{
			this.stopWatch = Stopwatch.StartNew();
			this.fileSize = -1;
			this.ResultCode = AttachmentResultCode.Success;
			this.error = null;
			this.errorMessage = string.Empty;
			this.spCallStopWatch = null;
			this.NumberOfItems = null;
			this.ErrorResponseHeaders = null;
			this.spCallsBuilder = new StringBuilder();
			this.totalSPCallTime = 0L;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007640 File Offset: 0x00005840
		public void SetFinish()
		{
			if (this.stopWatch.IsRunning)
			{
				this.stopWatch.Stop();
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000765A File Offset: 0x0000585A
		public void TrackSPCallBegin()
		{
			this.spCallStopWatch = Stopwatch.StartNew();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007668 File Offset: 0x00005868
		public void TrackSPCallEnd(string callName, string correlationId)
		{
			if (this.spCallStopWatch != null && this.spCallStopWatch.IsRunning)
			{
				this.spCallStopWatch.Stop();
				this.totalSPCallTime += this.spCallStopWatch.ElapsedMilliseconds;
				this.spCallsBuilder.AppendFormat("{0}_{1}_{2}|", callName, this.spCallStopWatch.ElapsedMilliseconds, correlationId);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000076D0 File Offset: 0x000058D0
		public void SetFileSize(int size)
		{
			this.fileSize = size;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000076D9 File Offset: 0x000058D9
		public void SetError(string error)
		{
			this.error = error;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000076E2 File Offset: 0x000058E2
		public void SetError(Exception exception)
		{
			this.error = exception.GetType().Name;
			this.ErrorMessage = exception.ToString();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007704 File Offset: 0x00005904
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("result", (int)this.ResultCode);
			if (this.userContext != null)
			{
				if (this.userContext.ExchangePrincipal != null)
				{
					dictionary.Add("user", this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				}
				if (this.error != null)
				{
					dictionary.Add("organizationId", this.userContext.LogonIdentity.GetOWAMiniRecipient().OrganizationId);
				}
			}
			if (this.stopWatch != null)
			{
				dictionary.Add("time", this.stopWatch.ElapsedMilliseconds.ToString());
			}
			if (this.spCallsBuilder != null)
			{
				dictionary.Add("spCalls", this.spCallsBuilder.ToString());
				dictionary.Add("totalSPCallTime", this.totalSPCallTime.ToString());
			}
			if (this.fileSize > -1)
			{
				dictionary.Add("size", this.fileSize);
			}
			if (this.NumberOfItems != null)
			{
				dictionary.Add("items", this.NumberOfItems.Value);
			}
			if (this.error != null)
			{
				dictionary.Add("error", this.error);
			}
			if (!string.IsNullOrEmpty(this.errorMessage))
			{
				dictionary.Add("errorMsg", this.errorMessage);
			}
			if (this.ErrorResponseHeaders != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				foreach (string text in this.ErrorResponseHeaders.AllKeys)
				{
					string text2 = this.ErrorResponseHeaders[text];
					if (!string.IsNullOrEmpty(text2))
					{
						stringBuilder.AppendFormat("{0}={1};", text, text2);
					}
				}
				stringBuilder.Append("]");
				dictionary.Add("responseHeaders", stringBuilder.ToString());
			}
			return dictionary;
		}

		// Token: 0x040000B2 RID: 178
		private Stopwatch stopWatch;

		// Token: 0x040000B3 RID: 179
		private Stopwatch spCallStopWatch;

		// Token: 0x040000B4 RID: 180
		private int fileSize;

		// Token: 0x040000B5 RID: 181
		private string error;

		// Token: 0x040000B6 RID: 182
		private string errorMessage;

		// Token: 0x040000B7 RID: 183
		private readonly UserContext userContext;

		// Token: 0x040000B8 RID: 184
		private StringBuilder spCallsBuilder;

		// Token: 0x040000B9 RID: 185
		private long totalSPCallTime;
	}
}

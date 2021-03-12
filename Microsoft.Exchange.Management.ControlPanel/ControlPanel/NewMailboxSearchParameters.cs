using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003DE RID: 990
	[DataContract]
	public class NewMailboxSearchParameters : BaseMailboxSearchParameters
	{
		// Token: 0x06003316 RID: 13078 RVA: 0x0009E638 File Offset: 0x0009C838
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.SearchAllDates)
			{
				base[SearchObjectSchema.StartDate] = null;
				base[SearchObjectSchema.EndDate] = null;
			}
			else
			{
				if (string.IsNullOrEmpty(this.SearchStartDate) && string.IsNullOrEmpty(this.SearchEndDate))
				{
					throw new FaultException(Strings.MailboxSearchSpecifySearchDate);
				}
				base[SearchObjectSchema.StartDate] = this.SearchStartDate.ToEcpExDateTime();
				base[SearchObjectSchema.EndDate] = this.SearchEndDate.ToEcpExDateTime();
			}
			if (this.SearchAllMailboxes)
			{
				base[SearchObjectSchema.SourceMailboxes] = null;
			}
			else
			{
				if (this.SourceMailboxes == null || this.SourceMailboxes.Length <= 0)
				{
					throw new FaultException(Strings.MailboxSearchSpecifySourceMailboxesError);
				}
				base[SearchObjectSchema.SourceMailboxes] = this.SourceMailboxes.ToIdParameters();
			}
			if (base.EstimateOnly)
			{
				base.ExcludeDuplicateMessages = false;
				base[SearchObjectSchema.LogLevel] = LoggingLevel.Suppress;
			}
			base[SearchObjectSchema.Language] = Thread.CurrentThread.CurrentUICulture.ToString();
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x0009E752 File Offset: 0x0009C952
		// (set) Token: 0x06003318 RID: 13080 RVA: 0x0009E75A File Offset: 0x0009C95A
		[DataMember]
		public bool SearchAllMailboxes { get; set; }

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x0009E763 File Offset: 0x0009C963
		// (set) Token: 0x0600331A RID: 13082 RVA: 0x0009E76B File Offset: 0x0009C96B
		[DataMember]
		public bool SearchAllDates { get; set; }

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x0600331B RID: 13083 RVA: 0x0009E774 File Offset: 0x0009C974
		// (set) Token: 0x0600331C RID: 13084 RVA: 0x0009E77C File Offset: 0x0009C97C
		[DataMember]
		public string SearchStartDate { get; set; }

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x0600331D RID: 13085 RVA: 0x0009E785 File Offset: 0x0009C985
		// (set) Token: 0x0600331E RID: 13086 RVA: 0x0009E78D File Offset: 0x0009C98D
		[DataMember]
		public string SearchEndDate { get; set; }

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x0009E796 File Offset: 0x0009C996
		// (set) Token: 0x06003320 RID: 13088 RVA: 0x0009E79E File Offset: 0x0009C99E
		[DataMember]
		public Identity[] SourceMailboxes { get; set; }

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x0009E7A7 File Offset: 0x0009C9A7
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-MailboxSearch";
			}
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x0009E7AE File Offset: 0x0009C9AE
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0400249C RID: 9372
		public const string RbacParameters = "?StartDate&EndDate&SourceMailboxes&Language";
	}
}

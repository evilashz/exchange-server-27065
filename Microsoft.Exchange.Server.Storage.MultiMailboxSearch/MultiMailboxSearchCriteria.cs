using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MultiMailboxSearch;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.MultiMailboxSearch
{
	// Token: 0x02000007 RID: 7
	internal sealed class MultiMailboxSearchCriteria
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003C1C File Offset: 0x00001E1C
		internal MultiMailboxSearchCriteria(Guid queryCorrelationId, SearchCriteria criteria, Guid mailboxGuid, int mailboxNumber, string query) : this(queryCorrelationId, criteria, mailboxGuid, mailboxNumber, query, 0, string.Empty, string.Empty)
		{
			MultiMailboxSearchCriteria.TraceFunction("Entering MultiMailboxSearchCriteria.ctor(searchCriteria, query)");
			MultiMailboxSearchCriteria.TraceFunction("Exiting MultiMailboxSearchCriteria.ctor(searchCriteria, query)");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003C58 File Offset: 0x00001E58
		internal MultiMailboxSearchCriteria(Guid queryCorrelationId, SearchCriteria criteria, Guid mailboxGuid, int mailboxNumber, string query, int pageSize, string sortSpecification, string paginationClause)
		{
			MultiMailboxSearchCriteria.TraceFunction("Entering MultiMailboxSearchCriteria.ctor");
			this.queryCorrelationId = queryCorrelationId;
			this.searchCriteria = criteria;
			this.mailboxGuid = mailboxGuid;
			this.query = query;
			this.pageSize = pageSize;
			this.sortSpecification = sortSpecification;
			this.paginationClause = paginationClause;
			this.mailboxNumber = mailboxNumber;
			MultiMailboxSearchCriteria.TraceFunction("Exiting MultiMailboxSearchCriteria.ctor");
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003CBC File Offset: 0x00001EBC
		internal string PaginationClause
		{
			get
			{
				return this.paginationClause;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003CC4 File Offset: 0x00001EC4
		internal int PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003CCC File Offset: 0x00001ECC
		internal string SortSpecification
		{
			get
			{
				return this.sortSpecification;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003CD4 File Offset: 0x00001ED4
		internal SearchCriteria SearchCriteria
		{
			get
			{
				return this.searchCriteria;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003CDC File Offset: 0x00001EDC
		internal string Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003CE4 File Offset: 0x00001EE4
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003CEC File Offset: 0x00001EEC
		internal Guid QueryCorrelationId
		{
			get
			{
				return this.queryCorrelationId;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003CF4 File Offset: 0x00001EF4
		internal int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003CFC File Offset: 0x00001EFC
		private static void TraceFunction(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (ExTraceGlobals.SearchTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.SearchTracer.TraceFunction(35728, 0L, message);
			}
		}

		// Token: 0x04000012 RID: 18
		private readonly SearchCriteria searchCriteria;

		// Token: 0x04000013 RID: 19
		private readonly Guid queryCorrelationId;

		// Token: 0x04000014 RID: 20
		private readonly Guid mailboxGuid;

		// Token: 0x04000015 RID: 21
		private readonly string query;

		// Token: 0x04000016 RID: 22
		private readonly int pageSize;

		// Token: 0x04000017 RID: 23
		private readonly string sortSpecification;

		// Token: 0x04000018 RID: 24
		private readonly string paginationClause;

		// Token: 0x04000019 RID: 25
		private readonly int mailboxNumber;
	}
}

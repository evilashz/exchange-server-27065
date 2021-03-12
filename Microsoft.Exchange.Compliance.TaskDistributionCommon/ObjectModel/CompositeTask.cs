using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000031 RID: 49
	internal class CompositeTask : ConfigurableObject
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public CompositeTask() : base(new SimplePropertyBag(ComplianceJobSchema.Identity, ComplianceJobSchema.ObjectState, ComplianceJobSchema.ExchangeVersion))
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007AD0 File Offset: 0x00005CD0
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public Guid TenantId { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007AE1 File Offset: 0x00005CE1
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007AE9 File Offset: 0x00005CE9
		public Guid JobRunId { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007AF2 File Offset: 0x00005CF2
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007AFA File Offset: 0x00005CFA
		public int TaskId { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007B03 File Offset: 0x00005D03
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007B0B File Offset: 0x00005D0B
		public ComplianceBindingType BindingType { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007B14 File Offset: 0x00005D14
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007B1C File Offset: 0x00005D1C
		public string UserMaster { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007B25 File Offset: 0x00005D25
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007B2D File Offset: 0x00005D2D
		public string UserList
		{
			get
			{
				return this.userList;
			}
			set
			{
				this.userList = value;
				this.users = (string.IsNullOrWhiteSpace(value) ? null : Utils.JsonStringToStringArray(value));
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007B4D File Offset: 0x00005D4D
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00007B55 File Offset: 0x00005D55
		public IEnumerable<string> Users
		{
			get
			{
				return this.users;
			}
			set
			{
				this.users = value;
				this.userList = ((value != null) ? Utils.StringArrayToJsonString(value) : null);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007B70 File Offset: 0x00005D70
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000D7 RID: 215
		private IEnumerable<string> users;

		// Token: 0x040000D8 RID: 216
		private string userList;
	}
}

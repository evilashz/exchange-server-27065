using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public abstract class ComplianceJob : ConfigurableObject
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000053FC File Offset: 0x000035FC
		public ComplianceJob() : base(new SimplePropertyBag(ComplianceJobSchema.Identity, ComplianceJobSchema.ObjectState, ComplianceJobSchema.ExchangeVersion))
		{
			this.CreatedTime = ComplianceJobConstants.MinComplianceTime;
			this.LastModifiedTime = ComplianceJobConstants.MinComplianceTime;
			this.JobStartTime = ComplianceJobConstants.MinComplianceTime;
			this.JobEndTime = ComplianceJobConstants.MinComplianceTime;
			this.bindings.Add(ComplianceBindingType.ExchangeBinding, new ComplianceBinding
			{
				BindingType = ComplianceBindingType.ExchangeBinding
			});
			this.bindings.Add(ComplianceBindingType.PublicFolderBinding, new ComplianceBinding
			{
				BindingType = ComplianceBindingType.PublicFolderBinding
			});
			this.bindings.Add(ComplianceBindingType.SharePointBinding, new ComplianceBinding
			{
				BindingType = ComplianceBindingType.SharePointBinding
			});
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000054A8 File Offset: 0x000036A8
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000054B0 File Offset: 0x000036B0
		public new ObjectId Identity
		{
			get
			{
				return base.Identity;
			}
			internal set
			{
				this.propertyBag.SetField(ComplianceJobSchema.Identity, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000054C4 File Offset: 0x000036C4
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000054D6 File Offset: 0x000036D6
		public string Name
		{
			get
			{
				return (string)this[ComplianceJobSchema.DisplayName];
			}
			set
			{
				this[ComplianceJobSchema.DisplayName] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000054E4 File Offset: 0x000036E4
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000054F6 File Offset: 0x000036F6
		public DateTime CreatedTime
		{
			get
			{
				return (DateTime)this[ComplianceJobSchema.CreatedTime];
			}
			set
			{
				this[ComplianceJobSchema.CreatedTime] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005509 File Offset: 0x00003709
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000551B File Offset: 0x0000371B
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)this[ComplianceJobSchema.LastModifiedTime];
			}
			set
			{
				this[ComplianceJobSchema.LastModifiedTime] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000552E File Offset: 0x0000372E
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005540 File Offset: 0x00003740
		public DateTime JobStartTime
		{
			get
			{
				return (DateTime)this[ComplianceJobSchema.JobStartTime];
			}
			set
			{
				this[ComplianceJobSchema.JobStartTime] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005553 File Offset: 0x00003753
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005565 File Offset: 0x00003765
		public DateTime JobEndTime
		{
			get
			{
				return (DateTime)this[ComplianceJobSchema.JobEndTime];
			}
			set
			{
				this[ComplianceJobSchema.JobEndTime] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005578 File Offset: 0x00003778
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000558A File Offset: 0x0000378A
		public string Description
		{
			get
			{
				return (string)this[ComplianceJobSchema.Description];
			}
			set
			{
				this[ComplianceJobSchema.Description] = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005598 File Offset: 0x00003798
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000055AA File Offset: 0x000037AA
		public string CreatedBy
		{
			get
			{
				return (string)this[ComplianceJobSchema.CreatedBy];
			}
			internal set
			{
				this[ComplianceJobSchema.CreatedBy] = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000055B8 File Offset: 0x000037B8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000055CA File Offset: 0x000037CA
		public string RunBy
		{
			get
			{
				return (string)this[ComplianceJobSchema.RunBy];
			}
			internal set
			{
				this[ComplianceJobSchema.RunBy] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000055D8 File Offset: 0x000037D8
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00005600 File Offset: 0x00003800
		public Guid TenantId
		{
			get
			{
				if (this[ComplianceJobSchema.TenantId] != null)
				{
					return (Guid)this[ComplianceJobSchema.TenantId];
				}
				return Guid.Empty;
			}
			internal set
			{
				this[ComplianceJobSchema.TenantId] = value;
				foreach (ComplianceBindingType key in this.bindings.Keys)
				{
					this.bindings[key].TenantId = value;
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005674 File Offset: 0x00003874
		public int NumBindings
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in this.bindings)
				{
					num += keyValuePair.Value.NumBindings;
				}
				return num;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000056D4 File Offset: 0x000038D4
		public int NumBindingsFailed
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in this.bindings)
				{
					num += keyValuePair.Value.NumBindingsFailed;
				}
				return num;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005754 File Offset: 0x00003954
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000058A8 File Offset: 0x00003AA8
		public ComplianceJobStatus JobStatus
		{
			get
			{
				bool flag = false;
				HashSet<ComplianceJobStatus> hashSet = new HashSet<ComplianceJobStatus>();
				foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in this.bindings)
				{
					hashSet.Add(keyValuePair.Value.JobStatus);
					switch (keyValuePair.Value.JobStatus)
					{
					case ComplianceJobStatus.Starting:
					case ComplianceJobStatus.InProgress:
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (hashSet.Any((ComplianceJobStatus status) => status == ComplianceJobStatus.Starting))
					{
						return ComplianceJobStatus.Starting;
					}
					return ComplianceJobStatus.InProgress;
				}
				else
				{
					if (hashSet.Any((ComplianceJobStatus status) => status == ComplianceJobStatus.NotStarted))
					{
						return ComplianceJobStatus.NotStarted;
					}
					if (hashSet.Any((ComplianceJobStatus status) => status == ComplianceJobStatus.Stopped))
					{
						return ComplianceJobStatus.Stopped;
					}
					if (hashSet.All((ComplianceJobStatus status) => status == ComplianceJobStatus.Succeeded))
					{
						return ComplianceJobStatus.Succeeded;
					}
					if (hashSet.All((ComplianceJobStatus status) => status == ComplianceJobStatus.Failed))
					{
						return ComplianceJobStatus.Failed;
					}
					return ComplianceJobStatus.PartiallySucceeded;
				}
			}
			set
			{
				this[ComplianceJobSchema.JobStatus] = value;
				foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in this.bindings)
				{
					keyValuePair.Value.JobStatus = value;
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00005914 File Offset: 0x00003B14
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000592C File Offset: 0x00003B2C
		public MultiValuedProperty<string> ExchangeBindings
		{
			get
			{
				return this.bindings[ComplianceBindingType.ExchangeBinding].BindingsArray;
			}
			internal set
			{
				this.bindings[ComplianceBindingType.ExchangeBinding].BindingsArray = ((value == null) ? null : value.ToArray());
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000594B File Offset: 0x00003B4B
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005963 File Offset: 0x00003B63
		public MultiValuedProperty<string> PublicFolderBindings
		{
			get
			{
				return this.bindings[ComplianceBindingType.PublicFolderBinding].BindingsArray;
			}
			internal set
			{
				this.bindings[ComplianceBindingType.PublicFolderBinding].BindingsArray = ((value == null) ? null : value.ToArray());
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005982 File Offset: 0x00003B82
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000599A File Offset: 0x00003B9A
		public MultiValuedProperty<string> SharePointBindings
		{
			get
			{
				return this.bindings[ComplianceBindingType.SharePointBinding].BindingsArray;
			}
			internal set
			{
				this.bindings[ComplianceBindingType.SharePointBinding].BindingsArray = ((value == null) ? null : value.ToArray());
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000059B9 File Offset: 0x00003BB9
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000059CC File Offset: 0x00003BCC
		public bool AllExchangeBindings
		{
			get
			{
				return this.bindings[ComplianceBindingType.ExchangeBinding].AllBindings;
			}
			set
			{
				this.bindings[ComplianceBindingType.ExchangeBinding].AllBindings = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000059E0 File Offset: 0x00003BE0
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000059F3 File Offset: 0x00003BF3
		public bool AllPublicFolderBindings
		{
			get
			{
				return this.bindings[ComplianceBindingType.PublicFolderBinding].AllBindings;
			}
			set
			{
				this.bindings[ComplianceBindingType.PublicFolderBinding].AllBindings = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005A07 File Offset: 0x00003C07
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005A1A File Offset: 0x00003C1A
		public bool AllSharePointBindings
		{
			get
			{
				return this.bindings[ComplianceBindingType.SharePointBinding].AllBindings;
			}
			set
			{
				this.bindings[ComplianceBindingType.SharePointBinding].AllBindings = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005A2E File Offset: 0x00003C2E
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005A54 File Offset: 0x00003C54
		public Guid JobRunId
		{
			get
			{
				if (this[ComplianceJobSchema.JobRunId] != null)
				{
					return (Guid)this[ComplianceJobSchema.JobRunId];
				}
				return Guid.Empty;
			}
			internal set
			{
				this[ComplianceJobSchema.JobRunId] = value;
				foreach (KeyValuePair<ComplianceBindingType, ComplianceBinding> keyValuePair in this.bindings)
				{
					keyValuePair.Value.JobRunId = value;
				}
				this.NewRunId = true;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005ADA File Offset: 0x00003CDA
		public bool Resume
		{
			get
			{
				return (bool)this[ComplianceJobSchema.Resume];
			}
			set
			{
				this[ComplianceJobSchema.Resume] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D1 RID: 209
		// (set) Token: 0x060000D2 RID: 210
		internal abstract byte[] JobData { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005AED File Offset: 0x00003CED
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005AFF File Offset: 0x00003CFF
		internal ComplianceJobObjectVersion JobObjectVersion
		{
			get
			{
				return (ComplianceJobObjectVersion)this[ComplianceJobSchema.JobObjectVersion];
			}
			set
			{
				this[ComplianceJobSchema.JobObjectVersion] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005B12 File Offset: 0x00003D12
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00005B24 File Offset: 0x00003D24
		internal ComplianceJobType JobType
		{
			get
			{
				return (ComplianceJobType)this[ComplianceJobSchema.JobType];
			}
			set
			{
				this[ComplianceJobSchema.JobType] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005B37 File Offset: 0x00003D37
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00005B49 File Offset: 0x00003D49
		internal byte[] TenantInfo
		{
			get
			{
				return (byte[])this[ComplianceJobSchema.TenantInfo];
			}
			set
			{
				this[ComplianceJobSchema.TenantInfo] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005B57 File Offset: 0x00003D57
		internal Dictionary<ComplianceBindingType, ComplianceBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005B5F File Offset: 0x00003D5F
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005B67 File Offset: 0x00003D67
		internal bool NewRunId { get; set; }

		// Token: 0x060000DC RID: 220 RVA: 0x00005B70 File Offset: 0x00003D70
		internal bool IsRunning()
		{
			switch (this.JobStatus)
			{
			case ComplianceJobStatus.Starting:
			case ComplianceJobStatus.InProgress:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005B9C File Offset: 0x00003D9C
		internal void CopyFromRow(TempDatabase.ComplianceJobTable row)
		{
			this.Name = row.DisplayName;
			this.CreatedTime = row.CreateTime;
			this.LastModifiedTime = row.LastModifiedTime;
			this.JobStartTime = row.StartTime;
			this.JobEndTime = row.EndTime;
			this.Identity = new ComplianceJobId(row.JobId);
			this.Description = row.Description;
			this.JobObjectVersion = row.JobObjectVersion;
			this.TenantId = row.TenantId;
			this.JobType = row.JobType;
			this.CreatedBy = row.CreatedBy;
			this.RunBy = row.RunBy;
			this.JobRunId = row.JobRunId;
			this.TenantInfo = row.TenantInfo;
			this.JobData = row.JobData;
			this.Resume = row.Resume;
			this.NewRunId = false;
		}

		// Token: 0x060000DE RID: 222
		internal abstract void UpdateJobResults();

		// Token: 0x04000071 RID: 113
		private Dictionary<ComplianceBindingType, ComplianceBinding> bindings = new Dictionary<ComplianceBindingType, ComplianceBinding>();
	}
}

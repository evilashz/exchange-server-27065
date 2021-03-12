using System;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000010 RID: 16
	internal class FailureEntry : DocEntry, IFailureEntry, IDocEntry, IEquatable<IDocEntry>, IEquatable<IFailureEntry>
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00005690 File Offset: 0x00003890
		public FailureEntry()
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005698 File Offset: 0x00003898
		public FailureEntry(ISearchResultItem item) : base(item)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000056A1 File Offset: 0x000038A1
		IIdentity IFailureEntry.ItemId
		{
			get
			{
				return base.ItemId;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000056A9 File Offset: 0x000038A9
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000056C3 File Offset: 0x000038C3
		public EvaluationErrors ErrorCode
		{
			get
			{
				if (this.errorCode == EvaluationErrors.None && this.IsPartiallyIndexed)
				{
					return EvaluationErrors.MarsWriterTruncation;
				}
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000056CC File Offset: 0x000038CC
		public LocalizedString ErrorDescription
		{
			get
			{
				return EvaluationErrorsHelper.GetErrorDescription(this.ErrorCode);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000056D9 File Offset: 0x000038D9
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000056E1 File Offset: 0x000038E1
		public string AdditionalInfo { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000056EA File Offset: 0x000038EA
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000056F2 File Offset: 0x000038F2
		public bool IsPartiallyIndexed { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000056FB File Offset: 0x000038FB
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00005703 File Offset: 0x00003903
		public DateTime? LastAttemptTime { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000570C File Offset: 0x0000390C
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005714 File Offset: 0x00003914
		public int AttemptCount { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000571D File Offset: 0x0000391D
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005725 File Offset: 0x00003925
		public bool IsPermanentFailure { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000572E File Offset: 0x0000392E
		internal new static IndexSystemField[] Schema
		{
			get
			{
				return FailureEntry.schema;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005738 File Offset: 0x00003938
		public static IndexSystemField[] GetSchema(FieldSet fieldSet)
		{
			switch (fieldSet)
			{
			case FieldSet.None:
				return Array<IndexSystemField>.Empty;
			case FieldSet.Default:
				return FailureEntry.Schema;
			case FieldSet.RetryFeederProperties:
				return FailureEntry.RetryFeederFields;
			case FieldSet.IndexRepairAssistant:
				return FailureEntry.IndexRepairAssistantFields;
			default:
				throw new ArgumentException(string.Format("Field set value '{0}' is not valid in this context.", fieldSet), "fieldSet");
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005794 File Offset: 0x00003994
		public override string ToString()
		{
			return string.Format("ItemId: {0}, ErrorCode: {1}{2}, Attempt: {3}", new object[]
			{
				base.ItemId,
				this.ErrorCode,
				(this.ErrorCode == EvaluationErrors.None) ? string.Empty : (this.IsPermanentFailure ? "(permanent)" : "(retriable)"),
				this.AttemptCount
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005800 File Offset: 0x00003A00
		public override bool Equals(object other)
		{
			return other is FailureEntry && base.Equals(other);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005820 File Offset: 0x00003A20
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005828 File Offset: 0x00003A28
		public bool Equals(IFailureEntry other)
		{
			return other != null && base.Equals(other);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005838 File Offset: 0x00003A38
		protected override void SetProp(string name, object value)
		{
			if (name == FastIndexSystemSchema.ErrorCode.Name)
			{
				this.ErrorCode = EvaluationErrorsHelper.GetErrorCode((int)((long)value));
				this.IsPermanentFailure = EvaluationErrorsHelper.IsPermanentError((int)((long)value));
				return;
			}
			if (name == FastIndexSystemSchema.ErrorMessage.Name)
			{
				this.AdditionalInfo = (string)value;
				return;
			}
			if (name == FastIndexSystemSchema.IsPartiallyProcessed.Name)
			{
				this.IsPartiallyIndexed = (bool)value;
				return;
			}
			if (name == FastIndexSystemSchema.LastAttemptTime.Name)
			{
				this.LastAttemptTime = new DateTime?((DateTime)value);
				return;
			}
			if (name == FastIndexSystemSchema.AttemptCount.Name)
			{
				this.AttemptCount = (int)((long)value);
			}
		}

		// Token: 0x04000046 RID: 70
		private static readonly IndexSystemField[] schema = new IndexSystemField[]
		{
			FastIndexSystemSchema.ItemId.Definition,
			FastIndexSystemSchema.MailboxGuid.Definition,
			FastIndexSystemSchema.AttemptCount.Definition,
			FastIndexSystemSchema.ErrorCode.Definition,
			FastIndexSystemSchema.ErrorMessage.Definition,
			FastIndexSystemSchema.LastAttemptTime.Definition,
			FastIndexSystemSchema.IsPartiallyProcessed.Definition
		};

		// Token: 0x04000047 RID: 71
		private static readonly IndexSystemField[] RetryFeederFields = new IndexSystemField[]
		{
			FastIndexSystemSchema.ItemId.Definition,
			FastIndexSystemSchema.MailboxGuid.Definition,
			FastIndexSystemSchema.AttemptCount.Definition,
			FastIndexSystemSchema.ErrorCode.Definition,
			FastIndexSystemSchema.LastAttemptTime.Definition
		};

		// Token: 0x04000048 RID: 72
		private static readonly IndexSystemField[] IndexRepairAssistantFields = new IndexSystemField[]
		{
			FastIndexSystemSchema.ErrorCode.Definition
		};

		// Token: 0x04000049 RID: 73
		private EvaluationErrors errorCode;
	}
}

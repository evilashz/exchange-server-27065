using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000041 RID: 65
	public abstract class DataAccessOperator : DisposableBase, IOperationExecutionTrackable
	{
		// Token: 0x060002EC RID: 748 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		protected DataAccessOperator(IConnectionProvider connectionProvider, DataAccessOperator.DataAccessOperatorDefinition operatorDefinition)
		{
			this.connectionProvider = connectionProvider;
			this.operatorDefinition = operatorDefinition;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00010C38 File Offset: 0x0000EE38
		public CultureInfo Culture
		{
			get
			{
				return this.OperatorDefinitionBase.Culture;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00010C45 File Offset: 0x0000EE45
		public CompareInfo CompareInfo
		{
			get
			{
				return this.OperatorDefinitionBase.CompareInfo;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00010C52 File Offset: 0x0000EE52
		public IConnectionProvider ConnectionProvider
		{
			get
			{
				return this.connectionProvider;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00010C5A File Offset: 0x0000EE5A
		public Connection Connection
		{
			get
			{
				return this.connectionProvider.GetConnection();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00010C67 File Offset: 0x0000EE67
		protected bool FrequentOperation
		{
			get
			{
				return this.OperatorDefinitionBase.FrequentOperation;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00010C74 File Offset: 0x0000EE74
		internal string OperatorName
		{
			get
			{
				return this.OperatorDefinitionBase.OperatorName;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00010C81 File Offset: 0x0000EE81
		public virtual bool Interrupted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00010C84 File Offset: 0x0000EE84
		protected DataAccessOperator.DataAccessOperatorDefinition OperatorDefinitionBase
		{
			get
			{
				return this.operatorDefinition;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00010C8C File Offset: 0x0000EE8C
		public virtual bool EnableInterrupts(IInterruptControl interruptControl)
		{
			return false;
		}

		// Token: 0x060002F6 RID: 758
		public abstract void EnumerateDescendants(Action<DataAccessOperator> operatorAction);

		// Token: 0x060002F7 RID: 759 RVA: 0x00010C8F File Offset: 0x0000EE8F
		public void GetDescendants(List<DataAccessOperator> planOperators)
		{
			this.EnumerateDescendants(new Action<DataAccessOperator>(planOperators.Add));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00010CA3 File Offset: 0x0000EEA3
		public virtual void RemoveChildren()
		{
		}

		// Token: 0x060002F9 RID: 761
		public abstract object ExecuteScalar();

		// Token: 0x060002FA RID: 762 RVA: 0x00010CA5 File Offset: 0x0000EEA5
		internal static IExecutionPlanner GetExecutionPlannerOrNull(SimpleQueryOperator sqo)
		{
			if (sqo != null)
			{
				return sqo.GetExecutionPlanner();
			}
			return null;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00010CB2 File Offset: 0x0000EEB2
		protected void TraceOperation(string operation)
		{
			if (this.summaryTracingEnabled)
			{
				this.DoTraceOperation(operation, null);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		protected void TraceOperation(string operation, string extraData)
		{
			if (this.summaryTracingEnabled)
			{
				this.DoTraceOperation(operation, extraData);
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00010CD8 File Offset: 0x0000EED8
		private void DoTraceOperation(string operation, string extraData)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			this.AppendOperationInfo(operation, stringBuilder);
			if (DataAccessOperator.MultiLineTracing)
			{
				stringBuilder.Append('\n');
				stringBuilder.Append('\t', 1);
			}
			else
			{
				stringBuilder.Append(" ");
			}
			StringFormatOptions stringFormatOptions = StringFormatOptions.None;
			if (this.detailTracingEnabled)
			{
				stringFormatOptions |= StringFormatOptions.IncludeDetails;
				stringFormatOptions |= StringFormatOptions.IncludeNestedObjectsId;
			}
			if (DataAccessOperator.MultiLineTracing)
			{
				stringFormatOptions |= StringFormatOptions.MultiLine;
			}
			this.OperatorDefinitionBase.AppendToStringBuilder(stringBuilder, stringFormatOptions, 1);
			if (extraData != null)
			{
				if (DataAccessOperator.MultiLineTracing)
				{
					stringBuilder.Append('\n');
					stringBuilder.Append('\t', 1);
				}
				else
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(extraData);
			}
			ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00010D8D File Offset: 0x0000EF8D
		protected void TraceOperationResult(string operation, Column resultColumn, object result)
		{
			if (this.summaryTracingEnabled)
			{
				this.DoTraceOperationResult(operation, resultColumn, result);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		private void DoTraceOperationResult(string operation, Column resultColumn, object result)
		{
			if (this.summaryTracingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				this.AppendOperationInfo(operation, stringBuilder);
				stringBuilder.Append("  result:[");
				if (resultColumn != null)
				{
					stringBuilder.Append(resultColumn.Name);
					stringBuilder.Append("=");
				}
				stringBuilder.AppendAsString(result);
				stringBuilder.Append("]");
				ExTraceGlobals.DbInteractionSummaryTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00010E18 File Offset: 0x0000F018
		protected void TraceCrumb(string operation)
		{
			if (this.detailTracingEnabled)
			{
				this.DoTraceSimpleOperation(operation, null, ExTraceGlobals.DbInteractionDetailTracer);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00010E2F File Offset: 0x0000F02F
		protected void TraceCrumb(string operation, object crumb)
		{
			if (this.detailTracingEnabled)
			{
				this.DoTraceSimpleOperation(operation, crumb.ToString(), ExTraceGlobals.DbInteractionDetailTracer);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00010E4B File Offset: 0x0000F04B
		protected void TraceMove(string operation, bool rowFound)
		{
			if (this.detailTracingEnabled)
			{
				this.DoTraceSimpleOperation(operation, rowFound ? "row found" : "no more rows", ExTraceGlobals.DbInteractionDetailTracer);
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010E70 File Offset: 0x0000F070
		private void DoTraceSimpleOperation(string operation, string extraData, Microsoft.Exchange.Diagnostics.Trace trace)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			this.AppendOperationInfo(operation, stringBuilder);
			if (extraData != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(extraData);
			}
			trace.TraceDebug(0L, stringBuilder.ToString());
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		protected void AppendOperationInfo(string operation, StringBuilder sb)
		{
			if (this.detailTracingEnabled)
			{
				sb.Append("cn:[");
				sb.Append(this.ConnectionProvider.GetHashCode());
				sb.Append("] ");
			}
			sb.Append(operation);
			if (this.detailTracingEnabled)
			{
				sb.Append(" op:[");
				sb.Append(this.OperatorName);
				sb.Append(" ");
				sb.Append(this.GetHashCode());
				sb.Append("]");
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00010F41 File Offset: 0x0000F141
		public override string ToString()
		{
			return this.ToString(StringFormatOptions.IncludeDetails);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00010F4C File Offset: 0x0000F14C
		public string ToString(StringFormatOptions formatOptions)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			this.AppendToStringBuilder(stringBuilder, formatOptions, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00010F70 File Offset: 0x0000F170
		internal void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel)
		{
			this.OperatorDefinitionBase.AppendToStringBuilder(sb, formatOptions, nestingLevel);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00010F80 File Offset: 0x0000F180
		[Conditional("DEBUG")]
		protected void ValidateCulture()
		{
			List<DataAccessOperator> list = new List<DataAccessOperator>(10);
			this.GetDescendants(list);
			bool flag = true;
			foreach (DataAccessOperator dataAccessOperator in list)
			{
				if (flag)
				{
					CultureInfo culture = dataAccessOperator.Culture;
					flag = false;
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00010FE4 File Offset: 0x0000F1E4
		public IOperationExecutionTrackingKey GetTrackingKey()
		{
			return this.OperatorDefinitionBase;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public virtual IExecutionPlanner GetExecutionPlanner()
		{
			return this.OperatorDefinitionBase.ExecutionPlanner;
		}

		// Token: 0x040000EE RID: 238
		internal static bool MultiLineTracing;

		// Token: 0x040000EF RID: 239
		protected bool summaryTracingEnabled = ExTraceGlobals.DbInteractionSummaryTracer.IsTraceEnabled(TraceType.DebugTrace);

		// Token: 0x040000F0 RID: 240
		protected bool intermediateTracingEnabled = ExTraceGlobals.DbInteractionIntermediateTracer.IsTraceEnabled(TraceType.DebugTrace);

		// Token: 0x040000F1 RID: 241
		protected bool detailTracingEnabled = ExTraceGlobals.DbInteractionDetailTracer.IsTraceEnabled(TraceType.DebugTrace);

		// Token: 0x040000F2 RID: 242
		private IConnectionProvider connectionProvider;

		// Token: 0x040000F3 RID: 243
		private DataAccessOperator.DataAccessOperatorDefinition operatorDefinition;

		// Token: 0x02000042 RID: 66
		public abstract class DataAccessOperatorDefinition : IOperationExecutionTrackingKey
		{
			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600030C RID: 780
			internal abstract string OperatorName { get; }

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x0600030D RID: 781 RVA: 0x00010FFB File Offset: 0x0000F1FB
			public CultureInfo Culture
			{
				get
				{
					return this.culture;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x0600030E RID: 782 RVA: 0x00011003 File Offset: 0x0000F203
			public CompareInfo CompareInfo
			{
				get
				{
					return this.compareInfo;
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600030F RID: 783 RVA: 0x0001100B File Offset: 0x0000F20B
			public bool FrequentOperation
			{
				get
				{
					return this.frequentOperation;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000310 RID: 784 RVA: 0x00011013 File Offset: 0x0000F213
			public IExecutionPlanner ExecutionPlanner
			{
				get
				{
					return this.planner;
				}
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0001101B File Offset: 0x0000F21B
			protected DataAccessOperatorDefinition(CultureInfo culture, bool frequentOperation)
			{
				this.culture = culture;
				if (culture != null)
				{
					this.compareInfo = culture.CompareInfo;
				}
				this.frequentOperation = frequentOperation;
			}

			// Token: 0x06000312 RID: 786 RVA: 0x00011040 File Offset: 0x0000F240
			int IOperationExecutionTrackingKey.GetTrackingKeyHashValue()
			{
				if (this.trackingKeyHashValue == 0)
				{
					this.CalculateHashValueForStatisticPurposes(out this.simpleHashValue, out this.trackingKeyHashValue);
				}
				return this.trackingKeyHashValue;
			}

			// Token: 0x06000313 RID: 787 RVA: 0x00011062 File Offset: 0x0000F262
			int IOperationExecutionTrackingKey.GetSimpleHashValue()
			{
				if (this.trackingKeyHashValue == 0)
				{
					this.CalculateHashValueForStatisticPurposes(out this.simpleHashValue, out this.trackingKeyHashValue);
				}
				return this.simpleHashValue;
			}

			// Token: 0x06000314 RID: 788 RVA: 0x00011084 File Offset: 0x0000F284
			bool IOperationExecutionTrackingKey.IsTrackingKeyEqualTo(IOperationExecutionTrackingKey other)
			{
				return this.IsEqualsForStatisticPurposes((DataAccessOperator.DataAccessOperatorDefinition)other);
			}

			// Token: 0x06000315 RID: 789 RVA: 0x00011094 File Offset: 0x0000F294
			string IOperationExecutionTrackingKey.TrackingKeyToString()
			{
				StringBuilder stringBuilder = new StringBuilder(500);
				this.AppendToStringBuilder(stringBuilder, StringFormatOptions.SkipParametersData, 0);
				return stringBuilder.ToString();
			}

			// Token: 0x06000316 RID: 790
			public abstract void EnumerateDescendants(Action<DataAccessOperator.DataAccessOperatorDefinition> operatorDefinitionAction);

			// Token: 0x06000317 RID: 791 RVA: 0x000110BB File Offset: 0x0000F2BB
			public void AttachPlanner(IExecutionPlanner planner)
			{
				this.planner = planner;
			}

			// Token: 0x06000318 RID: 792 RVA: 0x000110C4 File Offset: 0x0000F2C4
			public override string ToString()
			{
				return this.ToString(StringFormatOptions.IncludeDetails);
			}

			// Token: 0x06000319 RID: 793 RVA: 0x000110D0 File Offset: 0x0000F2D0
			public string ToString(StringFormatOptions formatOptions)
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				this.AppendToStringBuilder(stringBuilder, formatOptions, 0);
				return stringBuilder.ToString();
			}

			// Token: 0x0600031A RID: 794 RVA: 0x000110F4 File Offset: 0x0000F2F4
			internal static void AppendColumnsSummaryToStringBuilder(StringBuilder sb, IList<Column> columns, IList<object> values, StringFormatOptions formatOptions)
			{
				for (int i = 0; i < columns.Count; i++)
				{
					if (i != 0)
					{
						sb.Append(", ");
					}
					columns[i].AppendToString(sb, formatOptions);
					if (values != null && i < values.Count && (formatOptions & StringFormatOptions.SkipParametersData) == StringFormatOptions.None)
					{
						sb.Append("=[");
						if ((formatOptions & StringFormatOptions.IncludeDetails) == StringFormatOptions.IncludeDetails || !(values[i] is byte[]) || ((byte[])values[i]).Length <= 32)
						{
							sb.AppendAsString(values[i]);
						}
						else
						{
							sb.Append("<long blob>");
						}
						sb.Append("]");
					}
				}
			}

			// Token: 0x0600031B RID: 795 RVA: 0x000111A0 File Offset: 0x0000F3A0
			internal static void CheckValueSizes(IList<Column> columns, IList<object> values)
			{
				for (int i = 0; i < columns.Count; i++)
				{
					if (columns[i].MaxLength != 0 && values[i] != null)
					{
						int num = Math.Max(columns[i].MaxLength, columns[i].Size);
						if (columns[i].ExtendedTypeCode == ExtendedTypeCode.Binary)
						{
							byte[] array = values[i] as byte[];
							int num2;
							if (array != null)
							{
								num2 = array.Length;
							}
							else if (values[i] is ArraySegment<byte>)
							{
								num2 = ((ArraySegment<byte>)values[i]).Count;
							}
							else
							{
								num2 = ((FunctionColumn)values[i]).MaxLength;
							}
							if (num < num2)
							{
								DiagnosticContext.TraceDwordAndString((LID)51360U, (uint)num, columns[i].Name);
								DiagnosticContext.TraceDword((LID)34976U, (uint)num2);
								throw new StoreException((LID)52704U, ErrorCodeValue.TooBig, string.Format("Value too big. Column {0}, Size {1}", columns[i].Name, num2));
							}
						}
						else if (columns[i].ExtendedTypeCode == ExtendedTypeCode.String)
						{
							int length = ((string)values[i]).Length;
							if (num < length)
							{
								DiagnosticContext.TraceDwordAndString((LID)59552U, (uint)num, columns[i].Name);
								DiagnosticContext.TraceDword((LID)45216U, (uint)length);
								throw new StoreException((LID)46560U, ErrorCodeValue.TooBig, string.Format("Value too big. Column {0}, Size {1}", columns[i].Name, length));
							}
						}
					}
				}
			}

			// Token: 0x0600031C RID: 796
			internal abstract void AppendToStringBuilder(StringBuilder sb, StringFormatOptions formatOptions, int nestingLevel);

			// Token: 0x0600031D RID: 797 RVA: 0x0001134B File Offset: 0x0000F54B
			protected void Indent(StringBuilder sb, bool multiLine, int nestingLevel, bool additionalSpace)
			{
				if (multiLine)
				{
					sb.Append('\n');
					sb.Append('\t', nestingLevel);
					if (additionalSpace)
					{
						sb.Append("  ");
					}
				}
			}

			// Token: 0x0600031E RID: 798
			internal abstract void CalculateHashValueForStatisticPurposes(out int simple, out int detail);

			// Token: 0x0600031F RID: 799
			internal abstract bool IsEqualsForStatisticPurposes(DataAccessOperator.DataAccessOperatorDefinition other);

			// Token: 0x040000F4 RID: 244
			private readonly bool frequentOperation;

			// Token: 0x040000F5 RID: 245
			private CultureInfo culture;

			// Token: 0x040000F6 RID: 246
			private CompareInfo compareInfo;

			// Token: 0x040000F7 RID: 247
			private int trackingKeyHashValue;

			// Token: 0x040000F8 RID: 248
			private int simpleHashValue;

			// Token: 0x040000F9 RID: 249
			private IExecutionPlanner planner;
		}
	}
}

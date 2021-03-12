using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006BC RID: 1724
	[DataContract(Name = "{0}Results")]
	[KnownType(typeof(ValidatorInfo[]))]
	public class PowerShellResults<O> : PowerShellResults, IEnumerable<O>, IEnumerable
	{
		// Token: 0x170027F6 RID: 10230
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x000E050B File Offset: 0x000DE70B
		public bool SucceededWithValue
		{
			get
			{
				return base.Succeeded && this.HasValue;
			}
		}

		// Token: 0x170027F7 RID: 10231
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x000E051D File Offset: 0x000DE71D
		public bool HasValue
		{
			get
			{
				return this.Output != null && this.Output.Length == 1;
			}
		}

		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x000E0534 File Offset: 0x000DE734
		public O Value
		{
			get
			{
				if (this.HasValue)
				{
					return this.Output[0];
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x000E0550 File Offset: 0x000DE750
		// (set) Token: 0x0600498D RID: 18829 RVA: 0x000E0558 File Offset: 0x000DE758
		public AsyncGetListContext AsyncGetListContext { get; set; }

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x000E0561 File Offset: 0x000DE761
		// (set) Token: 0x0600498F RID: 18831 RVA: 0x000E0569 File Offset: 0x000DE769
		[DataMember(EmitDefaultValue = false)]
		public string[] SortColumnNames { get; set; }

		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x000E0572 File Offset: 0x000DE772
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x000E057A File Offset: 0x000DE77A
		[DataMember(EmitDefaultValue = false)]
		public int[][] SortData { get; set; }

		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x000E0583 File Offset: 0x000DE783
		// (set) Token: 0x06004993 RID: 18835 RVA: 0x000E058B File Offset: 0x000DE78B
		[DataMember(EmitDefaultValue = false)]
		public string[] SortDataRawId { get; set; }

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x000E0594 File Offset: 0x000DE794
		// (set) Token: 0x06004995 RID: 18837 RVA: 0x000E059C File Offset: 0x000DE79C
		[DataMember(EmitDefaultValue = false)]
		public int StartIndex { get; set; }

		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x000E05A5 File Offset: 0x000DE7A5
		// (set) Token: 0x06004997 RID: 18839 RVA: 0x000E05AD File Offset: 0x000DE7AD
		[DataMember(EmitDefaultValue = false)]
		public int EndIndex { get; set; }

		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x000E05B6 File Offset: 0x000DE7B6
		// (set) Token: 0x06004999 RID: 18841 RVA: 0x000E05BE File Offset: 0x000DE7BE
		[DataMember]
		public O[] Output { get; set; }

		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x000E05C7 File Offset: 0x000DE7C7
		// (set) Token: 0x0600499B RID: 18843 RVA: 0x000E05CF File Offset: 0x000DE7CF
		[DataMember(EmitDefaultValue = false)]
		public string[] ReadOnlyProperties { get; set; }

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x000E05D8 File Offset: 0x000DE7D8
		// (set) Token: 0x0600499D RID: 18845 RVA: 0x000E05E0 File Offset: 0x000DE7E0
		[DataMember(EmitDefaultValue = false)]
		public string[] NoAccessProperties { get; set; }

		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x000E05E9 File Offset: 0x000DE7E9
		// (set) Token: 0x0600499F RID: 18847 RVA: 0x000E05F1 File Offset: 0x000DE7F1
		[DataMember(EmitDefaultValue = false)]
		public JsonDictionary<ValidatorInfo[]> Validators { get; set; }

		// Token: 0x060049A0 RID: 18848 RVA: 0x000E05FA File Offset: 0x000DE7FA
		public PowerShellResults()
		{
			this.Output = Array<O>.Empty;
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x000E060D File Offset: 0x000DE80D
		public PowerShellResults<O> MergeAll(PowerShellResults<O> results)
		{
			this.Output = ((this.Output == null) ? results.Output : this.Output.Concat(results.Output).ToArray<O>());
			this.MergeProgressData<O>(results);
			return base.MergeErrors<O>(results);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x000E064C File Offset: 0x000DE84C
		public PowerShellResults<O> MergeProgressData<T>(PowerShellResults<T> results)
		{
			if (results != null)
			{
				this.StartIndex = results.StartIndex;
				this.EndIndex = results.EndIndex;
				this.AsyncGetListContext = results.AsyncGetListContext;
				this.SortColumnNames = results.SortColumnNames;
				this.SortData = results.SortData;
			}
			return this;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x000E0699 File Offset: 0x000DE899
		public PowerShellResults<O> MergeOutput(O[] output)
		{
			this.Output = ((this.Output == null) ? output : this.Output.Concat(output).ToArray<O>());
			return this;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x000E06C0 File Offset: 0x000DE8C0
		public override void UseAsRbacScopeInCurrentHttpContext()
		{
			if (this.HasValue)
			{
				BaseRow baseRow = this.Value as BaseRow;
				IConfigurable legacyTargetObject = (baseRow != null) ? baseRow.ConfigurationObject : null;
				RbacQuery.LegacyTargetObject = legacyTargetObject;
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x000E06F9 File Offset: 0x000DE8F9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x000E0701 File Offset: 0x000DE901
		public IEnumerator<O> GetEnumerator()
		{
			return ((IEnumerable<!0>)(this.Output ?? Array<O>.Empty)).GetEnumerator();
		}
	}
}

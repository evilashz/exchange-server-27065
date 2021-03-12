using System;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B6 RID: 182
	public class ParameterProfile
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00015E68 File Offset: 0x00014068
		public ParameterProfile()
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00015E70 File Offset: 0x00014070
		public ParameterProfile(string name, string reference, ParameterType type, IRunnable runnableTester)
		{
			this.name = name;
			this.reference = reference;
			this.parameterType = type;
			this.runnableTester = runnableTester;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00015E95 File Offset: 0x00014095
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00015E9D File Offset: 0x0001409D
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00015EA6 File Offset: 0x000140A6
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00015EAE File Offset: 0x000140AE
		[DefaultValue(null)]
		[DDIDataColumnExist]
		public string Reference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00015EB7 File Offset: 0x000140B7
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00015EBF File Offset: 0x000140BF
		[DefaultValue(null)]
		[DDIValidLambdaExpression]
		public string LambdaExpression { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00015EC8 File Offset: 0x000140C8
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00015ED0 File Offset: 0x000140D0
		public ParameterType ParameterType
		{
			get
			{
				return this.parameterType;
			}
			set
			{
				this.parameterType = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00015ED9 File Offset: 0x000140D9
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00015EE1 File Offset: 0x000140E1
		[DefaultValue(null)]
		public IRunnable RunnableTester
		{
			get
			{
				return this.runnableTester;
			}
			set
			{
				this.runnableTester = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00015EEA File Offset: 0x000140EA
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x00015EF2 File Offset: 0x000140F2
		[DefaultValue(null)]
		public string RunnableLambdaExpression { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00015EFB File Offset: 0x000140FB
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00015F03 File Offset: 0x00014103
		[TypeConverter(typeof(OrganizationTypesConverter))]
		[DefaultValue(null)]
		public OrganizationType[] OrganizationTypes { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00015F0C File Offset: 0x0001410C
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x00015F14 File Offset: 0x00014114
		[DefaultValue(false)]
		public bool HideDisplay { get; set; }

		// Token: 0x060005F1 RID: 1521 RVA: 0x00015F20 File Offset: 0x00014120
		public bool IsRunnable(DataRow row)
		{
			if (this.ParameterType == ParameterType.ModifiedColumn)
			{
				DataObjectStore dataObjectStore = row.Table.ExtendedProperties["DataSourceStore"] as DataObjectStore;
				if (dataObjectStore == null || !dataObjectStore.ModifiedColumns.Contains(this.Reference))
				{
					return false;
				}
			}
			if (!string.IsNullOrEmpty(this.RunnableLambdaExpression))
			{
				return (bool)ExpressionCalculator.CalculateLambdaExpression(ExpressionCalculator.BuildColumnExpression(this.RunnableLambdaExpression), typeof(bool), row, null);
			}
			return this.runnableTester == null || this.runnableTester.IsRunnable(row);
		}

		// Token: 0x040001E5 RID: 485
		private string name;

		// Token: 0x040001E6 RID: 486
		private string reference;

		// Token: 0x040001E7 RID: 487
		private ParameterType parameterType;

		// Token: 0x040001E8 RID: 488
		private IRunnable runnableTester;
	}
}

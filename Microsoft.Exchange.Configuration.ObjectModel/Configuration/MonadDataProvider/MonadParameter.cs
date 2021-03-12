using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D4 RID: 468
	internal class MonadParameter : DbParameter, ICloneable
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x00033CDB File Offset: 0x00031EDB
		public MonadParameter()
		{
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00033CF6 File Offset: 0x00031EF6
		public MonadParameter(string parameterName) : this()
		{
			this.parameterName = parameterName;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00033D05 File Offset: 0x00031F05
		public MonadParameter(string parameterName, object value) : this(parameterName)
		{
			this.value = value;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00033D15 File Offset: 0x00031F15
		public MonadParameter(string parameterName, DbType dbType, string sourceColumn) : this(parameterName)
		{
			this.dbType = dbType;
			this.sourceColumn = sourceColumn;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00033D2C File Offset: 0x00031F2C
		public override void ResetDbType()
		{
			this.dbType = DbType.Object;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00033D36 File Offset: 0x00031F36
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x00033D3E File Offset: 0x00031F3E
		[DefaultValue(DbType.Object)]
		public override DbType DbType
		{
			get
			{
				return this.dbType;
			}
			set
			{
				this.dbType = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00033D47 File Offset: 0x00031F47
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00033D4F File Offset: 0x00031F4F
		public override string ParameterName
		{
			get
			{
				return this.parameterName;
			}
			set
			{
				this.parameterName = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00033D58 File Offset: 0x00031F58
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00033D60 File Offset: 0x00031F60
		public bool IsSwitch
		{
			get
			{
				return this.isSwitch;
			}
			set
			{
				this.isSwitch = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00033D69 File Offset: 0x00031F69
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00033D71 File Offset: 0x00031F71
		public override object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value is DBNull)
				{
					this.value = null;
					return;
				}
				this.value = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00033D8A File Offset: 0x00031F8A
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00033D8D File Offset: 0x00031F8D
		public override ParameterDirection Direction
		{
			get
			{
				return ParameterDirection.Input;
			}
			set
			{
				if (ParameterDirection.Input != value)
				{
					throw new InvalidOperationException("ParameterDirection.Input is the only valid value.");
				}
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00033D9E File Offset: 0x00031F9E
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00033DA6 File Offset: 0x00031FA6
		public override string SourceColumn
		{
			get
			{
				return this.sourceColumn;
			}
			set
			{
				this.sourceColumn = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00033DAF File Offset: 0x00031FAF
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00033DB7 File Offset: 0x00031FB7
		public override bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullable = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00033DC0 File Offset: 0x00031FC0
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00033DC8 File Offset: 0x00031FC8
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this.sourceVersion;
			}
			set
			{
				this.sourceVersion = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00033DD1 File Offset: 0x00031FD1
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00033DD4 File Offset: 0x00031FD4
		public override bool SourceColumnNullMapping
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00033DD6 File Offset: 0x00031FD6
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00033DD9 File Offset: 0x00031FD9
		public override int Size
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00033DDC File Offset: 0x00031FDC
		private MonadParameter(MonadParameter source)
		{
			source.CopyTo(this);
			ICloneable cloneable = this.value as ICloneable;
			if (cloneable != null)
			{
				this.value = cloneable.Clone();
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00033E24 File Offset: 0x00032024
		private void CopyTo(MonadParameter destination)
		{
			destination.parameterName = this.parameterName;
			destination.value = this.value;
			destination.sourceColumn = this.sourceColumn;
			destination.sourceVersion = this.sourceVersion;
			destination.isNullable = this.isNullable;
			destination.dbType = this.dbType;
			destination.isSwitch = this.isSwitch;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00033E85 File Offset: 0x00032085
		public MonadParameter Clone()
		{
			return new MonadParameter(this);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00033E8D File Offset: 0x0003208D
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x040003AD RID: 941
		private string parameterName;

		// Token: 0x040003AE RID: 942
		private object value;

		// Token: 0x040003AF RID: 943
		private string sourceColumn;

		// Token: 0x040003B0 RID: 944
		private DbType dbType = DbType.Object;

		// Token: 0x040003B1 RID: 945
		private bool isNullable;

		// Token: 0x040003B2 RID: 946
		private bool isSwitch;

		// Token: 0x040003B3 RID: 947
		private DataRowVersion sourceVersion = DataRowVersion.Current;
	}
}

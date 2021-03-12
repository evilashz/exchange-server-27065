using System;
using System.ComponentModel;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014A RID: 330
	public class MonadDataHandler : SingleTaskDataHandler
	{
		// Token: 0x06000D5D RID: 3421 RVA: 0x000320E5 File Offset: 0x000302E5
		public MonadDataHandler(Type configObjectType) : this(null, configObjectType)
		{
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000320EF File Offset: 0x000302EF
		public MonadDataHandler(object objectID, Type configObjectType) : this(objectID, "get-" + configObjectType.Name.ToLowerInvariant(), "set-" + configObjectType.Name.ToLowerInvariant())
		{
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00032122 File Offset: 0x00030322
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MonadDataHandler(object objectID, string selectCommandText, string updateCommandText) : base(updateCommandText)
		{
			base.DataSource = null;
			this.Identity = objectID;
			this.selectCommand = new LoggableMonadCommand(selectCommandText, base.Connection);
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003214B File Offset: 0x0003034B
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x00032153 File Offset: 0x00030353
		[DefaultValue(null)]
		public object Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003215C File Offset: 0x0003035C
		internal MonadCommand SelectCommand
		{
			get
			{
				return this.selectCommand;
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00032164 File Offset: 0x00030364
		private bool HasIdentity()
		{
			bool flag = null == this.Identity;
			bool flag2 = this.Identity is string && string.IsNullOrEmpty(this.Identity as string);
			return !flag && !flag2;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000321A8 File Offset: 0x000303A8
		protected override void OnReadData()
		{
			if (!string.IsNullOrEmpty(this.selectCommand.CommandText))
			{
				this.selectCommand.Parameters.Remove("Identity");
				if (this.HasIdentity())
				{
					this.selectCommand.Parameters.AddWithValue("Identity", this.Identity);
				}
				object[] array = this.selectCommand.Execute();
				switch (array.Length)
				{
				case 1:
					base.DataSource = (IConfigurable)array[0];
					break;
				}
			}
			base.OnReadData();
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00032234 File Offset: 0x00030434
		protected override void AddParameters()
		{
			base.Parameters.Remove("Instance");
			base.Parameters.AddWithValue("Instance", base.DataSource);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003225D File Offset: 0x0003045D
		public override void Cancel()
		{
			base.Cancel();
			this.selectCommand.Cancel();
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00032270 File Offset: 0x00030470
		public override ValidationError[] Validate()
		{
			if (!this.IsModified())
			{
				return new ValidationError[0];
			}
			return base.Validate();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00032287 File Offset: 0x00030487
		protected override void HandleIdentityParameter()
		{
			if (this.HasIdentity())
			{
				base.HandleIdentityParameter();
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00032297 File Offset: 0x00030497
		protected override bool IsModified()
		{
			return base.ParameterNames.Count != 0;
		}

		// Token: 0x04000559 RID: 1369
		private MonadCommand selectCommand;

		// Token: 0x0400055A RID: 1370
		private object identity;
	}
}

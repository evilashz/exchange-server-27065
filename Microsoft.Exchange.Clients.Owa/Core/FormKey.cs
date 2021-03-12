using System;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200011D RID: 285
	internal sealed class FormKey
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00042EC4 File Offset: 0x000410C4
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00042ECC File Offset: 0x000410CC
		internal string Experience
		{
			get
			{
				return this.experience;
			}
			set
			{
				this.experience = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00042ED5 File Offset: 0x000410D5
		internal ApplicationElement Application
		{
			get
			{
				return this.application;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00042EDD File Offset: 0x000410DD
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00042EE5 File Offset: 0x000410E5
		internal string Class
		{
			get
			{
				return this.itemClass;
			}
			set
			{
				this.itemClass = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00042EEE File Offset: 0x000410EE
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00042EF6 File Offset: 0x000410F6
		internal string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00042EFF File Offset: 0x000410FF
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00042F07 File Offset: 0x00041107
		internal string State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00042F10 File Offset: 0x00041110
		internal FormKey(ApplicationElement application, string itemClass, string action, string state)
		{
			this.experience = string.Empty;
			this.application = application;
			this.itemClass = itemClass;
			this.action = action;
			this.state = state;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00042F40 File Offset: 0x00041140
		internal FormKey(string experience, ApplicationElement application, string itemClass, string action, string state)
		{
			this.experience = experience;
			this.application = application;
			this.itemClass = itemClass;
			this.action = action;
			this.state = state;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00042F70 File Offset: 0x00041170
		public new static bool Equals(object a, object b)
		{
			FormKey formKey = a as FormKey;
			FormKey formKey2 = b as FormKey;
			return formKey != null && formKey2 != null && (formKey.Action == formKey2.Action && formKey.Application == formKey2.Application && formKey.Class == formKey2.Class && formKey.State == formKey2.State) && formKey.Experience == formKey2.Experience;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00042FF0 File Offset: 0x000411F0
		public override int GetHashCode()
		{
			return this.experience.GetHashCode() ^ this.application.GetHashCode() ^ this.itemClass.GetHashCode() ^ this.action.GetHashCode() ^ this.state.GetHashCode();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0004303D File Offset: 0x0004123D
		public override bool Equals(object value)
		{
			return FormKey.Equals(value, this);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00043048 File Offset: 0x00041248
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Experience = {0}, Application = {1}, Class = {2}, Action = {3}, State = {4}", new object[]
			{
				this.experience,
				this.application,
				this.itemClass,
				this.action,
				this.state
			});
		}

		// Token: 0x040006ED RID: 1773
		private string experience;

		// Token: 0x040006EE RID: 1774
		private ApplicationElement application;

		// Token: 0x040006EF RID: 1775
		private string itemClass;

		// Token: 0x040006F0 RID: 1776
		private string action;

		// Token: 0x040006F1 RID: 1777
		private string state;
	}
}

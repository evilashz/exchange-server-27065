using System;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200010B RID: 267
	public sealed class Experience
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00040B74 File Offset: 0x0003ED74
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00040B7C File Offset: 0x0003ED7C
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x00040B84 File Offset: 0x0003ED84
		internal FormsRegistry FormsRegistry
		{
			get
			{
				return this.formsRegistry;
			}
			set
			{
				this.formsRegistry = value;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00040B8D File Offset: 0x0003ED8D
		internal Experience(string name, FormsRegistry formsRegistry)
		{
			this.name = name;
			this.formsRegistry = formsRegistry;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00040BA4 File Offset: 0x0003EDA4
		public new static bool Equals(object a, object b)
		{
			Experience experience = a as Experience;
			Experience experience2 = b as Experience;
			return experience != null && experience2 != null && experience.Name == experience2.Name && experience.FormsRegistry == experience2.FormsRegistry;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00040BEA File Offset: 0x0003EDEA
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ this.formsRegistry.GetHashCode();
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00040C03 File Offset: 0x0003EE03
		public override bool Equals(object value)
		{
			return Experience.Equals(value, this);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00040C0C File Offset: 0x0003EE0C
		internal static Experience Copy(Experience experience)
		{
			return new Experience(string.Copy(experience.Name), experience.FormsRegistry);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00040C24 File Offset: 0x0003EE24
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Name = {0}, FormsRegistry = {1}", new object[]
			{
				this.name,
				this.formsRegistry.Name
			});
		}

		// Token: 0x0400064D RID: 1613
		private string name;

		// Token: 0x0400064E RID: 1614
		private FormsRegistry formsRegistry;
	}
}
